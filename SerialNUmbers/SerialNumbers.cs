using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SerialNumbers
{
    public static class SerialNumbers
    {
        /// <summary>
        /// Create a new serial number
        /// </summary>
        /// <param name="productionSite">The production site</param>
        /// <param name="serialIndex">The serial index</param>
        /// <returns>The create serial number</returns>
        public static string CreateNew(string productionSite, int serialIndex)
        {
            string serialNumber = ProductionSiteToOriginCode(productionSite);

            DateTime now = DateTime.Now;
            ParseDate(now, out string year, out string month, out string day);

            serialNumber += $"{year}{month}{day}";
            serialNumber += serialIndex.ToString("0000");

            return serialNumber;
        }

        /// <summary>
        /// Convert the production site to the relative origin code (2 letter id)
        /// </summary>
        /// <param name="productionSite">The production site</param>
        /// <returns>The 2 letter id origin code</returns>
        private static string ProductionSiteToOriginCode(string productionSite)
        {
            string originCode = "";

            //IEnumerable<RegionInfo> regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            //RegionInfo region = regions.FirstOrDefault(x => x.EnglishName.Contains(productionSite));
            //originCode = region.TwoLetterISORegionName;

            originCode = productionSite;

            return originCode;
        }

        private static string DayToCode(int day)
        {
            string dayCode = "";

            if (day >= 1 && day <= 9)
                dayCode = day.ToString();
            else
                dayCode = Encoding.ASCII.GetString(new byte[] { (byte)(day - 10 + 65) }); // 10 = 'A', 65 = 'A'

            return dayCode;
        }

        /// <summary>
        /// Parse a <see cref="DateTime"/>
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to parse</param>
        /// <param name="year">The parsed year</param>
        /// <param name="month">The parsed month</param>
        /// <param name="day">The parsed day</param>
        private static void ParseDate(DateTime date, out string year, out string month, out string day)
        {
            year = date.Year.ToString().Substring(2);
            month = date.Month.ToString();
            day = DayToCode(date.Day);
        }
    }
}