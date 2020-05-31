using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarathonSkills.WpfApp.Extensions
{
	static class RegexUtilities
	{
		public static bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			try
			{
				// Normalize the domain
				email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
									  RegexOptions.None, TimeSpan.FromMilliseconds(200));

				// Examines the domain part of the email and normalizes it.
				string DomainMapper(Match match)
				{
					// Use IdnMapping class to convert Unicode domain names.
					var idn = new IdnMapping();

					// Pull out and process domain name (throws ArgumentException on invalid)
					var domainName = idn.GetAscii(match.Groups[2].Value);

					return match.Groups[1].Value + domainName;
				}
			}
			catch (RegexMatchTimeoutException e)
			{
				return false;
			}
			catch (ArgumentException e)
			{
				return false;
			}

			try
			{
				return Regex.IsMatch(email,
					@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
					RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}

		public static bool IsValidPassword(string pswd)
		{
			//•	Минимум 6 символов
			//•	Минимум 1 прописная буква
			//•	Минимум 1 цифра
			//•	По крайней мере один из следующих символов: ! @ # $ % ^

			bool IsSmallLetter(char c)
			{
				if (Char.IsLetter(c))
				{
					return c.ToString().ToLower() == c.ToString();
				}
				else return false;
			}

			bool b1 = pswd.Length >= 6;

			bool b2 = false;
			foreach (char c in pswd)
			{
				if (IsSmallLetter(c))
				{
					b2 = true;
					break;
				}
			}

			bool b3 = false;
			foreach (char c in pswd)
			{
				if (Char.IsDigit(c))
				{
					b3 = true;
					break;
				}
			}

			bool b4 = false;
			foreach (char c in pswd)
			{
				if (c == '!' || c == '@' || c == '#' || c == '$' || c == '%' || c == '^')
				{
					b4 = true;
					break;
				}
			}

			return b1 && b2 && b3 && b4;
		}
	}
}

