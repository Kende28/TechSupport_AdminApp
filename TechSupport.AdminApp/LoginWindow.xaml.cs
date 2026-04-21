using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TechSupport.AdminApp.ViewModels;

namespace TechSupport.AdminApp
{
	// Bejelentkező ablak code-behind
	public partial class LoginWindow : Window
	{
		// ViewModel bejelentkézéshez
		private readonly LoginViewModel _vm = new();

		// Ablak inicializálása
		public LoginWindow()
		{
			InitializeComponent();
			DataContext = _vm;
		}

		// Jelszó szinkronizálása (MVVM workaround)
		private void PasswordSync(object sender, RoutedEventArgs e)
		{
			_vm.Password = PasswordBox.Password;
		}
	}
}
