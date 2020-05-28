using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.WpfApp.Extensions
{
	public static class IEnumerableExtension
	{
		public static ObservableCollection<T> ToObsCol<T>(this IEnumerable<T> col) =>
			new ObservableCollection<T>(col);
	}
}
