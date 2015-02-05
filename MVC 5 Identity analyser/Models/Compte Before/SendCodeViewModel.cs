using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentitySample.Models
{	
	public class SendCodeViewModel
	{
	public string SelecProvider {get;set;}
	public ICollection<System.Web.Mvc.SelectListItem> Providers {get;set;}
	public string ReturnUrl {get;set; }
	}
}	