using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorteMonnaie
{
    class Pret
    {
        public double Amount { get; private set; }

        public Pret(double amount)
        {
            Amount = amount;
        }
    }
}
