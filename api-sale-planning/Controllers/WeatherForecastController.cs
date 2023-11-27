using api_sale_planning.Contexts;
using api_sale_planning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Data;

namespace api_sale_planning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly DBSCM _contextDBSCM;
        private ConnectDB _dbSCM = new ConnectDB("DBSCM");
        public WeatherForecastController(DBSCM contextDBSCM)
        {
            _contextDBSCM = contextDBSCM;
        }

        [HttpGet]
        [Route("/saleforecase/{ym}")]
        public IActionResult GetSaleForeCase(int ym)
        {
            var content = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym.ToString() && x.Lrev == "999").OrderBy(x => x.Id).ToList();
            return Ok(content);
        }


        [HttpPost]
        [Route("/saleforecast/getdistribution")]
        public IActionResult getDistribution([FromBody] MParam param)
        {
            return Ok(param);
        }

        [HttpPost]
        [Route("/saleforecast/update")]
        public IActionResult updateSaleForecast([FromBody] MSaveSaleForecase param)
        {
            string Ym = param.Ym;
            List<AlSaleForecaseMonth> data = param.data;
            List<AlSaleForecaseMonth> dataPrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == Ym).ToList();
            if (dataPrev.Count > 0)
            {
                foreach (AlSaleForecaseMonth itemPrev in dataPrev)
                {

                }
            }
            else
            {
                int rev = 1;
                int lrev = 1;
                foreach (AlSaleForecaseMonth item in data)
                {
                    item.Ym = Ym;
                    item.Rev = rev.ToString();
                    item.Lrev = lrev.ToString();
                    _contextDBSCM.AlSaleForecaseMonths.Add(item);
                }
                int insert = _contextDBSCM.SaveChanges();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/saleforecast/getsaleofmonth")]
        public IActionResult getSaleOfMonth([FromBody] MParam param)
        {
            string year = param.year.ToString();
            string month = ((int)param.month).ToString("D2");
            List<AlSaleForecaseMonth> res = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == $"{year}{month}").OrderBy(x => x.Id).ToList();
            return Ok(res);
        }

        [HttpPost]
        [Route("/updaterow")]
        public IActionResult FN_UPDATE_ROW([FromBody] MParam param)
        {
            string year = param.year.ToString()!;
            string month = ((int)param.month).ToString("D2");
            string ym = $"{year}{month}";
            int rev = 1;
            int lrev = 1;
            AlSaleForecaseMonth listSale = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym).OrderBy(x => x.Rev).FirstOrDefault()!;
            if (listSale != null)
            {
                rev = int.Parse(listSale.Rev);
                lrev = int.Parse(listSale.Lrev);
            }
            foreach (AlSaleForecaseMonth itemForecast in param.listalforecast)
            {
                itemForecast.CreateBy = "41256";
                itemForecast.CreateDate = DateTime.Now;
                itemForecast.ModelName = itemForecast.ModelCode;
                itemForecast.Rev = rev.ToString();
                itemForecast.Lrev = lrev.ToString();
                //var checkExist = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Rev == itemForecast.Rev && x.Lrev == itemForecast.Lrev && x.ModelCode == itemForecast.ModelCode && x.Customer == itemForecast.Customer && x.Sebango == itemForecast.Sebango && x.Pltype == itemForecast.Pltype).FirstOrDefault();
                //if (checkExist == null)
                //{
                    _contextDBSCM.AlSaleForecaseMonths.Add(itemForecast);
                //}
            }
            int insert = _contextDBSCM.SaveChanges();
            //sql.CommandText = $@"SELECT  * FROM [dbSCM].[dbo].[AL_SaleForecaseMonth] WHERE ID = {param.id}";
            //DataTable dt = _dbSCM.Query(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    SqlCommand sqlUpdate = new SqlCommand();
            //    sqlUpdate.CommandText = $@"UPDATE [dbSCM].[dbo].[AL_SaleForecaseMonth] SET {param.column} = '{param.val}' WHERE ID = {param.id}";
            //    int update = _dbSCM.ExecuteNonQuery(sqlUpdate);
            //    return Ok(new
            //    {
            //        status = update
            //    });
            //}
            //else
            //{
            //    int newId = 0;
            //    AlSaleForecaseMonth itemAdd = param.alforecast;
            //    _contextDBSCM.AlSaleForecaseMonths.Add(itemAdd);
            //    int insert = _contextDBSCM.SaveChanges();
            //    if (insert > 0)
            //    {
            //        newId = itemAdd.Id;
            //    }
            //    return Ok(new { status = insert, id = newId });
            //}
            return Ok(new
            {
                status = insert
            });
        }

        [HttpPost]
        [Route("/saleforecase/save")]
        public IActionResult SaveSaleForeCase([FromBody] MSaveSaleForecase param)
        {

            var context_prev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == param.Ym.ToString() && x.Lrev == param.Ym).ToList();

            //int rev = 0;
            //var exist = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == param.Ym.ToString() && x.Lrev == "999").OrderBy(x => x.Id).ToList();
            //if (exist.Count > 0)
            //{
            //    rev = int.Parse(exist.FirstOrDefault().Rev);
            //    foreach (var item in exist)
            //    {
            //        item.Lrev = rev.ToString();
            //        _contextDBSCM.AlSaleForecaseMonths.Update(item);
            //    }
            //    _contextDBSCM.SaveChanges();
            //}
            //foreach (AlSaleForecaseMonth row in param.data)
            //{
            //    AlSaleForecaseMonth itemAdd = new AlSaleForecaseMonth();
            //    itemAdd.CreateBy = row.CreateBy;
            //    itemAdd.Customer = row.Customer;
            //    itemAdd.Ym = row.Ym;
            //    itemAdd.ModelCode = row.ModelCode;
            //    itemAdd.ModelName = row.ModelName;
            //    itemAdd.Sebango = row.Sebango;
            //    itemAdd.Pltype = row.Pltype;
            //    itemAdd.Customer = row.Customer;
            //    itemAdd.CreateDate = DateTime.Now;
            //    itemAdd.D01 = row.D01;
            //    itemAdd.D02 = row.D02;
            //    itemAdd.D03 = row.D03;
            //    itemAdd.D04 = row.D04;
            //    itemAdd.D05 = row.D05;
            //    itemAdd.D06 = row.D06;
            //    itemAdd.D07 = row.D07;
            //    itemAdd.D08 = row.D08;
            //    itemAdd.D09 = row.D09;
            //    itemAdd.D10 = row.D10;
            //    itemAdd.D11 = row.D11;
            //    itemAdd.D12 = row.D12;
            //    itemAdd.D13 = row.D13;
            //    itemAdd.D14 = row.D14;
            //    itemAdd.D15 = row.D15;
            //    itemAdd.D16 = row.D16;
            //    itemAdd.D17 = row.D17;
            //    itemAdd.D18 = row.D18;
            //    itemAdd.D19 = row.D19;
            //    itemAdd.D20 = row.D20;
            //    itemAdd.D21 = row.D21;
            //    itemAdd.D22 = row.D22;
            //    itemAdd.D23 = row.D23;
            //    itemAdd.D24 = row.D24;
            //    itemAdd.D25 = row.D25;
            //    itemAdd.D26 = row.D26;
            //    itemAdd.D27 = row.D27;
            //    itemAdd.D28 = row.D28;
            //    itemAdd.D29 = row.D29;
            //    itemAdd.D30 = row.D30;
            //    itemAdd.D31 = row.D31;
            //    itemAdd.Rev = (rev + 1).ToString();
            //    itemAdd.Lrev = "999";
            //    _contextDBSCM.AlSaleForecaseMonths.Add(itemAdd);
            //}
            //int insert = _contextDBSCM.SaveChanges();
            int insert = 0;
            return Ok(new
            {
                status = insert
            });
        }

        [HttpGet]
        [Route("/saleforecase/model")]
        public IActionResult GetModel()
        {
            var content = _contextDBSCM.PnCompressors.ToList();
            return Ok(content);
        }

        [HttpGet]
        [Route("/saleforecase/customer")]
        public IActionResult GetCustomer()
        {
            var content = _contextDBSCM.AlCustomers.ToList();
            return Ok(content);
        }

        [HttpGet]
        [Route("/saleforecase/pltype")]
        public IActionResult GetPltype()
        {
            var content = _contextDBSCM.AlPalletTypeMappings.ToList();
            return Ok(content);
        }

        [HttpPost]
        [Route("/saleforecase/deleteAll")]
        public IActionResult DeleteAll([FromBody] MDeleteSaleForecast param)
        {
            var content = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == param.ym).ToList();
            if (content != null)
            {
                _contextDBSCM.AlSaleForecaseMonths.RemoveRange(content);
                int del = _contextDBSCM.SaveChanges();
                return Ok(new
                {
                    status = del
                });
            }
            else
            {
                return Ok(new
                {
                    status = 0,
                    msg = "ไม่พบข้อมูล"
                });
            }
        }
    }
}