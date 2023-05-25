using ARH.Front.Contracts;
using ARH.Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
namespace ARH.Front.Pages;


public class UserModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly ICalendarService calendarService;

    public IndexModel Index { get; set; }
    [BindProperty]
    public string SelectedName { get; set; } = string.Empty;
    [BindProperty]
    public string SelectedMonth { get; set; } = string.Empty;

    public UserModel(ILogger<IndexModel> logger, ICalendarService calendarService)
    {
        _logger = logger;
        this.calendarService = calendarService;

        Index = new IndexModel(_logger, calendarService);

    }
    public void OnGet()
    {
        if (Index == null)
        {
            Index = new IndexModel(_logger, calendarService);
        }
        Index.CurrentCalendar = Index.CurrentCalendar;
        Index.CurrentCalendar = calendarService.GetCalendar(new Models.CalendarRequest { UserName = User?.Identity?.Name ?? string.Empty, Date = DateTime.Today });
        Index.MonthNames = Index.CurrentCalendar.MonthName(DateTime.Now.Year, User?.Identity?.Name ?? string.Empty);

    }
    public void OnPost()
    {
        if (!string.IsNullOrEmpty(SelectedMonth))
        {



            int monthNumber = DateTime.ParseExact(SelectedMonth, "MMMM", CultureInfo.CurrentCulture).Month;
            Index.CurrentCalendar = calendarService.GetCalendar(new Models.CalendarRequest { UserName = User?.Identity?.Name ?? string.Empty, Date = new DateTime(DateTime.Now.Year, monthNumber, 1) });
        }
        Index.CurrentCalendar = Index.CurrentCalendar;


    }

}





