namespace MarathonSkills.WpfApp.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Charity")]
    public partial class Charity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Charity()
        {
            Registrations = new HashSet<Registration>();
        }

        public int CharityId { get; set; }

        [Required]
        [StringLength(100)]
        public string CharityName { get; set; }

        [StringLength(2000)]
        public string CharityDescription { get; set; }

        [StringLength(50)]
        public string CharityLogo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
