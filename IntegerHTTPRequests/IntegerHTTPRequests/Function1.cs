using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Microsoft.Extensions.Options;

namespace FunctionApp1Practice
{
    public class Numbers //I am first trying to create a functioning example of just two properties sent via a JSON request.
    {
        public string number1 { get; set; }
        public string number2 { get; set; }
    }

    public static class SumOfIntegers
    {
        [FunctionName("SumOfIntegers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var deserializedData = JsonConvert.DeserializeObject<Numbers>(requestBody);

            log.LogInformation($"{requestBody}");

            int Num1 = int.Parse(deserializedData.number1);
            int Num2 = int.Parse(deserializedData.number2);

            var list = new List<int>();
            list.Add(Num1);
            list.Add(Num2);
            int sum = list.Sum();
            string stringSum = sum.ToString();

            log.LogInformation($"{stringSum}");

            string responseMessage = string.IsNullOrEmpty(requestBody)
                ? "The requested queries are null"
                : $"The sum of the JSON request numbers is {stringSum}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }

}
