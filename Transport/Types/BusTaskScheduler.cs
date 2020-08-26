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


        readonly BlockingCollection<Task> _tasks = new BlockingCollection<Task>();
        readonly Thread _thread;
        volatile bool _disposed;

        public BusTaskScheduler ()
        {
            _thread = new Thread(Run);
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
                Console.WriteLine("Trying to execute task inline on same thread!");
                Console.WriteLine($"Task ID: {task.Id} and thread ID: {Thread.CurrentThread.ManagedThreadId}");
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
