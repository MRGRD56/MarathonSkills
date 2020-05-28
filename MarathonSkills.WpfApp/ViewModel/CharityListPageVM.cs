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
	public class CharityListPageVM
	{
		public ObservableCollection<Charity> Charities { get; set; }

		public CharityListPageVM()
		{
			Charities = App.DbContext.Charities.ToObsCol().Select(x => new Charity { CharityId = x.CharityId, CharityDescription = x.CharityDescription, 
				CharityName = x.CharityName, Registrations = x.Registrations, CharityLogo = $"/Images/CharityLogos/{x.CharityLogo}"}).ToObsCol();
		}
	}
}
