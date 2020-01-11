using System;
using System.Collections;
using System.ComponentModel;

namespace TripLog.ViewModels
{
	public class BaseValidationViewModel : BaseViewModel, INotifyDataErrorInfo
	{
		public bool HasErrors => throw new NotImplementedException();

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		public BaseValidationViewModel()
		{
		}

		public IEnumerable GetErrors(string propertyName)
		{
			throw new NotImplementedException();
		}
	}
}
