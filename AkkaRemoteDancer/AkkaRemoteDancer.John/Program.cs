using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Util.Internal;
using AkkaRemoteDancer.MyMessages;
using NLog;

namespace AkkaRemoteDancer.John
{
    public class Program
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("John");
            Stopwatch sw1;
            int fails;
            int success;
            {
                var maryStoreAddress = ConfigurationManager.AppSettings["MaryStoreAddress"];
                var storetoreActorSelection = actorSystem.ActorSelection(maryStoreAddress);
                var count = 10000;
                 fails = 0;
                 success = 0;
              
                System.Threading.Thread.Sleep(5000);
                 sw1 = new Stopwatch();
                sw1.Start();

                var storetoreActorIActorRef=   storetoreActorSelection.ResolveOne(TimeSpan.FromSeconds(3)).Result;


                Parallel.For(0,count, new ParallelOptions { MaxDegreeOfParallelism = 3 }, async x =>
                {
                    var sw= new Stopwatch();
                    sw.Start();
                    try
                    {
                        Log.Debug("trying to reserve " + x);
                        //await  storetoreActorSelection.ResolveOne(TimeSpan.FromSeconds(3));
                        var reserveResult =await storetoreActorSelection.Ask<ReserveCompletedMessage>(new ReserveMessage(x),TimeSpan.FromSeconds(5));
                        if (string.IsNullOrEmpty(reserveResult?.ReservationId))
                        {
                            throw new Exception("Unable to reserve" + x);
                        }
                        Log.Debug("reserved successfully" + x);
                    }
                    catch (Exception e)
                    {
                        fails++;
                        Console.ForegroundColor= ConsoleColor.DarkRed;
                        Console.WriteLine( "Failed , took  "+sw.ElapsedMilliseconds+" ms");
                        Console.ResetColor();
                      //  Console.WriteLine(e);
                    }
                  
                });

            }
            sw1.Stop();
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("All done !!!!! in "+ sw1.ElapsedMilliseconds+" ms Failures : "+ fails+"  - Successes : "+ success);
            Console.ReadLine();
        }
    }
}
