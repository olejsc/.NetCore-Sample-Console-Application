﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Types;

namespace Transport
{
    public class DieselBus : ABus
    {
        public DieselBus () : base(6, false, 2, 20, 8)
        {
            base.BusTaskScheduler = new BusTaskScheduler();
            //base.Engine.FuelFull += OnFuelFull;
            //base.Engine.FuelEmpty += OnFuelEmpty;
        }

        public override event EventHandler<EventArgs> DayStart;
        public override event EventHandler<EventArgs> DayEnd;
        public override event EventHandler<EventArgs> EndRoute;
        public override event EventHandler<EventArgs> ChangeRoute;
        public override event EventHandler<EventArgs> StartRoute;


        public override void Stop ()
        {
            throw new NotImplementedException();
        }

        public void OnDayStart (object sender, EventArgs args)
        {

        }

        public void OnFuelEmpty (object sender, EngineEventArgs args)
        {
            Console.WriteLine("Fuel is empty!");
        }

        public void OnFuelFull (object sender, EngineEventArgs args)
        {
            Console.WriteLine("Fuel is full!");
        }

        public override bool ArrivedAtTargetStop ()
        {
            return this.Location == this.Route.First.Value;
        }

        public override int CalculateDistanceToTarget ()
        {
            return this.Location + this.Route.First.Value;
        }

        public override bool CheckIfStopButtonPressed ()
        {
            // If there is no people, no button is pressed.
            if (People.Count == 0)
            {
                StopButtonPressed = false;
            }
            // If there are people, but only one stop left, the button is pressed.
            else if (Route.Count == 1 && People.Count > 0)
            {
                StopButtonPressed = true;
                CancellationDrivingTokenSource.Cancel();
            }
            // 
            else
            {
                if (((Route.Count * People.Count) / 100) > 0.5f)
                {
                    StopButtonPressed = true;
                    CancellationDrivingTokenSource.Cancel();
                }
                else
                {
                    StopButtonPressed = false;
                }
            }
            return StopButtonPressed;
        }


        public override void NotifyNextBusstops (int bussStopsInAdvanceToNofity)
        {
        }

        public override void ResetStopVariables ()
        {
            StopButtonPressed = false;

        }

        public override void Execute ()
        {
            Task engineTask = BusTaskFactory.StartNew(() =>
            {
                Engine.Start();
            }, DrivingCTS.Token, TaskCreationOptions.AttachedToParent, BusTaskScheduler);
            engineTask.Wait(DrivingCTS.Token);
            Task DriveOrganizingtask = BusTaskFactory.StartNew(() =>
            {
                while (!CancellationDrivingTokenSource.Token.IsCancellationRequested)
                {
                    var DriveTask = BusTaskFactory.StartNew(()=>
{

                    },CancellationDrivingTokenSource.Token, TaskCreationOptions.AttachedToParent,BusTaskScheduler)
                    .ContinueWith(NotifyBussStopsTask =>
                    {
                        // TODO
                        // notify 3 next busstops on the route about our current position, speed and time.
                    }, CancellationDrivingTokenSource.Token,
                        TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.AttachedToParent,
BusTaskScheduler)
                    .ContinueWith(CheckLocationTask =>
                    {
                        // Check if We've arrived at location. In case, exit these continuation tasks in the drive logic.
                        if (ArrivedAtTargetStop() && ShouldStopAtTargetStop())
                        {
                            Engine.Stop();
                            Task.Delay(2000);
                            CancellationDrivingTokenSource.Cancel();
                        }
                        // Arrived at stop but we shouldn't stop because no passengers to pick up.
                        else if (ArrivedAtTargetStop())
                        {
                            CancellationDrivingTokenSource.Cancel();
                        }
                        // We've not arrived at the stop yet, so keep driving.
                        else
                        {

                        }
                    }, CancellationDrivingTokenSource.Token, TaskContinuationOptions.AttachedToParent| TaskContinuationOptions.ExecuteSynchronously| TaskContinuationOptions.NotOnCanceled, BusTaskScheduler);

                    Drive(Route.Count > 1, CancellationDrivingTokenSource.Token);
}
            }, CancellationDrivingTokenSource.Token, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning, BusTaskScheduler);
            Task StopPressedTask = DriveOrganizingtask.ContinueWith(StopPressedTask =>
            {
                // If there is no people, no button is pressed.
                if(People.Count == 0)
                {
                    StopButtonPressed = false;
                }
                // If there are people, but only one stop left, the button is pressed.
                else if(Route.Count == 1 && People.Count > 0)
                {
                    StopButtonPressed = true;
                    CancellationDrivingTokenSource.Cancel();
                }
                // 
                else
                {
                    if(((Route.Count * People.Count) / 100) > 0.5f)
                    {
                        StopButtonPressed = true;
                        CancellationDrivingTokenSource.Cancel();
                    }
                    else
                    {
                        StopButtonPressed = false;
                    }
                }
                // check if stop button is pressed
            }, DrivingCTS.Token, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.AttachedToParent, BusTaskScheduler);
}

        public override bool ShouldStopAtTargetStop ()
        {
            throw new NotImplementedException();
        }

        public override void Drive (bool lastStopIsNextStop, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
            {
                if (Route.Count > 0)
                {
                    if (Location < Route.First.Value)
                    {
                        Location += 1;
                    }
                    else
                    {
                        CancellationDrivingTokenSource.Cancel();
                    }
                }
                else
                {
                    CancellationDrivingTokenSource.Cancel();
                }
            }

        }

        public override void LeaveStop ()
        {
            Route.RemoveFirst();
            ResetStopVariables();
        }

        public override void OpenDoors ()
        {
            Engine.Stop();
        }

        public override void CloseDoors ()
        {
            Engine.Start();
        }

        public override void WaitPassengerJoining ()
        {
            if(Route.First.Value == Location)
            {

            }
        }

        public override void WaitPassengersLeaving ()
        {
            if (StopButtonPressed && People.Count > 0)
            {
                People.RemoveAt(0);
            }
        }
    }
}