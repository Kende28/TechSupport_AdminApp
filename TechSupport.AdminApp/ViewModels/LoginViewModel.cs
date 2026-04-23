using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TechSupport.AdminApp.Models;
using TechSupport.AdminApp.Services;
using TechSupport.AdminApp.Helpers;

namespace TechSupport.AdminApp.ViewModels
{
    // Bejelentkezés ablak ViewModel
    public class LoginViewModel : INotifyPropertyChanged
    {
        // Backend felhasználói API
        private readonly UserApiClient _api = new();

        private string _username;
        // Bejelentkezési felhasználónév
        public string Username { get => _username; set { _username = value; Notify(); } }

        private string _password;
        // Bejelentkezési jelszó
        public string Password { get => _password; set { _password = value; Notify(); } }

        // Bejelentkezés gomb paranccsa
        public ICommand LoginCommand { get; }

        // Sikeresen lekért autentikációs token
        public string AuthenticationToken { get; private set; }

        // ViewModel inicializálása
        public LoginViewModel()
        {
            // Parancs hozzárendelése (aszinkron, paraméterként az ablakot kapja)
            LoginCommand = new AsyncRelayCommand(LoginAsync);
        }

        // Backend hitelesítés
        private async Task LoginAsync(object obj)
        {
            // Beviteli mezők ellenőrzése
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter both username and password.", "Login required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Backend API hívás
                var loginResponse = await _api.LoginAsync(new LoginDto(Username, Password));
                // Token és felhasználó adat mentése
                AuthenticationToken = loginResponse.Token;
                AppConfig.BearerToken = AuthenticationToken;
                AppConfig.CurrentUserName = loginResponse.UserName ?? Username;
                AppConfig.CurrentUserEmail = loginResponse.UserEmail ?? string.Empty;

                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Fő ablak megnyitása
                var mainWindow = new MainWindow();
                mainWindow.Show();

                // Bejelentkezés ablak bezárása
                if (obj is Window window)
                {
                    window.Close();
                }

            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("Login failed. Please check your username and password.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("An unexpected error occurred during login.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
