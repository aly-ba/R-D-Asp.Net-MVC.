using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



public  class VerifyPhoneNumberViewModel 
{  
    [Required]
    [Display(Name = "Code")]
    public string Code {get;set; }
	
  	[Required]
    [Phone]
    [Display(Name = "Phone Number")]
	public strng PhoneNumber {get;set;}
   
}