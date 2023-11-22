using System;
using System.Collections.Generic;

namespace c_.Models;

public partial class CustomerInformationTable
{
    public string CustomerId { get; set; } = null!;

    public string Salutation { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Forename1 { get; set; } = null!;

    public string? Forename2 { get; set; }

    public string CustomerAcronym { get; set; } = null!;

    public string VisionOuc { get; set; } = null!;

    public string VisionSbu { get; set; } = null!;

    public DateTime CustomerOpenDate { get; set; }

    public string CustomerGender { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string PlaceOfBirth { get; set; } = null!;

    public string MaritalStatus { get; set; } = null!;

    public string? SpouseName { get; set; }

    public int? SocialEconomicClass { get; set; }

    public string? NextOfKinName { get; set; }

    public int? NextOfKinIdType { get; set; }

    public string? NextOfKinIdNumber { get; set; }

    public string? NextOfKinTelephone { get; set; }

    public string? NextOfKinEmailId { get; set; }

    public int NumberOfDependants { get; set; }

    public string Nationality { get; set; } = null!;

    public string Residence { get; set; } = null!;

    public string CommAddress1 { get; set; } = null!;

    public string? CommAddress2 { get; set; }

    public string? CommVillage { get; set; }

    public string CommCountry { get; set; } = null!;

    public string CommResidenceType { get; set; } = null!;

    public string PermAddress1 { get; set; } = null!;

    public string? PermAddress2 { get; set; }

    public string? PermVillage { get; set; }

    public string PermCountry { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string WorkTelephone { get; set; } = null!;

    public string HomeTelephone { get; set; } = null!;

    public string? FaxNumber1 { get; set; }

    public string? FaxNumber2 { get; set; }

    public int Education { get; set; }

    public string? CustomerTin { get; set; }

    public int NaicsCode { get; set; }

    public string EconomicSubSectorCodeIsic { get; set; } = null!;

    public string RelatedParty { get; set; } = null!;

    public string RelationshipType { get; set; } = null!;

    public string? RelatedPartyName { get; set; }

    public string? SsnNumber { get; set; }

    public string? NationalIdType { get; set; }

    public string? NationalIdNumber { get; set; }

    public string HealthInsuranceNumber { get; set; } = null!;

    public int? Occupation { get; set; }

    public string? EmployerName { get; set; }

    public string? EmployeeId { get; set; }

    public string? EmpAddress1 { get; set; }

    public string? EmpAddress2 { get; set; }

    public string? EmpVillage { get; set; }

    public string? EmpCountry { get; set; }

    public int? Income { get; set; }

    public string? IncomeFrequency { get; set; }

    public int? LegalStatus { get; set; }

    public int CustomerStatus { get; set; }

    public DateTime DateLastModified { get; set; }
}
