using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdenitySample.Controllers
{

	[Authorise]
	public class ManageController :Controller
	{
		public ManageController()
		{
		
		
		}
	
		public ManageController(ApplicationUserMananger userManager)
		{
			YserManager = userManager;
		}
		
		private ApplicationUserManager _userManager;
		
		public ApplicationUserManager UserManager
		{
			 get 
			 {
				return _usernManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			 }
		}
		
		//GET: /Account/Index
		public async Task<ActionResult> Index(ManageMessageId? message)
		{
			ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two factor provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "The phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";
			
			var model = new IndexViewModel
			{
				HasPassword = HasPassword();
				PhoneNumber = await UserManager.GetPhoneNumberAsync(User.Identity.GetUserId())
				TwoFactor =await UserManager.GetTwoFactorEnableAsync(User.Identity.GetUserId()),
				Logins= await UserManager.GetLoginsAsync(User.Identity.GetUserId()),
				BrowserRemembered = await AuthentificatonManager.TwoFactorBrowserRememberedAsync(User, Identity.GetUserid())
				
			};
			return View(model);
		}
		
		//GET: /Account/RemoveLogin
		public ActionResult RemoveLogin()
		{
			var linkedAccounts = UserManagerGetLogins(User.Identity.GetUserId());
			ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count >1;
			return View(linkedAccounts);
			
	    }
		
		//
		//POST: /Manage/RemoveLogin
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> RemoveLogin(string loginProvider, string provderKey)
		{
			ManageMessageId? message;
		    
			var result = await    UserManage.RemoveLoginAsync(User.GetUserId(), new UserLoginInfo(login, providerKey));
			if(result.Succeeded)
			{
				var user = awit UserManager.FindByUdAsync(User.Identity.GetUserId()));
				if(User !=null)
				{
					await(SignInAsync(user, isPersistent:false);
				}
				else {
					message = ManageMessageId.Error;
				}
				return RedirecToAction("ManageLogins", new {Message =message});
			 
			 }
			 return RedirectToAction("ManageLogins", new {Message =message });
		}
		
		 //
        // GET: /Account/AddPhoneNumber
		public ActionResult AddPhoneNumber()
		{
				return View();
		}
		
		 // GET: /Account/AddPhoneNumber
		[HttpPost]
		[ValidateAntiForgeryToken]
		
		public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
		{
				if(!ModelState.IsValid)
				{
					return view(model)
				}
				
				//Generate the token    and send it
				var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
				if(UserManager.SmsService !=null )
				{
					var message = new IdentityMessage 
					{
						Destination = model.Number,
						Body = "Your security code is:" +code
					};
					await UserManager.SmsService.SendAsync(message);
				}
				return RedirectionToAction("VerifyPhoneNumber", new  {PhoneNumber =model.Number });
		}
		
		// POST: /Manage/RememberBrowser
		public ActionResult RememberBrowser()
		{
		       var rememberBrowserIdentity = AuthentificationManager.CreateTwoFactorRememberBrowserIdentity(User.Identity.GetUSerId());
			   AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent= true}, rememberBrowserIdentity);
			   return RedirectToAction("Index", "Manage");
		
	    }
		
		//
		// POST: /Manage/ForGetBrowser
		[HttpPost]
		public async Task<ActionResult> EnableTFA()
		{
		await UserManager.SetTwoFactoEnabledAsync(User.Identity.GetUserId(), true);
		var user = await UserManager.FindByIdAsync(user.Identity.GetUserId());
		if(user!=null)
			{
				
					await SignInAsync(user, isPersistent: false);
			}
			  return RedirectToAction("Index", "Manage");
		}
		
		//GET : /Account.VerifyPhoneNumber
		public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
		{
			 // This code allows you exercise the flow without actually sending codes
            // For production use please register a SMS provider in IdentityConfig and generate a code here.
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            ViewBag.Status = "For DEMO purposes only, the current code is " + code;
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
		}
		
		//
		//POST: /Account/VerifyPhoneNumber
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
		{
				if(!ModelState.IsValid)
				{
					return View(model);
				}
		}
		
		var result = await User.Manager.ChangePhoneNumberAsync(User.Identity.GetUSerId(), model.PhoneNumber, model.Code);
		if(result.Succeededà
		{
			var user = await UserManager.FindByIdAsync(User.Identiy.GetUserId());
			if(user != null)
			{
				await SignInAsync(user, isPersistent:false);
			}
			     return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
		
		}
		
		//
        // GET: /Account/RemovePhoneNumber
		public async Task<ActionResult> RemovePhoneNumber()
		{
			var result = await UserManger.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
			if(!result.Succeeded) 
			{
				return RediretToActio("Index", new {Message = ManageMessageId.Error });
			}
			var user =await UserManager.FindByIdAsync(User.Identity.GetUserId());
			if(user != null)
			{
				await SignInAsync(user, isPersistent:false);
			}
			return RedirectToAction("Index", new {Message = ManageMessageId.RemovePhoneSuccess});
		}
		
		
		//
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }
		
		
		
		// POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }
		
		 //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }
		
		
		 // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
		
		public async Task<ActionResult> ManageLogins(ManageMessageId? message)
		{
			 ViewBag.StatusMessage=
			 message == ManageMessageId.RemoveLoginsSucecess? "The external logins was removed"
			 :message == ManageMessageId.Error ?"An error has occured."
			 :"";
			 var user = await UserMAnage.FindByIdAsync(User.Identity.GetUserId());
			 if(user == null) 
			 {
				return View("Error");
			 }
			 var userLogins = await UserManager.GetLoginsAsync(USer.Identity.GetUserId());
			 var otherLogins=AuthenticationManager.GetExternalAuthenticationTypes().Where(auth =>user.Logins.All(ul =>auth.AuthenticationType != ul.LoginProvider)).ToList();
			 ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count> 1;
			 return View (
			 
			   new ManageLoginsViewMOdel {
			   CurrentLogins =userLogins,
			   OtherLogins=otherLogins 
			 });
			 }
			
	   public ActionResult LinkLogin(string provider)
	   {
	     //Request a redirect to the external login provider to link a login for the current user
		 return AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Idenity.GetUserId());
	   }
	   
	   
		//GET : /Manage/LinkLogin Callback
		public async Task<ActionResult> LinkLoginCallback()
		{
		  var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identiy.GetUserId());
		  
		  if(loginINfo == null)
		  {
		  return RedirectionToAction("ManageLogins", new {Message= ManaMessageId.Error});
		  }
		  var result = await UserManager.AddLoginAsync(USer.Identity.GetUserId(); loginInfo;Login);
		  return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectTOAction"
		  }
		}
		
		#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
		
		}
		
		
		private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }
		
		
		private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
		
		 private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
		
		private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }
		
		public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
		
		
		
		
		
	}

}