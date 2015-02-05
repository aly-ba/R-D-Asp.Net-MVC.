using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


public  class ConfigureTwoFactorViewModel
{  
	public string SelectedProvider {get;set; }
	public ICollection<System.Web.Mvc.SelectListItem> Providers {get;set; }
}