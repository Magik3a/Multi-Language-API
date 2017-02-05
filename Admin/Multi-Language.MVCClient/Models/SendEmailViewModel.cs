using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models
{
    public class SendEmailViewModel
    {
        [Required]
        [Display(Name = "Your name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Your email")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}