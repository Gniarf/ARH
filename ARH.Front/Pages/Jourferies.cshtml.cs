using ARH.Front.Contracts;
using ARH.Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Linq;

namespace ARH.Front.Pages
{
    public class JOURModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICalendarService calendarService;
        public MonthlyCalendar CurrentCalendar { get; set; }
        // Ajoutez une liste pour stocker les dates
        [BindProperty]
        public List<Holyday> Dates { get; set; }
        public ICollection<string> UserIdcollection { get; set; } = new string[0];
        public JOURModel(ILogger<IndexModel> logger, ICalendarService calendarService)
        {
            _logger = logger;
            this.calendarService = calendarService;
            CurrentCalendar = new MonthlyCalendar();

            // Initialisez la liste des dates
            Dates = new List<Holyday>();
            UserIdcollection = new List<string>();
        }

        public void OnGet()
        {
            var request = new HolydayRequest
            {


                UserId = User?.Identity?.Name ?? string.Empty,

                Year = DateTime.Now.Year


            };
            Dates = calendarService.GetHolidaysForUser(request).ToList();
        }


        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                for (int i=0;i<Dates.Count;i++)
                {
                    var request = new HolydaySetRequest
                    {
                        Date = Dates[i].Date,
                        YearIncluded = Dates[i].YearIncluded,
                        HolydayId = Dates[i].Id,                    
                    };
                    calendarService.SetHolyday(request);
                }
            }
        }

    }
}