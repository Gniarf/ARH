using ARH.Front.Contracts;
using ARH.Front.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ARH.Front.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICalendarService calendarService;

    public MonthlyCalendar CurrentCalendar { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ICalendarService calendarService)
    {
        _logger = logger;
        this.calendarService = calendarService;
        CurrentCalendar = new MonthlyCalendar();
    }

    public void OnGet()
    {
        CurrentCalendar = calendarService.GetCalendar(new Models.CalendarRequest { UserName = User?.Identity?.Name ?? string.Empty, Date = DateTime.Today });
    }
}
