using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.WpfApp.Extensions
{
	public static class ObjectExtension
	{
		public static int ToInt(this object obj) => Convert.ToInt32(obj);
	}
}
