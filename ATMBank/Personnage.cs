using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorteMonnaie
{
    class Personnage
    {
        private string _wallet;
        public Wallet Wallet { get; }
        public string Name { get; }

        public Personnage(string name)
        {
            Name = name;
            Wallet = new Wallet();
        }

        public void GiveMoney(Personnage receveur, double amount)
        {
            Wallet.SubAmount(amount);
            receveur.Wallet.AddAmount(amount);
        }
    }
}
