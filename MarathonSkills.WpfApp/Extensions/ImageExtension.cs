using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MarathonSkills.WpfApp.Extensions
{
	public static class ImageExtension
	{
		public static void SetSource(this Image img, string path)
		{
			var bi = new BitmapImage();
			bi.BeginInit();
			bi.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
			bi.EndInit();
			img.Source = bi;
		}
	}
}
