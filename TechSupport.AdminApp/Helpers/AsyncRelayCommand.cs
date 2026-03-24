using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TechSupport.AdminApp.Helpers
{
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

			try
			{
				await _execute();
			}
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
