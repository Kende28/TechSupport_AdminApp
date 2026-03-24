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
		private readonly ComponentApiClient _api = new();

		public MainViewModel()
		{
			Components = new ObservableCollection<ComponentDto>();

			LoadCommand = new AsyncRelayCommand(LoadAsync);
			AddCommand = new AsyncRelayCommand(AddAsync);
			UpdateCommand = new AsyncRelayCommand(UpdateAsync);
			DeleteCommand = new AsyncRelayCommand(DeleteSelectedAsync);
		}

		public ObservableCollection<ComponentDto> Components { get; }

		private ComponentDto _selectedComponent;
		public ComponentDto SelectedComponent
		{
			get => _selectedComponent;
			set { _selectedComponent = value; Notify(); PopulateFieldsFromSelected(); }
		}

		private string _name;
		public string Name { get => _name; set { _name = value; Notify(); } }

		private string _brand;
		public string Brand { get => _brand; set { _brand = value; Notify(); } }

		private string _category;
		public string Category { get => _category; set { _category = value; Notify(); } }

		private int _price;
		public int Price { get => _price; set { _price = value; Notify(); } }

		public ICommand LoadCommand { get; }
		public ICommand AddCommand { get; }
		public ICommand UpdateCommand { get; }
		public ICommand DeleteCommand { get; }

		public async Task LoadAsync()
		{
			Components.Clear();
			var items = await _api.GetAllAsync();
			foreach (var item in items)
				Components.Add(item);
		}

		public async Task AddAsync()
		{
			var dto = new ComponentDto
			{
				name = Name,
				brand = Brand,
				category = Category,
				price = Price
			};

			await _api.CreateAsync(dto);
			await LoadAsync();
			ClearFields();
		}

		public async Task UpdateAsync()
		{
			if (SelectedComponent == null) return;

			SelectedComponent.name = Name;
			SelectedComponent.brand = Brand;
			SelectedComponent.category = Category;
			SelectedComponent.price = Price;

			await _api.UpdateAsync(SelectedComponent.id, SelectedComponent);
			await LoadAsync();
		}

		public async Task DeleteSelectedAsync()
		{
			if (SelectedComponent == null) return;

			await _api.DeleteAsync(SelectedComponent.id);
			SelectedComponent = null;
			await LoadAsync();
			ClearFields();
		}

		private void ClearFields()
		{
			Name = string.Empty;
			Brand = string.Empty;
			Category = null;
			Price = 0;
		}

		private void PopulateFieldsFromSelected()
		{
			if (SelectedComponent != null)
			{
				Name = SelectedComponent.name;
				Brand = SelectedComponent.brand;
				Category = SelectedComponent.category;
				Price = SelectedComponent.price;
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
