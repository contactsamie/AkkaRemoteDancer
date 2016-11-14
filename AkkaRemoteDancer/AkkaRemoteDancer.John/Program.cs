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
            {
                var maryStoreAddress = ConfigurationManager.AppSettings["MaryStoreAddress"];
                var storetoreActorSelection = actorSystem.ActorSelection(maryStoreAddress);
                var count = 10000;
                var fails = 0;
                var Success = 0;
              
                System.Threading.Thread.Sleep(10000);

                Parallel.For(0,count, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async x =>
                {
                    var sw= new Stopwatch();
                    sw.Start();

                    try
                    {

                        Console.WriteLine("trying to reserve " + x);
                        //await  storetoreActorSelection.ResolveOne(TimeSpan.FromSeconds(3));
                        var reserveResult =
                            await
                                storetoreActorSelection.Ask<ReserveCompletedMessage>(new ReserveMessage(x),
                                    TimeSpan.FromSeconds(3));
                        if (string.IsNullOrEmpty(reserveResult?.ReservationId))
                        {
                            throw new Exception("Unable to reserve" + x);
                        }
                        Console.WriteLine("reserved successfully" + x);
                      
                     
                    }
                    catch (Exception e)
                    {
                   Console.WriteLine(sw.ElapsedMilliseconds);
                      //  Console.WriteLine(e);
                    }

                });
              

              

            }
            Console.WriteLine("All done !!!!!");
            Console.ReadLine();
        }
    }
}
