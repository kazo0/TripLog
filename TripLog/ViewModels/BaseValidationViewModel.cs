using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TripLog.Services;

namespace TripLog.ViewModels
{
	public class BaseValidationViewModel : BaseViewModel, INotifyDataErrorInfo
	{
		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
		private IDictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

		public BaseValidationViewModel(INavService navService) : base(navService)
		{
		}

		public bool HasErrors => _errors?.Values?.Any() ?? false;

		public IEnumerable GetErrors(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				return _errors.SelectMany(x => x.Value);
			}

			var errors = new List<string>();
			if (_errors.ContainsKey(propertyName))
			{
				errors = _errors[propertyName];
			}

			return errors;
		}

		protected void Validate(Func<bool> rule, string error, [CallerMemberName] string propertyName = null)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				return;
			}

			if (_errors.ContainsKey(propertyName))
			{
				_errors.Remove(propertyName);
			}

			if (!rule())
			{
				_errors.Add(propertyName, new List<string>() { error });
			}

			OnPropertyChanged(nameof(HasErrors));

			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
		}
	}
}
