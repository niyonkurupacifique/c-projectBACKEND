using System;
using System.Collections.Generic;

namespace c_.Models;

public partial class CustomerStatusTable
{
    public byte PensionCustomerStatus { get; set; }

    public string PensionCustomerStatusDescription { get; set; } = null!;
}
