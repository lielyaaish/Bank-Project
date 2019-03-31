using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW20._03._19
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer1 = new Customer(1, "Liel", 89);
            Customer customer2 = new Customer(2, "Itay", 55);
            Customer customer3 = new Customer(3, "Lidor", 88);
            Customer customer4 = new Customer(4, "Revital", 99);

            Account account1 = new Account(customer1, 8_000);
            Account account2 = new Account(customer2, 2_500);
            Account account3 = new Account(customer3, 5_500);
            Account account4 = new Account(customer4, 18_000);

            Bank bank = new Bank();
            bank.NewCustomer(customer1); bank.NewCustomer(customer2);
            bank.NewCustomer(customer3); bank.NewCustomer(customer4);
            bank.NewAccount(account1, customer1); bank.NewAccount(account2, customer2);
            bank.NewAccount(account3, customer3); bank.NewAccount(account4, customer4);

            Account PullOutAccountByNumber = bank.GetAccountByNumber(88);
            List<Account> PullOutAccountCustomer = bank.GetAccountByCustomer(customer1);
            Customer PullOutCustomerByID = bank.GetCustomerById(4);
            Customer PullOutCustomerByNumber = bank.GetCustomerByNumber(99);

            Console.WriteLine("Misson Complete!");
            
        }
    }
}
