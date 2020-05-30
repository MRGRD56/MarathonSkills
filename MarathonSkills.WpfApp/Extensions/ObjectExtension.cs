using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.WpfApp.Extensions
{
	public static class ObjectExtension
	{
		/// <summary>
		/// Конвертирует объект в System.Int32.
		/// </summary>
		/// <param name="obj">Объект.</param>
		/// <returns>Число.</returns>
		public static int ToInt(this object obj) => Convert.ToInt32(obj);
	}
}
