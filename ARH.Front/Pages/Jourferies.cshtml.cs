using ARH.Front.Contracts;
using ARH.Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
namespace ARH.Front.Pages;


public class JOURModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly ICalendarService calendarService;

        // Ajoutez une liste pour stocker les dates
        public List<DateTime> Dates { get; set; }

        public JOURModel(ILogger<IndexModel> logger, ICalendarService calendarService)
        {
            _logger = logger;
            this.calendarService = calendarService;

            // Initialisez la liste des dates
            Dates = new List<DateTime>();
        }

        public void OnGet()
        {
      
        }

        public void OnPost()
        {
             
           
            }

         
    }     






