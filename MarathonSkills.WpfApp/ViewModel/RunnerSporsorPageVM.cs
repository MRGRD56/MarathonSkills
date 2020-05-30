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
	/// <summary>
	/// Представляет ViewModel для RunnerSporsorPage.
	/// </summary>
	public class RunnerSporsorPageVM
	{
		public ObservableCollection<RunnerExt> RunnersExts { get; set; }

		/// <summary>
		/// Благотворительная организация выбранного бегуна.
		/// </summary>
		public Charity SelectedCharity { get; set; }

		public RunnerSporsorPageVM()
		{
			//Объединение Runners & Users
			var runnersUsers = App.DbContext.Runners.ToObsCol()
				.Join(App.DbContext.Users.ToObsCol(),
				r => r.Email, //Runners
				u => u.Email, //Users
				(r, u) => new { u.FirstName, u.LastName, r.CountryCode, r.RunnerId });

			//Объединение (Runners & Users) & Regustrations
			var ruRegistrations = runnersUsers
				.Join(App.DbContext.Registrations.ToObsCol(),
				ru => ru.RunnerId, //Runners & Users
				g => g.RunnerId, //Registrations
				(ru, g) => new { ru.FirstName, ru.LastName, ru.CountryCode, ru.RunnerId, g.RegistrationId, g.CharityId });

			//Объединение (Runners & Users & Regustrations) & RegistrationEvents
			var rugEvents = ruRegistrations
				.Join(App.DbContext.RegistrationEvents.ToObsCol(),
				rug => rug.RegistrationId, //Runners & Users & Regirstrations
				e => e.RegistrationId, //RegistrationEvents
				(rug, e) => new { rug.FirstName, rug.LastName, rug.CountryCode, e.BibNumber, rug.RunnerId, rug.RegistrationId, rug.CharityId });

			//Объединение (Runners & Users & Regustrations & RegistrationEvents) & Charities
			var rugeCharities = rugEvents
				.Join(App.DbContext.Charities.ToObsCol(),
				ruge => ruge.CharityId,
				c => c.CharityId,
				(ruge, c) => new { ruge.FirstName, ruge.LastName, ruge.CountryCode, ruge.BibNumber, ruge.RunnerId, ruge.RegistrationId, c.CharityId });

			//Добавление в коллекцию и сортировка.
			RunnersExts = rugEvents
				.Select(x => new RunnerExt(x.RunnerId, x.RegistrationId, x.FirstName, x.LastName, x.CountryCode, x.BibNumber, x.CharityId))
				.OrderBy(x => x.FirstName)
				.ThenBy(x => x.LastName)
				.ToObsCol();
		}
	}
}
