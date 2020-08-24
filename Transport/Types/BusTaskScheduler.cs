using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Types
{

    public class BusTaskScheduler : TaskScheduler
    {

        
        private readonly Thread _busMainThread = null;
        private BlockingCollection<Task> _tasks = new BlockingCollection<Task>();

        public BusTaskScheduler ()
        {
            if (!_busMainThread.IsAlive)
            {
                _busMainThread.IsBackground = true;
                _busMainThread.Start();
            }
            {

            }
        }
        protected override IEnumerable<Task> GetScheduledTasks ()
        {
            throw new NotImplementedException();
        }

        protected override void QueueTask (Task task)
        {
            throw new NotImplementedException();
        }

        protected override bool TryExecuteTaskInline (Task task, bool taskWasPreviouslyQueued)
        {
            

            throw new NotImplementedException();
        }
    }
}
