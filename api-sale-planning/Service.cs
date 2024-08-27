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

        internal void GetMasterSale()
        {

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
            int numMonth = 1;
            List<DictMstr> ListModelOfCustomer = _EF_SCM.DictMstrs.Where(x => x.DictSystem == "SALEFC" && x.DictType == "CUST_PL" && x.DictStatus == "ACTIVE").ToList();
            List<WMS_MDW27_MODEL_MASTER> rMDW27 = new List<WMS_MDW27_MODEL_MASTER>();
            SqlCommand sqlGetMDW27ModelMaster = new SqlCommand();
            sqlGetMDW27ModelMaster.CommandText = @" select mstr.code as CUSTOMER,mdw27.MODEL,mdw27.SEBANGO AS SEBANGO,mdw27.PLTYPE,mdw27.DIAMETER from [dbSCM].[dbo].[DictMstr] mstr 
  left join [dbSCM].[dbo].[WMS_MDW27_MODEL_MASTER] mdw27 on mstr.REF_CODE = mdw27.MODEL  
  where  mdw27.ACTIVE = 'ACTIVE' AND DICT_SYSTEM = 'SALEFC' AND DICT_TYPE = 'CUST_MODEL'
  group by mstr.code,mdw27.MODEL,mdw27.SEBANGO,mdw27.PLTYPE,mdw27.DIAMETER
  order by mstr.code asc,mdw27.MODEL";
            DataTable dt = _dbSCM.Query(sqlGetMDW27ModelMaster);
            foreach (DataRow dr in dt.Rows)
            {
                string model = dr["MODEL"].ToString();
                string pltype = dr["PLTYPE"].ToString();
                if (dr["CUSTOMER"].ToString() == "DAP")
                {
                    List<string> ModelOfCustomer = ListModelOfCustomer.Where(x => x.Code == "DAP" && x.RefCode == model).Select(x=>x.Ref1).ToList();
                    if (ModelOfCustomer.Count > 0 && ModelOfCustomer.Contains(pltype) == true)
                    {
                        WMS_MDW27_MODEL_MASTER oMdw27 = new WMS_MDW27_MODEL_MASTER();
                        oMdw27.customer = dr["CUSTOMER"].ToString();
                        oMdw27.model = model;
                        oMdw27.sebango = dr["SEBANGO"].ToString();
                        oMdw27.pltype = pltype;
                        oMdw27.diameter = dr["DIAMETER"].ToString();
                        rMDW27.Add(oMdw27);
                    }
                }
                else
                {
                    WMS_MDW27_MODEL_MASTER oMdw27 = new WMS_MDW27_MODEL_MASTER();
                    oMdw27.customer = dr["CUSTOMER"].ToString();
                    oMdw27.model = dr["MODEL"].ToString();
                    oMdw27.sebango = dr["SEBANGO"].ToString();
                    oMdw27.pltype = pltype;
                    oMdw27.diameter = dr["DIAMETER"].ToString();
                    rMDW27.Add(oMdw27);
                }
            }
            while (numMonth <= 12)
            {
                foreach (WMS_MDW27_MODEL_MASTER oMdw27 in rMDW27)
                {
                    GstSalMdl oDiameter = rDiameter.FirstOrDefault(x => x.modelName.Trim() == oMdw27.model);
                    string mmyyyy = $"{numMonth.ToString("D2")}/{yyyy}";
                    AlSaleForecaseMonth mSale = new AlSaleForecaseMonth();
                    mSale.Ym = $"{yyyy}{numMonth.ToString("D2")}";
                    mSale.Customer = oMdw27.customer;
                    mSale.ModelName = oMdw27.model;
                    mSale.ModelCode = oMdw27.sebango;
                    mSale.Sebango = oMdw27.sebango;
                    mSale.Pltype = oMdw27.pltype;
                    mSale.Diameter = oDiameter != null ? oDiameter.sku : "";
                    mSale.Rev = rev != null ? rev : "1";
                    mSale.Lrev = lrev != null ? lrev : "1";
                    mSale.CreateBy = empcode;
                    mSale.CreateDate = DateTime.Now;
                    mSale.UpdateDate = DateTime.Now;
                    rSaleForecase.Add(mSale);
                }
                numMonth++;
            }
            return rSaleForecase;
        }

    }
}
