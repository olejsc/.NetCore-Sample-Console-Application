using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Transport.Types;

namespace Transport
{
    class Program
    {

        static void Main (string[] args)
        {

            IVehicleFactory factory = new BusFactory();
            ABus[] busses = factory.CreateVehicles(2);
            Run(busses);
            Console.ReadLine();
        }

        private static void Run (ABus[] busses)
        {


            List<int> routes = new List<int>{1,2,3,4,7,10,14};
            int i = 0;
            foreach(ABus bus in busses)
            {
                Thread.Sleep(200);
                i++;
                BusTaskScheduler AntecedentBusTaskScheduler = new BusTaskScheduler("busThread"+i);
                // antecedent task settings ("the bus")
                TaskContinuationOptions AntecedentBusTaskContinuationOptions = TaskContinuationOptions.None;
                using var cts = new CancellationTokenSource();
                CancellationToken cancellationToken = cts.Token;
                TaskFactory AntecedentBusTaskFactory = new TaskFactory(cancellationToken,
                    TaskCreationOptions.LongRunning,
                    TaskContinuationOptions.AttachedToParent,
                    AntecedentBusTaskScheduler);
                // initialize a new "bus" thread.
                Task AntecedentBusTask = AntecedentBusTaskFactory.StartNew(
                ()=>
                    {
                        bus.Route = new LinkedList<int>(routes);
                        bus.Execute(AntecedentBusTaskScheduler);
                    }, cancellationToken,TaskCreationOptions.LongRunning,AntecedentBusTaskScheduler
                );
            }

        }



    }
}
