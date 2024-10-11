using API_DCI_DIAGRAM_SVG.Contexts;
using api_sale_planning.Contexts;
using api_sale_planning.Models;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace api_sale_planning.Controllers
{
    /* A003 (20240604) : เปลี่ยน master saleforecase จากนำข้อมูลเดิมให้ไปใช้ DBSCM => WMS_MDW27_MODEL_MASTER*/
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public ClsHelper oHelp = new ClsHelper();
        private ConnectDB _dbSCM = new ConnectDB("DBSCM");
        private readonly DBSCM _contextDBSCM;
        private readonly DBHRM _contextDBHRM;
        private readonly DBBCS _contextDBBCS;
        private OraConnectDB _ALPHAPD = new OraConnectDB("ALPHAPD");
        private OraConnectDB _ALPHAPD1 = new OraConnectDB("ALPHA01");
        private OraConnectDB _ALPHAPD2 = new OraConnectDB("ALPHA02");
        private Service service = new Service();


        public WeatherForecastController(DBSCM contextDBSCM, DBHRM contextDBHRM, DBBCS contextDBBCS)
        {
            _contextDBSCM = contextDBSCM;
            _contextDBHRM = contextDBHRM;
            _contextDBBCS = contextDBBCS;
        }

        [HttpGet]
        [Route("/saleforecase/{ym}")]
        public IActionResult GetSaleForeCase(int ym)
        {
            var content = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym.ToString() && x.Lrev == "999").OrderBy(x => x.Id).ToList();
            return Ok(content);
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
                if (itemForecast.Id != 0 && itemForecast.Id != null)
                {
                    var itemExist = _contextDBSCM.AlSaleForecaseMonths.FirstOrDefault(x => x.Id == itemForecast.Id);
                    if (itemExist != null)
                    {
                        itemExist.ModelCode = itemForecast.ModelCode;
                        itemExist.ModelName = itemForecast.ModelName;
                        itemExist.Pltype = itemForecast.Pltype;
                        itemExist.Customer = itemForecast.Customer;
                        itemExist.Sebango = itemForecast.Sebango;
                        itemExist.D01 = itemForecast.D01;
                        itemExist.D02 = itemForecast.D02;
                        itemExist.D03 = itemForecast.D03;
                        itemExist.D04 = itemForecast.D04;
                        itemExist.D05 = itemForecast.D05;
                        itemExist.D06 = itemForecast.D06;
                        itemExist.D07 = itemForecast.D07;
                        itemExist.D08 = itemForecast.D08;
                        itemExist.D09 = itemForecast.D09;
                        itemExist.D10 = itemForecast.D10;
                        itemExist.D11 = itemForecast.D11;
                        itemExist.D12 = itemForecast.D12;
                        itemExist.D13 = itemForecast.D13;
                        itemExist.D14 = itemForecast.D14;
                        itemExist.D15 = itemForecast.D15;
                        itemExist.D16 = itemForecast.D16;
                        itemExist.D17 = itemForecast.D17;
                        itemExist.D18 = itemForecast.D18;
                        itemExist.D19 = itemForecast.D19;
                        itemExist.D20 = itemForecast.D20;
                        itemExist.D21 = itemForecast.D21;
                        itemExist.D22 = itemForecast.D22;
                        itemExist.D23 = itemForecast.D23;
                        itemExist.D24 = itemForecast.D24;
                        itemExist.D25 = itemForecast.D25;
                        itemExist.D26 = itemForecast.D26;
                        itemExist.D27 = itemForecast.D27;
                        itemExist.D28 = itemForecast.D28;
                        itemExist.D29 = itemForecast.D29;
                        itemExist.D30 = itemForecast.D30;
                        itemExist.D31 = itemForecast.D31;
                        _contextDBSCM.AlSaleForecaseMonths.Update(itemExist);
                    }
                }
                else
                {
                    itemForecast.CreateBy = "41256";
                    itemForecast.CreateDate = DateTime.Now;
                    itemForecast.ModelName = itemForecast.ModelCode;
                    itemForecast.Rev = rev.ToString();
                    itemForecast.Lrev = lrev.ToString();
                    _contextDBSCM.AlSaleForecaseMonths.Add(itemForecast);
                }
            }
            int insert = _contextDBSCM.SaveChanges();
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
            int insert = 0;
            return Ok(new
            {
                status = insert
            });
        }

        [HttpGet]
        [Route("/sale/model")]
        public IActionResult GetModel()
        {
            var content = _contextDBSCM.PnCompressors.Where(x => x.ModelCode != "BMC" && x.ModelCode != "BMLROTOR" && x.ModelCode != "BMLSTATOR" && x.ModelCode != "PACK" && x.ModelCode != "SPECIAL" && x.ModelCode != "BMSROTOR" && x.ModelCode != "BMSSTATOR").ToList();
            //var DBBCS_RES_PART_LIST = _contextDBBCS.ResPartLists.Select(x => x.Model).Distinct().ToList();
            //var GroupModelCode = content.GroupBy(x => x.ModelCode);
            //var response = from x in DBBCS_RES_PART_LIST
            //               select new
            //               {
            //                   model = x,
            //                   ModelCode = content.FirstOrDefault(b => b.Model == x) != null ? content.FirstOrDefault(b => b.Model == x)!.ModelCode : ""
            //               };
            var response = content.Select(rows => new
            {
                ModelCode = rows.ModelCode,
                model = rows.Model
            }).ToList();
            return Ok(response);
        }

        [HttpGet]
        [Route("/sale/customer")]
        public IActionResult GetCustomer()
        {
            var content = _contextDBSCM.AlCustomers.OrderBy(x => x.CustomerCode).ToList();
            return Ok(content);
        }


        [HttpGet]
        [Route("/get/pltype")]
        public IActionResult GetPltype()
        {
            List<MFilter> res = new List<MFilter>();
            var content = _contextDBSCM.AlPalletTypeMappings.ToList();
            foreach (var item in content)
            {
                MFilter i = new MFilter();
                i.value = item.Pltype;
                i.label = item.Pltype;
                res.Add(i);
            }
            return Ok(res);
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

        [NonAction]
        public MGetRevAndLrev GetRevAndLrev(string ym = "")
        {
            MGetRevAndLrev response = new MGetRevAndLrev();
            var lastRev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym).OrderByDescending(x => x.Rev).FirstOrDefault();
            AlSaleForecaseMonth RevAndLrevCurrent = new AlSaleForecaseMonth();
            if (lastRev != null)
            {
                RevAndLrevCurrent = _contextDBSCM.AlSaleForecaseMonths.OrderByDescending(x => x.Lrev).FirstOrDefault(x => x.Ym == ym && x.Rev == lastRev.Rev);
                if (RevAndLrevCurrent != null)
                {
                    if (RevAndLrevCurrent.Lrev == "999")
                    {
                        response.lrev = 999;
                    }
                    else
                    {
                        response.lrev = int.Parse(RevAndLrevCurrent.Lrev);
                    }
                    response.rev = int.Parse(RevAndLrevCurrent.Rev);
                }
            }
            return response;
        }

        [HttpPost]
        [Route("/get/sale")]
        public IActionResult GetSale([FromBody] MParam param)
        {
            List<MFilter> filterCustomer = param.filterCustomer;
            List<MFilter> filterSBU = param.filterSBU;
            var models = _contextDBSCM.PnCompressors.Where(x => x.ModelCode != "BMC" && x.ModelCode != "SPECIAL" && x.ModelCode != "PACK" && x.ModelCode != "BMLROTOR" && x.ModelCode != "BMLSTATOR").ToList();
            var customers = _contextDBSCM.AlCustomers.ToList();
            List<AlSaleForecaseMonth> list = new List<AlSaleForecaseMonth>();
            string ym = param.ym!;
            if (ym != "" && ym != "null" && ym != null)
            {
                MGetRevAndLrev oGetRevAndLrev = GetRevAndLrev(ym);
                int RevCurrent = oGetRevAndLrev.rev;
                int LrevCurrent = oGetRevAndLrev.lrev;
                if (RevCurrent != 0 && LrevCurrent != 0)
                {
                    if (LrevCurrent != 999) // Not Distribution
                    {
                        list = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == RevCurrent.ToString() && (x.Rev == x.Lrev) && (x.Customer != "" && (x.ModelCode != "" && x.ModelCode != null) && x.ModelName != "" && x.Sebango != "" && x.Pltype != "")).OrderByDescending(x => x.Customer).ThenBy(x => x.Id).ToList();

                        var rowEmpty = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == RevCurrent.ToString() && x.Lrev == RevCurrent.ToString() && (x.Customer == "" && (x.ModelCode == "" || x.ModelCode == null) && x.ModelName == "" && x.Sebango == "" && x.Pltype == "")).ToList();
                        list.AddRange(rowEmpty);
                        if (list.Count < 10)
                        {
                            for (int i = list.Count; i < 10; i++)
                            {
                                AlSaleForecaseMonth newRow = new AlSaleForecaseMonth();
                                newRow.ModelCode = "";
                                newRow.ModelName = "";
                                newRow.Sebango = "";
                                newRow.Pltype = "";
                                newRow.Customer = "";
                                newRow.Ym = ym;
                                newRow.Rev = RevCurrent.ToString();
                                newRow.Lrev = LrevCurrent.ToString();
                                newRow.CreateDate = DateTime.Now;
                                newRow.CreateBy = param.empcode;
                                _contextDBSCM.AlSaleForecaseMonths.Add(newRow);
                                _contextDBSCM.SaveChanges();
                                int Id = newRow.Id;
                                var recordNow = _contextDBSCM.AlSaleForecaseMonths.FirstOrDefault(x => x.Id == Id);
                                if (recordNow != null)
                                {
                                    list.Add(recordNow);
                                }
                            }
                        }
                    }
                    else // Current is Distribution
                    {
                        list = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == RevCurrent.ToString() && x.Lrev == "999" && x.Customer != "" && x.ModelCode != "" && x.ModelName != "" && x.Sebango != "" && x.Pltype != "").OrderByDescending(x => x.Customer).ThenBy(x => x.Id).ToList();
                    }
                    if (filterCustomer != null && filterCustomer.Count > 0)
                    {
                        List<string> strListCustomer = filterCustomer.Select(x => x.value).ToList();
                        list = list.Where(x => strListCustomer.Contains(x.Customer)).ToList();
                    }
                    if (filterSBU != null && filterSBU.Count > 0)
                    {
                        List<string> strListSBU = filterSBU.Select(x => x.value!).ToList();
                        foreach (AlSaleForecaseMonth iData in list.ToList())
                        {
                            string modelName = iData.ModelName!;
                            bool match = false;
                            foreach (string itemSBU in strListSBU)
                            {
                                if (itemSBU != "%")
                                {
                                    if (modelName != "" && modelName.Substring(0, itemSBU.Length) == itemSBU)
                                    {
                                        match = true;
                                    }
                                }
                                else
                                {
                                    if (modelName != "" && (modelName.Substring(0, 1) != "J" && modelName.Substring(0, 3) != "1YC" && modelName.Substring(0, 3) != "2YC"))
                                    {
                                        match = true;
                                    }
                                }

                                if (match == false)
                                {
                                    var iHave = list.Find(i => i.Id == iData.Id);
                                    if (iHave != null)
                                    {
                                        list.Remove(iHave);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        AlSaleForecaseMonth newRow = new AlSaleForecaseMonth();
                        newRow.ModelCode = "";
                        newRow.ModelName = "";
                        newRow.Sebango = "";
                        newRow.Pltype = "";
                        newRow.Customer = "";
                        newRow.Ym = ym;
                        newRow.Rev = "1";
                        newRow.Lrev = "1";
                        newRow.CreateDate = DateTime.Now;
                        newRow.CreateBy = param.empcode;
                        _contextDBSCM.AlSaleForecaseMonths.Add(newRow);
                    }
                    int insertNewRow = _contextDBSCM.SaveChanges();
                    if (insertNewRow > 0)
                    {
                        list = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == "1" && x.Lrev == "1").ToList();
                    }
                }
            }
            return Ok(new
            {
                data = list,
                customer = customers,
                model = models
            });
        }


        [HttpPost]
        [Route("/status/sale")]
        public IActionResult GetSaleStatus([FromBody] MParam param)
        {
            string ym = param.ym;
            MGetRevAndLrev oGetRevAndLrev = GetRevAndLrev(ym);
            int RevCurrent = oGetRevAndLrev.rev;
            int LrevCurrent = oGetRevAndLrev.lrev;
            return Ok(new
            {
                isDistribution = LrevCurrent == 999 ? true : false,
                rev = RevCurrent
            });
        }

        [HttpGet]
        [Route("/get/customer")]
        public IActionResult getCustomer()
        {
            List<AlCustomer> rCustomer = _contextDBSCM.AlCustomers.ToList();
            return Ok(rCustomer);
        }

        [HttpGet]
        [Route("/get/model")]
        public IActionResult getModel()
        {
            List<MFilter> res = new List<MFilter>();
            var content = _contextDBSCM.PnCompressors.Where(x => x.ModelCode != "BMC" && x.ModelCode != "BMLROTOR" && x.ModelCode != "BMLSTATOR" && x.ModelCode != "PACK" && x.ModelCode != "SPECIAL" && x.ModelCode != "BMSROTOR" && x.ModelCode != "BMSSTATOR").ToList();
            foreach (var item in content)
            {
                MFilter i = new MFilter();
                i.label = item.Model;
                i.value = item.Model.Trim();
                res.Add(i);
            }
            return Ok(res);
        }

        [HttpGet]
        [Route("/get/sebango")]
        public IActionResult getSebango()
        {
            List<MFilter> res = new List<MFilter>();
            var content = _contextDBSCM.PnCompressors.Where(x => x.ModelCode != "BMC" && x.ModelCode != "BMLROTOR" && x.ModelCode != "BMLSTATOR" && x.ModelCode != "PACK" && x.ModelCode != "SPECIAL" && x.ModelCode != "BMSROTOR" && x.ModelCode != "BMSSTATOR").GroupBy(x => x.ModelCode).Select(g => new
            {
                modelCode = g.Key
            }).ToList();
            foreach (var item in content.ToList())
            {
                MFilter i = new MFilter();
                i.label = item.modelCode;
                i.value = item.modelCode;
                res.Add(i);
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("/update/sale")]
        public IActionResult UpdateSale([FromBody] MUpdateSale param)
        {
            string ym = param.ym;
            string rev = "1";
            string lrev = "1";
            List<MSale> data = param.listSale;
            List<AlSaleForecaseMonth> contentPrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == x.Lrev).OrderByDescending(x => x.Rev).ToList();
            if (contentPrev.Count > 0 && contentPrev.FirstOrDefault() != null)
            {
                rev = contentPrev.FirstOrDefault()!.Rev!;
                lrev = contentPrev.FirstOrDefault()!.Rev!;
            }
            foreach (MSale item in data)
            {
                try
                {
                    if (item.id != null)
                    {
                        AlSaleForecaseMonth prev = _contextDBSCM.AlSaleForecaseMonths.FirstOrDefault(x => x.Ym == ym && x.Id == item.id)!;
                        if (prev != null)
                        {
                            prev.UpdateDate = DateTime.Now;
                            prev.Ym = ym;
                            prev.Customer = item.customer;
                            prev.ModelCode = item.sebango;
                            prev.ModelName = item.modelName;
                            prev.Sebango = item.sebango;
                            prev.Pltype = item.pltype;
                            prev.D01 = service.setNumSale(item.D01!);
                            prev.D02 = service.setNumSale(item.D02!);
                            prev.D03 = service.setNumSale(item.D03!);
                            prev.D04 = service.setNumSale(item.D04!);
                            prev.D05 = service.setNumSale(item.D05!);
                            prev.D06 = service.setNumSale(item.D06!);
                            prev.D07 = service.setNumSale(item.D07!);
                            prev.D08 = service.setNumSale(item.D08!);
                            prev.D09 = service.setNumSale(item.D09!);
                            prev.D10 = service.setNumSale(item.D10!);
                            prev.D11 = service.setNumSale(item.D11!);
                            prev.D12 = service.setNumSale(item.D12!);
                            prev.D13 = service.setNumSale(item.D13!);
                            prev.D14 = service.setNumSale(item.D14!);
                            prev.D15 = service.setNumSale(item.D15!);
                            prev.D16 = service.setNumSale(item.D16!);
                            prev.D17 = service.setNumSale(item.D17!);
                            prev.D18 = service.setNumSale(item.D18!);
                            prev.D19 = service.setNumSale(item.D19!);
                            prev.D20 = service.setNumSale(item.D20!);
                            prev.D21 = service.setNumSale(item.D21!);
                            prev.D22 = service.setNumSale(item.D22!);
                            prev.D23 = service.setNumSale(item.D23!);
                            prev.D24 = service.setNumSale(item.D24!);
                            prev.D25 = service.setNumSale(item.D25!);
                            prev.D26 = service.setNumSale(item.D26!);
                            prev.D27 = service.setNumSale(item.D27!);
                            prev.D28 = service.setNumSale(item.D28!);
                            prev.D29 = service.setNumSale(item.D29!);
                            prev.D30 = service.setNumSale(item.D30!);
                            prev.D31 = service.setNumSale(item.D31!);

                            _contextDBSCM.AlSaleForecaseMonths.Update(prev);
                        }
                    }
                    else
                    {
                        if (item.customer != "" && item.modelName != "" && item.sebango != "" && item.pltype != "")
                        {
                            AlSaleForecaseMonth itemAdd = new AlSaleForecaseMonth();
                            itemAdd.Ym = ym;
                            itemAdd.Customer = item.customer;
                            itemAdd.ModelCode = item.sebango;
                            itemAdd.ModelName = item.modelName;
                            itemAdd.Sebango = item.sebango;
                            itemAdd.Pltype = item.pltype;
                            itemAdd.D01 = service.setNumSale(item.D01!);
                            itemAdd.D02 = service.setNumSale(item.D02!);
                            itemAdd.D03 = service.setNumSale(item.D03!);
                            itemAdd.D04 = service.setNumSale(item.D04!);
                            itemAdd.D05 = service.setNumSale(item.D05!);
                            itemAdd.D06 = service.setNumSale(item.D06!);
                            itemAdd.D07 = service.setNumSale(item.D07!);
                            itemAdd.D08 = service.setNumSale(item.D08!);
                            itemAdd.D09 = service.setNumSale(item.D09!);
                            itemAdd.D10 = service.setNumSale(item.D10!);
                            itemAdd.D11 = service.setNumSale(item.D11!);
                            itemAdd.D12 = service.setNumSale(item.D12!);
                            itemAdd.D13 = service.setNumSale(item.D13!);
                            itemAdd.D14 = service.setNumSale(item.D14!);
                            itemAdd.D15 = service.setNumSale(item.D15!);
                            itemAdd.D16 = service.setNumSale(item.D16!);
                            itemAdd.D17 = service.setNumSale(item.D17!);
                            itemAdd.D18 = service.setNumSale(item.D18!);
                            itemAdd.D19 = service.setNumSale(item.D19!);
                            itemAdd.D20 = service.setNumSale(item.D20!);
                            itemAdd.D21 = service.setNumSale(item.D21!);
                            itemAdd.D22 = service.setNumSale(item.D22!);
                            itemAdd.D23 = service.setNumSale(item.D23!);
                            itemAdd.D24 = service.setNumSale(item.D24!);
                            itemAdd.D25 = service.setNumSale(item.D25!);
                            itemAdd.D26 = service.setNumSale(item.D26!);
                            itemAdd.D27 = service.setNumSale(item.D27!);
                            itemAdd.D28 = service.setNumSale(item.D28!);
                            itemAdd.D29 = service.setNumSale(item.D29!);
                            itemAdd.D30 = service.setNumSale(item.D30!);
                            itemAdd.D31 = service.setNumSale(item.D31!);
                            itemAdd.CreateBy = param.empcode;
                            itemAdd.CreateDate = DateTime.Now;
                            itemAdd.UpdateDate = DateTime.Now;
                            itemAdd.Rev = rev;
                            itemAdd.Lrev = lrev;
                            _contextDBSCM.AlSaleForecaseMonths.Add(itemAdd);
                        }
                    }
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }
            }
            int update = _contextDBSCM.SaveChanges();
            return Ok(new
            {
                status = update
            });
        }

        //[HttpPost]
        //[Route("/distribution/sale")]
        //public IActionResult DistributionSale([FromBody] MParam param)
        //{
        //    string empcode = param.empcode;
        //    string ym = param.ym;
        //    MGetRevAndLrev oGetRevAndLrev = GetRevAndLrev(ym);
        //    int RevCurrent = oGetRevAndLrev.rev;
        //    int LrevCurrent = oGetRevAndLrev.lrev;
        //    string message = "";
        //    int count = 0;
        //    int countUpdate = 0;
        //    bool result = false;
        //    if (oGetRevAndLrev != null && ym != "")
        //    {
        //        SqlCommand sql = new SqlCommand();
        //        sql.CommandText = @"SELECT *  FROM [dbSCM].[dbo].[AL_SaleForecaseMonth] WHERE YM = '" + ym + "' AND REV = '" + RevCurrent.ToString() + "' AND LREV = '" + LrevCurrent.ToString() + "'";
        //        DataTable dtSaleforecase = _dbSCM.Query(sql);
        //        count = dtSaleforecase.Rows.Count;
        //        foreach (DataRow dr in dtSaleforecase.Rows)
        //        {
        //            try
        //            {
        //                string ID = dr["ID"].ToString();
        //                SqlCommand sqlUpdate = new SqlCommand();
        //                sqlUpdate.CommandText = @"UPDATE [dbo].[AL_SaleForecaseMonth] SET [LREV] = 999,[CreateBy] = '" + empcode + "' WHERE ID = '" + ID + "'";
        //                int update = _dbSCM.ExecuteNonQuery(sqlUpdate);
        //                if (update > 0)
        //                {
        //                    countUpdate++;
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                message = e.Message;
        //            }
        //        }
        //        if (count == countUpdate)
        //        {
        //            result = true;
        //        }
        //        return Ok(new
        //        {
        //            status = result,
        //            msg = message,
        //        });
        //    }
        //    else
        //    {
        //        return Ok(new
        //        {
        //            status = false,
        //            error = $"ไม่พบข้อมูล {ym}"
        //        });
        //    }
        //}

        [HttpPost]
        [Route("/status/list/sale")]
        public IActionResult StatusSale([FromBody] MParam param)
        {
            int number = 0;
            string year = param.year.ToString();
            string[] listMonth = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            List<MStatusSale> res = new List<MStatusSale>();
            foreach (string month in listMonth)
            {
                DateTime? dt = DateTime.Parse("1900-01-01");
                string ym = $"{year}{month}";
                bool isDistribution = false;
                var content = _contextDBSCM.AlSaleForecaseMonths.FirstOrDefault(x => x.Ym == ym && x.Lrev == "999");
                if (content != null)
                {
                    isDistribution = true;
                    number = int.Parse(content.Rev);
                    dt = content.CreateDate;
                }
                else
                {
                    var contentPrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym).OrderByDescending(x => x.Rev).FirstOrDefault();
                    if (contentPrev != null)
                    {
                        number = int.Parse(contentPrev.Rev);
                        dt = contentPrev.CreateDate;
                    }
                }
                res.Add(new MStatusSale()
                {
                    isDistribution = isDistribution,
                    year = year,
                    month = month,
                    rev = number,
                    dt = dt
                });
                number = 0;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("/clear/sale")]
        public IActionResult ClearSale([FromBody] MParam param)
        {
            string ym = param.ym;
            try
            {
                if (ym != "" && ym != null && ym != "null")
                {
                    List<AlSaleForecaseMonth> listSale = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym).ToList();
                    _contextDBSCM.AlSaleForecaseMonths.RemoveRange(listSale);
                    int delete = _contextDBSCM.SaveChanges();
                    return Ok(new
                    {
                        status = delete
                    });
                    //var content = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym).OrderByDescending(x => x.Rev).FirstOrDefault();
                    //if (content != null)
                    //{
                    //    var listPrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == content.Rev && x.Lrev == content.Rev).ToList();
                    //    listPrev.ForEach(x => x.Lrev = "0");
                    //    listPrev.ForEach(x => x.CreateBy = param.empcode);
                    //    listPrev.ForEach(x => x.UpdateDate = DateTime.Now);
                    //    int updatePrev = _contextDBSCM.SaveChanges();
                    //    if (updatePrev > 0 && listPrev.Count > 0)
                    //    {
                    //        for (int i = 0; i < 10; i++)
                    //        {
                    //            AlSaleForecaseMonth newRow = new AlSaleForecaseMonth();
                    //            newRow.ModelCode = "";
                    //            newRow.ModelName = "";
                    //            newRow.Sebango = "";
                    //            newRow.Pltype = "";
                    //            newRow.Customer = "";
                    //            newRow.Ym = ym;
                    //            newRow.Rev = listPrev.FirstOrDefault().Rev;
                    //            newRow.Lrev = listPrev.FirstOrDefault().Rev;
                    //            newRow.CreateDate = DateTime.Now;
                    //            newRow.CreateBy = param.empcode;
                    //            _contextDBSCM.AlSaleForecaseMonths.Add(newRow);

                    //        }
                    //        _contextDBSCM.SaveChanges();
                    //    }
                    //    return Ok(new
                    //    {
                    //        status = updatePrev
                    //    });
                    //}
                    //else
                    //{
                    //    return Ok(new
                    //    {
                    //        status = false
                    //    });
                    //}
                }
                else
                {
                    return Ok(new
                    {
                        status = false,
                        error = $"ไม่พบข้อมูลที่คุณต้องการลบ {ym}"
                    });
                }
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = false,
                    error = e.Message.ToString()
                });
            }
        }

        [HttpPost]
        [Route("/status/change/sale")] // ปรับจากโหมด แจกจ่าย --> แก้ไข
        public IActionResult ChangeStatusSale([FromBody] MParam param)
        {
            string ym = param.ym;
            bool status = false;
            MGetRevAndLrev oGetRevAndLrev = GetRevAndLrev(ym);
            int RevCurrent = oGetRevAndLrev.rev;
            int LrevCurrent = oGetRevAndLrev.lrev;
            if (RevCurrent != 0 && LrevCurrent != 0 && ym != "")
            {
                List<AlSaleForecaseMonth> rSaleForecase = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == RevCurrent.ToString() && x.Lrev == LrevCurrent.ToString()).OrderBy(x => x.Id).ToList();
                if (rSaleForecase.Count > 0)
                {
                    string rev = (RevCurrent + 1).ToString();
                    rSaleForecase.ForEach(x => x.Lrev = rev);
                    int updatePrev = _contextDBSCM.SaveChanges();
                    if (updatePrev > 0)
                    {
                        foreach (AlSaleForecaseMonth item in rSaleForecase)
                        {
                            AlSaleForecaseMonth itemAdd = new AlSaleForecaseMonth();
                            itemAdd.ModelCode = item.ModelCode;
                            itemAdd.ModelName = item.ModelName;
                            itemAdd.Pltype = item.Pltype;
                            itemAdd.Customer = item.Customer;
                            itemAdd.D01 = item.D01;
                            itemAdd.D02 = item.D02;
                            itemAdd.D03 = item.D03;
                            itemAdd.D04 = item.D04;
                            itemAdd.D05 = item.D05;
                            itemAdd.D06 = item.D06;
                            itemAdd.D07 = item.D07;
                            itemAdd.D08 = item.D08;
                            itemAdd.D09 = item.D09;
                            itemAdd.D10 = item.D10;
                            itemAdd.D11 = item.D11;
                            itemAdd.D12 = item.D12;
                            itemAdd.D13 = item.D13;
                            itemAdd.D14 = item.D14;
                            itemAdd.D15 = item.D15;
                            itemAdd.D16 = item.D16;
                            itemAdd.D17 = item.D17;
                            itemAdd.D18 = item.D18;
                            itemAdd.D19 = item.D19;
                            itemAdd.D20 = item.D20;
                            itemAdd.D21 = item.D21;
                            itemAdd.D22 = item.D22;
                            itemAdd.D23 = item.D23;
                            itemAdd.D24 = item.D24;
                            itemAdd.D25 = item.D25;
                            itemAdd.D26 = item.D26;
                            itemAdd.D27 = item.D27;
                            itemAdd.D28 = item.D28;
                            itemAdd.D29 = item.D29;
                            itemAdd.D30 = item.D30;
                            itemAdd.D31 = item.D31;
                            itemAdd.CreateDate = DateTime.Now;
                            itemAdd.CreateBy = item.CreateBy;
                            itemAdd.Rev = rev;
                            itemAdd.Lrev = LrevCurrent == 999 ? rev : "999";
                            itemAdd.Sebango = item.Sebango;
                            itemAdd.Row = item.Row;
                            itemAdd.Ym = item.Ym;
                            _contextDBSCM.AlSaleForecaseMonths.Add(itemAdd);
                        }
                        int upgrade = _contextDBSCM.SaveChanges();
                        if (upgrade > 0)
                        {
                            status = true;
                        }
                    }
                }
                return Ok(new
                {
                    status = status
                });
            }
            else
            {
                return Ok(new
                {
                    status = false,
                    error = $"ไม่พบข้อมูล {ym}"
                });
            }
        }


        [HttpPost]
        [Route("/employee/login")]
        public IActionResult EmployeeLogin([FromBody] MLogin param)
        {
            Employee login = _contextDBHRM.Employees.FirstOrDefault(x => x.Code == param.empcode)!;
            if (login != null)
            {
                return Ok(new
                {
                    name = $"{login.Pren.ToUpper()}{login.Name}.{login.Surn!.Substring(0, 1)}",
                    status = true
                });
            }
            return Ok(new
            {
                name = "",
                status = false
            });
        }

        [HttpPost]
        [Route("/newrow/sale")]
        public IActionResult NewRowSale([FromBody] MNewrow param)
        {
            string empcode = param.empcode;
            string ym = param.ym;
            MGetRevAndLrev oGetRevAndLrev = GetRevAndLrev(ym);
            int RevCurrent = oGetRevAndLrev.rev;
            int LrevCurrent = oGetRevAndLrev.lrev;
            List<AlSaleForecaseMonth> listNewRow = param.data;
            foreach (AlSaleForecaseMonth row in listNewRow)
            {
                row.ModelName = "";
                row.Ym = ym;
                row.Rev = RevCurrent.ToString();
                row.Lrev = LrevCurrent.ToString();
                row.CreateDate = DateTime.Now;
                row.UpdateDate = DateTime.Now;
                row.ModelCode = "";
                row.CreateBy = param.empcode;
                _contextDBSCM.AlSaleForecaseMonths.Add(row);
            }
            int insertRow = _contextDBSCM.SaveChanges();
            return Ok(new
            {
                status = insertRow
            });
        }

        [HttpPost]
        [Route("/clearempty/sale")]
        public IActionResult ClearEmptySale([FromBody] MUpdateSale param)
        {
            List<MSale> data = param.listSale;
            string ym = param.ym;
            MGetRevAndLrev oGetRevAndLrev = GetRevAndLrev(ym);
            int RevCurrent = oGetRevAndLrev.rev;
            int LrevCurrent = oGetRevAndLrev.lrev;
            int delete = 999;
            if (RevCurrent != 0 && LrevCurrent != 0)
            {
                List<AlSaleForecaseMonth> rSaleForecase = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == RevCurrent.ToString() && x.Lrev == LrevCurrent.ToString()).ToList();
                foreach (AlSaleForecaseMonth itemSale in rSaleForecase)
                {
                    if (itemSale.Id != null && itemSale.Customer!.Trim() == "" && (itemSale.ModelCode == "" || itemSale.ModelCode == null) && itemSale.Sebango!.Trim() == "" && itemSale.Pltype!.Trim() == "")
                    {
                        _contextDBSCM.AlSaleForecaseMonths.Remove(itemSale);
                    }
                }
                delete = _contextDBSCM.SaveChanges();
            }
            return Ok(new
            {
                status = delete
            });
        }

        [HttpPost]
        [Route("/saleforecase/undistribution")]
        public IActionResult UnDistribution([FromBody] MDistributionSaleForecase obj)
        {
            try
            {
                Service service = new Service(_contextDBSCM, _contextDBHRM, _contextDBBCS, _ALPHAPD, _ALPHAPD1, _ALPHAPD2);
                string year = obj.year;
                string empcode = obj.empcode;
                string[] ver = service.GetVersion(year);
                string rev = ver[1];
                string lrev = ver[2];
                List<AlSaleForecaseMonth> rSaleModelNotNumberic = new List<AlSaleForecaseMonth>();
                List<AlSaleForecaseMonth> rSaleDontHaveInSaleNew = new List<AlSaleForecaseMonth>();
                if (ver[0] == "1" && rev != lrev && lrev == "999" || obj.dev == true)
                {
                    List<AlSaleForecaseMonth> rSaleCurrent = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym.StartsWith(year) && x.Rev == rev && x.Lrev == lrev && (x.D01 > 0 || x.D02 > 0 || x.D03 > 0 || x.D04 > 0 || x.D05 > 0 || x.D06 > 0 || x.D07 > 0 || x.D08 > 0 || x.D09 > 0 || x.D10 > 0 || x.D11 > 0 || x.D12 > 0 || x.D13 > 0 || x.D14 > 0 || x.D15 > 0 || x.D16 > 0 || x.D17 > 0 || x.D18 > 0 || x.D19 > 0 || x.D20 > 0 || x.D21 > 0 || x.D22 > 0 || x.D23 > 0 || x.D24 > 0 || x.D25 > 0 || x.D26 > 0 || x.D27 > 0 || x.D28 > 0 || x.D29 > 0 || x.D30 > 0 || x.D31 > 0)).ToList();
                    rev = (int.Parse(rev) + 1).ToString();
                    List<AlSaleForecaseMonth> rSaleNow = service.InitNewRowSaleForecase(year, empcode, rev, rev);
                    _contextDBSCM.AlSaleForecaseMonths.AddRange(rSaleNow);
                    int insert = _contextDBSCM.SaveChanges();
                    if (insert > 0)
                    {
                        rSaleNow = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym.StartsWith(year) && x.Rev == rev && x.Lrev == rev).ToList();
                        foreach (AlSaleForecaseMonth oSaleCurrent in rSaleCurrent)
                        {
                            int i;
                            bool bNum = int.TryParse(oSaleCurrent.ModelCode, out i);
                            AlSaleForecaseMonth oSaleNow = null;
                            if (bNum == false)
                            {
                                oSaleNow = rSaleNow.FirstOrDefault(x => x.Ym == oSaleCurrent.Ym && x.ModelName == oSaleCurrent.ModelName && x.Customer == oSaleCurrent.Customer && x.Diameter == oSaleCurrent.Diameter && x.Pltype == oSaleCurrent.Pltype && x.Rev == rev && x.Lrev == rev);
                                if (oSaleNow == null)
                                {
                                    rSaleModelNotNumberic.Add(oSaleCurrent);
                                }
                            }
                            else
                            {
                                oSaleNow = rSaleNow.FirstOrDefault(x => x.Ym == oSaleCurrent.Ym && x.ModelName == oSaleCurrent.ModelName && x.Customer == oSaleCurrent.Customer && x.Diameter == oSaleCurrent.Diameter && x.Pltype == oSaleCurrent.Pltype && x.Rev == rev && x.Lrev == rev && x.ModelCode == oSaleCurrent.ModelCode);
                            }

                            if (oSaleNow != null)
                            {
                                SqlCommand strUpdate = new SqlCommand();
                                strUpdate.CommandText = @"UPDATE [dbo].[AL_SaleForecaseMonth] SET  [D01] = @d01 ,[D02] = @d02 ,[D03] = @d03 ,[D04] = @d04 ,[D05] = @d05 ,[D06] = @d06 ,[D07] = @d07 ,[D08] = @d08 ,[D09] = @d09 ,[D10] = @d10 ,[D11] = @d11 ,[D12] = @d12 ,[D13] = @d13 ,[D14] = @d14 ,[D15] = @d15 ,[D16] = @d16 ,[D17] = @d17 ,[D18] = @d18 ,[D19] = @d19 ,[D20] = @d20 ,[D21] = @d21 ,[D22] = @d22 ,[D23] = @d23 ,[D24] = @d24 ,[D25] = @d25 ,[D26] = @d26 ,[D27] = @d27 ,[D28] = @d28 ,[D29] = @d29 ,[D30] = @d30 ,[D31] = @d31  ,[UpdateDate] = getdate()  WHERE  ID = @id";
                                strUpdate.Parameters.Add(new SqlParameter("@id", oSaleNow.Id));
                                strUpdate.Parameters.Add(new SqlParameter("@d01", oSaleCurrent.D01 != null ? oSaleCurrent.D01 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d02", oSaleCurrent.D02 != null ? oSaleCurrent.D02 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d03", oSaleCurrent.D03 != null ? oSaleCurrent.D03 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d04", oSaleCurrent.D04 != null ? oSaleCurrent.D04 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d05", oSaleCurrent.D05 != null ? oSaleCurrent.D05 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d06", oSaleCurrent.D06 != null ? oSaleCurrent.D06 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d07", oSaleCurrent.D07 != null ? oSaleCurrent.D07 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d08", oSaleCurrent.D08 != null ? oSaleCurrent.D08 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d09", oSaleCurrent.D09 != null ? oSaleCurrent.D09 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d10", oSaleCurrent.D10 != null ? oSaleCurrent.D10 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d11", oSaleCurrent.D11 != null ? oSaleCurrent.D11 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d12", oSaleCurrent.D12 != null ? oSaleCurrent.D12 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d13", oSaleCurrent.D13 != null ? oSaleCurrent.D13 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d14", oSaleCurrent.D14 != null ? oSaleCurrent.D14 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d15", oSaleCurrent.D15 != null ? oSaleCurrent.D15 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d16", oSaleCurrent.D16 != null ? oSaleCurrent.D16 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d17", oSaleCurrent.D17 != null ? oSaleCurrent.D17 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d18", oSaleCurrent.D18 != null ? oSaleCurrent.D18 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d19", oSaleCurrent.D19 != null ? oSaleCurrent.D19 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d20", oSaleCurrent.D20 != null ? oSaleCurrent.D20 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d21", oSaleCurrent.D21 != null ? oSaleCurrent.D21 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d22", oSaleCurrent.D22 != null ? oSaleCurrent.D22 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d23", oSaleCurrent.D23 != null ? oSaleCurrent.D23 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d24", oSaleCurrent.D24 != null ? oSaleCurrent.D24 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d25", oSaleCurrent.D25 != null ? oSaleCurrent.D25 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d26", oSaleCurrent.D26 != null ? oSaleCurrent.D26 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d27", oSaleCurrent.D27 != null ? oSaleCurrent.D27 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d28", oSaleCurrent.D28 != null ? oSaleCurrent.D28 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d29", oSaleCurrent.D29 != null ? oSaleCurrent.D29 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d30", oSaleCurrent.D30 != null ? oSaleCurrent.D30 : 0));
                                strUpdate.Parameters.Add(new SqlParameter("@d31", oSaleCurrent.D31 != null ? oSaleCurrent.D31 : 0));
                                int update = _dbSCM.ExecuteNonCommand(strUpdate);
                            }
                            else
                            {
                                rSaleDontHaveInSaleNew.Add(oSaleCurrent);
                                if (oSaleCurrent.Diameter != "")
                                {
                                    Console.WriteLine("asdasd");
                                }
                            }
                        }
                        List<AlSaleForecaseMonth> rSalePrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym.StartsWith(year) && x.Rev == (Convert.ToInt32(rev) - 1).ToString() && x.Lrev == "999").ToList();
                        rSalePrev.ForEach(x => x.Lrev = rev);
                        _contextDBSCM.AlSaleForecaseMonths.UpdateRange(rSalePrev);
                        int updatePrev = _contextDBSCM.SaveChanges();
                        return Ok(new
                        {
                            status = updatePrev > 0 ? true : false,
                            messsage = $"[E001] เกิดข้อผิดพลาดระหว่างการสร้างข้อมูลใหม่ เนื่องจากข้อมูลใหม่ไม่ถูกต้องตามระบบ (year : {year}, rev : {rev}, lrev : 999)!"
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            status = false,
                            messsage = $"[E002] เกิดข้อผิดพลาดระหว่างการสร้างข้อมูลใหม่ เนื่องจากข้อมูลใหม่ไม่ถูกต้องตามระบบ (year : {year}, rev : {rev}, lrev : 999)!"
                        });

                    }
                }
                else
                {
                    return Ok(new
                    {
                        status = false,
                        messsage = $"[E003] เกิดข้อผิดพลาดระหว่างการสร้างข้อมูลใหม่ เนื่องจากไม่พบข้อมูล (year : {year}, rev : {rev}, lrev : 999)!"
                    });
                }
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = false,
                    messsage = $"[E004] เกิดข้อผิดพลาดระหว่างการสร้างข้อมูลใหม่  ({e.Message}) !"
                });
            }

        }

        [HttpPost]
        [Route("/saleforecase/distribution")]
        public IActionResult DistributionSaleForecase([FromBody] MDistributionSaleForecase obj)
        {
            try
            {
                int count = 0;
                int remove = 0;
                string year = obj.year;
                string empcode = obj.empcode;
                string[] ver = service.GetVersion(year);
                string rev = ver[1];
                string lrev = ver[2];
                if (ver[0] == "1" && rev == lrev)
                {
                    List<AlSaleForecaseMonth> rSaleCurrent = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym.StartsWith(year) && x.Rev == rev && x.Lrev == lrev).ToList();
                    //                  var EfGetID = _contextDBSCM.AlSaleForecaseMonths.FromSqlRaw($@"SELECT * FROM [dbSCM].[dbo].[AL_SaleForecaseMonth]
                    //WHERE YM LIKE '{year}%' AND (D01 != 0 OR D02 != 0 OR D03 != 0 OR D04 != 0 OR D05 != 0 OR D06 != 0 OR D07 != 0 OR D08 != 0 OR D09 != 0 OR D10 != 0 OR D11 != 0 OR D12 != 0 OR D13 != 0 OR D14 != 0 OR D15 != 0 OR D16 != 0 OR D17 != 0 OR D18 != 0 OR D19 != 0 OR D20 != 0 OR D21 != 0 OR D22 != 0 OR D23 != 0 OR D24 != 0 OR D25 != 0 OR D26 != 0 OR D27 != 0 OR D28 != 0 OR D29 != 0 OR D30 != 0  OR D31 != 0)").ToList();
                    //                  int RESDel = 0;
                    //                  string JoinID = string.Join("','", EfGetID.Select(x => x.Id).ToList());
                    //                  string CondNotInID = JoinID.Length > 0 ? $@" AND ID NOT IN ('{JoinID}')" : "";

                    //                  SqlCommand STRDel = new SqlCommand();
                    //                  STRDel.CommandText = $@"DELETE FROM [dbSCM].[dbo].[AL_SaleForecaseMonth] WHERE YM LIKE '{year}%' AND ID NOT IN ('{JoinID}')";
                    //                  RESDel = _dbSCM.ExecuteNonCommand(STRDel);

                    //                  SqlCommand STRUpdateDistribute = new SqlCommand();
                    //                  STRUpdateDistribute.CommandText = $@"UPDATE  [dbSCM].[dbo].[AL_SaleForecaseMonth] SET LREV = '999',UpdateDate = '{DateTime.Now}' WHERE YM LIKE '{year}%'";
                    //                  int RESDis = _dbSCM.ExecuteNonCommand(STRUpdateDistribute);
                    //                  return Ok(new
                    //                  {
                    //                      status = RESDis,
                    //                      message = RESDis == 0 ? "ไม่สามารถเปลี่ยนแปลงเวอร์ชั่นได้ ติดต่อ IT" : ""
                    //                  });


                    foreach (AlSaleForecaseMonth oSale in rSaleCurrent)
                    {
                        if (oSale.D01 > 0 || oSale.D02 > 0 || oSale.D03 > 0 || oSale.D04 > 0 || oSale.D05 > 0 || oSale.D06 > 0 || oSale.D07 > 0 || oSale.D08 > 0 || oSale.D09 > 0 || oSale.D10 > 0 || oSale.D11 > 0 || oSale.D12 > 0 || oSale.D13 > 0 || oSale.D14 > 0 || oSale.D15 > 0 || oSale.D16 > 0 || oSale.D17 > 0 || oSale.D18 > 0 || oSale.D19 > 0 || oSale.D20 > 0 || oSale.D21 > 0 || oSale.D22 > 0 || oSale.D23 > 0 || oSale.D24 > 0 || oSale.D25 > 0 || oSale.D26 > 0 || oSale.D27 > 0 || oSale.D28 > 0 || oSale.D29 > 0 || oSale.D30 > 0 || oSale.D31 > 0)
                        {
                            oSale.Lrev = "999";
                            oSale.UpdateDate = DateTime.Now;
                            _contextDBSCM.AlSaleForecaseMonths.Update(oSale);
                            count++;
                        }
                        else
                        {
                            _contextDBSCM.AlSaleForecaseMonths.Remove(oSale);
                            remove++;
                        }
                    }
                    if (remove == rSaleCurrent.Count && rSaleCurrent.Count != 0)
                    {
                        return Ok(new
                        {
                            status = 0,
                            message = $"ไม่พบข้อมูลที่มีแผนการขายในปี : {year}"
                        });
                    }
                    else
                    {
                        int update = _contextDBSCM.SaveChanges();
                        return Ok(new
                        {
                            status = update,
                            message = "ไม่สามารถเปลี่ยนแปลงเวอร์ชั่นได้ ติดต่อ IT"
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        status = false,
                        messsage = $"ไม่พบข้อมูลสำหรับ Distribution (year : {year}, rev : {rev}, lrev : {lrev}) !"
                    });
                }
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = false,
                    messsage = $"เกิดข้อผิดพลาด Distribution  ({e.Message}) !"
                });
            }

        }

        [HttpPost]
        [Route("/saleforecase/get")]
        public IActionResult GetSalsForecase([FromBody] MGetSaleForecase obj)
        {
            List<AlSaleForecaseMonth> rSaleForecase = new List<AlSaleForecaseMonth>();
            List<GstSalMdl> rDiameter = new List<GstSalMdl>();
            string empcode = obj.empcode;
            string yyyy = obj.year;
            Service service = new Service(_contextDBSCM, _contextDBHRM, _contextDBBCS, _ALPHAPD, _ALPHAPD1, _ALPHAPD2);
            string[] ver = service.GetVersion(yyyy);
            string haveData = ver[0];
            string rev = ver[1];
            string lrev = ver[2];
            if (haveData == "0") // ไม่มีข้อมูลปีนั้นๆ ใน table
            {
                // สร้างข้อมูลใหม่
                rSaleForecase = service.InitNewRowSaleForecase(yyyy, empcode);
                _contextDBSCM.AlSaleForecaseMonths.AddRange(rSaleForecase);
                int insert = _contextDBSCM.SaveChanges();
            }
            else
            {
                rSaleForecase = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym.StartsWith(yyyy) && x.Rev == rev && x.Lrev == lrev).OrderBy(x => x.Customer).ThenBy(x => x.ModelName).ThenBy(x => x.Diameter).ToList();

            }
            return Ok(new
            {
                status = lrev,
                data = rSaleForecase.OrderBy(x => x.Ym)
            });
        }


        [HttpGet]
        [Route("/saleforecase/get/filter/{column}/{year}")]
        public IActionResult GetFilterSaleForecase(string column, string year)
        {
            column = Uri.UnescapeDataString(column);
            List<MChoose> rChoose = new List<MChoose>();
            if (column == "MM/YYYY")
            {
                for (int i = 1; i <= 12; i++)
                {
                    string month = i.ToString("D2");
                    rChoose.Add(new MChoose() { key = $"{year}{month}", value = $"{month}/{year}" });
                }
            }
            else if (column == "CUSTOMER")
            {
                //SqlCommand sql = new SqlCommand();
                //sql.CommandText = @"SELECT * FROM [dbSCM].[dbo].[VDMstr] ORDER BY VenderShortName asc";
                //DataTable dt = _dbSCM.Query(sql);
                //rChoose = _contextDBSCM.vdms
                //rChoose = _contextDBSCM.AlCustomers.Select(x => new MChoose() { key = x.CustomerNameShort, value = x.CustomerNameShort }).OrderBy(x => x.key).ToList();
                rChoose = _contextDBSCM.Vdmstrs.Select(x => new MChoose() { key = x.VenderShortName, value = x.VenderShortName }).OrderBy(x => x.key).ToList();
            }
            else if (column == "MODEL NAME")
            {
                //rChoose = _contextDBSCM.PnCompressors.Where(x => x.Status == "ACTIVE" && x.ModelCode != "SPECIAL" && x.ModelCode != "PACK" && x.ModelCode != "BMC" && x.ModelCode != "BMLSTATOR" && x.ModelCode != "BMSSTATOR" && x.ModelCode != "BMLROTOR" && x.ModelCode != "BMSROTOR").GroupBy(x => new MChoose() { key = x.Model, value = x.ModelCode }).Select(x => new MChoose() { key = x.Key.key, value = $"{x.Key.key} ({x.Key.value})" }).ToList();
                SqlCommand sql = new SqlCommand();
                sql.CommandText = @"SELECT   [MODEL]   ,[SEBANGO] 
  FROM [dbSCM].[dbo].[WMS_MDW27_MODEL_MASTER] 
  group by  [MODEL]  ,[SEBANGO] ";
                DataTable dt = _dbSCM.Query(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    MChoose oChoose = new MChoose();
                    oChoose.key = dr["MODEL"].ToString();
                    oChoose.value = $"{dr["MODEL"].ToString()} ({dr["SEBANGO"].ToString()})";
                    rChoose.Add(oChoose);
                }
            }
            else if (column == "MODEL CODE")
            {
                //rChoose = _contextDBSCM.PnCompressors.Where(x => x.Status == "ACTIVE" && x.ModelCode != "SPECIAL" && x.ModelCode != "PACK" && x.ModelCode != "BMC" && x.ModelCode != "BMLSTATOR" && x.ModelCode != "BMSSTATOR" && x.ModelCode != "BMLROTOR" && x.ModelCode != "BMSROTOR").GroupBy(x => new MChoose() { key = x.ModelCode, value = x.Model }).Select(x => new MChoose() { key = x.Key.key, value = $"{x.Key.key} ({x.Key.value})" }).ToList();
                SqlCommand sql = new SqlCommand();
                sql.CommandText = @"SELECT   [MODEL]   ,[SEBANGO] 
  FROM [dbSCM].[dbo].[WMS_MDW27_MODEL_MASTER] 
  group by  [MODEL]  ,[SEBANGO] ";
                DataTable dt = _dbSCM.Query(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    MChoose oChoose = new MChoose();
                    oChoose.key = dr["SEBANGO"].ToString();
                    oChoose.value = $"{dr["SEBANGO"].ToString()} ({dr["MODEL"].ToString()})";
                    rChoose.Add(oChoose);
                }
            }
            else if (column == "DIAMETER")
            {
                List<GstSalMdl> rDiameter = service.GetListDiameter(_ALPHAPD1);
                rChoose = rDiameter.Where(x => x.sku != "").GroupBy(o => o.sku).Select(x => new MChoose() { key = x.Key, value = x.Key }).ToList();
            }
            else if (column == "PLTYPE")
            {
                rChoose = _contextDBSCM.AlPalletTypeMappings.GroupBy(x => new MChoose() { key = x.Pltype, value = x.Pltype }).Select(x => new MChoose() { key = x.Key.key, value = x.Key.value }).ToList();
            }
            return Ok(rChoose);
        }

        [HttpPost]
        [Route("/saleforecase/update")]
        public IActionResult UpdateSaleforecase([FromBody] ModelUpdateSale obj)
        {
            string empcode = obj.empcode;
            string yyyy = obj.year;
            int action = 0;
            string[] rVer = service.GetVersion(yyyy);
            string rev = null;
            string lrev = null;
            if (rVer[0] == "1")
            {
                rev = rVer[1];
                lrev = rVer[2];
            }
            List<MDataSaleForecase> rSaleUpdate = obj.sales;
            List<AlSaleForecaseMonth> rSaleCurrent = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym.StartsWith(obj.year) && x.Rev == rev && x.Lrev == lrev).ToList();
            foreach (MDataSaleForecase oSale in rSaleUpdate)
            {
                AlSaleForecaseMonth mSaleDev = rSaleCurrent.FirstOrDefault(x => x.Ym == oSale.ym && x.Diameter == oSale.diameter && x.ModelCode == oSale.modelCode && x.ModelName == oSale.modelName && x.Sebango == oSale.modelCode && x.Pltype == oSale.pltype && x.Customer == oSale.customer);
                if (mSaleDev != null)
                {
                    mSaleDev.Ym = oSale.ym;
                    mSaleDev.Diameter = oSale.diameter;
                    mSaleDev.ModelCode = oSale.modelCode;
                    mSaleDev.ModelName = oSale.modelName;
                    mSaleDev.Sebango = oSale.modelCode;
                    mSaleDev.Pltype = oSale.pltype;
                    mSaleDev.Customer = oSale.customer;
                    mSaleDev.D01 = oSale.d01;
                    mSaleDev.D02 = oSale.d02;
                    mSaleDev.D03 = oSale.d03;
                    mSaleDev.D04 = oSale.d04;
                    mSaleDev.D05 = oSale.d05;
                    mSaleDev.D06 = oSale.d06;
                    mSaleDev.D07 = oSale.d07;
                    mSaleDev.D08 = oSale.d08;
                    mSaleDev.D09 = oSale.d09;
                    mSaleDev.D10 = oSale.d10;
                    mSaleDev.D11 = oSale.d11;
                    mSaleDev.D12 = oSale.d12;
                    mSaleDev.D13 = oSale.d13;
                    mSaleDev.D14 = oSale.d14;
                    mSaleDev.D15 = oSale.d15;
                    mSaleDev.D16 = oSale.d16;
                    mSaleDev.D17 = oSale.d17;
                    mSaleDev.D18 = oSale.d18;
                    mSaleDev.D19 = oSale.d19;
                    mSaleDev.D20 = oSale.d20;
                    mSaleDev.D21 = oSale.d21;
                    mSaleDev.D22 = oSale.d22;
                    mSaleDev.D23 = oSale.d23;
                    mSaleDev.D24 = oSale.d24;
                    mSaleDev.D25 = oSale.d25;
                    mSaleDev.D26 = oSale.d26;
                    mSaleDev.D27 = oSale.d27;
                    mSaleDev.D28 = oSale.d28;
                    mSaleDev.D29 = oSale.d29;
                    mSaleDev.D30 = oSale.d30;
                    mSaleDev.D31 = oSale.d31;
                    mSaleDev.CreateBy = empcode;
                    mSaleDev.UpdateDate = DateTime.Now;
                    _contextDBSCM.AlSaleForecaseMonths.Update(mSaleDev);
                }
            }
            string message = "";
            action = _contextDBSCM.SaveChanges();
            return Ok(new
            {
                status = action > 0 ? true : false,
                action = action,
                message = message
            });
        }

        [HttpPost]
        [Route("/saleforecase/diameter")]
        public IActionResult UpdateDiameter()
        {
            List<AlSaleForecaseMonth> rCurrent = _contextDBSCM.AlSaleForecaseMonths.Where(x => (x.Diameter == null || x.Diameter == "") && x.Rev == "9" && x.Lrev == "999").ToList();
            List<GstSalMdl> rDiameter = service.GetListDiameter(_ALPHAPD1);
            foreach (AlSaleForecaseMonth item in rCurrent)
            {
                GstSalMdl oDiameter = rDiameter.FirstOrDefault(x => x.modelName == item.ModelName);
                if (oDiameter != null)
                {
                    string diameter = oDiameter.sku;
                    item.Diameter = diameter;
                    _contextDBSCM.AlSaleForecaseMonths.Update(item);
                }
                else
                {

                }
            }
            _contextDBSCM.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("/getCustomerSetting")]
        public IActionResult GetCustomers()
        {
            List<string> rDict = new List<string>();
            SqlCommand sqlGetCustomer = new SqlCommand();
            sqlGetCustomer.CommandText = @"SELECT  distinct code FROM [dbSCM].[dbo].[DictMstr]  where dict_type = 'CUST_MODEL' and dict_status = 'active' order by code asc ";
            DataTable dt = _dbSCM.Query(sqlGetCustomer);
            foreach (DataRow dr in dt.Rows)
            {
                rDict.Add(dr["code"].ToString());
            }
            return Ok(rDict);
        }

        [HttpGet]
        [Route("/getModelByCustomerCode/{customercode}")]
        public IActionResult GetModelOfCustomer(string customercode = "")
        {
            List<DictMstr> rDict = new List<DictMstr>();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = @" SELECT  DICT_ID  ,[CODE]  ,[REF_CODE] FROM [dbSCM].[dbo].[DictMstr] where dict_system = 'SALEFC' and dict_type = 'CUST_MODEL' and dict_status = 'active' and code = @CUSTOMERCODE
  order by update_date desc ";
            sql.Parameters.Add(new SqlParameter("@CUSTOMERCODE", customercode));
            DataTable dt = _dbSCM.Query(sql);
            foreach (DataRow dr in dt.Rows)
            {
                DictMstr oCustomer = new DictMstr();
                oCustomer.DictId = oHelp.ConvStr2Int(dr["DICT_ID"].ToString());
                oCustomer.Code = dr["CODE"].ToString();
                oCustomer.RefCode = dr["REF_CODE"].ToString();
                rDict.Add(oCustomer);
            }
            return Ok(rDict);
        }
        [HttpGet]
        [Route("/saleforecase/customersetting/getmodel")]
        public IActionResult GetModels()
        {
            List<string> rDict = new List<string>();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = @" SELECT distinct model  as code 
  FROM [dbSCM].[dbo].[WMS_MDW27_MODEL_MASTER]
  order by model asc";
            DataTable dt = _dbSCM.Query(sql);
            foreach (DataRow dr in dt.Rows)
            {
                rDict.Add(service.RemoveLineEndings(dr["code"].ToString()));
            }
            return Ok(rDict);
        }

        [HttpPost]
        [Route("/saleforecase/customersetting/addmodeltocustomer")]
        public IActionResult AddModelToCustomer([FromBody] DictMstr obj)
        {
            bool status = false;
            string message = "";
            SqlCommand sqlExist = new SqlCommand();
            sqlExist.CommandText = @"SELECT DICT_ID FROM [dbSCM].[dbo].[DictMstr] WHERE dict_system = 'SALEFC' and dict_type = 'CUST_MODEL' and code = @CODE and ref_code = @MODEL";
            sqlExist.Parameters.Add(new SqlParameter("@CODE", obj.Code));
            sqlExist.Parameters.Add(new SqlParameter("@MODEL", obj.RefCode));
            DataTable dt = _dbSCM.Query(sqlExist);
            if (dt.Rows.Count == 0)
            {
                SqlCommand sql = new SqlCommand();
                sql.CommandText = @"INSERT INTO [dbo].[DictMstr]
           ([DICT_SYSTEM]
           ,[DICT_TYPE]
           ,[CODE]
           ,[DESCRIPTION]
           ,[REF_CODE]
           ,[NOTE]
           ,[CREATE_DATE]
           ,[UPDATE_DATE]
           ,[DICT_STATUS])
     VALUES
           (@DICT_SYSTEM,@DICT_TYPE,@CODE ,'',@REF_CODE,'',GETDATE(),GETDATE(),'ACTIVE')";
                sql.Parameters.Add(new SqlParameter("@DICT_SYSTEM", "SALEFC"));
                sql.Parameters.Add(new SqlParameter("@DICT_TYPE", "CUST_MODEL"));
                sql.Parameters.Add(new SqlParameter("@CODE", obj.Code));
                sql.Parameters.Add(new SqlParameter("@REF_CODE", obj.RefCode));
                int insert = _dbSCM.ExecuteNonQuery(sql);
                if (insert > 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                    message = "ไม่สามารถเพิ่มได้ ติดต่อ เบียร์ IT (250)";
                }
            }
            else
            {
                status = false;
                message = $"มี model นี้อยู่แล้วที่ Customer {obj.Code}";
            }

            return Ok(new
            {
                status = status,
                message = message
            });
        }

        [HttpPost]
        [Route("/saleforecase/customersetting/delmodelofcustomer")]
        public IActionResult DelModelOfCustomer([FromBody] ParamDeleteModelOfCustomer param)
        {
            string DictID = param.dictId;
            string CustShortName = param.custShortName;
            if (DictID != "" && CustShortName != "")
            {
                SqlCommand sqlGet = new SqlCommand();
                sqlGet.CommandText = $@"SELECT * FROM [dbSCM].[dbo].[DictMstr] WHERE DICT_ID = '{DictID}'";
                DataTable dtCheck = _dbSCM.Query(sqlGet);
                if (dtCheck.Rows.Count > 0)
                {
                    string Model = dtCheck.Rows[0]["REF_CODE"].ToString();
                    SqlCommand sql = new SqlCommand();
                    sql.CommandText = $@"DELETE FROM [dbSCM].[dbo].[DictMstr] WHERE DICT_ID = '{DictID}'";
                    int delete = _dbSCM.ExecuteNonQuery(sql);
                    if (delete > 0)
                    {
                        SqlCommand sqlPalletUsedByModel = new SqlCommand();
                        sqlPalletUsedByModel.CommandText = $@"DELETE FROM [dbSCM].[dbo].[DictMstr] WHERE DICT_SYSTEM = 'SALEFC' AND DICT_TYPE = 'CUST_PL' AND CODE = '{CustShortName}' AND REF_CODE = '{Model}'";
                        int deletePallet = _dbSCM.ExecuteNonCommand(sqlPalletUsedByModel);
                        return Ok(new
                        {
                            status = delete > 0 ? true : false,
                            message = "ไม่สามารถลบได้ ติดต่อ เบียร์ IT (250)"
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            status = delete > 0 ? true : false,
                            message = "ไม่สามารถลบได้ ติดต่อ เบียร์ IT (250)"
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        status = false,
                        message = "ไม่สามารถลบได้ ติดต่อ เบียร์ IT (250)"
                    });
                }
            }
            else
            {
                return Ok(new
                {
                    status = false,
                    message = "ไม่สามารถลบได้ ติดต่อ เบียร์ IT (250)"
                });
            }
        }

        [HttpGet]
        [Route("/saleforecase/refresh/sebango/{year}")]
        public IActionResult refreshSebango(string year)
        {
            List<WMS_MDW27_MODEL_MASTER> rMDW27 = new List<WMS_MDW27_MODEL_MASTER>();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = @" SELECT   [MODEL],[SEBANGO]  FROM [dbSCM].[dbo].[WMS_MDW27_MODEL_MASTER]  group by  [MODEL]  ,[SEBANGO] ";
            DataTable dt = _dbSCM.Query(sql);
            foreach (DataRow dr in dt.Rows)
            {
                WMS_MDW27_MODEL_MASTER oDict = new WMS_MDW27_MODEL_MASTER();
                oDict.model = dr["MODEL"].ToString();
                oDict.sebango = dr["SEBANGO"].ToString();
                rMDW27.Add(oDict);
            }

            string[] ver = service.GetVersion(year);
            string rev = ver[1];
            string lrev = ver[2];
            List<AlSaleForecaseMonth> rSaleModelNotNumberic = new List<AlSaleForecaseMonth>();
            List<AlSaleForecaseMonth> rSaleDontHaveInSaleNew = new List<AlSaleForecaseMonth>();
            int logUpdate = 0;
            if (ver[0] == "1" && rev != lrev && lrev == "999")
            {
                List<AlSaleForecaseMonth> rSaleCurrent = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym.StartsWith(year) && x.Rev == rev && x.Lrev == lrev && (x.D01 > 0 || x.D02 > 0 || x.D03 > 0 || x.D04 > 0 || x.D05 > 0 || x.D06 > 0 || x.D07 > 0 || x.D08 > 0 || x.D09 > 0 || x.D10 > 0 || x.D11 > 0 || x.D12 > 0 || x.D13 > 0 || x.D14 > 0 || x.D15 > 0 || x.D16 > 0 || x.D17 > 0 || x.D18 > 0 || x.D19 > 0 || x.D20 > 0 || x.D21 > 0 || x.D22 > 0 || x.D23 > 0 || x.D24 > 0 || x.D25 > 0 || x.D26 > 0 || x.D27 > 0 || x.D28 > 0 || x.D29 > 0 || x.D30 > 0 || x.D31 > 0)).ToList();
                foreach (AlSaleForecaseMonth oSale in rSaleCurrent)
                {
                    WMS_MDW27_MODEL_MASTER oMDW27 = rMDW27.FirstOrDefault(x => x.model == oSale.ModelName);
                    if (oMDW27 != null && oMDW27.sebango != "" && oMDW27.sebango != oSale.Sebango)
                    {
                        oSale.Sebango = int.Parse(oMDW27.sebango).ToString("D4");
                        _contextDBSCM.AlSaleForecaseMonths.Update(oSale);
                    }
                }
                int update = _contextDBSCM.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        [Route("/GetPalletOfCustomer/{CustCode}")]
        public IActionResult GetPalletOfCustomer(string CustCode)
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = $@"  SELECT PL.MODEL,PL.PLTYPE, 
  ISNULL((SELECT DICT_STATUS FROM [dbSCM].[dbo].[DictMstr]  
  WHERE DICT_SYSTEM = 'SALEFC' AND DICT_TYPE = 'CUST_PL' AND CODE = '{CustCode}' AND REF_CODE = PL.MODEL AND  REF1 = PL.PLTYPE  GROUP BY DICT_STATUS),'INACTIVE')   AS ACTIVE
  FROM [dbSCM].[dbo].[DictMstr] M
  LEFT JOIN [dbSCM].[dbo].[WMS_MDW27_MODEL_MASTER] PL
  ON PL.MODEL = M.REF_CODE
  LEFT JOIN [dbSCM].[dbo].[DictMstr] C
  ON C.CODE = PL.PLTYPE
  WHERE M.DICT_SYSTEM = 'SALEFC' AND M.CODE = '{CustCode}' AND M.DICT_STATUS = 'ACTIVE' AND M.DICT_TYPE = 'CUST_MODEL'
  GROUP BY  PL.MODEL,PL.PLTYPE,C.CODE ";
            DataTable dt = _dbSCM.Query(sql);
            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        [Route("/GetCustomers")]
        public IActionResult GetCustomerALPHA()
        {
            SqlCommand sql = new SqlCommand();
            sql.CommandText = @"SELECT * FROM [dbSCM].[dbo].[VDMstr] ORDER BY VenderShortName asc";
            DataTable dt = _dbSCM.Query(sql);
            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        [Route("/UpdatePalletOfCustomer")]
        public IActionResult UpdatePalletOfCustomer([FromBody] ParamUpdatePalletOfCustomer param)
        {
            bool action = false;
            string custShortName = param.CUSTOMER_SHORT_NAME;
            string pallet = param.PLTYPE;
            string active = param.ACTIVE;
            string model = param.MODEL;
            SqlCommand sqlCheck = new SqlCommand();
            sqlCheck.CommandText = $@"SELECT * FROM  [dbo].[DictMstr] WHERE DICT_SYSTEM = 'SALEFC' AND DICT_TYPE = 'CUST_PL' AND CODE = '{custShortName}' AND REF_CODE = '{model}' AND REF1 = '{pallet}'";
            DataTable dtCheck = _dbSCM.Query(sqlCheck);
            if (dtCheck.Rows.Count > 0)
            {
                try
                {
                    string DictID = dtCheck.Rows[0]["DICT_ID"].ToString();
                    if (DictID != "")
                    {
                        SqlCommand sqlUpdate = new SqlCommand();
                        sqlUpdate.CommandText = $@"UPDATE [dbo].[DictMstr] SET DICT_STATUS = '{active}' WHERE DICT_ID = '{DictID}'";
                        int update = _dbSCM.ExecuteNonCommand(sqlUpdate);
                        if (update > 0)
                        {
                            action = true;
                        }
                    }
                }
                catch
                {
                    action = false;
                }
            }
            else
            {
                SqlCommand sqlInsert = new SqlCommand();
                sqlInsert.CommandText = $@"INSERT INTO [dbo].[DictMstr] ([DICT_SYSTEM],[DICT_TYPE],[CODE],[DESCRIPTION],[REF_CODE],[REF1],[CREATE_DATE],[UPDATE_BY],[UPDATE_DATE],[DICT_STATUS]) VALUES  ('SALEFC','CUST_PL','{custShortName}','','{model}','{pallet}',GETDATE(),'SYSTEM' ,GETDATE(),'ACTIVE')";
                int insert = _dbSCM.ExecuteNonCommand(sqlInsert);
                if (insert > 0)
                {
                    action = true;
                }
            }
            return Ok(new
            {
                status = action
            });
        }



        //********************** 10/10/224 API COMPRESSOR HOLD *******************************//
        [HttpGet]
        [Route("/GetCompressordata/{itemModel}/{status}")]
        public IActionResult GetCompressordata(string itemModel,string status)
        {
            string src = "";
            List<MCoreInterface_Parent> Parent_list = new List<MCoreInterface_Parent>();

            if (itemModel == "ALL" && status == "Click")
            {
                src = $"AND H.MODEL LIKE '%'";
            }
            else if (itemModel == "1YC" && status == "Click")
            {
                src = $"AND H.MODEL LIKE '1Y%'";
            }
            else if (itemModel == "2YC" && status == "Click")
            {
                src = $"AND H.MODEL LIKE '2Y%'";
            }
            else if (itemModel == "SCR" && status == "Click")
            {
                src = $"AND H.MODEL LIKE 'J%'";
            }
            else if(itemModel == "ODM" && status == "Click")
            {
                src = $"AND H.MODEL NOT LIKE '1Y%' AND H.MODEL NOT LIKE '2Y%' AND  H.MODEL NOT LIKE 'J%'";
            }
            else
            {           
               src = $"AND H.MODEL LIKE '{status}%'";                            
            }

            string model = "";

            OracleCommand cmdModel = new OracleCommand();
            cmdModel.CommandText = $@"SELECT DISTINCT H.MODEL
              FROM WMS_WCCTL H
              LEFT JOIN WMS_WCDTL D ON D.WCORDER = H.WCORDER
              WHERE UPPER(SUBSTR(REFNO,1,1)) IN('H') AND STATUS = 'Finished' {src}
              GROUP BY H.MODEL";

            DataTable dtcmdModel = _ALPHAPD.Query(cmdModel);

            foreach (DataRow drModel in dtcmdModel.Rows)
            {
                List<MCoreInterface_Child> Child_list = new List<MCoreInterface_Child>();
                MCoreInterface_Parent Parent = new MCoreInterface_Parent();

                Parent.model = drModel["MODEL"].ToString(); 

                model = drModel["MODEL"].ToString();
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = $@"SELECT UPPER(REFNO) H_REFNO, H.MODEL, H.CHGDATE H_CHGDATE, H.FROMWC H_FROMWC, H.TOWC H_TOWC, COUNT(serial) H_CNT, H.REMARK H_REMARK, '' C_REFNO, '' C_CHGDATE ,'' C_FROMWC ,'' C_TOWC, 0 C_CNT, '' C_REMARK 
              FROM WMS_WCCTL H
              LEFT JOIN WMS_WCDTL D ON D.WCORDER = H.WCORDER
              WHERE UPPER(SUBSTR(REFNO,1,1)) IN('H') AND STATUS = 'Finished' AND H.MODEL LIKE '{model}'
              GROUP BY UPPER(REFNO), H.MODEL, H.CHGDATE, H.REMARK, H.FROMWC, H.TOWC";

                DataTable dt = _ALPHAPD.Query(cmd);

                OracleCommand cmd2 = new OracleCommand();
                cmd2.CommandText = $@"SELECT '' H_REFNO, H.MODEL, '' H_CHGDATE, '' H_FROMWC, '' H_TOWC, 0 H_CNT, '' H_REMARK, UPPER(REFNO) C_REFNO,  H.CHGDATE C_CHGDATE, H.FROMWC C_FROMWC, H.TOWC C_TOWC, COUNT(serial) C_CNT, H.REMARK C_REMARK 
                FROM WMS_WCCTL H
                LEFT JOIN WMS_WCDTL D ON D.WCORDER = H.WCORDER  
                WHERE UPPER(SUBSTR(REFNO,1,1)) IN ('C') AND STATUS = 'Finished' AND H.MODEL LIKE '{model}'
                GROUP BY UPPER(REFNO), H.MODEL, H.CHGDATE, H.REMARK, H.FROMWC, H.TOWC";

                DataTable dt2 = _ALPHAPD.Query(cmd2);

                int i = 0;
                foreach (DataRow dr in dt2.Rows)
                {
                    if (i < dt.Rows.Count)
                    {
                        dt.Rows[i]["C_REFNO"] = dr["C_REFNO"].ToString() == "" ? "" : dr["C_REFNO"].ToString();
                        dt.Rows[i]["C_CHGDATE"] = dr["C_CHGDATE"].ToString();
                        dt.Rows[i]["C_CNT"] = dr["C_CNT"].ToString();
                        dt.Rows[i]["C_REMARK"] = dr["C_REMARK"].ToString();
                    }
                    else
                    {
                        dt.Rows.Add("", model, "", "", "", 0, "", dr["C_REFNO"].ToString(), dr["C_CHGDATE"].ToString(), "", "", dr["C_CNT"], dr["C_REMARK"].ToString());
                    }
                    i++;
                }

                decimal sumStockHold = 0;
                OracleCommand cmd3 = new OracleCommand();
                cmd3.CommandText = $@"select model, count(serial) cnt from fh001 where comid='DCI' and nwc in ('HWH','RWQ') and MODEL = '{model}' group by model";

                DataTable dt3 = _ALPHAPD.Query(cmd3);

                foreach (DataRow dr in dt3.Rows)
                {
                    sumStockHold = Convert.ToDecimal(dr["cnt"]);
                }

                decimal _sum_hold = 0, _sum_unhold = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    MCoreInterface_Child child = new MCoreInterface_Child();

                    decimal h = (Convert.ToDecimal(dr["H_CNT"].ToString()));
                    decimal c = (Convert.ToDecimal(dr["C_CNT"].ToString()));

                    _sum_hold += h;
                    _sum_unhold += c;

                    child.model = dr["MODEL"].ToString();
                    child.hold = dr["H_REFNO"].ToString();
                    child.hdate = dr["H_CHGDATE"].ToString();
                    child.hqty = dr["H_CNT"].ToString();
                    child.remark1 = dr["H_REMARK"].ToString();
                    child.unhold = dr["C_REFNO"].ToString();
                    child.cdate = dr["C_CHGDATE"].ToString();
                    child.unqty = dr["C_CNT"].ToString();
                    child.remark2 = dr["C_REMARK"].ToString();
                    child.sumqtyh = _sum_hold.ToString();
                    child.sumqtyc = _sum_unhold.ToString();
                    child.stockhold = sumStockHold.ToString();

                    Child_list.Add(child);
                    Parent.sumhold = _sum_hold.ToString();
                    Parent.sumunhold = _sum_unhold.ToString();
                    Parent.stockhold = sumStockHold.ToString();
                    Parent.Children = Child_list;
                }

                Parent_list.Add(Parent);              
            }
            return Ok(Parent_list);
        }
        //********************* END API COMPRESSOR HOLD *************************************//
    }
}