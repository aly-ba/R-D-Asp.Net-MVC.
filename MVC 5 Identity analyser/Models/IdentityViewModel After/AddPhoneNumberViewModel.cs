using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public  class AddPhoneNumberViewModel 
{
	[Required]
    [Phone]
    [Display(Name = "Phone Number")]
	public string Number {get;set; }

}