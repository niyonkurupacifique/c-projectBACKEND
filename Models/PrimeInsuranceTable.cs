using System;
using System.Collections.Generic;

namespace c_.Models;

public partial class PrimeInsuranceTable
{
    public int IdRecord { get; set; }

    public string? CategoryId { get; set; }

    public string? CategoryType { get; set; }

    public int? PolicyholderSumInsured { get; set; }

    public int? SpouseSumInsured { get; set; }

    public int? KidsSumInsured { get; set; }

    public int? MonthlyPremium { get; set; }

    public int? AnnualyPremium { get; set; }

    public int? MonthlyMinSavings { get; set; }

    public int? AnnualyMinSavings { get; set; }

    public int? BaseKids { get; set; }

    public int? MonthlyAddPremium { get; set; }

    public int? AnnualyAddPremium { get; set; }

    public int? Status { get; set; }

    public DateTime? RecordDate { get; set; }

    public string? UserName { get; set; }

    public int? CommissionRate { get; set; }

    public int? ParentSumInsured { get; set; }

    public int? MonthlyAddPmParent { get; set; }

    public int? FuneralAmount { get; set; }

    public int? HospitalAmount { get; set; }

    public int? DriverEmergencyAmount { get; set; }
}
