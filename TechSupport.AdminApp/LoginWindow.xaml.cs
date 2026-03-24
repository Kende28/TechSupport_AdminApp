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
	public partial class LoginWindow : Window
	{
		private readonly LoginViewModel _vm = new();

		public LoginWindow()
		{
			InitializeComponent();
			DataContext = _vm;
		}

		// PasswordBox Binding workaround(nemtudom magyarul most)
		private void PasswordSync(object sender, RoutedEventArgs e)
		{
			_vm.Password = PasswordBox.Password;
		}
	}
}
