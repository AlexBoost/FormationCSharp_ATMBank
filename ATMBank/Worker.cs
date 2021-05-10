using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorteMonnaie
{
    class Worker
    {
        private List<Bank> _bankList;
        private List<ATM> _atmList;
        private List<Personnage> _persoList;

        public Worker()
        {
            _bankList = new List<Bank>();
            _atmList = new List<ATM>();
            _persoList = new List<Personnage>();

            var cic = new Bank("CIC");
            var lcl = new Bank("LCL");
            var sc = new Bank("Société Générale");

            _bankList.Add(cic);
            _bankList.Add(lcl);
            _bankList.Add(sc);

            _atmList.Add(new ATM("ATM 1", cic));
            _atmList.Add(new ATM("ATM 2", cic));
            _atmList.Add(new ATM("ATM 1", lcl));
            _atmList.Add(new ATM("ATM 1", sc));

            _persoList.Add(new Personnage("John"));
            _persoList.Add(new Personnage("Michel"));
        }

        public void Start()
        {
            /*
            - Créer un ATM
            - Créer une banque
            - Créer un Compte Bancaire
            - Créer une formule de prêt

            - Associer une banque à un ATM
            - Associer un compte bancaire à une banque
            - Associer une formule de prêt à une banque

            - Retirer de l'argent de l'ATM et du compte bancaire
            - Retirer de l'argent du porte feuille
            - Déposer de l'argent dans l'ATM sur un compte bancaire
            - Déposer de l'argent dans le porte feuille
            */
            Console.WriteLine("Comment tu t'appelles ?");
            var name = Console.ReadLine();

            Personnage currentPerso = _persoList.FirstOrDefault(perso => perso.Name == name);

            if (currentPerso == null)
            {
                Console.WriteLine("Le personnage n'existe pas !");
                return;
            }

            while (true)
            {
                //MONPERSO EXISTE

                Console.WriteLine("Voici la liste des ATMs disponible : ");

                foreach (var atm in _atmList)
                    Console.WriteLine($"{_atmList.IndexOf(atm)} => {atm.Name} - {atm.Bank.Name}");

                //for (int i = 0; i < _atmList.Count; i++)
                //{
                //    Console.WriteLine($"{i} => {_atmList[i].Name} - {_atmList[i].Bank.Name}");
                //}

                Console.WriteLine("Selectionnez un ATM :");
                var key = Console.ReadLine();

                int atmNb;

                if (!Int32.TryParse(key, out atmNb))
                {
                    Console.WriteLine("Le numéro que vous avez demandé n'est pas attribué.");
                    return;
                }

                //ICI atmNB il a valeur défini par TryParse

                if (atmNb >= _atmList.Count)
                {
                    Console.WriteLine("L'atm demandé n'existe pas.");
                    return;
                }

                var currentAtm = _atmList[atmNb];

                if (currentAtm == null)
                {
                    Console.WriteLine("L'atm séléctionné n'existe pas.");
                    return;
                }

                Console.WriteLine($"Vous avez séléctionné l'ATM {currentAtm.Name} - {currentAtm.Bank.Name}");

                if (currentAtm.Bank.BankAccounts == null || !currentAtm.Bank.BankAccounts.Any())
                {
                    Console.WriteLine("La banque pour cet ATM n'a pas de compte bancaire.");
                    if (!AskCreateAccount(currentAtm, currentPerso))
                        return;
                }

                //ICI la bank a au moins 1 compte bancaire.

                var bankAccountList = currentAtm.Bank.BankAccounts.Where(account => account.Owner == currentPerso).ToList();

                if (bankAccountList == null || !bankAccountList.Any())
                {
                    if (!AskCreateAccount(currentAtm, currentPerso))
                        return;
                }

                var currentBankAccount = SelectBankAccount(currentAtm, currentPerso);

                if (currentBankAccount == null)
                    return;

                Console.WriteLine("Séléctionnez une action :");
                ChooseAction(currentBankAccount);
            }
        }

        private void ChooseAction(BankAccount currentBankAccount)
        {
            Console.WriteLine("0 => Déposer de l'argent");
            Console.WriteLine("1 => Retirer de l'argent");
            Console.WriteLine("2 => Consulter le Solde");
            Console.WriteLine("3 => Faire un prêt");

            var key = Console.ReadLine();

            int nb;

            if (!Int32.TryParse(key, out nb))
            {
                Console.WriteLine("Wtf man, c'est 1 ou 2 la réponse !");
                return;
            }

            switch (nb)
            {
                case 0:
                    Deposit(currentBankAccount);
                    break;
                case 1:
                    Withdraw(currentBankAccount);
                    break;
                case 2:
                    Solde(currentBankAccount);
                    break;
                case 3:
                    //TODO POUR VOUS
                    break;
                default:
                    Console.WriteLine("Wtf man, c'est 1 ou 2 la réponse !");
                    return;
            }

            //if (nb == 0)
            //    Deposit(currentBankAccount);
            //else if (nb == 1)
            //    Withdraw(currentBankAccount);
            //else if (nb == 2)
            //    Solde(currentBankAccount);
            //else
            //{
            //    Console.WriteLine("Wtf man, c'est 1 ou 2 la réponse !");
            //    return;
            //}
        }

        private void Solde(BankAccount currentBankAccount)
        {
            Console.WriteLine($"Le solde de votre compte est de : {currentBankAccount.Amount}$");
        }

        private void Withdraw(BankAccount currentBankAccount)
        {
            Console.WriteLine("Combien d'argent voulez-vous retirer ?");

            var key = Console.ReadLine();

            int nb;

            if (!Int32.TryParse(key, out nb))
            {
                Console.WriteLine("Wtf man ???");
                return;
            }

            if (nb > 0)
            {
                if (currentBankAccount.Withdraw(nb))
                    Console.WriteLine($"Vous avez bien retiré {nb}$ de votre compte.");
                else
                    Console.WriteLine($"Vous n'avez pas pu retirer {nb}$ de votre compte. (Solde inférieur)");
            }
        }

        private void Deposit(BankAccount currentBankAccount)
        {
            Console.WriteLine("Combien d'argent voulez-vous déposer ?");

            var key = Console.ReadLine();

            int nb;

            if (!Int32.TryParse(key, out nb))
            {
                Console.WriteLine("Wtf man ?????");
                return;
            }

            if (nb > 0)
            {
                currentBankAccount.Deposit(nb);
                Console.WriteLine($"Vous avez bien déposé {nb}$ sur votre compte.");
            }
        }

        private BankAccount SelectBankAccount(ATM currentAtm, Personnage currentPerso)
        {
            Console.WriteLine("Voici la liste de tout vos comptes bancaire :");

            var bankAccountList = currentAtm.Bank.BankAccounts.Where(account => account.Owner == currentPerso).ToList();

            foreach (var bankAccount in bankAccountList)
                Console.WriteLine($"{bankAccountList.IndexOf(bankAccount)} => {bankAccount.Name}");

            Console.WriteLine("Selectionnez un Compte Bancaire :");
            var key = Console.ReadLine();

            int nb;

            if (!Int32.TryParse(key, out nb))
            {
                Console.WriteLine("Le numéro de compte que vous avez demandé n'est pas attribué.");
                return null;
            }

            if (nb >= _atmList.Count)
            {
                Console.WriteLine("L'atm demandé n'existe pas.");
                return null;
            }

            var currentBankAccount = bankAccountList[nb];

            if (currentAtm == null)
            {
                Console.WriteLine("Le compte bancaire séléctionné n'existe pas.");
                return null;
            }

            //LE COMPTE EXISTE ET EST SÉLÉCTIONNÉ
            return currentBankAccount;
        }

        private bool AskCreateAccount(ATM currentAtm, Personnage currentPerso)
        {
            Console.WriteLine("Vous ne disposez pas de compte bancaire, voulez-vous en créer un ? O/N");
            var answer = Console.ReadKey();
            if (answer.Key == ConsoleKey.O)
            {
                Console.WriteLine("Quel nom voulez-vous pour votre compte ?");
                var accountName = Console.ReadLine();
                currentAtm.Bank.RegisterAccount(accountName, currentPerso);
                Console.WriteLine($"Votre compte {accountName} à bien été créé !");
                return true;
            }
            return false;
        }
    }
}
