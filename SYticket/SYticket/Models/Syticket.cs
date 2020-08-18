namespace SYticket.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Syticket : DbContext
    {
        public Syticket()
            : base("name=Syticket")
        {
        }

        public virtual DbSet<Admin_panel> Admin_panel { get; set; }
        public virtual DbSet<companyAddress> companyAddresses { get; set; }
        public virtual DbSet<companyContact> companyContacts { get; set; }
        public virtual DbSet<companyInfo> companyInfoes { get; set; }
        public virtual DbSet<reservation> reservations { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Travel> Travels { get; set; }
        public virtual DbSet<userContact> userContacts { get; set; }
        public virtual DbSet<userPanel> userPanels { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin_panel>()
                .Property(e => e.admin_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Admin_panel>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<Admin_panel>()
                .HasMany(e => e.companyInfoes)
                .WithRequired(e => e.Admin_panel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin_panel>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Admin_panel)
                .HasForeignKey(e => e.adminLogin_ID);

            modelBuilder.Entity<companyAddress>()
                .Property(e => e.company_Street)
                .IsUnicode(false);

            modelBuilder.Entity<companyAddress>()
                .Property(e => e.company_neighborhood)
                .IsUnicode(false);

            modelBuilder.Entity<companyAddress>()
                .Property(e => e.company_city)
                .IsUnicode(false);

            modelBuilder.Entity<companyAddress>()
                .Property(e => e.company_zip_code)
                .IsUnicode(false);

            modelBuilder.Entity<companyContact>()
                .Property(e => e.company_PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<companyContact>()
                .Property(e => e.company_Email)
                .IsUnicode(false);

            modelBuilder.Entity<companyContact>()
                .Property(e => e.company_Website)
                .IsUnicode(false);

            modelBuilder.Entity<companyInfo>()
                .Property(e => e.company_Name)
                .IsUnicode(false);

            modelBuilder.Entity<companyInfo>()
                .Property(e => e.company_Logo)
                .IsUnicode(false);

            modelBuilder.Entity<companyInfo>()
                .HasMany(e => e.companyAddresses)
                .WithRequired(e => e.companyInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<companyInfo>()
                .HasMany(e => e.companyContacts)
                .WithRequired(e => e.companyInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<companyInfo>()
                .HasMany(e => e.Travels)
                .WithRequired(e => e.companyInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<companyInfo>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.companyInfo)
                .HasForeignKey(e => e.companyLogin_ID);

            modelBuilder.Entity<reservation>()
                .Property(e => e.seatNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Travel>()
                .Property(e => e.from_city)
                .IsUnicode(false);

            modelBuilder.Entity<Travel>()
                .Property(e => e.to_city)
                .IsUnicode(false);

            modelBuilder.Entity<Travel>()
                .HasMany(e => e.reservations)
                .WithRequired(e => e.Travel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<userContact>()
                .Property(e => e.userContact_PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<userContact>()
                .Property(e => e.userContact_email)
                .IsUnicode(false);

            modelBuilder.Entity<userContact>()
                .HasMany(e => e.userPanels)
                .WithRequired(e => e.userContact)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<userPanel>()
                .Property(e => e.userPanel_Name)
                .IsUnicode(false);

            modelBuilder.Entity<userPanel>()
                .Property(e => e.userPanel_Surname)
                .IsUnicode(false);

            modelBuilder.Entity<userPanel>()
                .Property(e => e.userPanel_national_ID)
                .IsUnicode(false);

            modelBuilder.Entity<userPanel>()
                .HasMany(e => e.reservations)
                .WithRequired(e => e.userPanel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<userPanel>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.userPanel)
                .HasForeignKey(e => e.userLogin_ID);

            modelBuilder.Entity<User>()
                .Property(e => e.user_Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_role)
                .IsUnicode(false);
        }
    }
}
