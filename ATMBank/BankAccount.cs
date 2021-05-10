using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorteMonnaie
{
    class BankAccount
    {
        public string Name { get; private set; }
        public Personnage Owner { get; private set; }
        public double Amount { get; private set; }

        public BankAccount(string name, Personnage owner)
        {
            Name = name;
            Owner = owner;
        }

        public bool Deposit(double amount)
        {
            Amount += amount;
            return true;
        }

        public bool Withdraw(double amount)
        {
            if ((Amount - amount) < 0)
                return false;
            Amount -= amount;
            return true;
        }
    }
}
