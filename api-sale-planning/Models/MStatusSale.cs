namespace api_sale_planning.Models
{
    public class MStatusSale
    {
        public string? year { get; set; }
        public string? month { get; set; }  
        public bool? isDistribution { get; set; }   
        public int? rev {  get; set; }
        public DateTime? dt {  get; set; }
    }
}
