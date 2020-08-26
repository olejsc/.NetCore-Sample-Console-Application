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
            ABus[] busses = factory.CreateVehicles(3);
            Run(busses);
            Console.ReadLine();
        }

        private static void Run (ABus[] busses)
        {

            // antecedent task settings ("the bus")
            BusTaskScheduler AntecedentBusTaskScheduler = new BusTaskScheduler();
            TaskContinuationOptions AntecedentBusTaskContinuationOptions = TaskContinuationOptions.None;
            using var cts = new CancellationTokenSource();
            CancellationToken cancellationToken = cts.Token;
            TaskFactory AntecedentBusTaskFactory = new TaskFactory(cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskContinuationOptions.AttachedToParent,AntecedentBusTaskScheduler);


            List<int> routes = new List<int>{1,2,3,4,7,10,14};
            int i = 0;
            foreach(ABus bus in busses)
            {

                // initialize a new "bus" thread.
                Task AntecedentBusTask = AntecedentBusTaskFactory.StartNew(
                ()=>
                    {
                        Console.WriteLine($"Creating Antecedent Bus Task with task ID: {Task.CurrentId} on Thread: {Thread.CurrentThread.ManagedThreadId} on bus: {bus.BusID}");

                        bus.Route = new LinkedList<int>(routes);
                        bus.Execute();
                        Console.WriteLine("Executed bus.");
                    }, cancellationToken,TaskCreationOptions.LongRunning,AntecedentBusTaskScheduler
                );
            }

        }



    }
}
