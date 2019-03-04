using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HijackMonitoringApplication.Resources
{
    public class GeneralHelper
    {
        public static string Encode(string forEncode)
        {
            string newString = string.Empty;
            char[] chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
			Random rndChar = new Random();

			for (int i = 0; i < 8; i++)
			{
				int index = rndChar.Next(0, chars.Length - 1);
				newString += chars[index].ToString();
			}
			forEncode.Insert(forEncode.Length, newString);

			var firstEncode = Convert.ToBase64String(Encoding.ASCII.GetBytes(forEncode));
            for (int i = 0; i < 8; i++)
            {
                int index = rndChar.Next(0, chars.Length - 1);
                newString += chars[index].ToString();
            }

            var reEncode =
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(firstEncode.Insert(firstEncode.Length / 2 + 3, newString)));

            return reEncode;
        }
        public static string Decode(string forDecoded)
        {
            var firstDecode = Encoding.UTF8.GetString(System.Convert.FromBase64String(forDecoded));
            int locate = (firstDecode.Length - 8) / 2;
            var restructure = firstDecode.Remove(locate + 3, 8);
            var reDecode = Encoding.UTF8.GetString(System.Convert.FromBase64String(restructure));
			reDecode = reDecode.Remove(reDecode.Length - 8);
            return reDecode;
        }

    }
}