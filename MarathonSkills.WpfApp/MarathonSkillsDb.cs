namespace MarathonSkills.WpfApp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using MarathonSkills.WpfApp.DataModels;

    public partial class MarathonSkillsDb : DbContext
    {
        public MarathonSkillsDb()
            : base("name=MarathonSkillsDb")
        {
        }

        public virtual DbSet<Charity> Charities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Marathon> Marathons { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<RaceKitOption> RaceKitOptions { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<RegistrationEvent> RegistrationEvents { get; set; }
        public virtual DbSet<RegistrationStatu> RegistrationStatus { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Runner> Runners { get; set; }
        public virtual DbSet<Sponsorship> Sponsorships { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Timesheet> Timesheets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Charity>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.Charity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.CountryCode)
                .IsFixedLength();

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Marathons)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Runners)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Volunteers)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.EventId)
                .IsFixedLength();

            modelBuilder.Entity<Event>()
                .Property(e => e.EventTypeId)
                .IsFixedLength();

            modelBuilder.Entity<Event>()
                .Property(e => e.Cost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.RegistrationEvents)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventType>()
                .Property(e => e.EventTypeId)
                .IsFixedLength();

            modelBuilder.Entity<EventType>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.EventType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Gender>()
                .HasMany(e => e.Runners)
                .WithRequired(e => e.Gender1)
                .HasForeignKey(e => e.Gender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Gender>()
                .HasMany(e => e.Volunteers)
                .WithRequired(e => e.Gender1)
                .HasForeignKey(e => e.Gender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Marathon>()
                .Property(e => e.CountryCode)
                .IsFixedLength();

            modelBuilder.Entity<Marathon>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Marathon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.PositionName)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.PositionDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.PayPeriod)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.Payrate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Staffs)
                .WithRequired(e => e.Position)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RaceKitOption>()
                .Property(e => e.RaceKitOptionId)
                .IsFixedLength();

            modelBuilder.Entity<RaceKitOption>()
                .Property(e => e.Cost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RaceKitOption>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.RaceKitOption)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Registration>()
                .Property(e => e.RaceKitOptionId)
                .IsFixedLength();

            modelBuilder.Entity<Registration>()
                .Property(e => e.Cost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Registration>()
                .Property(e => e.SponsorshipTarget)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Registration>()
                .HasMany(e => e.RegistrationEvents)
                .WithRequired(e => e.Registration)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Registration>()
                .HasMany(e => e.Sponsorships)
                .WithRequired(e => e.Registration)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RegistrationEvent>()
                .Property(e => e.EventId)
                .IsFixedLength();

            modelBuilder.Entity<RegistrationStatu>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.RegistrationStatu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleId)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Runner>()
                .Property(e => e.CountryCode)
                .IsFixedLength();

            modelBuilder.Entity<Runner>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.Runner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sponsorship>()
                .Property(e => e.Amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Staff>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Timesheets)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Timesheet>()
                .Property(e => e.PayAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<User>()
                .Property(e => e.RoleId)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Runners)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Volunteer>()
                .Property(e => e.CountryCode)
                .IsFixedLength();
        }
    }
}
