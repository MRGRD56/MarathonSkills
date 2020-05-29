using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.WpfApp.ViewModel
{
	public class RunnerExt
	{
		/// <summary>
		/// RunnerExt
		/// </summary>
		/// <param name="id">RunnerId</param>
		/// <param name="ri">RegistrationId</param>
		/// <param name="fn">FirstName</param>
		/// <param name="ln">LastName</param>
		/// <param name="cc">CountryCode</param>
		/// <param name="bn">BibNumber</param>
		/// <param name="ci">CharityId</param>
		public RunnerExt(int id, int ri, string fn, string ln, string cc, short? bn, int ci) => 
			(RunnerId, RegistrationId, FirstName, LastName, CountryCode, BibNumber, CharityId) = (id, ri, fn, ln, cc, bn, ci);

		public int RunnerId { get; set; }

		public int RegistrationId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string CountryCode { get; set; }

		public short? BibNumber { get; set; }

		public int CharityId { get; set; }

		public override string ToString() =>
			$"{FirstName} {LastName} - {BibNumber} ({CountryCode})";
	}
}
