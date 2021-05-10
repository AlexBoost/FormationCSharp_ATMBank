using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorteMonnaie
{
    class ATM
    {
        public string Name { get; private set; }
        public Bank Bank { get; private set; }

        public ATM(string name, Bank bank)
        {
            Name = name;
            Bank = bank;
        }
    }
}
