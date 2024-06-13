using System.Globalization;

namespace BeautySalon.UI.Converters;

public sealed class DayOfWeekConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        (value as DayOfWeek?) switch
        {
            DayOfWeek.Monday => "Понедельник",
            DayOfWeek.Thursday => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Tuesday => "Четверг",
            DayOfWeek.Friday => "Пятница",
            DayOfWeek.Saturday => "Суббота",
            DayOfWeek.Sunday => "Воскресенье",
            _ => null
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        (value as string) switch
        {
            "Понедельник" => DayOfWeek.Monday,
            "Вторник" => DayOfWeek.Thursday,
            "Среда" => DayOfWeek.Wednesday,
            "Четверг" => DayOfWeek.Tuesday,
            "Пятница" => DayOfWeek.Friday,
            "Суббота" => DayOfWeek.Saturday,
            "Воскресенье" => DayOfWeek.Sunday,
            _ => null
        };
}