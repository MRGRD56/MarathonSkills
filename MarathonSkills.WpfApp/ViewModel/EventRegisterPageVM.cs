using MarathonSkills.WpfApp.DataModels;
using MarathonSkills.WpfApp.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.WpfApp.ViewModel
{
	public class EventRegisterPageVM
	{
		public ObservableCollection<Charity> Charities { get; set; }

		public EventRegisterPageVM()
		{
			Charities = App.DbContext.Charities.ToObsCol();
		}
	}
}
