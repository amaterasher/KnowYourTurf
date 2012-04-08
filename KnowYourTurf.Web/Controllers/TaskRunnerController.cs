﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using System.Linq;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Services.EmailHandlers;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class TaskRunnerController : Controller
    {
        private readonly IEmailTemplateService _emailService;
        private readonly IRepository _repository;

        public TaskRunnerController( IEmailTemplateService emailService)
        {
            _emailService = emailService;
            _repository = new Repository(RepoConfig.NoFiltersSpecialInterceptor);
        }

        public ActionResult temp(ViewModel input)
        {
            var companies = _repository.FindAll<Company>();
            var webClient = new WebClient();
            var jss = new JavaScriptSerializer();


            var d = 21;
            while (d > 0)
            {
                companies.Each(x => loadLastWeeksWeatherObject(jss, webClient, x, DateTime.Now.Date.AddDays(-d)));
                d--;
                _repository.UnitOfWork.Commit();
                Thread.Sleep(70000);
            }
            return null;
        }


        public ActionResult GetWeather(ViewModel input)
        {
            var companies = _repository.FindAll<Company>();
            var webClient = new WebClient();
            var jss = new JavaScriptSerializer();

            companies.Each(x =>
                               {
                                   loadWeatherObject(jss, webClient, x);
//                                   loadLastWeeksWeatherObject(jss, webClient, x);
                               });
            _repository.UnitOfWork.Commit();
            return null;
        }

        private void loadWeatherObject(JavaScriptSerializer jss, WebClient webClient, Company item)
        {
            var url = "http://api.wunderground.com/api/8c25a57f987344bd/yesterday/q/" + item.ZipCode + ".json";
            var weather = new Weather
            {
                CompanyId = item.EntityId,
                Date = DateTime.Now.Date.AddDays(-1),
            };
            loadWeather(jss,webClient,weather,url);
        }
        private void loadLastWeeksWeatherObject(JavaScriptSerializer jss, WebClient webClient, Company item, DateTime date)
        {
            var url = "http://api.wunderground.com/api/8c25a57f987344bd/history_" + date.ToString("yyyyMMd") + "/q/" + item.ZipCode + ".json";
            var weather = _repository.Query<Weather>(x => x.Date == date).FirstOrDefault() ??
                          new Weather { CompanyId = item.EntityId, Date = date };
            loadWeather(jss, webClient, weather, url);
        }
//        private void loadLastWeeksWeatherObject(JavaScriptSerializer jss, WebClient webClient, Company item)
//        {
//            var url = "http://api.wunderground.com/api/8c25a57f987344bd/history_" + DateTime.Now.AddDays(-7).ToString("yyyyMMd") + "/q/" + item.ZipCode + ".json";
//            var weather = _repository.Query<Weather>(x => x.Date == DateTime.Now.Date.AddDays(-8)).FirstOrDefault() ??
//                          new Weather { CompanyId = item.EntityId, Date = DateTime.Now.Date.AddDays(-8) };
//            loadWeather(jss, webClient, weather, url);
//        }

        private void loadWeather(JavaScriptSerializer jss, WebClient webClient, Weather weather, string url)
        {
            var result = webClient.DownloadString(url);
            if (result.IsEmpty()) return;
            var companyWeatherInfoDto = jss.Deserialize<CompanyWeatherInfoDto>(result);
            if (companyWeatherInfoDto == null || companyWeatherInfoDto.History == null || companyWeatherInfoDto.History.DailySummary == null) return;
            var dewPoint = 0d;
            var maxTemp = 0d;
            var minTemp = 0d;
            var precip = 0d;
            var maxWindGust = 0d;
            var humidity = 0d;
            var dailySummary = companyWeatherInfoDto.History.DailySummary.FirstOrDefault();
            Double.TryParse(dailySummary.maxdewpti, out dewPoint);
            Double.TryParse(dailySummary.maxtempi, out maxTemp);
            Double.TryParse(dailySummary.mintempi, out minTemp);
            Double.TryParse(dailySummary.rain, out precip);
            Double.TryParse(dailySummary.maxwspdi, out maxWindGust);
            Double.TryParse(dailySummary.maxhumidity, out humidity);
            weather.DewPoint = dewPoint;
            weather.HighTemperature = maxTemp;
            weather.LowTemperature = minTemp;
            weather.RainPrecipitation = precip;
            weather.WindSpeed = maxWindGust;
            weather.Humidity = humidity;
            _repository.Save(weather);
        }

        // this is obviously shite. plese rewrite the whole thing if people start using it.
        public ActionResult ProcessEmail(ViewModel input)
        {
            var notification = new Notification { Success = true };
            var emailJobs = _repository.FindAll<EmailJob>();
            emailJobs.Each(x =>
                               {
                                   if (x.Status == Status.Active.ToString() && (
                                       x.Frequency == EmailFrequency.Daily.ToString()
                                       || x.Frequency == EmailFrequency.Once.ToString()
                                       || (x.Frequency == EmailFrequency.Weekly.ToString() && DateTime.Now.Day == 1)))
                                   {
                                       var emailTemplateHandler =
                                           ObjectFactory.Container.GetInstance<IEmailTemplateHandler>(
                                               x.EmailJobType.Name + "Handler");
                                       try
                                       {
                                           x.Subscribers.Each(s =>
                                                                  {
                                                                      var model = emailTemplateHandler.CreateModel(x, s);
                                                                      _emailService.SendSingleEmail(model);
                                                                  });
                                       }
                                       catch (Exception ex)
                                       {
                                           notification.Success = false;
                                           notification.Message = ex.Message;
                                       }
                                   }
                               });

            return Json(notification, JsonRequestBehavior.AllowGet);
        }
    }

    public class CompanyWeatherInfoDto
    {
        public WeatherHistory History { get; set; }
    }

    public class WeatherHistory
    {
        public IEnumerable<DailySummary> DailySummary { get; set; }
    }

    public class DailySummary
    {
        public string rain { get; set; }
        public string maxtempi { get; set; }
        public string mintempi { get; set; }
        public string maxhumidity { get; set; }
        public string maxdewpti { get; set; }
        public string maxwspdi { get; set; }
    }

}