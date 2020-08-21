using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public abstract class AStop
    {
        /// <summary>
        /// The name of the stop.
        /// </summary>
        private string _name;
        /// <summary>
        /// Persons at the stop.
        /// </summary>
        private Queue<APerson> _persons;
        private Byte _busCapacity;

        /// <summary>
        /// The name of the stop.
        /// </summary>
        public string Name
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        /// <summary>
        /// Persons at the stop.
        /// </summary>
        public Queue<APerson> Persons
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

    }
}