using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Threading;

namespace TestSuite.Tests
{
    class AsyncTest : ITest
    {
        string ITest.Title
        {
            get
            {
                return "Async learning test";
            }
        }

        void ITest.Run()
        {
            while (true)
            {
                Console.WriteLine("Please get the command: ");
                var res = Console.ReadLine();
                if(res == "1")
                {
                    LongOperationAsync();
                }
                else if(res == "h")
                {
                    Console.WriteLine("Press '1' for operation...");
                }
                else if (res == "q")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Unnown copmmand :(");
                }
            }           
        }
        private void ShowResult(ConsoleColor clr, string message)
        {
            Console.ForegroundColor = clr; // ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private async Task LongOperationAsync()
        {
            string result;
            result =  await Task.Run(()=> {
                Thread.Sleep(3000);
                return "Operation copmpleted...";
            });
            ShowResult(ConsoleColor.Green, result);
            result = await Task.Run(() => {
                Thread.Sleep(1000);
                return "Post-operation copmpleted...";
            });
            ShowResult(ConsoleColor.Yellow, result);
        }

        private void LongOperation()
        {
            var task = Task.Run(() => {
                Thread.Sleep(3000);
            }
            );
            //task.ContinueWith((t) => {
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine("Operation copmpleted...");
            //    Console.ForegroundColor = ConsoleColor.White;
            //});
            task.ConfigureAwait(true)
                .GetAwaiter()
                .OnCompleted(() => ShowResult(ConsoleColor.Green, string.Empty));
            //using (var client = new WebClient())
            //{
            //    client.DownloadDataCompleted += DownloadDataCompleted;
            //    client.DownloadDataAsync(new Uri("http://www.lanteria.com/"));
            //    //var res = System.Text.Encoding.UTF8.GetString(client.DownloadData("http://www.lanteria.com/"));
            //    //Console.WriteLine($"Result = {res}");
            //}
        }
        //void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        //{
        //    Thread.Sleep(3000);
        //    var res = System.Text.Encoding.UTF8.GetString(e.Result);
        //    Console.WriteLine($"Result = {res}");

        //}
    }
}
