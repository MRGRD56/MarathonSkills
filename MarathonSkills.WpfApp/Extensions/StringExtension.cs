using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.WpfApp.Extensions
{
	public static class StringExtension
	{
		public static bool IsNotNull(this string str) => !string.IsNullOrEmpty(str);
	}
}
