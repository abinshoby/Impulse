using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Impulse.EF;
using Microsoft.Extensions.Configuration;

namespace Impulse.Data.BLL.EF
{
    public class DataContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<LookUpAddressType> LookUpAddressType { get; set; }

        public virtual DbSet<LookUpContactType> LookUpContactType { get; set; }
        public virtual DbSet<LookUpUserType> LookUpUserType { get; set; }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<LookUpUserStatus> LookUpUserStatus { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<LookUpCompanyType> LookUpCompanyType { get; set; }
        public virtual DbSet<VendorInterest> VendorInterest { get; set; }
        public virtual DbSet<VendorInterestRate> VendorInterestRate { get; set; }
        public virtual DbSet<LookUpRateType> LookUpRateType { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<EventSubType> EventSubType { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventStatusType> EventStatusType { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<ScheduleType> ScheduleType { get; set; }
        public virtual DbSet<SurfaceType> SurfaceType { get; set; }
        public virtual DbSet<EventDetails> EventDetails { get; set; }
        public virtual DbSet<EventType_Place> EventType_Place { get; set; }
        public virtual DbSet<VendorInvitation> VendorInvitation { get; set; }
        public virtual DbSet<Citizen> Citizen { get; set; }


        // Unable to generate entity type for table 'dbo.BatchFee'. Please see the warning messages.


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseMySQL(Configuration["Logging:ConnectionStrings:DefaultConnection"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("Impulse.EF.Address", b =>
            {
                b.Property<long>("AddressId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Address1")
                    .IsRequired()
                    .HasColumnName("Address")
                    .IsUnicode(false);

                b.Property<string>("CreatedBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<DateTime?>("CreatedDate")
                    .HasColumnType("datetime");

                b.Property<long>("LookUpAddressTypeId");

                b.Property<long>("Pin");

                b.Property<string>("RecordStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<string>("Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<string>("UpdatedBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<DateTime?>("UpdatedDate")
                    .HasColumnType("datetime");

                b.Property<long>("UserId");

                b.HasKey("AddressId");

                b.HasIndex("LookUpAddressTypeId");

                b.HasIndex("UserId");

                b.ToTable("Address");
            });

            modelBuilder.Entity("Impulse.EF.Company", b =>
            {
                b.Property<long>("CompanyId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("CompanyName")
                    .IsRequired()
                    .IsUnicode(false);

                b.Property<long>("LookUpCompanyTypeId");

                b.Property<long>("Pin");

                b.Property<string>("RecordStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<long>("UserId");

                b.HasKey("CompanyId");

                b.HasIndex("LookUpCompanyTypeId");

                b.ToTable("Company");
            });

            modelBuilder.Entity("Impulse.EF.Contact", b =>
            {
                b.Property<long>("ContactId")
                    .ValueGeneratedOnAdd();

                b.Property<long?>("LookUpContactTypeId");

                b.Property<string>("RecordStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<long?>("UserId");

                b.Property<string>("Value")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                b.HasKey("ContactId");

                b.HasIndex("LookUpContactTypeId");

                b.HasIndex("UserId");

                b.ToTable("Contact");
            });

            modelBuilder.Entity("Impulse.EF.EventSubType", b =>
            {
                b.Property<int>("EventSubTypeId")
                    .ValueGeneratedOnAdd();

                b.Property<int?>("EventTypeId");

                b.Property<bool?>("IsDeleted");

                b.Property<string>("SubTypeName");

                b.HasKey("EventSubTypeId");

                b.HasIndex("EventTypeId");

                b.ToTable("EventSubType");
            });

            modelBuilder.Entity("Impulse.EF.EventType", b =>
            {
                b.Property<int>("EventTypeId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("EventTypeName");

                b.Property<bool?>("IsDeleted");

                b.HasKey("EventTypeId");

                b.ToTable("EventType");
            });

            modelBuilder.Entity("Impulse.EF.LookUpAddressType", b =>
            {
                b.Property<long>("LookUpAddressTypeId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("AddressType")
                    .IsRequired()
                    .IsUnicode(false);

                b.Property<string>("CreatedBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<DateTime?>("CreatedDate")
                    .HasColumnType("datetime");

                b.Property<string>("RecordStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<string>("UpdatedBy")
                    .IsUnicode(false);

                b.Property<DateTime?>("UpdatedDate")
                    .HasColumnType("datetime");

                b.HasKey("LookUpAddressTypeId");

                b.ToTable("LookUpAddressType");
            });

            modelBuilder.Entity("Impulse.EF.EventStatusType", b =>
            {
                b.Property<int>("StatusId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("StatusName")
                    .IsRequired()
                    .IsUnicode(false);
                b.Property<bool?>("IsDeleted");


                b.HasKey("StatusId");

                b.ToTable("EventStatusType");
            });

            modelBuilder.Entity("Impulse.EF.LookUpCompanyType", b =>
            {
                b.Property<long>("LookUpCompanyTypeId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("CompanyType")
                    .IsRequired()
                    .IsUnicode(false);

                b.Property<string>("CreatedBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<DateTime?>("CreatedDate")
                    .HasColumnType("datetime");

                b.Property<string>("RecordStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<string>("UpdatedBy")
                    .IsUnicode(false);

                b.Property<DateTime?>("UpdatedDate")
                    .HasColumnType("datetime");

                b.HasKey("LookUpCompanyTypeId");

                b.ToTable("LookUpCompanyType");
            });

            modelBuilder.Entity("Impulse.EF.LookUpContactType", b =>
            {
                b.Property<long>("LookUpContactTypeId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("ContactType")
                    .IsRequired()
                    .IsUnicode(false);

                b.Property<string>("RecordStatus")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.HasKey("LookUpContactTypeId");

                b.ToTable("LookUpContactType");
            });

            modelBuilder.Entity("Impulse.EF.LookUpRateType", b =>
            {
                b.Property<long>("LookUpRateTypeId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("RateType");

                b.Property<string>("RecordStatus");

                b.HasKey("LookUpRateTypeId");

                b.ToTable("LookUpRateType");
            });

            modelBuilder.Entity("Impulse.EF.LookUpUserStatus", b =>
            {
                b.Property<long>("LookUpUserStatusId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("CreatedBy")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime");

                b.Property<string>("RecordStatus")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<string>("UpdatedBy")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                b.Property<DateTime?>("UpdatedDate")
                    .HasColumnType("datetime");

                b.Property<string>("UserStatus")
                    .IsRequired()
                    .IsUnicode(false);

                b.HasKey("LookUpUserStatusId");

                b.ToTable("LookUpUserStatus");
            });

            modelBuilder.Entity("Impulse.EF.LookUpUserType", b =>
            {
                b.Property<long>("LookUpUserTypeId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("CreatedBy")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime");

                b.Property<string>("RecordStatus")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<string>("UpdatedBy")
                    .IsUnicode(false);

                b.Property<DateTime?>("UpdatedDate")
                    .HasColumnType("datetime");

                b.Property<string>("UserType")
                    .IsRequired()
                    .IsUnicode(false);

                b.HasKey("LookUpUserTypeId");

                b.ToTable("LookUpUserType");
            });

            modelBuilder.Entity("Impulse.EF.Organiser", b =>
            {
                b.Property<long>("OrganiserId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("OrganiserDescription")
                    .IsUnicode(false);

                b.Property<string>("OrganiserLogo")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                b.Property<string>("OrganiserName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                b.Property<string>("RecordStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<long?>("UserId");

                b.HasKey("OrganiserId");

                b.HasIndex("UserId");

                b.ToTable("Organiser");
            });

            

        

            modelBuilder.Entity("Impulse.EF.Team", b =>
            {
                b.Property<long>("TeamId")
                    .ValueGeneratedOnAdd();

                b.Property<long?>("CompanyId");

                b.Property<string>("RecordStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<string>("TeamDescription")
                    .IsUnicode(false);

                b.Property<string>("TeamLogo")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                b.Property<string>("TeamName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                b.Property<long?>("UserId");

                b.HasKey("TeamId");

                b.HasIndex("UserId");

                b.ToTable("Team");
            });

            modelBuilder.Entity("Impulse.EF.User", b =>
            {
                b.Property<long>("UserId")
                    .ValueGeneratedOnAdd();

                b.Property<long?>("LookUpUserStatusId");

                b.Property<long>("LookUpUserTypeId");

                b.Property<string>("Password")
                    .IsUnicode(false);

                b.Property<string>("RecordStatus")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                b.Property<string>("UserName")
                    .IsUnicode(false);

                b.HasKey("UserId");

                b.HasIndex("LookUpUserStatusId");

                b.HasIndex("LookUpUserTypeId");

                b.ToTable("User");
            });

            modelBuilder.Entity("Impulse.EF.VendorInterest", b =>
            {
                b.Property<long>("VendorInterestId")
                    .ValueGeneratedOnAdd();

                b.Property<long?>("EventSubTypeId");

                b.Property<long?>("EventTypeId");

                b.Property<string>("Place");

                b.Property<long?>("PlaceId");

                b.Property<string>("RecordStatus");

                b.Property<long?>("UserId");

                b.HasKey("VendorInterestId");

                b.HasIndex("UserId");

                b.ToTable("VendorInterest");
            });

            modelBuilder.Entity("Impulse.EF.VendorInterestRate", b =>
            {
                b.Property<long>("VendorInterestRateId")
                    .ValueGeneratedOnAdd();

                b.Property<long?>("LookupRateTypeId");

                b.Property<string>("RecordStatus");

                b.Property<long>("VendorInterestId");

                b.Property<decimal>("rate");

                b.HasKey("VendorInterestRateId");

                b.ToTable("VendorInterestRate");
            });

            modelBuilder.Entity("Impulse.EF.Address", b =>
            {
                b.HasOne("Impulse.EF.LookUpAddressType", "LookUpAddressType")
                    .WithMany("Address")
                    .HasForeignKey("LookUpAddressTypeId")
                    .HasConstraintName("FK_Address_LookUpAddressType");

                b.HasOne("Impulse.EF.User", "User")
                    .WithMany("Address")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_Address_User");
            });

            modelBuilder.Entity("Impulse.EF.Company", b =>
            {
                b.HasOne("Impulse.EF.LookUpCompanyType", "LookUpCompanyType")
                    .WithMany("Company")
                    .HasForeignKey("LookUpCompanyTypeId")
                    .HasConstraintName("FK_Address_LookUpCompanyType");
            });

            modelBuilder.Entity("Impulse.EF.Contact", b =>
            {
                b.HasOne("Impulse.EF.LookUpContactType", "LookUpContactType")
                    .WithMany("Contact")
                    .HasForeignKey("LookUpContactTypeId")
                    .HasConstraintName("FK_Contact_LookUpContactType");

                b.HasOne("Impulse.EF.User", "User")
                    .WithMany("Contact")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_Contact_User");
            });

            modelBuilder.Entity("Impulse.EF.EventSubType", b =>
            {
                b.HasOne("Impulse.EF.EventType", "EventType")
                    .WithMany("EventSubTypes")
                    .HasForeignKey("EventTypeId");
            });

            modelBuilder.Entity("Impulse.EF.Organiser", b =>
            {
                b.HasOne("Impulse.EF.User", "user")
                    .WithMany("Organiser")
                    .HasForeignKey("UserId");
            });

            modelBuilder.Entity("Impulse.EF.Team", b =>
            {
                b.HasOne("Impulse.EF.User", "User")
                    .WithMany("Team")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_Team_User");
            });

            modelBuilder.Entity("Impulse.EF.User", b =>
            {
                b.HasOne("Impulse.EF.LookUpUserStatus", "LookUpUserStatus")
                    .WithMany("User")
                    .HasForeignKey("LookUpUserStatusId")
                    .HasConstraintName("FK_User_LookUpUserStatus");

                b.HasOne("Impulse.EF.LookUpUserType", "LookUpUserType")
                    .WithMany("User")
                    .HasForeignKey("LookUpUserTypeId")
                    .HasConstraintName("FK_User_LookUpUserType");
            });

            modelBuilder.Entity("Impulse.EF.VendorInterest", b =>
            {
                b.HasOne("Impulse.EF.User", "User")
                    .WithMany()
                    .HasForeignKey("UserId");
            });
        }
    }
}
