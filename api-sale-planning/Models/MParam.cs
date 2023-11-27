namespace api_sale_planning.Models
{
    public class MParam
    {
        public int? year {  get; set; }  
        public int? month { get; set; }

        public string? column { get; set; } 
        public int? val { get; set; }   

        public int? id {  get; set; }   

        public AlSaleForecaseMonth? alforecast { get; set; }
        public List<AlSaleForecaseMonth>? listalforecast { get; set; }   
        public string? empcode {  get; set; }
    }
}
