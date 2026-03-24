using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace TechSupport.AdminApp.ViewModels
{
	public class LoginViewModel : INotifyPropertyChanged
	{
		private string _username;
		public string Username { get => _username; set { _username = value; Notify(); } }

		private string _password;
		public string Password { get => _password; set { _password = value; Notify(); } }

		public ICommand LoginCommand { get; }

		public LoginViewModel()
		{
			LoginCommand = new RelayCommand(Login);
		}

		private void Login(object obj)
		{
			// DEMO LOGIN!!!!
			if (Username == "admin" && Password == "1234")
			{
				MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

				// Megnyitjuk MainWindow-t
				var mainWindow = new MainWindow();
				mainWindow.Show();

				// Bezárjuk login ablakot
				if (obj is Window window)
					window.Close();
			}
			else
			{
				MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void Notify([CallerMemberName] string propertyName = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public class RelayCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly Predicate<object> _canExecute;

		public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

		public void Execute(object parameter) => _execute(parameter);

		public event EventHandler CanExecuteChanged;
		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
