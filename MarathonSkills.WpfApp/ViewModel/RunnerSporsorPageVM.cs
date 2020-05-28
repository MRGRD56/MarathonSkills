using MarathonSkills.WpfApp.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.WpfApp.ViewModel
{
	public class RunnerSporsorPageVM
	{
		public ObservableCollection<RunnerExt> RunnersExts { get; set; }

		public RunnerSporsorPageVM()
		{
			//TODO: лучше передавать ещё и Registration.RegistrationId
			var runnersUsers = App.DbContext.Runners.ToObsCol()
				.Join(App.DbContext.Users.ToObsCol(),
				r => r.Email, //Runners
				u => u.Email, //Users
				(r, u) => new { u.FirstName, u.LastName, r.CountryCode, r.RunnerId });
			var ruRegistrations = runnersUsers
				.Join(App.DbContext.Registrations.ToObsCol(),
				ru => ru.RunnerId, //Runners & Users
				g => g.RunnerId, //Registrations
				(ru, g) => new { ru.FirstName, ru.LastName, ru.CountryCode, ru.RunnerId, g.RegistrationId });
			var rugEvents = ruRegistrations // Runners & Users & Registrations & RegistrationEvents
				.Join(App.DbContext.RegistrationEvents.ToObsCol(),
				rug => rug.RegistrationId, //Runners & Users & Regirstrations
				e => e.RegistrationId, //RegistrationEvents
				(rug, e) => new { rug.FirstName, rug.LastName, rug.CountryCode, e.BibNumber, rug.RunnerId });

			RunnersExts = rugEvents
				.Select(x => new RunnerExt(x.RunnerId, x.FirstName, x.LastName, x.CountryCode, x.BibNumber))
				.OrderBy(x => x.FirstName)
				.ThenBy(x => x.LastName)
				.ToObsCol();
		}
	}
}
