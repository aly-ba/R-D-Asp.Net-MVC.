using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentitySample.Models
{
	public class SendCodeViewModel {
	
	 [Required]
     public string  Provider {get;set; }
	
	 [Required]
     [Display(Name = "Code")]
     public string Code { get; set; }
	 
	 private string ReturnUrl {get;set; }
	 
	 [Display(Name = "Remember this browser?")]
     public bool RememberBrowser { get; set; }
	 
	}	
}	