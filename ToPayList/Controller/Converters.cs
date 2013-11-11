using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ToPayList.Controller.Converters
{
	[ValueConversion(typeof(object), typeof(string))]
	public class DatetimeToDate : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return System.Convert.ToDateTime(value).ToString("dd/MM/yyyy");
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
	[ValueConversion(typeof(object), typeof(string))]
	public class IntToBool : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if(value == null)
				return false;
			return (System.Convert.ToInt32(value) > 0);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (System.Convert.ToBoolean(value) ? 1 : 0);
		}
	}

	[ValueConversion(typeof(object), typeof(string))]
	public class FormattingConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string formatString = parameter as string;
			if (formatString != null)
			{
				return string.Format(culture, formatString, value);
			}
			else
			{
				return value.ToString();
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			// we don't intend this to ever be called
			return null;
		}
	}
}
