using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TechSupport.AdminApp.Models;
using TechSupport.AdminApp.Services;

namespace TechSupport.AdminApp.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private readonly PartApiClient _api = new();

		public MainViewModel()
		{
			Components = new ObservableCollection<ComponentDto>();

			ReadCommand = new AsyncRelayCommand(ReadAsync);
			CreateCommand = new AsyncRelayCommand(CreateAsync);
			UpdateCommand = new AsyncRelayCommand(UpdateAsync);
			DeleteCommand = new AsyncRelayCommand(DeleteSelectedAsync);
			ClearCommand = new AsyncRelayCommand(ClearAsync);
		}

		public ObservableCollection<ComponentDto> Components { get; }

		private ComponentDto _selectedComponent;
		public ComponentDto SelectedComponent
		{
			get => _selectedComponent;
			set { _selectedComponent = value; Notify(); PopulateFieldsFromSelected(); }
		}

		private int _id;
		public int PartId { get => _id; set { _id = value; Notify(); } }

		private string _name;
		public string PartName { get => _name; set { _name = value; Notify(); } }

		private string _description;
		public string PartDescription { get => _description; set { _description = value; Notify(); } }

		private bool _visible;
		public bool PartVisible { get => _visible; set { _visible = value; Notify(); } }

		public ICommand ReadCommand { get; }
		public ICommand CreateCommand { get; }
		public ICommand UpdateCommand { get; }
		public ICommand DeleteCommand { get; }
		public ICommand ClearCommand { get; }

		public async Task ReadAsync()
		{
			Components.Clear();
			var items = await _api.GetAllAsync();
			foreach (var item in items)
				Components.Add(item);
		}

		public async Task CreateAsync()
		{
			var dto = new ComponentDto
			{
				PartName = PartName,
				PartDescription = PartDescription,
				PartVisible = PartVisible,
			};

			await _api.CreateAsync(dto);
			await ReadAsync();
			ClearFields();
		}

		public async Task UpdateAsync()
		{
			if (SelectedComponent == null) return;

			SelectedComponent.PartName = PartName;
			SelectedComponent.PartDescription = PartDescription;
			SelectedComponent.PartVisible = PartVisible;

			await _api.UpdateAsync(SelectedComponent.PartId, SelectedComponent);
			await ReadAsync();
		}

		public async Task DeleteSelectedAsync()
		{
			if (SelectedComponent == null) return;

			await _api.DeleteAsync(SelectedComponent.PartId);
			SelectedComponent = null;
			await ReadAsync();
			ClearFields();
		}

		public Task ClearAsync()
		{
			SelectedComponent = null;
			ClearFields();
			return Task.CompletedTask;
		}

		private void ClearFields()
		{
			PartId = 0;
			PartName = string.Empty;
			PartDescription = string.Empty;
			PartVisible = false;
		}

		private void PopulateFieldsFromSelected()
		{
			if (SelectedComponent != null)
			{
				PartId = SelectedComponent.PartId;
				PartName = SelectedComponent.PartName;
				PartDescription = SelectedComponent.PartDescription;
				PartVisible = SelectedComponent.PartVisible;
			}
			else
			{
				PartId = 0;
				PartName = string.Empty;
				PartDescription = string.Empty;
				PartVisible = false;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void Notify([CallerMemberName] string propertyName = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	// AsyncRelayCommand
	public class AsyncRelayCommand : ICommand
	{
		private readonly Func<Task> _execute;
		private bool _isExecuting;

		public AsyncRelayCommand(Func<Task> execute)
		{
			_execute = execute;
		}

		public bool CanExecute(object parameter) => !_isExecuting;

		public async void Execute(object parameter)
		{
			_isExecuting = true;
			OnCanExecuteChanged();

			try { await _execute(); }
			finally
			{
				_isExecuting = false;
				OnCanExecuteChanged();
			}
		}

		public event EventHandler CanExecuteChanged;
		protected virtual void OnCanExecuteChanged()
			=> CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
