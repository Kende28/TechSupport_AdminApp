using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechSupport.AdminApp.ViewModels;

namespace TechSupport.AdminApp
{
	// Főablak code-behind - admin szekezet
	public partial class MainWindow : Window
	{
		// ViewModel az admin adatkezeléshez
		private readonly MainViewModel _vm = new();

		// Ablak inicializálása és adatok betöltése
		public MainWindow()
		{
			InitializeComponent();
			DataContext = _vm;

			// Ablak megnyitásakor automatikus adat betöltés
			Loaded += async (_, __) => await _vm.ReadAsync();
		}
	}
}
