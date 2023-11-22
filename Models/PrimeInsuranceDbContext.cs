using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace c_.Models;

public partial class PrimeInsuranceDbContext : DbContext
{
    public PrimeInsuranceDbContext()
    {
    }

    public PrimeInsuranceDbContext(DbContextOptions<PrimeInsuranceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CountriesTable> CountriesTables { get; set; }

    public virtual DbSet<CustomerInformationTable> CustomerInformationTables { get; set; }

    public virtual DbSet<CustomerStatusTable> CustomerStatusTables { get; set; }

    public virtual DbSet<CustomersUser> CustomersUsers { get; set; }

    public virtual DbSet<EconomicSsectorCodeTable> EconomicSsectorCodeTables { get; set; }

    public virtual DbSet<EducatiionTable> EducatiionTables { get; set; }

    public virtual DbSet<IncomeRangeTable> IncomeRangeTables { get; set; }

    public virtual DbSet<NaicsCodeTable> NaicsCodeTables { get; set; }

    public virtual DbSet<OccupationTable> OccupationTables { get; set; }

    public virtual DbSet<PaymentDetailsTable> PaymentDetailsTables { get; set; }

    public virtual DbSet<PrimeInsuranceTable> PrimeInsuranceTables { get; set; }

    public virtual DbSet<RelationshipTypeTable> RelationshipTypeTables { get; set; }

    public virtual DbSet<RwandaTable> RwandaTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Prime_insurance_DB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CountriesTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CountriesTable");

            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.CountryDescription)
                .HasMaxLength(50)
                .HasColumnName("Country_Description");
        });

        modelBuilder.Entity<CustomerInformationTable>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.ToTable("customerInformationTable");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("Customer_ID");
            entity.Property(e => e.CommAddress1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Comm_Address_1");
            entity.Property(e => e.CommAddress2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Comm_Address_2");
            entity.Property(e => e.CommCountry)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Comm_Country");
            entity.Property(e => e.CommResidenceType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Comm_Residence_Type");
            entity.Property(e => e.CommVillage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Comm_Village");
            entity.Property(e => e.CustomerAcronym)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Customer_Acronym");
            entity.Property(e => e.CustomerGender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Customer_Gender");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("Customer_Name");
            entity.Property(e => e.CustomerOpenDate)
                .HasColumnType("date")
                .HasColumnName("Customer_Open_Date");
            entity.Property(e => e.CustomerStatus).HasColumnName("Customer_Status");
            entity.Property(e => e.CustomerTin)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Customer_TIN");
            entity.Property(e => e.DateLastModified)
                .HasColumnType("date")
                .HasColumnName("Date_Last_Modified");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_of_Birth");
            entity.Property(e => e.EconomicSubSectorCodeIsic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Economic_Sub_Sector_Code_ISIC");
            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Email_ID");
            entity.Property(e => e.EmpAddress1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Emp_Address_1");
            entity.Property(e => e.EmpAddress2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Emp_Address_2");
            entity.Property(e => e.EmpCountry)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Emp_Country");
            entity.Property(e => e.EmpVillage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Emp_Village");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_ID");
            entity.Property(e => e.EmployerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employer_Name");
            entity.Property(e => e.FaxNumber1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Fax_Number_1");
            entity.Property(e => e.FaxNumber2)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Fax_Number_2");
            entity.Property(e => e.Forename1)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("Forename_1");
            entity.Property(e => e.Forename2)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("Forename_2");
            entity.Property(e => e.HealthInsuranceNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Health_Insurance_Number");
            entity.Property(e => e.HomeTelephone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Home_Telephone");
            entity.Property(e => e.IncomeFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Income_Frequency");
            entity.Property(e => e.LegalStatus).HasColumnName("Legal_status");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.NaicsCode).HasColumnName("NAICS_Code");
            entity.Property(e => e.NationalIdNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("National_ID_Number");
            entity.Property(e => e.NationalIdType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("National_ID_Type");
            entity.Property(e => e.Nationality)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NextOfKinEmailId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("Next_of_kin_Email_ID");
            entity.Property(e => e.NextOfKinIdNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Next_of_kin_ID_Number");
            entity.Property(e => e.NextOfKinIdType).HasColumnName("Next_of_kin_ID_Type");
            entity.Property(e => e.NextOfKinName)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("Next_of_kin_Name");
            entity.Property(e => e.NextOfKinTelephone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Next_of_kin_Telephone");
            entity.Property(e => e.NumberOfDependants).HasColumnName("Number_Of_Dependants");
            entity.Property(e => e.PermAddress1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Perm_Address_1");
            entity.Property(e => e.PermAddress2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Perm_Address_2");
            entity.Property(e => e.PermCountry)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Perm_Country");
            entity.Property(e => e.PermVillage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Perm_Village");
            entity.Property(e => e.PlaceOfBirth)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("Place_of_Birth");
            entity.Property(e => e.RelatedParty)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Related_Party");
            entity.Property(e => e.RelatedPartyName)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("Related_Party_Name");
            entity.Property(e => e.RelationshipType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Relationship_Type");
            entity.Property(e => e.Residence)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Salutation)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SocialEconomicClass).HasColumnName("Social_Economic_Class");
            entity.Property(e => e.SpouseName)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("Spouse_Name");
            entity.Property(e => e.SsnNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SSN_Number");
            entity.Property(e => e.Surname)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.VisionOuc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Vision_OUC");
            entity.Property(e => e.VisionSbu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Vision_SBU");
            entity.Property(e => e.WorkTelephone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Work_Telephone");
        });

        modelBuilder.Entity<CustomerStatusTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("customerStatusTable");

            entity.Property(e => e.PensionCustomerStatus).HasColumnName("Pension_Customer_Status");
            entity.Property(e => e.PensionCustomerStatusDescription)
                .HasMaxLength(50)
                .HasColumnName("Pension_Customer_Status_Description");
        });

        modelBuilder.Entity<CustomersUser>(entity =>
        {
            entity.HasKey(e => e.IdRecord).HasName("PK__Customer__356CCF9A3921C70D");

            entity.ToTable("Customers_User");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Names)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NationalId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NationalID");
            entity.Property(e => e.Otpcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("OTPCode");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RecordedDate).HasColumnType("datetime");
            entity.Property(e => e.Telephone)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EconomicSsectorCodeTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("economicSsectorCodeTable");

            entity.Property(e => e.EconomicSubSectorCodeIsic).HasColumnName("ECONOMIC_SUB_SECTOR_CODE_ISIC");
            entity.Property(e => e.EconomicSubSectorCodeIsicDesc)
                .HasMaxLength(150)
                .HasColumnName("ECONOMIC_SUB_SECTOR_CODE_ISIC_DESC");
        });

        modelBuilder.Entity<EducatiionTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("educatiionTable");

            entity.Property(e => e.BenefitYears).HasColumnName("Benefit_years");
            entity.Property(e => e.ContributionYears).HasColumnName("Contribution_years");
            entity.Property(e => e.PremiumFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Premium_frequency");
            entity.Property(e => e.RatePerMille).HasColumnName("rate_per_mille");
        });

        modelBuilder.Entity<IncomeRangeTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("incomeRangeTable");

            entity.Property(e => e.MonthlySalaryInFrw)
                .HasMaxLength(50)
                .HasColumnName("Monthly_salary_in_Frw");
        });

        modelBuilder.Entity<NaicsCodeTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("NaicsCodeTable");

            entity.Property(e => e.NaicsCodes).HasColumnName("NAICS_Codes");
            entity.Property(e => e.NaicsDescription)
                .HasMaxLength(100)
                .HasColumnName("NAICS_Description");
        });

        modelBuilder.Entity<OccupationTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OccupationTable");

            entity.Property(e => e.OccupationDescription)
                .HasMaxLength(100)
                .HasColumnName("Occupation_Description");
        });

        modelBuilder.Entity<PaymentDetailsTable>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__paymentD__9B57CF727E423AAE");

            entity.ToTable("paymentDetailsTable");

            entity.Property(e => e.TransactionId).HasColumnName("transactionId");
            entity.Property(e => e.AnnualMinSaving).HasColumnName("annualMinSaving");
            entity.Property(e => e.AnnualRiskPremium).HasColumnName("annualRiskPremium");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customerEmail");
            entity.Property(e => e.CustomerMartalStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("customerMartalStatus");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customerName");
            entity.Property(e => e.CustomerNationalId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("customerNationalId");
            entity.Property(e => e.CustomerPhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("customerPhoneNumber");
            entity.Property(e => e.MonthlyMinSaving).HasColumnName("monthlyMinSaving");
            entity.Property(e => e.NumberDirectParent).HasColumnName("numberDirectParent");
            entity.Property(e => e.NumberofChildren).HasColumnName("numberofChildren");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("paymentMode");
            entity.Property(e => e.PrimiumFrequency)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primiumFrequency");
            entity.Property(e => e.RecordedDate)
                .HasColumnType("date")
                .HasColumnName("recordedDate");
            entity.Property(e => e.RiskPremium).HasColumnName("riskPremium");
            entity.Property(e => e.RiskPremiumMonthlyMinSavings).HasColumnName("riskPremiumMonthlyMinSavings");
            entity.Property(e => e.SelectedCategoryType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("selectedCategoryType");
        });

        modelBuilder.Entity<PrimeInsuranceTable>(entity =>
        {
            entity.HasKey(e => e.IdRecord).HasName("PK__primeIns__39D9712DEDEF0F70");

            entity.ToTable("primeInsuranceTable");

            entity.Property(e => e.IdRecord).HasColumnName("Id_Record");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RecordDate).HasColumnType("date");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RelationshipTypeTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("relationshipTypeTable");

            entity.Property(e => e.RelationshipType).HasColumnName("Relationship_Type");
            entity.Property(e => e.RelationshipTypeDescription)
                .HasMaxLength(50)
                .HasColumnName("Relationship_Type_Description");
        });

        modelBuilder.Entity<RwandaTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RwandaTable");

            entity.Property(e => e.CellList)
                .HasMaxLength(50)
                .HasColumnName("Cell_List");
            entity.Property(e => e.CellListDescription)
                .HasMaxLength(50)
                .HasColumnName("Cell_List_Description");
            entity.Property(e => e.DistrictList)
                .HasMaxLength(50)
                .HasColumnName("District_List");
            entity.Property(e => e.DistrictListDescription)
                .HasMaxLength(50)
                .HasColumnName("District_List_Description");
            entity.Property(e => e.ProvinceList)
                .HasMaxLength(50)
                .HasColumnName("Province_List");
            entity.Property(e => e.ProvinceListDescription)
                .HasMaxLength(50)
                .HasColumnName("Province_List_Description");
            entity.Property(e => e.SectorList)
                .HasMaxLength(50)
                .HasColumnName("Sector_List");
            entity.Property(e => e.SectorListDescription)
                .HasMaxLength(50)
                .HasColumnName("Sector_List_Description");
            entity.Property(e => e.VilageListDescription)
                .HasMaxLength(50)
                .HasColumnName("Vilage_List_Description");
            entity.Property(e => e.VillageList)
                .HasMaxLength(50)
                .HasColumnName("Village_List");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
