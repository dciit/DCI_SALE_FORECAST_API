using System;
using System.Collections.Generic;

namespace api_sale_planning.Models
{
    public partial class EkbWipPartStock
    {
        public string Ym { get; set; } = null!;
        public string Wcno { get; set; } = null!;
        public string Partno { get; set; } = null!;
        public string Cm { get; set; } = null!;
        public string? PartDesc { get; set; }
        public decimal? Lbal { get; set; }
        public decimal? Recqty { get; set; }
        public decimal? Issqty { get; set; }
        public decimal? Bal { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? Ptype { get; set; }
    }
}
