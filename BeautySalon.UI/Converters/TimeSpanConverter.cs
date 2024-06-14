using System.Globalization;

namespace BeautySalon.UI.Converters;

public sealed class TimeSpanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TimeSpan timeSpan) return value;
        return timeSpan switch
        {
            { Hours: > 0, Minutes: > 0 } => $"{timeSpan.Hours} ч {timeSpan.Minutes} мин",
            { Hours: > 0 } => $"{timeSpan.Hours} ч",
            { Minutes: > 0 } => $"{timeSpan.Minutes} мин",
            _ => timeSpan
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}