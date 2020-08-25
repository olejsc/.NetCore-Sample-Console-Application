using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Transport.Types
{

    public class BusTaskScheduler : TaskScheduler, IDisposable
    {
        // source code https://codereview.stackexchange.com/questions/43814/taskscheduler-that-uses-a-dedicated-thread/224916


        readonly BlockingCollection<Task> _tasks = new BlockingCollection<Task>();
        readonly Thread _thread;
        readonly CancellationTokenSource _cancellationTokenSource;
        volatile bool _disposed;

        public BusTaskScheduler ()
        {
            _cancellationTokenSource = new CancellationTokenSource();
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
                    var task = _tasks.Take(_cancellationTokenSource.Token);
                    //Debug.Assert(TryExecuteTask(task));
                    TryExecuteTask(task);
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

            if(Thread.CurrentThread == _thread)
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
            _cancellationTokenSource.Cancel();
        }
    }
}
