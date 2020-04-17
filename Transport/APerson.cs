using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public abstract class APerson : ICustomer
    {
        /// <summary>
        /// The age of the person.
        /// </summary>
        /// <remarks>This field determines what type of ticket the person have a right to.</remarks>
        private byte _age;
        /// <summary>
        /// The ticket the person have.
        /// </summary>
        private ITicket _ticket;

        /// <summary>
        /// The age of the person.
        /// </summary>
        public byte Age
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public abstract ITicket BuyTicket (APerson person);
        public abstract void ExtendTicket (ITicket ITicket);
    }
}