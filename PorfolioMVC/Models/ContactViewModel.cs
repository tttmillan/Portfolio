using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PorfolioMVC.Models
{
    public class ContactViewModel
    {
        //This is a ViewModel for the contact form functionality. It is a ViewModel because it is not tied to a database.
        [Required(ErrorMessage = "**Please provide a Name**")]
        public string Name { get; set; }

        [Required(ErrorMessage = "**Please provide an Email**")]
        [EmailAddress]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = "**Please provide a Message**")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}