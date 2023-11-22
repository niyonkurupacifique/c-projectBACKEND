using System;
using System.Collections.Generic;

namespace c_.Models;

public partial class PaymentDetailsTable
{
    public int TransactionId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerEmail { get; set; }

    public string? CustomerPhoneNumber { get; set; }

    public string? CustomerNationalId { get; set; }

    public string? CustomerMartalStatus { get; set; }

    public string? SelectedCategoryType { get; set; }

    public int? NumberDirectParent { get; set; }

    public string? PrimiumFrequency { get; set; }

    public int? NumberofChildren { get; set; }

    public int? RiskPremium { get; set; }

    public int? AnnualRiskPremium { get; set; }

    public int? MonthlyMinSaving { get; set; }

    public int? AnnualMinSaving { get; set; }

    public int? RiskPremiumMonthlyMinSavings { get; set; }

    public int? AnnualRiskPremiumAnnualyMinSavings { get; set; }

    public DateTime? RecordedDate { get; set; }

    public string? PaymentMode { get; set; }

    public int? IdRecord { get; set; }
}
