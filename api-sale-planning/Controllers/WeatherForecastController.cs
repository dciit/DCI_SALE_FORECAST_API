using api_sale_planning.Contexts;
using api_sale_planning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace api_sale_planning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private Service service = new Service();
        private ConnectDB _dbSCM = new ConnectDB("DBSCM");
        private readonly DBSCM _contextDBSCM;
        private readonly DBHRM _contextDBHRM;
        private readonly DBBCS _contextDBBCS;

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

        //[HttpPost]
        //[Route("/saleforecast/distribution")]
        //public IActionResult Distribution([FromBody] MParam param)
        //{
        //    string ym = param.ym;
        //    List<AlSaleForecaseMonth> listPrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym).ToList();
        //    if (listPrev.Count > 0)
        //    {
        //        string rev = listPrev[0].Rev;
        //        string? lrev = "999";
        //        listPrev.ForEach(x => x.Lrev = lrev);
        //        _contextDBSCM.AlSaleForecaseMonths.UpdateRange(listPrev);
        //        int update = _contextDBSCM.SaveChanges();
        //        return Ok(new
        //        {
        //            status = update
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

        //[HttpPost]
        //[Route("/saleforecast/getsaleofmonth")]
        //public IActionResult getSaleOfMonth([FromBody] MParam param)
        //{
        //    string year = param.year.ToString();
        //    string month = ((int)param.month).ToString("D2");
        //    List<AlSaleForecaseMonth> res = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == $"{year}{month}").OrderBy(x => x.Id).ToList();
        //    return Ok(res);
        //}

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

                //var checkExist = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Rev == itemForecast.Rev && x.Lrev == itemForecast.Lrev && x.ModelCode == itemForecast.ModelCode && x.Customer == itemForecast.Customer && x.Sebango == itemForecast.Sebango && x.Pltype == itemForecast.Pltype).FirstOrDefault();
                //if (checkExist == null)
                //{

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
                            //list = list.Where(x => strListSBU.Contains(x.ModelName!)).ToList();
                        }
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
                        //int itemupdate = _contextDBSCM.SaveChanges();
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

        [HttpPost]
        [Route("/distribution/sale")]
        public IActionResult DistributionSale([FromBody] MParam param)
        {
            string empcode = param.empcode;
            string ym = param.ym;
            MGetRevAndLrev oGetRevAndLrev = GetRevAndLrev(ym);
            int RevCurrent = oGetRevAndLrev.rev;
            int LrevCurrent = oGetRevAndLrev.lrev;
            string message = "";
            int count = 0;
            int countUpdate = 0;
            bool result = false;
            if (oGetRevAndLrev != null && ym != "")
            {
                SqlCommand sql = new SqlCommand();
                sql.CommandText = @"SELECT *  FROM [dbSCM].[dbo].[AL_SaleForecaseMonth] WHERE YM = '" + ym + "' AND REV = '" + RevCurrent.ToString() + "' AND LREV = '" + LrevCurrent.ToString() + "'";
                DataTable dtSaleforecase = _dbSCM.Query(sql);
                count = dtSaleforecase.Rows.Count;
                foreach (DataRow dr in dtSaleforecase.Rows)
                {
                    try
                    {
                        //string MODELCODE = dr["ModelCode"].ToString();
                        //string MODELNAME = dr["ModelName"].ToString();
                        //string PLTYPE = dr["PLTYPE"].ToString();
                        //string SEBANGO = dr["Sebango"].ToString();
                        //string CUSTOMER = dr["Customer"].ToString();
                        string ID = dr["ID"].ToString();
                        SqlCommand sqlUpdate = new SqlCommand();
                        sqlUpdate.CommandText = @"UPDATE [dbo].[AL_SaleForecaseMonth] SET [LREV] = 999,[CreateBy] = '" + empcode + "',UpdateDate = '" + DateTime.Now + "' WHERE ID = '" + ID + "'";
                        int update = _dbSCM.ExecuteNonQuery(sqlUpdate);
                        if (update > 0)
                        {
                            countUpdate++;
                        }
                        //if (MODELCODE != "" && MODELNAME != "" && PLTYPE != "" && SEBANGO != "")
                        //{
                        //    SqlCommand sqlUpdate = new SqlCommand();
                        //    sqlUpdate.CommandText = @"UPDATE [dbo].[AL_SaleForecaseMonth] SET [LREV] = 999,[CreateBy] = '" + empcode + "',UpdateDate = '" + DateTime.Now + "' WHERE YM = '" + ym + "' AND YM = '" + ym + "' AND REV = '" + RevCurrent + "' AND LREV = '" + LrevCurrent + "' AND ModelCode = '" + MODELCODE + "' AND ModelName = '" + MODELNAME + "' AND PLTYPE = '" + PLTYPE + "' AND Customer = '" + CUSTOMER + "'";
                        //    int update = _dbSCM.ExecuteNonQuery(sqlUpdate);


                        //    if (update > 0)
                        //    {
                        //        countUpdate++;
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine("123");
                        //    }
                        //}
                    }
                    catch (Exception e)
                    {
                        message = e.Message;
                    }
                }
                if (count == countUpdate)
                {
                    result = true;
                }
                return Ok(new
                {
                    status = result,
                    msg = message,
                });

                //List<AlSaleForecaseMonth> listPrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == RevCurrent.ToString() && x.Lrev == LrevCurrent.ToString()).ToList();
                //if (listPrev.Count > 0)
                //{

                //    foreach (AlSaleForecaseMonth item in listPrev)
                //    {
                //        item.Lrev = "999";
                //        item.CreateBy = empcode;
                //        item.UpdateDate = DateTime.Now;
                //        _contextDBSCM.AlSaleForecaseMonths.Update(item);
                //    }
                //    int update = _contextDBSCM.SaveChanges();
                //    return Ok(new
                //    {
                //        status = update
                //    });
                //}
                //else
                //{
                //    return Ok(new
                //    {
                //        status = false,
                //        error = $"ไม่พบข้อมูล {ym}"
                //    });
                //}
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
                    var content = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym).OrderByDescending(x => x.Rev).FirstOrDefault();
                    if (content != null)
                    {
                        var listPrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == content.Rev && x.Lrev == content.Rev).ToList();
                        listPrev.ForEach(x => x.Lrev = "0");
                        listPrev.ForEach(x => x.CreateBy = param.empcode);
                        listPrev.ForEach(x => x.UpdateDate = DateTime.Now);
                        int updatePrev = _contextDBSCM.SaveChanges();
                        if (updatePrev > 0 && listPrev.Count > 0)
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
                                newRow.Rev = listPrev.FirstOrDefault().Rev;
                                newRow.Lrev = listPrev.FirstOrDefault().Rev;
                                newRow.CreateDate = DateTime.Now;
                                newRow.CreateBy = param.empcode;
                                _contextDBSCM.AlSaleForecaseMonths.Add(newRow);

                            }
                            _contextDBSCM.SaveChanges();
                        }
                        return Ok(new
                        {
                            status = updatePrev
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            status = false
                        });
                    }
                    //if (content.Count > 0)
                    //{
                    //    _contextDBSCM.AlSaleForecaseMonths.RemoveRange(content);
                    //}
                    //int delete = _contextDBSCM.SaveChanges();
                    //return Ok(new
                    //{
                    //    status = delete
                    //});
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
                //List<AlSaleForecaseMonth> rSaleForecase = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == RevCurrent.ToString()).OrderBy(x => x.Id).ToList();
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
            //bool status = false;
            //var contentPrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym).OrderByDescending(x => x.Rev).FirstOrDefault();
            //if (contentPrev != null)
            //{
            //    List<AlSaleForecaseMonth> listPrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == contentPrev.Rev).OrderBy(x => x.Id).ToList();
            //    if (listPrev.Count > 0)
            //    {
            //        string rev = (int.Parse(listPrev[0]!.Rev!) + 1).ToString();
            //        listPrev.ForEach(x => x.Lrev = rev);
            //        int updatePrev = _contextDBSCM.SaveChanges();
            //        if (updatePrev > 0)
            //        {
            //            foreach (AlSaleForecaseMonth item in listPrev)
            //            {
            //                AlSaleForecaseMonth itemAdd = new AlSaleForecaseMonth();
            //                itemAdd.ModelCode = item.ModelCode;
            //                itemAdd.ModelName = item.ModelName;
            //                itemAdd.Pltype = item.Pltype;
            //                itemAdd.Customer = item.Customer;
            //                itemAdd.D01 = item.D01;
            //                itemAdd.D02 = item.D02;
            //                itemAdd.D03 = item.D03;
            //                itemAdd.D04 = item.D04;
            //                itemAdd.D05 = item.D05;
            //                itemAdd.D06 = item.D06;
            //                itemAdd.D07 = item.D07;
            //                itemAdd.D08 = item.D08;
            //                itemAdd.D09 = item.D09;
            //                itemAdd.D10 = item.D10;
            //                itemAdd.D11 = item.D11;
            //                itemAdd.D12 = item.D12;
            //                itemAdd.D13 = item.D13;
            //                itemAdd.D14 = item.D14;
            //                itemAdd.D15 = item.D15;
            //                itemAdd.D16 = item.D16;
            //                itemAdd.D17 = item.D17;
            //                itemAdd.D18 = item.D18;
            //                itemAdd.D19 = item.D19;
            //                itemAdd.D20 = item.D20;
            //                itemAdd.D21 = item.D21;
            //                itemAdd.D22 = item.D22;
            //                itemAdd.D23 = item.D23;
            //                itemAdd.D24 = item.D24;
            //                itemAdd.D25 = item.D25;
            //                itemAdd.D26 = item.D26;
            //                itemAdd.D27 = item.D27;
            //                itemAdd.D28 = item.D28;
            //                itemAdd.D29 = item.D29;
            //                itemAdd.D30 = item.D30;
            //                itemAdd.D31 = item.D31;
            //                itemAdd.CreateDate = DateTime.Now;
            //                itemAdd.CreateBy = item.CreateBy;
            //                itemAdd.Rev = rev;
            //                itemAdd.Lrev = rev;
            //                itemAdd.Sebango = item.Sebango;
            //                itemAdd.Row = item.Row;
            //                itemAdd.Ym = item.Ym;
            //                _contextDBSCM.AlSaleForecaseMonths.Add(itemAdd);
            //            }
            //            int upgrade = _contextDBSCM.SaveChanges();
            //            if (upgrade > 0)
            //            {
            //                status = true;
            //            }
            //        }
            //    }
            //    return Ok(new
            //    {
            //        status = status
            //    });
            //}
            //else
            //{
            //    return Ok(new
            //    {
            //        status = false,
            //        error = $"ไม่พบข้อมูล {ym}"
            //    });
            //}

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
            string ym = param.ym;
            string rev = "1";
            string lrev = "1";
            List<AlSaleForecaseMonth> contentPrev = _contextDBSCM.AlSaleForecaseMonths.Where(x => x.Ym == ym && x.Rev == x.Lrev).OrderByDescending(x => x.Rev).ToList();
            if (contentPrev.Count > 0 && contentPrev.FirstOrDefault() != null)
            {
                rev = contentPrev.FirstOrDefault()!.Rev!;
                lrev = contentPrev.FirstOrDefault()!.Rev!;
            }
            List<AlSaleForecaseMonth> listNewRow = param.data;
            foreach (AlSaleForecaseMonth row in listNewRow)
            {
                row.ModelName = "";
                row.Ym = ym;
                row.Rev = rev;
                row.Lrev = lrev;
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

    }

}