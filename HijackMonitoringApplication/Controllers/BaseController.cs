using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using Newtonsoft.Json;

namespace HijackMonitoringApplication.Controllers
{
	public class BaseController : Controller
	{
		public UserDto CurrentAccount;
		public static readonly log4net.ILog log =
			log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);

			// Grab the user's login information from Identity
			if (!(User is ClaimsPrincipal authenticatedUser)) return;
			var claims = authenticatedUser.Claims.ToList();

			var userStateString = claims.FirstOrDefault(p => p.Type == "userInfo")?.Value;

			if (!string.IsNullOrEmpty(userStateString))
				CurrentAccount = JsonConvert.DeserializeObject<UserDto>(userStateString);
		}

		public string Encode(string forEncode)
		{
			string newString = string.Empty;
			char[] chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
			Random rndChar = new Random();

			for (int i = 0; i < 8; i++)
			{
				int index = rndChar.Next(0, chars.Length - 1);
				newString += chars[index].ToString();
			}
			forEncode = forEncode.Insert(forEncode.Length, newString);
			newString = string.Empty;
			var firstEncode = Convert.ToBase64String(Encoding.ASCII.GetBytes(forEncode));
			for (int i = 0; i < 8; i++)
			{

				int index = rndChar.Next(0, chars.Length - 1);
				newString += chars[index].ToString();
			}

			var reEncode =
				Convert.ToBase64String(
					Encoding.ASCII.GetBytes(firstEncode.Insert(firstEncode.Length / 2 + 3, newString)));

			return JsonConvert.SerializeObject(reEncode);
		}

		public string Decode(string forDecoded)
		{
			var firstDecode = Encoding.UTF8.GetString(System.Convert.FromBase64String(forDecoded));
			int locate = (firstDecode.Length - 8) / 2;
			var restructure = firstDecode.Remove(locate + 3, 8);
			var reDecode = Encoding.UTF8.GetString(System.Convert.FromBase64String(restructure));
			reDecode = reDecode.Remove(reDecode.Length - 8);
			return JsonConvert.SerializeObject(reDecode);
		}
	}
}