using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Types
{

    public class BusTaskScheduler : TaskScheduler, IDisposable
    {
        // source code https://codereview.stackexchange.com/questions/43814/taskscheduler-that-uses-a-dedicated-thread/224916

        public override int MaximumConcurrencyLevel { get { return 1; } }
        readonly BlockingCollection<Task> _tasks = new BlockingCollection<Task>();
        readonly Thread _thread;
        private static int threadIDCounter = 0;

        volatile bool _disposed;

        public BusTaskScheduler ()
        {
            threadIDCounter++;
            _thread = new Thread(Run);
            _thread.Name = $"Thread{threadIDCounter}";
            _thread.Start();

        }

        public BusTaskScheduler(string threadName)
        {
            threadIDCounter++;
            _thread = new Thread(Run);
            _thread.Name = $"{threadName}{threadIDCounter}";
            _thread.Start();
        }
        protected override IEnumerable<Task> GetScheduledTasks ()
        {
            return _tasks;
        }

        protected override void QueueTask (Task task)
        {
            _tasks.Add(task);
        }

        void Run ()
        {
            while (!_disposed)
            {
                try
                {
                    var task = _tasks.Take();
                    //Debug.Assert(TryExecuteTask(task));
                    //TryExecuteTask(task);

                    TryExecuteTaskInline(task,false);
                }
                catch (OperationCanceledException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    //Debug.Assert(_disposed);
                }
            }
        }

        protected override bool TryExecuteTaskInline (Task task, bool taskWasPreviouslyQueued)
        {

            if (Thread.CurrentThread == _thread)
            {
                return TryExecuteTask(task);
            }
            else
            {
                return false;
            }
        }

        public void Dispose ()
        {
            _disposed = true;
        }
    }
}
