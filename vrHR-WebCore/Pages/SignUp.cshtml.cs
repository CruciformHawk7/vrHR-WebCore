using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using vrHR_WebCore.Classes;

namespace vrHR_WebCore.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public HR hr { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string abc { get; set; }
        public void OnPost()
        {
            abc = hr.FirstName + " " + hr.LastName;
        }
    }
}