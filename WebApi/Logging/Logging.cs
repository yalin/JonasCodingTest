using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using WebApi.Models;

namespace WebApi.Logging
{
    public class Logging
    {
        /// <summary>
        /// I decided to add logging into csv
        /// Proper way is to save into db
        /// But csv is the closer view to db more than saving into txt file 
        /// </summary>
        /// <param name="webApiError"></param>
        public void InsertLog(WebApiError webApiError)
        {
            // Create a list so can be append to csv
            var records = new List<WebApiError>() { webApiError };

            // Append to the file.
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Not to write the header again.
                HasHeaderRecord = false,
            };

            using (var stream = File.Open("log.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(records);
            }
        }
    }
}