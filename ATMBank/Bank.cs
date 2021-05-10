using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorteMonnaie
{
    class Bank
    {
        public string Name { get; private set; }

        public List<BankAccount> BankAccounts { get; private set; }

        public Bank(string name)
        {
            Name = name;
        }

        public void RegisterAccount(string name, Personnage owner)
        {
            if (BankAccounts == null)
                BankAccounts = new List<BankAccount>();

            BankAccounts.Add(new BankAccount(name, owner));
        }
    }
}
