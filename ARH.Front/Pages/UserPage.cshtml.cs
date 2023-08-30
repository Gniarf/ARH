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
    [BindProperty]
    public IEnumerable<string> UserNames { get; set; }
    [BindProperty]
    public int SelectedYear { get; set; } = DateTime.Now.Year;
    public IEnumerable<int> SelectableYears { get; set; }
    public bool IsSubmited { get; set; } = false;
    public UserModel(ILogger<IndexModel> logger, ICalendarService calendarService)
    {
        _logger = logger;
        this.calendarService = calendarService;
        UserNames = new List<string>();
        Index = new IndexModel(_logger, calendarService);
        List<int> selectableYears = new();
        int maxYear = DateTime.Now.Year;
        for (int year = 2023; year <= maxYear; ++year)
        {
            selectableYears.Add(year);
        }
        SelectableYears = selectableYears;
    }


    public float stringValuetoInt(Length init)
    {
        float val = 0;
        switch (init)
        {
            case Length.One:
                val = 1;
                break;
            case Length.O75:
                val = 0.75f;
                break;
            case Length.O50:
                val = 0.50f;
                break;
            case Length.O25:
                val = 0.25f;
                break;
            default:
                val = 0;
                break;
        }
        return val;
    }
    public void OnGet()
    {

        if (Index == null)
        {
            Index = new IndexModel(_logger, calendarService);
        }
        Index.CurrentCalendar = Index.CurrentCalendar;
        Index.CurrentCalendar = calendarService.GetCalendar(new Models.CalendarRequest { UserName = SelectedName ?? string.Empty, Date = DateTime.Today });
        Index.MonthNames = Index.CurrentCalendar.MonthName();
        UserNames = calendarService.GetDistinctUsernames(new Models.CalendarRequest { });
        SelectedMonth = DateTime.Now.ToString("MMMM");


    }
    public void OnPost()
    {
        if (!string.IsNullOrEmpty(SelectedMonth))
        {


            UserNames = calendarService.GetDistinctUsernames(new Models.CalendarRequest { });
            int monthNumber = DateTime.ParseExact(SelectedMonth, "MMMM", CultureInfo.CurrentCulture).Month;
            Index.CurrentCalendar = calendarService.GetCalendar(new Models.CalendarRequest { UserName = SelectedName ?? string.Empty, Date = new DateTime(SelectedYear, monthNumber, 1) });
        }
        Index.CurrentCalendar = Index.CurrentCalendar;

        IsSubmited = true;
    }

}





