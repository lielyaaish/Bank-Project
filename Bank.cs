using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW20._03._19
{
    public class Bank : IBank
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public int CustomerCount { get; private set; }
        private double TotalMoneyInTheBank;
        private double profits;

        private List<Account> accounts = new List<Account>();
        private List<Customer> customers = new List<Customer>();

        Dictionary<int, Customer> CustomerNumberIDDic = new Dictionary<int, Customer>();
        Dictionary<int, Customer> CustomerNumberDic = new Dictionary<int, Customer>();
        Dictionary<int, Account> AccountNumberDic = new Dictionary<int, Account>();
        Dictionary<Customer, List<Account>> AccountOwnerDic = new Dictionary<Customer, List<Account>>();

        public Bank()
        {

        }

        public Customer GetCustomerById(int customerID)
        {
            if (CustomerNumberIDDic.ContainsKey(customerID))
            {
                throw new CustomerNotFoundException($"Customer ID: {customerID} has NOT been found!");
            }
            return CustomerNumberIDDic[customerID];
        }

        public Customer GetCustomerByNumber(int customerNumber)
        {
            if (!CustomerNumberDic.ContainsKey(customerNumber))
            {
                throw new CustomerNotFoundException($"Customer Number: {customerNumber} has NOT been found!");
            }
            return CustomerNumberDic[customerNumber];
        }

        public Account GetAccountByNumber(int accountNumber)
        {
            if (!AccountNumberDic.ContainsKey(accountNumber))
            {
                throw new AccountNotFoundException($"Account number: {accountNumber} has NOT been found!");
            }
            return AccountNumberDic[accountNumber];
        }

        public List<Account> GetAccountByCustomer(Customer customer)
        {
            if (!AccountOwnerDic.ContainsKey(customer))
            {
                throw new AccountNotFoundException($"Customer {customer.Name} Account was NOT been found!");
            }
            return AccountOwnerDic[customer];
        }

        public void NewAccount(Account account, Customer customer)
        {
            if (accounts.Contains(account))
            {
                throw new AccountAlreadyExistException($"Account number: {account.AccountOwner} & Customer {customer.Name} is already exist!");
            }

            else
            {
                accounts.Add(account);
                AccountNumberDic.Add(account.AccountNumber, account);

                List<Account> NewAccounts = new List<Account>();
                NewAccounts.Add(account);
                AccountOwnerDic.Add(customer, NewAccounts);
            }
        }

        public void NewCustomer(Customer customer)
        {
            if (customers.Contains(customer))
                throw new CustomerAlreadyExistException($"Customer {customer.Name} is already exist!");
            else
            {
                customers.Add(customer);
                CustomerNumberIDDic.Add(customer.CustomerID, customer);
                CustomerNumberDic.Add(customer.CustomerNumber, customer);
                CustomerCount++;
            }
        }

        public double PutMoneyInTheBankס(Account account, double amount)
        {
            account.Add(amount);
            TotalMoneyInTheBank += amount;
            return TotalMoneyInTheBank;
        }

        public double AccountHasMinus(Account account, double amount)
        {
            if (TotalMoneyInTheBank - amount > account.MaxMinusAllowed)
            {
                account.Subtract(amount);
                TotalMoneyInTheBank -= amount;
            }
            else
            {
                throw new BalanceException();
            }
            return TotalMoneyInTheBank;
        }

        public double GetCustomerTotalNewBalance(Account account)
        { return account.Balance; }

        public void CloseAccount(Account account, Customer customer)
        {
            if (customers.Contains(customer))
                customers.Remove(customer);
            if (accounts.Contains(account))
                accounts.Remove(account);
        }
        public void ChargeAnnualCommossion(float percentage)
        {
            double commision = 0;
            foreach (Account account in accounts)
            {
                commision = account.Balance * percentage;
                account.Subtract(commision);
                profits += commision;
            }
        }
        public void GettingAccountsTogether(Account account1, Account account2)
        {
            Account newAccount3 = account1 + account2;
            Customer oldCustomer = null;
            List<Account> AllAccount1 = new List<Account>();
            List<Account> AllAccount2 = new List<Account>();

            foreach (KeyValuePair<Customer, List<Account>> OwnerOfTheAccount in AccountOwnerDic)
            {
                if ((OwnerOfTheAccount.Value.Contains(account1)) && (OwnerOfTheAccount.Value.Contains(account2)))
                    oldCustomer = OwnerOfTheAccount.Key;
            }

            if (oldCustomer == null)
            {
                throw new NotSameCustomerException();
            }
            accounts.Remove(account1);
            accounts.Remove(account2);
            accounts.Add(newAccount3);
            customers.Remove(oldCustomer);
            CloseAccount(account1, oldCustomer);

            Customer NewCustomer = new Customer(newAccount3.AccountOwner.CustomerID,
                newAccount3.AccountOwner.Name, newAccount3.AccountOwner.PhNumber);
            customers.Add(NewCustomer);
        }

        public static void SaveDataToXML(Bank bank)
        {
            string fileNameBank = @"D:\XMLfiles\BankXml.xml";
            SaveBankDetails bankDataToSave = new SaveBankDetails()
            {
                accounts = bank.accounts,
                customers = bank.customers,
                customersWithCustomerID = bank.CustomerNumberIDDic,
                customersWithCustomerNumber = bank.CustomerNumberDic,
                accountWithAccountNumber = bank.AccountNumberDic,
                accountIsTheAccountOwner = bank.AccountOwnerDic,

                TotalMoneyInTheBank = bank.TotalMoneyInTheBank,
                myProfits = bank.profits,
                Name = bank.Name,
                Address = bank.Address,
                CustomerCount = bank.CustomerCount
            };
            SaveBankDetails.SerializeABank(fileNameBank, bankDataToSave);
        }

        public static Bank LoadDataFromXML(string filename)
        {
            SaveBankDetails bankDataToSave = SaveBankDetails.DeserializeABank(filename);

            return new Bank()
            {
                accounts = bankDataToSave.accounts,
                customers = bankDataToSave.customers,

                CustomerNumberIDDic = bankDataToSave.customersWithCustomerID,
                CustomerNumberDic = bankDataToSave.customersWithCustomerNumber,

                AccountNumberDic = bankDataToSave.accountWithAccountNumber,
                AccountOwnerDic = bankDataToSave.accountIsTheAccountOwner,

                Name = bankDataToSave.Name,
                Address = bankDataToSave.Address,
                CustomerCount = bankDataToSave.CustomerCount,
                TotalMoneyInTheBank = bankDataToSave.TotalMoneyInTheBank,
                profits = bankDataToSave.myProfits
            };
        }
    }
}
