using System;
using System.Collections.Generic;

namespace c_.Models;

public partial class EducatiionTable
{
    public int Age { get; set; }

    public string PremiumFrequency { get; set; } = null!;

    public int BenefitYears { get; set; }

    public int ContributionYears { get; set; }

    public double RatePerMille { get; set; }
}
