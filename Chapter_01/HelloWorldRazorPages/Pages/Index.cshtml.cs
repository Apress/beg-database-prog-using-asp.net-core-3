using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelloWorldRazorPages.Models;


namespace HelloWorldRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AppMessage Heading { get; set; }

        public string SubHeading { get; set; }

        public void OnGet()
        {
            this.Heading = new AppMessage();
            this.Heading.Message = "Hello World!";
        }

        public void OnPost()
        {
            this.SubHeading = "Message changed.";
        }

    }
}