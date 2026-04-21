using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TechSupport.AdminApp;
using TechSupport.AdminApp.Models;
using TechSupport.AdminApp.Services;

namespace TechSupport.AdminApp.ViewModels
{
    // Főablak ViewModel - alkatrész CRUD kezeléshez
    public class MainViewModel : INotifyPropertyChanged
    {
        // Backend alkatrész API
        private readonly PartApiClient _api = new();

        private string _statusMessage;
        // Operáció és státusz üzenetek
        public string StatusMessage { get => _statusMessage; set { _statusMessage = value; Notify(); } }

        private string _userName;
        // Bejelentkezett felhasználó neve
        public string UserName { get => _userName; set { _userName = value; Notify(); } }

        // ViewModel inicializálása
        public MainViewModel()
        {
            // Alkatrész lista és parancsok
            Components = new ObservableCollection<ComponentDto>();

            ReadCommand = new AsyncRelayCommand(ReadAsync);
            CreateCommand = new AsyncRelayCommand(CreateAsync);
            UpdateCommand = new AsyncRelayCommand(UpdateAsync);
            DeleteCommand = new AsyncRelayCommand(DeleteSelectedAsync);
            ClearCommand = new AsyncRelayCommand(ClearAsync);
            //LogoutCommand = new AsyncRelayCommand(LogoutAsync);

            UserName = AppConfig.CurrentUserName;
            StatusMessage = "Készen áll a műveletekre.";
        }

        // Alkatrészek listája
        public ObservableCollection<ComponentDto> Components { get; }

        // Kiválasztott alkatrész az adatrácsban
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
        public ICommand LogoutCommand { get; }

        public async Task ReadAsync()
        {
            StatusMessage = "Adatok betöltése...";
            try
            {
                var items = await _api.GetAllAsync();
                Components.Clear();
                foreach (var item in items)
                    Components.Add(item);

                StatusMessage = $"Lista frissítve ({Components.Count})";
            }
            catch (Exception)
            {
                StatusMessage = "Hiba történt az adatok lekérésekor.";
            }
        }

        public async Task CreateAsync()
        {
            if (string.IsNullOrWhiteSpace(PartName))
            {
                StatusMessage = "Az alkatrész név megadása kötelező.";
                return;
            }

            var dto = new ComponentDto
            {
                PartName = PartName,
                PartDescription = PartDescription,
                PartVisible = PartVisible,
            };

            try
            {
                var success = await _api.CreateAsync(dto);
                StatusMessage = success ? "Alkatrész létrehozva." : "Az alkatrész létrehozása sikertelen.";
            }
            catch (Exception)
            {
                StatusMessage = "Hiba történt az alkatrész létrehozásakor.";
            }

            await ReadAsync();
            ClearFields();
        }

        public async Task UpdateAsync()
        {
            if (SelectedComponent == null)
            {
                StatusMessage = "Nincs kiválasztott alkatrész a módosításhoz.";
                return;
            }

            SelectedComponent.PartName = PartName;
            SelectedComponent.PartDescription = PartDescription;
            SelectedComponent.PartVisible = PartVisible;

            try
            {
                var success = await _api.UpdateAsync(SelectedComponent.PartId, SelectedComponent);
                StatusMessage = success ? "Alkatrész módosítva." : "Az alkatrész módosítása sikertelen.";
            }
            catch (Exception)
            {
                StatusMessage = "Hiba történt az alkatrész módosításakor.";
            }

            await ReadAsync();
        }

        public async Task DeleteSelectedAsync()
        {
            if (SelectedComponent == null)
            {
                StatusMessage = "Nincs kiválasztott alkatrész a törléshez.";
                return;
            }

            try
            {
                var success = await _api.DeleteAsync(SelectedComponent.PartId);
                StatusMessage = success ? "Alkatrész törölve." : "Az alkatrész törlése sikertelen.";
            }
            catch (Exception)
            {
                StatusMessage = "Hiba történt az alkatrész törlésekor.";
            }

            SelectedComponent = null;
            await ReadAsync();
            ClearFields();
        }

        public async Task LogoutAsync(object obj)
        {
            AppConfig.BearerToken = string.Empty;
            AppConfig.CurrentUserName = string.Empty;
            AppConfig.CurrentUserEmail = string.Empty;
            StatusMessage = "Kijelentkezés...";

            var loginWindow = new LoginWindow();
            loginWindow.Show();

            if (obj is Window window)
            {
                window.Close();
            }

            await Task.CompletedTask;
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
