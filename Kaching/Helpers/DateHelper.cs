using System.Globalization;

namespace Kaching.Helpers;

public class DateHelper
{
    private readonly List<string> _months;

    public DateHelper()
    {
        _months = new List<string>
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };
    }

    public int GetCurrentMonthNumber()
    {
        return DateTime.Now.Month;
    }

    public int GetMonthNumber(string month)
    {
        return DateTime.ParseExact
            (month, "MMMM", CultureInfo.CurrentCulture).Month;
    }

    public bool StringIsMonth(string month)
    {
        return _months.Contains(month);
    }

    public bool StringIsYear(string year)
    {
        return true;
    }

    public string GetCurrentYear()
    {
        return DateTime.Now.Year.ToString();
    }
}