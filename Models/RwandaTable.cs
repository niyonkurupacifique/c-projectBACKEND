using System;
using System.Collections.Generic;

namespace c_.Models;

public partial class RwandaTable
{
    public string VillageList { get; set; } = null!;

    public string VilageListDescription { get; set; } = null!;

    public string CellList { get; set; } = null!;

    public string CellListDescription { get; set; } = null!;

    public string SectorList { get; set; } = null!;

    public string SectorListDescription { get; set; } = null!;

    public string? DistrictList { get; set; }

    public string? DistrictListDescription { get; set; }

    public string ProvinceList { get; set; } = null!;

    public string ProvinceListDescription { get; set; } = null!;
}
