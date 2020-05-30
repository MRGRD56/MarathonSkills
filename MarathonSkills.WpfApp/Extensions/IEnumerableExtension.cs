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
		/// <summary>
		/// Конвертирует любую коллекцию в ObservableCollection&lt;<typeparamref name="T"/>&gt;.
		/// </summary>
		/// <typeparam name="T">Тип коллекции.</typeparam>
		/// <param name="col">Входная коллекция.</param>
		/// <returns>Выходная коллекция ObservableCollection&lt;<typeparamref name="T"/>&gt;.</returns>
		public static ObservableCollection<T> ToObsCol<T>(this IEnumerable<T> col) =>
			new ObservableCollection<T>(col);
	}
}
