namespace api_sale_planning.Models
{   


    public class MCoreInterface_Parent
    {
        public string model { get; set; }
        public string sumhold { get; set; }
        public string sumunhold { get; set; }
        public string stockhold { get; set; }

        public List<MCoreInterface_Child> Children { get; set; }
    }



    public class MCoreInterface_Child
    {
        public string type { get; set; }
        public string model { get; set; }
        public string hold { get; set; }
        public string hdate { get; set; }
        public string hqty { get; set; }
        public string remark1 { get; set; }
        public string unhold { get; set; }
        public string cdate { get; set; }
        public string unqty { get; set; }
        public string remark2 { get; set; }
        public string stockhold { get; set; }
        public string sumqtyh { get; set; }
        public string sumqtyc { get; set; }

        public MCoreInterface_Child()
        {
            this.type = "";
            this.model = "";
            this.hold = "";
            this.hdate = "";
            this.hqty = "";
            this.remark1 = "";

            this.unhold = "";
            this.cdate = "";
            this.unqty = "";
            this.remark2 = "";

            this.stockhold = "";
            this.sumqtyh = "";
            this.sumqtyc = "";
        }
    }
}
