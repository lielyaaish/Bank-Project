using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW20._03._19
{
    public class Customer
    {
        private static int numberOfCust = 0;
        private readonly int customerID;
        private readonly int customerNumber;
        public int PhNumber { get; private set; }

        public string Name { get; private set; }
        public int CustomerPhoneNumber { get; }

        public Customer()
        {

        }

        public int CustomerID
        {
            get
            { return customerID; }
        }

        public int CustomerNumber
        {
            get
            { return customerNumber; }
        }

        public Customer(int customerID, string name, int PhNumber)
        {
            this.customerID = customerID;
            this.customerNumber = numberOfCust++;
            this.Name = name;
            this.PhNumber = PhNumber;
        }

        public static bool operator ==(Customer c1, Customer c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null))
                return true;
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
                return false;
            if (c1.customerNumber == c2.customerNumber)
                return true;
            return false;
        }

        public static bool operator !=(Customer c1, Customer c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            Customer otherCustomer = obj as Customer;
            if (otherCustomer == null)
                return false;
            return (otherCustomer.customerNumber == this.customerNumber);
        }

        public override int GetHashCode()
        {
            return this.customerNumber;
        }
    }
}