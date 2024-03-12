using System;
using System.Collections.Generic;

namespace api_sale_planning.Models
{
    public partial class AlGsdActpln
    {
        public int? Wcno { get; set; }
        public string? Prdymd { get; set; }
        public string? Model { get; set; }
        public string? DataType { get; set; }
        public decimal? Qty { get; set; }
        public string? Userid { get; set; }
        public DateTime? Cdate { get; set; }
        public DateTime? Udate { get; set; }
        public DateTime? DataDate { get; set; }
    }
}
