using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows.Forms;
using LawnMower.Shared.Helper;
using LawnMower.Shared.Model;
using RestBus.Client;
using RestBus.RabbitMQ;
using RestBus.RabbitMQ.Client;

namespace LawnMower.ConsoleApp
{
    class Program
    {
        private static RestBusClient client;
        static void Main(string[] args)
        {

            var amqpUrl = "amqp://localhost:5672"; //AMQP URL for RabbitMQ server
            var serviceName = "LawnMover"; //The unique identifier for the target service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);

            client = new RestBusClient(msgMapper);

            RequestOptions requestOptions = null;
             Initialize();
             AcceptCommands();
            

         //   var response = SendMessage(client, requestOptions).Result;


            ////Display response
            ////Console.WriteLine(response.StatusCode);
            //var message = "";
            //HttpError error = null;
            //if (response.StatusCode == HttpStatusCode.InternalServerError)
            //    error = response.Content.ReadAsAsync<HttpError>().Result;
            //else
            //    message = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            client.Dispose();
            Console.ReadKey();
        }

        private static void AcceptCommands()
        {
            Console.WriteLine("You can Arrow Keys to opperate the Mower. You can veriy the logs. For Each move the log will be upated");
            Console.WriteLine("use `S` to Start/Stop Toggle. If the engine is off other Instructions will be ignored");
            Console.WriteLine($"use {Keys.Left} key to Turn Left. This will take 10 Seconds");
            Console.WriteLine($"use {Keys.Right} key to Turn Right. This will take 10 Seconds");
            Console.WriteLine($"use {Keys.Up} key to Move forward. This will take 10 Seconds");
            Console.WriteLine($"use {Keys.Down} key to Mow. This will take 15 Seconds");
            Console.WriteLine($"use {Keys.Return} to Stop. To Terminate");
            ConsoleKeyInfo keyinfo = new ConsoleKeyInfo();
            while (keyinfo.KeyChar != (char)Keys.Return)
            {
                keyinfo = Console.ReadKey();
                Console.Write(keyinfo.Key);

                switch (keyinfo.Key)
                {
                    case ConsoleKey.RightArrow:
                        TurnRight();
                        break;
                    case ConsoleKey.LeftArrow:
                        TurnLeft().Wait();
                        break;
                    case ConsoleKey.UpArrow:
                        MoveForward();
                        break;
                    case ConsoleKey.DownArrow:
                        Mow();
                        break;
                }
            };


            Console.WriteLine("Thank you for using Smart Lawn Mower");
        }

        private static void Mow()
        {
            throw new NotImplementedException();
        }

        private static void MoveForward()
        {
            throw new NotImplementedException();
        }

        private static async Task TurnLeft()
        {

              var directionHelper = new DirectionHelper();
            var directionUri = new Uri(GlobalUrlConstants.LawnMowerApiBase + GlobalUrlConstants.Direction);
            var currentDirection = await GetMessage<Direction>(directionUri, null);
                var newDirection =  directionHelper.GetNextDirectionFromSide(Side.Left.ToString(), currentDirection);
            var direction = new FormUrlEncodedContent(new Dictionary<string, string> { { "direction", currentDirection.ToString() } });
            
              var result = PutMessage(client, GlobalUrlConstants.Direction, direction).Result;
        }

        private static void TurnRight()
        {
            throw new NotImplementedException();
        }

        //private static async Task<HttpResponseMessage> SendMessage(RestBusClient client, RequestOptions requestOptions)
        //{
        //    //Send Request
        //                              //return await client.GetAsync(uri, requestOptions);
        //                              // var values = new Dictionary<string, string> { { "direction", "North" } };
        //    var values = new Dictionary<string, string> { { "direction", "North" } };
        //    //   var values = new Dictionary<string, string> { { "direction", "North" } };
        //    //   var values = new Dictionary<string, string> { { "direction", "North" } };
        //    var content = new FormUrlEncodedContent(values);
        //    return await client.PutAsync(uri, content);

        //    //return Ok("test");
        //}
        private static async Task<HttpResponseMessage> PutMessage(RestBusClient client, string uri, HttpContent content)
        {

            //  client.BaseAddress = new Uri(GlobalUrlConstants.LawnMowerApiBase);
            return await client.PutAsync(uri, content);
               //  .ContinueWith((post) => post.Result.EnsureSuccessStatusCode());

        }
        private static void Initialize()
        {
            Console.WriteLine("Welcome to Smat Lawn Mower Setup");
            Console.WriteLine("=================================");
            Console.WriteLine("We need to configure the Lawnmower before we use. Please enter the follwowing parameters. \n\n All the parameters should be non decimal integers");
            var lawnMowerSetupParams = new LawnMowerSetupParams
            {
                Length = GetNumber("Length"),
                Width = GetNumber("Width"),
                LawnX = GetNumber("Mower X Position. The initial X postiion of mower"),
                LawnY = GetNumber("Mower Y Postion. The initial Y postiion of mower")
            };
            var uri = new Uri(GlobalUrlConstants.LawnMowerApiBase + GlobalUrlConstants.Lawn);
            var result = PostMessage<LawnMowerSetupParams>(uri, lawnMowerSetupParams).Result;


            Console.WriteLine("Great Now we are ready to use. Please follow the instructions");
        }

        private static async Task<HttpResponseMessage> PostMessage<T>(Uri uri, T content) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                //   client.BaseAddress = new Uri(GlobalConstants.LawnMowerApiBaseUri);

                return await client.PostAsJsonAsync<T>(uri, content)
                     .ContinueWith((post) => post.Result.EnsureSuccessStatusCode());
            }
        }
       
        private static async Task<T> GetMessage<T>(Uri uri, RequestOptions requestOptions)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(GlobalUrlConstants.LawnMowerApiBase);
                var reponse = await client.GetAsync(uri, requestOptions)
                     .ContinueWith((get) => get.Result.EnsureSuccessStatusCode());

                return reponse.Content.ReadAsAsync<T>().Result;
            }


        }
        private static int GetNumber(string nameOfTheInput)
        {
            int number;
            Console.WriteLine($"Enter the {nameOfTheInput} of the Lawn");
            do
            {
                bool isValidInteger = int.TryParse(Console.ReadLine(), out number);
                if (!isValidInteger)
                {
                    Console.WriteLine("Enter a valid number");
                }
                if (number < 0)
                {
                    Console.WriteLine("Enter a valid positive number");
                }
            } while (number <= 0);

            return number;
        }


    }
}
