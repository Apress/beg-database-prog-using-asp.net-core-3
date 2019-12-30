using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApi.Models;
using Microsoft.AspNetCore.Mvc;



namespace HelloWorldWebApi.Controllers
{
    [Route("api/[controller]")]
    
    public class ValuesController : Controller
    {
        [HttpGet]
        public IEnumerable<AppMessage> Get()
        {
            List<AppMessage> messages = new List<AppMessage>();
            messages.Add(new AppMessage() { Message = "Hello World!" });
            messages.Add(new AppMessage() { Message = "Hello Galaxy!" });
            messages.Add(new AppMessage() { Message = "Hello Universe!" });

            return messages;
        }
    }
}
