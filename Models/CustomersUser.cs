using System;
using System.Collections.Generic;

namespace c_.Models;

public partial class CustomersUser
{
    public int IdRecord { get; set; }

    public string Names { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string NationalId { get; set; } = null!;

    public string? Telephone { get; set; }

    public bool? Active { get; set; }

    public DateTime RecordedDate { get; set; }

    public string? Otpcode { get; set; }
    public DateTime? OTPExpirationTimestamp { get; set; }
    public string? OtpVerification { get; set; }
}
