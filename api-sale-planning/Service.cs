using api_sale_planning.Models;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace api_sale_planning
{
    public class Service
    {
        internal int? setNumSale(int? valSale)
        {
            if (valSale != null)
            {
                return valSale;
            }
            else
            {
                return 0;
            }
        }
    }
}
