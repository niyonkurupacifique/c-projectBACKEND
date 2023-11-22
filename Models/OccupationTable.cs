using System;
using System.Collections.Generic;

namespace c_.Models;

public partial class OccupationTable
{
    public short Occupation { get; set; }

    public string OccupationDescription { get; set; } = null!;
}
