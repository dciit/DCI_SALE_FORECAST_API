using api_sale_planning.Contexts;
using api_sale_planning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_sale_planning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly DBSCM _contextDBSCM;

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
        [Route("/saleforecase/save")]
        public IActionResult SaveSaleForeCase([FromBody] MSaveSaleForecase param)
        {
            int rev = 0;
            var exist = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == param.Ym.ToString() && x.Lrev == "999").OrderBy(x => x.Id).ToList();
            if (exist.Count > 0)
            {
                rev = int.Parse(exist.FirstOrDefault().Rev);
                foreach (var item in exist)
                {
                    item.Lrev = rev.ToString();
                    _contextDBSCM.AlSaleForecaseMonths.Update(item);
                }
                _contextDBSCM.SaveChanges();
            }
            foreach (AlSaleForecaseMonth row in param.data)
            {
                AlSaleForecaseMonth itemAdd = new AlSaleForecaseMonth();
                itemAdd.CreateBy = row.CreateBy;
                itemAdd.Customer = row.Customer;
                itemAdd.Ym = row.Ym;
                itemAdd.ModelCode = row.ModelCode;
                itemAdd.ModelName = row.ModelName;
                itemAdd.Sebango = row.Sebango;
                itemAdd.Pltype = row.Pltype;
                itemAdd.Customer = row.Customer;
                itemAdd.CreateDate = DateTime.Now;
                itemAdd.D01 = row.D01;
                itemAdd.D02 = row.D02;
                itemAdd.D03 = row.D03;
                itemAdd.D04 = row.D04;
                itemAdd.D05 = row.D05;
                itemAdd.D06 = row.D06;
                itemAdd.D07 = row.D07;
                itemAdd.D08 = row.D08;
                itemAdd.D09 = row.D09;
                itemAdd.D10 = row.D10;
                itemAdd.D11 = row.D11;
                itemAdd.D12 = row.D12;
                itemAdd.D13 = row.D13;
                itemAdd.D14 = row.D14;
                itemAdd.D15 = row.D15;
                itemAdd.D16 = row.D16;
                itemAdd.D17 = row.D17;
                itemAdd.D18 = row.D18;
                itemAdd.D19 = row.D19;
                itemAdd.D20 = row.D20;
                itemAdd.D21 = row.D21;
                itemAdd.D22 = row.D22;
                itemAdd.D23 = row.D23;
                itemAdd.D24 = row.D24;
                itemAdd.D25 = row.D25;
                itemAdd.D26 = row.D26;
                itemAdd.D27 = row.D27;
                itemAdd.D28 = row.D28;
                itemAdd.D29 = row.D29;
                itemAdd.D30 = row.D30;
                itemAdd.D31 = row.D31;
                itemAdd.Rev = (rev + 1).ToString();
                itemAdd.Lrev = "999";
                _contextDBSCM.AlSaleForecaseMonths.Add(itemAdd);
            }
            int insert = _contextDBSCM.SaveChanges();
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