using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW20._03._19
{
    public class Account
    {
        private static int numberOfAcc = 0;
        private readonly Customer accountOwner;
        private readonly int accountNumber;
        private int maxMinusAllowed;
        public int MaxMinusAllowed { get; }
        private int monthlyIncome;

        public int AccountNumber
        {
            get
            {
                return accountNumber;
            }

        }
        public Customer AccountOwner
        {
            get
            {
                return accountOwner;
            }
        }

        public double Balance { get; private set; }

        public Account(Customer accountOwner, int monthlyIncome)
        {
            this.accountNumber = numberOfAcc++;
            this.accountOwner = accountOwner;
            this.monthlyIncome = monthlyIncome;
            //Monthly Income for 3 months Max Minus Allowed
            maxMinusAllowed = this.monthlyIncome * 3;
        }

        public void Add(double Amount)
        {
            this.Balance += Amount;
        }

        public void Subtract(int Amount)
        {
            this.Balance -= Amount;
        }

        public void Subtract(double Amount)
        {
            this.Balance -= Amount;
        }

        public static bool operator ==(Account a1, Account a2)
        {
            if (ReferenceEquals(a1, null) && ReferenceEquals(a2, null))
                return true;
            if (ReferenceEquals(a1, null) || ReferenceEquals(a2, null))
                return false;
            if (a1.accountNumber == a2.accountNumber)
                return true;
            return false;
        }

        public static bool operator !=(Account a1, Account a2)
        {
            return !(a1 == a2);
        }

        public override bool Equals(object obj)
        {
            Account otherAcount = obj as Account;
            if (otherAcount == null)
                return false;
            return (otherAcount.accountNumber == this.accountNumber);
        }

        public override int GetHashCode()
        {
            return this.accountNumber;
        }

        public static Account operator +(Account a1, Account a2)
        {
            Customer NewCustomer = new Customer(a1.accountOwner.CustomerID + a2.accountOwner.CustomerID,
                $"{a1.accountOwner.Name} + {a2.accountOwner.Name}", 5006465);
            return new Account(NewCustomer, 10000);
        }

        public static Account operator +(Account a1, double amount)
        {
            Account account = new Account(a1.accountOwner, a1.monthlyIncome);
            account.Add(amount);
            return account;
        }

        public static Account operator -(Account a1, double amount)
        {
            Account account = new Account(a1.accountOwner, a1.monthlyIncome);
            account.Subtract(amount);
            return account;
        }
    }
}