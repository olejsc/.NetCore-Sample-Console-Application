using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    class Program
    {

        static void Main (string[] args)
        {
            IVehicleFactory factory = new BusFactory();
            ABus[] busses = factory.CreateVehicles(4);
            Object time = new BusTime(ushort.MaxValue,100);
            Run(busses, time);

        }

        private static void Run (ABus[] busses, object time)
        {
            foreach (ABus bus in busses)
            {
                bus.Drive(time);
            }
        }

        /*
         * You might be wondering then, "when should I use inheritance?" It depends on your problem at hand, but this is a decent list of when inheritance makes more sense than composition:

    1. Your inheritance represents an "is-a" relationship and not a "has-a" relationship (Human->Animal vs. User->UserDetails).
    2. You can reuse code from the base classes (Humans can move like all animals).
    3. You want to make global changes to derived classes by changing a base class (Change the caloric expenditure of all animals when they move).

    * Single Responsibility Principle (SRP)
*/



    }
}
