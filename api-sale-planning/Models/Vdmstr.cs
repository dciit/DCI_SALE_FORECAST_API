using System;
using System.Collections.Generic;

namespace api_sale_planning.Models
{
    public partial class Vdmstr
    {
        public string VenderCode { get; set; } = null!;
        public string VenderName { get; set; } = null!;
        public string VenderShortName { get; set; } = null!;
        public string? VenderAddress { get; set; }
        public string? VenderNation { get; set; }
        public string? VenderTel { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime? CreateDt { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime? UpdateDt { get; set; }
        public string? VenderActive { get; set; }
    }
}
