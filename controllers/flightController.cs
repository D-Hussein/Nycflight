using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nycflight.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nycflight.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class flightController : Controller
    {
        private Dictionary<int, string> monthsByNumber = new Dictionary<int, string>()
            { {1, "Jan" }, {2, "Feb"}, {3, "Mar"},  {4, "Apr"},  {5, "May"},  {6, "Jun"},
                {7, "Jul"},  {8, "Aug"},  {9, "Sep"},  {10, "Oct"},  {11, "Nov"},  {12, "Dec"}};

        #region Flights per month
        //1. GET: api/Nycflight/FlightsPerMonth
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsPerMonth()
        {
            var context = new DB_Context();

            return context.Flights.Select(f => f.month).ToList().GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }

        //2.1. GET: api/Nycflight/FlightsPerMonthForJFK
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsPerMonthForJFK()
        {
            var context = new DB_Context();

            return context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("JFK")).Select(f => f.month).ToList()
                .GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }

        //2.2. GET: api/Nycflight/FlightsPerMonthForEWR
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsPerMonthForEWR()
        {
            var context = new DB_Context();

            return context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("EWR")).Select(f => f.month).ToList()
                .GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }

        //2.3. GET: api/Nycflight/FlightsPerMonthForLGA
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsPerMonthForLGA()
        {
            var context = new DB_Context();

            return context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("LGA")).Select(f => f.month).ToList()
                .GroupBy(m => m).OrderBy(g => g.Key).ToDictionary(g => monthsByNumber[g.Key], g => g.Count());
        }
        #endregion

        #region Top ten destinations
        //3.1. GET: api/Nycflight/FlightsToTopTenDestinations
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsToTopTenDestinations()
        {
            var context = new DB_Context();

            return context.Flights.Select(f => f.dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value);
        }

        //3.2. GET: api/Nycflight/FlightsToTopTenDestinationsFromJFK
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsToTopTenDestinationsFromJFK()
        {
            var context = new DB_Context();

            List<string> topTenDestinations = context.Flights.Select(f => f.dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value).Keys.ToList();

            Dictionary<string, int> flightsToTopTenDestinationsFromJFK = new Dictionary<string, int>();
            foreach (string dest in topTenDestinations)
            {
                flightsToTopTenDestinationsFromJFK.Add(dest, context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("JFK") &&
                    !string.IsNullOrEmpty(f.dest) && f.dest.Equals(dest)).Count());
            }

            return flightsToTopTenDestinationsFromJFK;
        }

        //3.3. GET: api/Nycflight/FlightsToTopTenDestinationsFromEWR
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsToTopTenDestinationsFromEWR()
        {
            var context = new DB_Context();

            List<string> topTenDestinations = context.Flights.Select(f => f.dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value).Keys.ToList();

            Dictionary<string, int> flightsToTopTenDestinationsFromEWR = new Dictionary<string, int>();
            foreach (string dest in topTenDestinations)
            {
                flightsToTopTenDestinationsFromEWR.Add(dest, context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("EWR") &&
                    !string.IsNullOrEmpty(f.dest) && f.dest.Equals(dest)).Count());
            }

            return flightsToTopTenDestinationsFromEWR;
        }

        //3.4. GET: api/Nycflight/FlightsToTopTenDestinationsFromLGA
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsToTopTenDestinationsFromLGA()
        {
            var context = new DB_Context();

            List<string> topTenDestinations = context.Flights.Select(f => f.dest).ToList().GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(val => val.Value).Take(10).ToDictionary(g => g.Key, g => g.Value).Keys.ToList();

            Dictionary<string, int> flightsToTopTenDestinationsFromLGA = new Dictionary<string, int>();
            foreach (string dest in topTenDestinations)
            {
                flightsToTopTenDestinationsFromLGA.Add(dest, context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("LGA") &&
                    !string.IsNullOrEmpty(f.dest) && f.dest.Equals(dest)).Count());
            }

            return flightsToTopTenDestinationsFromLGA;
        }
        #endregion

        //4. GET: api/Nycflight/MeanAirtimeForOrigins
        [HttpGet("[action]")]
        public Dictionary<string, string> MeanAirtimeForOrigins()
        {
            var context = new DB_Context();

            double? averageAirtimeJFK = context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("JFK")).Select(f => f.air_time).ToList().Average();
            double? averageAirtimeEWR = context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("EWR")).Select(f => f.air_time).ToList().Average();
            double? averageAirtimeLGA = context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("LGA")).Select(f => f.air_time).ToList().Average();

            TimeSpan tavgAirtimeJFK = new TimeSpan(), tavgAirtimeEWR = new TimeSpan(), tavgAirtimeLGA = new TimeSpan();

            if (averageAirtimeJFK != null)
                tavgAirtimeJFK = TimeSpan.FromMinutes((double)averageAirtimeJFK);

            if (averageAirtimeEWR != null)
                tavgAirtimeEWR = TimeSpan.FromMinutes((double)averageAirtimeEWR);

            if (averageAirtimeLGA != null)
                tavgAirtimeLGA = TimeSpan.FromMinutes((double)averageAirtimeLGA);

            return new Dictionary<string, string>() { { "JFK", tavgAirtimeJFK.ToString("hh\\:mm\\:ss") },
                { "EWR", tavgAirtimeEWR.ToString("hh\\:mm\\:ss") },
                { "LGA", tavgAirtimeLGA.ToString("hh\\:mm\\:ss") } };
        }

        //5. GET: api/Nycflight/WeatherObservationsForOrigins
        [HttpGet("[action]")]
        public Dictionary<string, int> WeatherObservationsForOrigins()
        {
            var context = new DB_Context();

            return context.Weather.Select(w => w.origin).ToList().GroupBy(o => o).ToDictionary(g => g.Key, g => g.Count());
        }

        //6.1. GET: api/Nycflight/TemperatureInCelsiusForEWR
        [HttpGet("[action]")]
        public Dictionary<DateTime, float> TemperatureInCelsiusForEWR()
        {
            var context = new DB_Context();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.origin) && w.origin.Equals("EWR") && w.temp >= 0)
                .Select(w => new { w.time_hour, w.temp }).ToDictionary(g => g.time_hour, g => (g.temp - 32) * 5 / 9);
        }

        //6.2. GET: api/Nycflight/TemperatureInCelsiusForLGA
        [HttpGet("[action]")]
        public Dictionary<DateTime, float> TemperatureInCelsiusForLGA()
        {
            var context = new DB_Context();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.origin) && w.origin.Equals("LGA"))
                .Select(w => new { w.time_hour, w.temp }).ToDictionary(g => g.time_hour, g => (g.temp - 32) * 5 / 9);
        }

        //7. GET: api/Nycflight/TemperatureInCelsiusForJFK
        [HttpGet("[action]")]
        public Dictionary<DateTime, float> TemperatureInCelsiusForJFK()
        {
            var context = new DB_Context();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.origin) && w.origin.Equals("JFK"))
                .Select(w => new { w.time_hour, w.temp }).ToDictionary(g => g.time_hour, g => (g.temp - 32) * 5 / 9);
        }

        //8. GET: api/Nycflight/DailyMeanTempInCelsiusForJFK
        [HttpGet("[action]")]
        public Dictionary<string, float> DailyMeanTempInCelsiusForJFK()
        {
            var context = new DB_Context();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.origin) && w.origin.Equals("JFK"))
                .GroupBy(g => g.time_hour.Date.ToShortDateString()).ToDictionary(p => p.Key, p => (p.Average(g => g.temp) - 32) * 5 / 9);
        }

        //9.1. GET: api/Nycflight/DailyMeanTempInCelsiusForEWR
        [HttpGet("[action]")]
        public Dictionary<string, float> DailyMeanTempInCelsiusForEWR()
        {
            var context = new DB_Context();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.origin) && w.origin.Equals("EWR") && w.temp != null)
                .GroupBy(g => g.time_hour.Date.ToShortDateString()).ToDictionary(p => p.Key, p => (p.Average(g => g.temp) - 32) * 5 / 9);
        }

        //9.2. GET: api/Nycflight/DailyMeanTempInCelsiusForLGA
        [HttpGet("[action]")]
        public Dictionary<string, float> DailyMeanTempInCelsiusForLGA()
        {
            var context = new DB_Context();

            return context.Weather.Where(w => !string.IsNullOrEmpty(w.origin) && w.origin.Equals("LGA"))
                .GroupBy(g => g.time_hour.Date.ToShortDateString()).ToDictionary(p => p.Key, p => (p.Average(g => g.temp) - 32) * 5 / 9);
        }

        //10.1. GET: api/Nycflight/MeanDepartureAndArrivalDelayForJFK
        [HttpGet("[action]")]
        public Dictionary<string, string> MeanDepartureAndArrivalDelayForJFK()
        {
            var context = new DB_Context();

            double? averageDepDelayJFK = context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("JFK")).Select(f => f.dep_delay).ToList().Average();
            double? averageArrDelayJFK = context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("JFK")).Select(f => f.arr_delay).ToList().Average();

            TimeSpan tavgDepDelayJFK = new TimeSpan();
            TimeSpan tavgArrDelayJFK = new TimeSpan();

            if (averageDepDelayJFK != null)
                tavgDepDelayJFK = TimeSpan.FromMinutes((double)averageDepDelayJFK);

            if (averageArrDelayJFK != null)
                tavgArrDelayJFK = TimeSpan.FromMinutes((double)averageArrDelayJFK);

            return new Dictionary<string, string>() { { tavgDepDelayJFK.TotalSeconds >= 0 ? tavgDepDelayJFK.ToString("hh\\:mm\\:ss") : "-" + tavgDepDelayJFK.ToString("hh\\:mm\\:ss"),
                tavgArrDelayJFK.TotalSeconds >= 0 ? tavgArrDelayJFK.ToString("hh\\:mm\\:ss") : "-" + tavgArrDelayJFK.ToString("hh\\:mm\\:ss") }  };
        }

        //10.2. GET: api/Nycflight/MeanDepartureAndArrivalDelayForEWR
        [HttpGet("[action]")]
        public Dictionary<string, string> MeanDepartureAndArrivalDelayForEWR()
        {
            var context = new DB_Context();

            double? averageDepDelayEWR = context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("EWR")).Select(f => f.dep_delay).ToList().Average();
            double? averageArrDelayEWR = context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("EWR")).Select(f => f.arr_delay).ToList().Average();

            TimeSpan tavgDepDelayEWR = new TimeSpan();
            TimeSpan tavgArrDelayEWR = new TimeSpan();

            if (averageDepDelayEWR != null)
                tavgDepDelayEWR = TimeSpan.FromMinutes((double)averageDepDelayEWR);

            if (averageArrDelayEWR != null)
                tavgArrDelayEWR = TimeSpan.FromMinutes((double)averageArrDelayEWR);

            return new Dictionary<string, string>() { { tavgDepDelayEWR.TotalSeconds >= 0 ? tavgDepDelayEWR.ToString("hh\\:mm\\:ss") : "-" + tavgDepDelayEWR.ToString("hh\\:mm\\:ss"),
                tavgArrDelayEWR.TotalSeconds >= 0 ? tavgArrDelayEWR.ToString("hh\\:mm\\:ss") : "-" + tavgArrDelayEWR.ToString("hh\\:mm\\:ss") }  };
        }

        //10.3. GET: api/Nycflight/MeanDepartureAndArrivalDelayForLGA
        [HttpGet("[action]")]
        public Dictionary<string, string> MeanDepartureAndArrivalDelayForLGA()
        {
            var context = new DB_Context();

            double? averageDepDelayLGA = context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("LGA")).Select(f => f.dep_delay).ToList().Average();
            double? averageArrDelayLGA = context.Flights.Where(f => !string.IsNullOrEmpty(f.origin) && f.origin.Equals("LGA")).Select(f => f.arr_delay).ToList().Average();

            TimeSpan tavgDepDelayLGA = new TimeSpan();
            TimeSpan tavgArrDelayLGA = new TimeSpan();

            if (averageDepDelayLGA != null)
                tavgDepDelayLGA = TimeSpan.FromMinutes((double)averageDepDelayLGA);

            if (averageArrDelayLGA != null)
                tavgArrDelayLGA = TimeSpan.FromMinutes((double)averageArrDelayLGA);

            return new Dictionary<string, string>() { { tavgDepDelayLGA.TotalSeconds >= 0 ? tavgDepDelayLGA.ToString("hh\\:mm\\:ss") : "-" + tavgDepDelayLGA.ToString("hh\\:mm\\:ss"),
                tavgArrDelayLGA.TotalSeconds >= 0 ? tavgArrDelayLGA.ToString("hh\\:mm\\:ss") : "-" + tavgArrDelayLGA.ToString("hh\\:mm\\:ss") }  };
        }

        //11. GET: api/Nycflight/ManufacturersMoreThanTwoHundredPlanes
        [HttpGet("[action]")]
        public Dictionary<string, int> ManufacturersMoreThanTwoHundredPlanes()
        {
            var context = new DB_Context();

            return context.Planes.Select(p => p.manufacturer).ToList()
                .GroupBy(m => m).Where(m => m.Count() >= 200).ToDictionary(g => g.Key, g => g.Count());
        }

        //12. GET: api/Nycflight/FlightsManufacturersMoreThanTwoHundredPlanes
        [HttpGet("[action]")]
        public Dictionary<string, int> FlightsManufacturersMoreThanTwoHundredPlanes()
        {
            var context = new DB_Context();

            Dictionary<string, int> flightsManufacturersMoreThanTwoHundredPlanes = new Dictionary<string, int>();
            Dictionary<string, int> manufacturersMoreThanTwoHundredPlanes = ManufacturersMoreThanTwoHundredPlanes();

            Dictionary<string, int> countByTailNum = context.Flights.Select(f => f.tailnum).ToList().GroupBy(m => m).ToDictionary(g => g.Key, g => g.Count());

            foreach (KeyValuePair<string, int> pair in manufacturersMoreThanTwoHundredPlanes)
            {
                List<string> tailNums = context.Planes.Where(p => p.manufacturer.Equals(pair.Key)).Select(p => p.tailnum).ToList();
                int flights = 0;
                int flightsForTailNum = 0;

                foreach (string tailNum in tailNums)
                {
                    countByTailNum.TryGetValue(tailNum, out flightsForTailNum);
                    flights += flightsForTailNum;
                }

                flightsManufacturersMoreThanTwoHundredPlanes.Add(pair.Key, flights);
            }

            return flightsManufacturersMoreThanTwoHundredPlanes;
        }

        //13. GET: api/Nycflight/PlanesforAirbus
        [HttpGet("[action]")]
        public Dictionary<string, int> PlanesforAirbus()
        {
            var context = new DB_Context();

            return context.Planes.Where(p => !string.IsNullOrEmpty(p.manufacturer) && p.manufacturer.Contains("AIRBUS"))
                .Select(p => p.manufacturer).GroupBy(g => g).ToDictionary(g => g.Key, g => context.Planes.Where
                (p => p.manufacturer.Equals(g.Key)).Select(p => p.tailnum).Count());
        }

    }
}