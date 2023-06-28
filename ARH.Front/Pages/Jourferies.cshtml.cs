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

    
    
    public JOURModel(ILogger<IndexModel> logger, ICalendarService calendarService)
    {
        _logger = logger;

    }

    
    
      
    public void OnGet()
    {

      
      

    }
    public void OnPost()
    {
    
    }

}





