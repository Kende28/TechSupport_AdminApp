using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TechSupport.AdminApp.Helpers
{
    // Aszinkron parancs megvalósítása WPF számára
	// Támogatja mind paraméter nélküli, mind objektum paramétert fogadó aszinkron műveleteket
	public class AsyncRelayCommand : ICommand
	{
		// Egységesített delegátum, amely objektum paramétert fogad
		private readonly Func<object, Task> _execute;

		// Végrehajtás alatt van-e a parancs
		private bool _isExecuting;

		// Konstruktor paraméter nélküli aszinkron feladattal
		public AsyncRelayCommand(Func<Task> execute)
		{
			_execute = _ => execute();
		}

		// Konstruktor objektum paramétert fogadó aszinkron feladattal
		public AsyncRelayCommand(Func<object, Task> execute)
		{
			_execute = execute;
		}

		// Parancs végrehajtható-e
		public bool CanExecute(object parameter) => !_isExecuting;

		// Parancs végrehajtása aszinkron módban
		public async void Execute(object parameter)
		{
			_isExecuting = true;
			OnCanExecuteChanged();

			try
			{
				await _execute(parameter);
			}
			finally
			{
				_isExecuting = false;
				OnCanExecuteChanged();
			}
		}

		public event EventHandler CanExecuteChanged;
		// Tudósítás a parancs állapotának változásáról
		protected virtual void OnCanExecuteChanged()
			=> CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
