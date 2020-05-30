using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.WpfApp.Extensions
{
	public static class StringExtension
	{
		/// <summary>
		/// Определяет, имеет ли строка значение null. Если нет - возвращает true.
		/// </summary>
		/// <param name="str">Строка.</param>
		/// <returns>Не имеет значение null.</returns>
		public static bool IsNotNull(this string str) => !string.IsNullOrEmpty(str);
	}
}
