using api_sale_planning.Contexts;
using api_sale_planning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace api_sale_planning
{
    public class Service
    {
        private ConnectDB _dbSCM = new ConnectDB("DBSCM");
        private readonly DBSCM _EF_SCM;
        private readonly DBHRM _EF_HRM;
        private readonly DBBCS _EF_BCS;
        private OraConnectDB _ALPHAPD = new OraConnectDB("ALPHAPD");
        private OraConnectDB _ALPHAPD1 = new OraConnectDB("ALPHA01");
        private OraConnectDB _ALPHAPD2 = new OraConnectDB("ALPHA02");

        public Service(DBSCM eF_SCM, DBHRM eF_HRM, DBBCS eF_BCS, OraConnectDB aLPHAPD, OraConnectDB aLPHAPD1, OraConnectDB aLPHAPD2)
        {
            _EF_SCM = eF_SCM;
            _EF_HRM = eF_HRM;
            _EF_BCS = eF_BCS;
            _ALPHAPD = aLPHAPD;
            _ALPHAPD1 = aLPHAPD1;
            _ALPHAPD2 = aLPHAPD2;
        }

        public Service()
        {
        }

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
        public string RemoveLineEndings(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();
            if (value.Contains("\r\n"))
            {
                Console.WriteLine("asd");
            }
            return value.Replace("\r\n", string.Empty)
                        .Replace("\n", string.Empty)
                        .Replace("\r", string.Empty)
                        .Replace(lineSeparator, string.Empty)
                        .Replace(paragraphSeparator, string.Empty).Replace(Environment.NewLine, "");
        }

        public List<GstSalMdl> GetListDiameter(OraConnectDB _ALPHAPD1)
        {
            List<GstSalMdl> rDiameter = new List<GstSalMdl>();
            OracleCommand strGstSalMdl = new OracleCommand();
            //strGstSalMdl.CommandText = @"SELECT G.AREA SKU, G.MODL_NM MODELNAME  FROM PLAN.GST_SALMDL G where lrev = '999'";
            strGstSalMdl.CommandText = @"SELECT G.AREA SKU, G.MODL_NM MODELNAME  FROM PLAN.GST_SALMDL G where  AREA IS NOT NULL ";
            DataTable dtGstSalMdl = _ALPHAPD1.Query(strGstSalMdl);
            foreach (DataRow drGstSalMdl in dtGstSalMdl.Rows)
            {
                GstSalMdl oGstSalMdl = new GstSalMdl();
                string modelName = drGstSalMdl["MODELNAME"].ToString();
                string sku = drGstSalMdl["SKU"].ToString();
                oGstSalMdl.modelName = modelName;
                oGstSalMdl.sku = sku;
                rDiameter.Add(oGstSalMdl);
            }
            return rDiameter;
        }

        internal string[] GetVersion(string yyyy)
        {
            string[] ver = new string[3];  //  1 = HAVE DATA, 2 = REV, 3 = LREV
            SqlCommand sqlCheckVersion = new SqlCommand();
            sqlCheckVersion.CommandText = @"SELECT TOP(1) REV,LREV FROM [dbSCM].[dbo].[AL_SaleForecaseMonth] WHERE ym LIKE '" + yyyy + "%'   order by CAST(rev as int) desc , CAST(lrev as int) desc";
            DataTable dtCheckVersion = _dbSCM.Query(sqlCheckVersion);
            if (dtCheckVersion.Rows.Count > 0)
            {
                ver[0] = "1";
                ver[1] = dtCheckVersion.Rows[0]["REV"].ToString();
                ver[2] = dtCheckVersion.Rows[0]["LREV"].ToString();
            }
            else
            {
                ver[0] = "0";
                ver[1] = "0";
                ver[2] = "0";
            }
            return ver;
        }

        internal List<AlSaleForecaseMonth> GetSaleForecase(string yyyy, string empcode, string rev = null, string lrev = null)
        {
            List<AlSaleForecaseMonth> rSaleForecase = new List<AlSaleForecaseMonth>();
            List<GstSalMdl> rDiameter = GetListDiameter(_ALPHAPD1);
            string test = "1Yasdasda";
            string cut = test.Substring(1, 1);
            //List<PnCompressor> rModel = contextDBSCM.PnCompressors.Where(x => x.Status == "ACTIVE").ToList();
            var rModel = _EF_SCM.AlSaleForecaseMonths.Where(x =>  x.ModelCode != "" && x.ModelCode != "-" && x.ModelCode != "#REF!" && x.ModelName != "" && x.Customer != "" && EF.Functions.IsNumeric(x.ModelCode) && x.ModelName.Substring(0, 2) != "3P" && x.ModelName.Substring(0, 2) != "4P").GroupBy(y => new
            {
                modelCode = y.ModelCode,
                modelName = y.ModelName,
                customer = y.Customer,
                pltype = y.Pltype
            }).Select(z => new
            {
                z.Key.modelCode,
                z.Key.modelName,
                z.Key.customer,
                z.Key.pltype
            }).ToList();
            foreach (var oModel in rModel)
            {
                GstSalMdl oDiameter = rDiameter.FirstOrDefault(x => x.modelName.Trim() == oModel.modelName);
                for (int i = 1; i <= 12; i++)
                {
                    string mmyyyy = $"{i.ToString("D2")}/{yyyy}";
                    AlSaleForecaseMonth mSale = new AlSaleForecaseMonth();
                    mSale.Ym = $"{yyyy}{i.ToString("D2")}";
                    mSale.Customer = oModel.customer;
                    mSale.ModelName = oModel.modelName;
                    mSale.ModelCode = oModel.modelCode;
                    mSale.Sebango = oModel.modelCode;
                    mSale.Pltype = oModel.pltype;
                    mSale.Diameter = oDiameter != null ? oDiameter.sku : "";
                    mSale.Rev = rev != null ? rev : "1";
                    mSale.Lrev = lrev != null ? lrev : "1";
                    mSale.CreateBy = empcode;
                    mSale.CreateDate = DateTime.Now;
                    mSale.UpdateDate = DateTime.Now;
                    rSaleForecase.Add(mSale);
                }
            }
            return rSaleForecase;
        }

    }
}
