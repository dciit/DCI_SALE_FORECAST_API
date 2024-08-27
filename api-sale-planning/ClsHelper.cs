
namespace API_DCI_DIAGRAM_SVG.Contexts
{
    public class ClsHelper
    {

        public ClsHelper()
        {
            
        }

        public string generateNbr()
        {
            DateTime dtNow = DateTime.Now;
            Random rand = new Random(1000000);
            int nxt = rand.Next(1,999999);

            return $"{dtNow.ToString("yyyyMMddHHmmssffff")}{nxt.ToString("000000")}";
        }

        public decimal ConvStr2Dec(string val) {
            try
            {
                return Convert.ToDecimal(val);
            }
            catch {
                return 0;
            }
        }

        public decimal ConvInt2Dec(int? val)
        {
            try
            {
                return Convert.ToDecimal(val);
            }
            catch
            {
                return 0;
            }
        }
        public double ConvIntToDB(int val)
        {
            try
            {
                return Convert.ToDouble(val);
            }
            catch
            {
                return 0;
            }
        }
        public int ConvStr2Int(string val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return 0;
            }
        }
        public int ConvDec2Int(decimal val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return 0;
            }
        }
        public int ConvUnDec2Int(decimal? val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return 0;
            }
        }

        public DateTime ConvStrToDate(string ymd)
        {
            try
            {
                return new DateTime(Convert.ToInt16(ymd.Substring(0,4)), Convert.ToInt16(ymd.Substring(4,2)), Convert.ToInt16(ymd.Substring(6,2)));
            }
            catch
            {
                return new DateTime(1900,1,1);
            }
        }

    }
}
