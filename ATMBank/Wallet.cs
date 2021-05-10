using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorteMonnaie
{

    class Wallet
    {
        public double Amount { get; private set; }

        public void AddAmount(double amount)
        {
            if (amount <= 0)
                return;

            Amount += amount;
        }

        public void SubAmount(double amount)
        {
            if (amount <= 0)
                return;

            Amount -= amount;
        }
    }
}
