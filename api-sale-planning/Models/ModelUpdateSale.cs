namespace api_sale_planning.Models
{
    public class ModelUpdateSale
    {
        public string year { get; set; }
        public string empcode {  get; set; }
        public List<MDataSaleForecase> sales {  get; set; } = new List<MDataSaleForecase>();    
    }
}
