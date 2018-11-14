// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. 

using Microsoft.Azure.Devices;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

namespace IoTWorkbench
{
    public class DeviceObject
    {
        public string topic;
    }

    public class ShakeShakeException : Exception
    {
        public ShakeShakeException(string message)
            : base($"ShakeShakeError:{message}")
        {
        }
    }

    public static class ShakeShake
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("IoTHubShakeShake")]
        public static void Run([IoTHubTrigger("%eventHubConnectionPath%", Connection = "eventHubConnectionString")]EventData message, ILogger log)
        {
            // Send back to device
            string deviceId = "demo1";

            log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");

            string myEventHubMessage = Encoding.UTF8.GetString(message.Body.Array);
            // Get the hash tag
            string hashTag = string.Empty;
            try
            {
                DeviceObject deviceObject = Newtonsoft.Json.JsonConvert.DeserializeObject<DeviceObject>(myEventHubMessage);
                if (String.IsNullOrEmpty(deviceObject.topic))
                {
                    // No hash tag or this is a heartbeat package
                    return;
                }
                hashTag = deviceObject.topic;
                log.LogInformation($"Topic: {hashTag}");
            }
            catch (Exception ex)
            {
                throw new ShakeShakeException($"Failed to deserialize message:{myEventHubMessage}. Error detail: {ex.Message}");
            }

            // Retrieve the tweet according to the given hash tag
            string twitterMessage = string.Empty;
            try
            {
                string tweet = string.Empty;
                string url = "https://api.twitter.com/1.1/search/tweets.json" + "?count=3&q=%23" + HttpUtility.UrlEncode(hashTag);
                string authHeader = "Bearer " + "AAAAAAAAAAAAAAAAAAAAAGVU0AAAAAAAucpxA9aXc2TO6rNMnTcVit1P3YM%3DrQpyFeQ6LOwyvy7cqW5djhLPnFfjEK8H3hA1qfGDh93JRbI1le";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("Authorization", authHeader);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

                string objText = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        objText = reader.ReadToEnd();
                    }
                }

                try
                {
                    JObject o = JObject.Parse(objText);
                    string name = o["statuses"][0]["user"]["name"].ToString();
                    string txt = o["statuses"][0]["text"].ToString();

                    twitterMessage = $"@{name}:\n {txt}";
                }
                catch
                {
                    twitterMessage = "No new tweet.";
                }
            }
            catch (Exception ex)
            {
                throw new ShakeShakeException($"Failed to call twitter API: {ex.Message}");
            }
            log.LogInformation($"Twitter: {twitterMessage}");

            try
            {
                string connectionString = System.Environment.GetEnvironmentVariable("iotHubConnectionString");
                using (ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(connectionString))
                {
                    Message commandMessage = new Message(Encoding.UTF8.GetBytes(twitterMessage));
                    serviceClient.SendAsync(deviceId, commandMessage).Wait();
                }
            }
            catch (Exception ex)
            {
                throw new ShakeShakeException($"Failed to send C2D message: {ex.Message}");
            }

        }
    }
}