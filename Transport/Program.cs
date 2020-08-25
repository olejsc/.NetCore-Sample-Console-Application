using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transport.Types;

namespace Transport
{
    class Program
    {

        static void Main (string[] args)
        {
            // antecedent task settings ("the bus")
            BusTaskScheduler AntecedentBusTaskScheduler = new BusTaskScheduler();
            TaskCreationOptions AntecedentBusTaskCreationOptions = TaskCreationOptions.LongRunning;
            TaskContinuationOptions AntecedentBusTaskContinuationOptions = TaskContinuationOptions.None;
            using var cts = new CancellationTokenSource();
            CancellationToken cancellationToken = cts.Token;
            TaskFactory AntecedentBusTaskFactory = new TaskFactory(cancellationToken,AntecedentBusTaskCreationOptions,AntecedentBusTaskContinuationOptions,AntecedentBusTaskScheduler);

            // Child tasks inside the "bus", settings:


            // initialize a new "bus" thread.
            Task AntecedentBusTask = AntecedentBusTaskFactory.StartNew(
                ()=>
            {
                Console.WriteLine($"Creating Antecedent Bus Task with task ID: {Task.CurrentId}");

            }, cancellationToken,AntecedentBusTaskCreationOptions,AntecedentBusTaskScheduler
            );



            BusTaskScheduler busTaskScheduler = new BusTaskScheduler();
            TaskCreationOptions taskCreationOptions = TaskCreationOptions.LongRunning;
            CancellationToken busTaskCancellationToken = cts.Token;
            TaskContinuationOptions taskContinuationOptions = TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.AttachedToParent;
            TaskFactory busTaskFactory = new TaskFactory(busTaskCancellationToken,taskCreationOptions, taskContinuationOptions,busTaskScheduler);

            IVehicleFactory factory = new BusFactory();
            ABus[] busses = factory.CreateVehicles(4);
            Object time = new BusTime(ushort.MaxValue,100);
            Run(busses, time);
            Console.ReadLine();
        }

        private static void Run (ABus[] busses, object time)
        {
            BusTime busTime = (BusTime)time;
            DateTime now = new DateTime();
            DateTime target = now.AddMilliseconds(busTime.Duration);
            UInt16 numberOfTicks = (ushort)((target.Millisecond - now.Millisecond)/busTime.Timestep);

            // antecedent task settings ("the bus")
            BusTaskScheduler AntecedentBusTaskScheduler = new BusTaskScheduler();
            TaskCreationOptions AntecedentBusTaskCreationOptions = TaskCreationOptions.LongRunning;
            TaskContinuationOptions AntecedentBusTaskContinuationOptions = TaskContinuationOptions.None;
            using var cts = new CancellationTokenSource();
            CancellationToken cancellationToken = cts.Token;
            TaskFactory AntecedentBusTaskFactory = new TaskFactory(cancellationToken,AntecedentBusTaskCreationOptions,AntecedentBusTaskContinuationOptions,AntecedentBusTaskScheduler);



            foreach (ABus bus in busses)
            {
                // initialize a new "bus" thread.
                Task AntecedentBusTask = AntecedentBusTaskFactory.StartNew(
                ()=>
                {
                    Console.WriteLine($"Creating Antecedent Bus Task with task ID: {Task.CurrentId}");

                }, cancellationToken,AntecedentBusTaskCreationOptions,AntecedentBusTaskScheduler
            );
            }

            /*Parallel.ForEach(busses, (bus) =>
            {
                // initialize a new "bus" thread.
                Task AntecedentBusTask = AntecedentBusTaskFactory.StartNew(
                ()=>
                    {
                        Console.WriteLine($"Creating Antecedent Bus Task with task ID: {Task.CurrentId}");
                        bus.Drive(time);

                    }, cancellationToken,AntecedentBusTaskCreationOptions,AntecedentBusTaskScheduler
                );

            });*/

        }



    }
}
