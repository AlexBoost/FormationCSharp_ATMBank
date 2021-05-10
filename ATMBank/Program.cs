using System;

namespace PorteMonnaie
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            worker.Start();

            // john.Wallet.AddAmount(100);
            //michel.Wallet.AddAmount(100);

            //Console.WriteLine($"{john.Name} à {john.Wallet.Amount} en poche.");
            //Console.WriteLine($"{michel.Name} à {michel.Wallet.Amount} en poche.");

            //john.GiveMoney(michel, 50); //john 50 //Michel 150
            //michel.GiveMoney(john, 60); // john 110 //Michel 90

            //Console.WriteLine($"{john.Name} à {john.Wallet.Amount} en poche.");
            //Console.WriteLine($"{michel.Name} à {michel.Wallet.Amount} en poche.");
        }
    }
}
