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
	/// ViewModel для RunnerRegistrationPage.
	/// </summary>
	public class RunnerRegistrationPageVM
	{
		public ObservableCollection<Gender> Genders { get; set; }

		public ObservableCollection<Country> Countries { get; set; }

		//private string SelectedImage { get; set; }
		//private string SelectedImageName { get; set; }

		public RunnerRegistrationPageVM()
		{
			Genders = App.DbContext.Genders.ToObsCol();
			Countries = App.DbContext.Countries.ToObsCol();
		}
	}
}
