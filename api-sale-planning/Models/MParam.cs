namespace api_sale_planning.Models
{
    public class MParam
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public string? ym { get; set; }
        public string? column { get; set; }
        public int? val { get; set; }
        public int? id { get; set; }
        public AlSaleForecaseMonth? alforecast { get; set; }
        public List<AlSaleForecaseMonth>? listalforecast { get; set; }
        public string? empcode { get; set; }
        public List<List<MExcel>>? data { get; set; }
        public List<MFilter>? filterCustomer {  get; set; }
        public List<MFilter>? filterSBU { get; set; }
    }

    public class MFilter
    {
        public string? value { get; set; }
        public string? label {  get; set; }
    }
    public class MExcel
    {
        public string? value { get; set; }
    }
}
