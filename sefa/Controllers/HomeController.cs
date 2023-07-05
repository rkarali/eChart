using Newtonsoft.Json;
using sefa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ASPNET_MVC_ChartsDemo.Controllers
{
    public class HomeController : Controller
    {
        private saastek_dbEntities _db = new saastek_dbEntities();
        public ActionResult Index()
        {
            try
            {
                var list1 = _db.CAY.Where(t => t.U1_AKTIF.HasValue && t.U1_AKTIF.Value > 0).OrderBy(t => t.TARIH).ToList();
                var list2 = _db.ARISU.Where(t => t.U1_AKTIF.HasValue && t.U1_AKTIF.Value > 0).OrderBy(t => t.TARIH).ToList();
                var list3 = _db.ZALA.Where(t => t.U1_AKTIF.HasValue && t.U1_AKTIF.Value > 0).OrderBy(t => t.TARIH).ToList();

                List<string> tarihler = new List<string>();

                List<double> sayilarCAY = new List<double>();
                List<double> sayilarARISU = new List<double>();
                List<double> sayilarZALA = new List<double>();


                foreach (var item in list1)
                {
                    if (!tarihler.Any(t => t == item.TARIH.Value.ToString("dd-MMM-yyy HH" + ":00")))
                    //if (!tarihler.Any(t=> t == item.TARIH.Value.ToString("dd-MMM-yyy HH:mm")))
                    {
                        tarihler.Add(item.TARIH.Value.ToString("dd-MMM-yyy HH" + ":00"));
                        //tarihler.Add(item.TARIH.Value.ToString("dd-MMM-yyy HH:mm"));
                    }

                    sayilarCAY.Add(Convert.ToDouble(item.U1_AKTIF));
                }

                foreach (var item in list2)
                {
                    if (!tarihler.Any(t => t == item.TARIH.Value.ToString("dd-MMM-yyy HH" + ":00")))
                    //if (!tarihler.Any(t => t == item.TARIH.Value.ToString("dd-MMM-yyy HH:mm")))
                    {
                        tarihler.Add(item.TARIH.Value.ToString("dd-MMM-yyy HH" + ":00"));
                        //tarihler.Add(item.TARIH.Value.ToString("dd-MMM-yyy HH:mm"));
                    }

                    sayilarARISU.Add(Convert.ToDouble(item.U1_AKTIF));
                }

                foreach (var item in list3)
                {
                    if (!tarihler.Any(t => t == item.TARIH.Value.ToString("dd-MMM-yyy HH" + ":00")))
                    //if (!tarihler.Any(t => t == item.TARIH.Value.ToString("dd-MMM-yyy HH:mm")))
                    {
                        tarihler.Add(item.TARIH.Value.ToString("dd-MMM-yyy HH" + ":00"));
                        //tarihler.Add(item.TARIH.Value.ToString("dd-MMM-yyy HH:mm"));
                    }

                    sayilarZALA.Add(Convert.ToDouble(item.U1_AKTIF));
                }

                tarihler = tarihler.OrderBy(t => t).ToList();

                //foreach (var item in tarihler)
                //{
                //    sayilarCAY.Add(list1.Any(t => t.TARIH.Value.ToString("dd-MMM-yyy HH:mm") == item) ? Convert.ToDouble(list1.Where(t => t.TARIH.Value.ToString("dd-MMM-yyy HH:mm") == item).First().U1_AKTIF) : (double)0);
                //    sayilarARISU.Add(list2.Any(t => t.TARIH.Value.ToString("dd-MMM-yyy HH:mm") == item) ? Convert.ToDouble(list2.Where(t => t.TARIH.Value.ToString("dd-MMM-yyy HH:mm") == item).First().U1_AKTIF) : (double)0);
                //    sayilarZALA.Add(list3.Any(t => t.TARIH.Value.ToString("dd-MMM-yyy HH:mm") == item) ? Convert.ToDouble(list3.Where(t => t.TARIH.Value.ToString("dd-MMM-yyy HH:mm") == item).First().U1_AKTIF) : (double)0);
                //}


                ViewBag.DataPointsCAY = JsonConvert.SerializeObject(sayilarCAY);
                ViewBag.DataPointsARISU = JsonConvert.SerializeObject(sayilarARISU);
                ViewBag.DataPointsZALA = JsonConvert.SerializeObject(sayilarZALA);

                ViewBag.DataPointsTarihler = tarihler;

                return View();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return View("Error");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return View("Error");
            }
        }
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
    }
}
