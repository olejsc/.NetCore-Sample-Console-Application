using System;
using System.Threading;

namespace Transport
{
    public class ElectricBus : ABus
    {
        public override event EventHandler<EventArgs> DayStart;
        public override event EventHandler<EventArgs> DayEnd;
        public override event EventHandler<EventArgs> EndRoute;
        public override event EventHandler<EventArgs> ChangeRoute;
        public override event EventHandler<EventArgs> StartRoute;

        public override bool ArrivedAtTargetStop ()
        {
            throw new NotImplementedException();
        }

        public override int CalculateDistanceToTarget ()
        {
            throw new NotImplementedException();
        }

        public override bool CheckIfStopButtonPressed ()
        {
            throw new NotImplementedException();
        }

        public override void CloseDoors ()
        {
            throw new NotImplementedException();
        }


        public override void Drive (bool lastStopIsNextStop, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override void LeaveStop ()
        {
            throw new NotImplementedException();
        }

        public override void NotifyNextBusstops (int bussStopsInAdvanceToNofity)
        {
            throw new NotImplementedException();
        }

        public override void OpenDoors ()
        {
            throw new NotImplementedException();
        }

        public override void ResetStopVariables ()
        {
            throw new NotImplementedException();
        }

        public override bool ShouldStopAtTargetStop ()
        {
            throw new NotImplementedException();
        }

        public override void Stop ()
        {
            throw new NotImplementedException();
        }

        public override void WaitPassengerJoining ()
        {
            throw new NotImplementedException();
        }

        public override void WaitPassengersLeaving ()
        {
            throw new NotImplementedException();
        }
    }
}