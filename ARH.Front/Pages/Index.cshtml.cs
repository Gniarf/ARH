using ARH.Front.Contracts;
using ARH.Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace ARH.Front.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICalendarService calendarService;
    [BindProperty]
    public MonthlyCalendar CurrentCalendar { get; set; }
    public List<string> MonthNames { get; set; }

    [BindProperty]
    public string SelectedMonth { get; set; } = string.Empty;
    [BindProperty]
    public string Comment { get; set; } = string.Empty;
    public IndexModel(ILogger<IndexModel> logger, ICalendarService calendarService)
    {
        _logger = logger;
        this.calendarService = calendarService;
        CurrentCalendar = new MonthlyCalendar();

        MonthNames = new List<string>();
        string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        MonthNames.AddRange(monthNames);
    }


    public string DayNameToCssclass(string Day ,DateTime date)
    {  
        string cssClass;
        switch (Day)
        {
            case "samedi":
            case "dimanche":
                cssClass = "table-success";
                break;
            case "vendredi":
                cssClass = "vendredi";
                break;
            default:
            cssClass = IsHoliday(date) ? "bg-success" : "joursur";
            break;
    }
                
        
        return cssClass;
    }
 

public bool IsHoliday(DateTime date)
{
    // Liste des jours fériés avec leurs dates
    Dictionary<DateTime, string> holidays = new Dictionary<DateTime, string>
    {
        { new DateTime(DateTime.Now.Year, 1, 1), "Jour de l'an" },
        { new DateTime(DateTime.Now.Year, 4, 10), "Lundi de Pâques" },
        { new DateTime(DateTime.Now.Year, 5, 1), "Fête du Travail" },
        { new DateTime(DateTime.Now.Year, 5, 8), "Fête Victoire" },
        { new DateTime(DateTime.Now.Year, 5, 18), "Ascension" },
        { new DateTime(DateTime.Now.Year, 5, 29), "Lundi de Pentecôte" },
        { new DateTime(DateTime.Now.Year, 7, 14), "Fête nationale" },
        { new DateTime(DateTime.Now.Year, 8, 15), "Assomption" },
        { new DateTime(DateTime.Now.Year, 11, 1), "Toussaint" },
        { new DateTime(DateTime.Now.Year, 11, 11), "Armistice" },
        { new DateTime(DateTime.Now.Year, 12, 25), "Noël" }
    };

    // Vérifiez si la date est un jour férié
    return holidays.ContainsKey(date.Date);
}

    public void OnGet()
    {
        CurrentCalendar = calendarService.GetCalendar(new CalendarRequest { UserName = User?.Identity?.Name ?? string.Empty, Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) });
        MonthNames = CurrentCalendar.MonthName(DateTime.Now.Year, User?.Identity?.Name ?? string.Empty);
        SelectedMonth = DateTime.Now.ToString("MMMM");
    }

    public void OnPostSelectedMonth()
    {
        if (!string.IsNullOrEmpty(SelectedMonth))
        {
            int monthNumber = DateTime.ParseExact(SelectedMonth, "MMMM", CultureInfo.CurrentCulture).Month;
            CurrentCalendar = calendarService.GetCalendar(new CalendarRequest { UserName = User?.Identity?.Name ?? string.Empty, Date = new DateTime(DateTime.Now.Year, monthNumber, 1) });
            MonthNames = CurrentCalendar.MonthName(DateTime.Now.Year, User?.Identity?.Name ?? string.Empty);
        }
    }

    public void OnPostSubmit()
    {
        int targetMonth = string.IsNullOrEmpty(SelectedMonth)
                                   ? DateTime.Now.Month
                                   : DateTime.ParseExact(SelectedMonth, "MMMM", CultureInfo.CurrentCulture).Month;

        calendarService.SetCalendar(CurrentCalendar);
        var request = new CalendarRequest { UserName = User?.Identity?.Name ?? string.Empty, Date = new DateTime(DateTime.Now.Year, targetMonth, 1) };
        CurrentCalendar = calendarService.GetCalendar(request);
    }
}