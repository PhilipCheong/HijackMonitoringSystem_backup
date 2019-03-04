using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using HijackMonitoringApplication.ViewModel;

namespace HijackMonitoringApplication.Controllers
{
	[Authorize(Roles = "Toffstech_Admin")]
	public class AccountController : BaseController
    {
        private readonly UserService userService = new UserService();
        private readonly HijackingDomainService hijackingDomainService = new HijackingDomainService();

		[AllowAnonymous]
        public ActionResult Login()
        {
            dynamic returnData = new ExpandoObject();
            returnData.Error = "";
            return View(returnData);
        }
		[AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserViewModel authenticatedUser)
        {
            var userData = userService.LoginVerification(authenticatedUser.Username.ToUpper(), authenticatedUser.Password);
            dynamic returnData = new ExpandoObject();
            if (userData.Id != null)
            {
                var userInfo = new List<Claim>()
                {
                    new Claim(ClaimTypes.SerialNumber, userData.Id),
                    new Claim(ClaimTypes.Name, userData.Username),
                    new Claim(ClaimTypes.Email, userData.Email),
                    new Claim(ClaimTypes.GroupSid, userData.CustomerId),
                    new Claim(ClaimTypes.Role, userData.Type),
                    new Claim("userInfo", JsonConvert.SerializeObject(userData))

                };
                var identity = new ClaimsIdentity(userInfo, DefaultAuthenticationTypes.ApplicationCookie);
                var authentication = HttpContext.GetOwinContext().Authentication;
                authentication.SignIn(new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false,
                    ExpiresUtc = DateTime.Now.ToLocalTime().AddDays(1)
                }, identity);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                returnData.Error = "Username or passowrd is incorrect. Please check your input and try again";
                return View(returnData);
            }
        }

		public ActionResult Logout()
		{
			var authentication = HttpContext.GetOwinContext().Authentication;

			authentication.SignOut();

			return RedirectToAction("Login");
		}

		public ActionResult Index()
        {
            dynamic returnData = new ExpandoObject();
			try
			{
				returnData.userName = userService.GetAll();
				returnData.customerId = userService.GetAllCustomerId().Where(p => !p.Equals(CurrentAccount.CustomerId));
				returnData.selfId = CurrentAccount.CustomerId;
				returnData.userType = new[] { "Toffstech_Admin", "Admin", "User" };
			}catch(Exception ex)
			{
				log.Error(ex);
			}
            return View(returnData);
        }

        public ActionResult AddOrEditForUser(UserViewModel userModel)
        {
            var userDto = new UserDto();

			try { 
            if (userModel.Id != null)
            {
                userDto = userService.GetById(userModel.Id);
            }

            userDto.Username = userModel.Username.ToUpper();
            userDto.Password = userModel.Password;
            userDto.Type = userModel.Type;
            userDto.Email = userModel.Email.ToLower();
            userDto.CustomerId = userModel.CustomerId.ToUpper();

            userService.AddOrEdit(userDto);
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return RedirectToAction("Index");
        }

        public string GroupSearch(string customerId)
        {
			try
			{
				if (customerId.ToUpper().Equals("TOFFSTECH"))
				{
					return JsonConvert.SerializeObject(userService.GetAll());
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(userService.GetAll().Where(p => p.CustomerId.Equals(customerId)));
        }

        public string FulfillEdit(string id)
        {
			if (string.IsNullOrEmpty(id)) return JsonConvert.SerializeObject("");
            return JsonConvert.SerializeObject(userService.GetById(id));
        }
        [HttpPost]
        public ActionResult DeleteUser(string Id)
        {
			if (string.IsNullOrEmpty(Id)) return RedirectToAction("Index");
            userService.Remove(Id);

            return RedirectToAction("Index");
        }

		[HttpPost]
		public string IsUsernameExisted (string username, string id)
		{
			var result = false;

			if (string.IsNullOrEmpty(id))
			{
				result = userService.Find(s => s.Username.Equals(username.ToUpper().Trim())).Any();
			}
			else
			{
				result = userService.Find(s => !s.Id.Equals(id) && s.Username.Equals(username.ToUpper().Trim())).Any();
			}

			return JsonConvert.SerializeObject(new { result });
		}

		[HttpPost]
		public string IsEmailExisted(string email, string id)
		{
			var result = false;
			if (string.IsNullOrEmpty(id))
			{
				result = userService.Find(s => s.Email.Equals(email.ToLower().Trim())).Any();
			}
			else
			{
				result = userService.Find(s => !s.Id.Equals(id) && s.Email.Equals(email.ToUpper().Trim())).Any();
			}

			return JsonConvert.SerializeObject(new { result });
		}
	}
}
