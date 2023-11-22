using System;
using System.Collections.Generic;

namespace c_.Models;

public partial class RelationshipTypeTable
{
    public byte RelationshipType { get; set; }

    public string RelationshipTypeDescription { get; set; } = null!;
}
