
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace bme280_add_function
{
    public static class AddData
    {
        [FunctionName("AddData")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string temperature = req.Query["t"];
            string pressure = req.Query["p"];
            string hum = req.Query["h"];

            if (string.IsNullOrEmpty(temperature) || string.IsNullOrEmpty(pressure) || string.IsNullOrEmpty(hum))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var config = new ConfigurationBuilder()
             .SetBasePath(context.FunctionAppDirectory)
             .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();

            var str = config["ConnectionStrings:MySqlConnection"];
            using (MySqlConnection conn = new MySqlConnection(str))
            {
                conn.Open();

                var text = "INSERT INTO Bme280 (temperature, pressure, hum, Created) " +
                        "VALUES(@temperature, @pressure, @hum, NOW())";

                using (MySqlCommand cmd = new MySqlCommand(text, conn))
                {
                    cmd.Parameters.AddWithValue("@temperature", temperature);
                    cmd.Parameters.AddWithValue("@pressure", pressure);
                    cmd.Parameters.AddWithValue("@hum", hum);
                    // Execute the command and log the # rows affected.
                    var rows = await cmd.ExecuteNonQueryAsync();
                    log.LogInformation($"{rows} rows were updated");
                }
            }

            return null;
        }
    }
}
