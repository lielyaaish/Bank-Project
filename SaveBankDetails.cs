using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HW20._03._19
{
    class SaveBankDetails
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int CustomerCount { get; set; }
        public double TotalMoneyInTheBank;
        public double myProfits;
        public List<Account> accounts = new List<Account>();
        public List<Customer> customers = new List<Customer>();

        public Dictionary<int, Customer> customersWithCustomerID = new Dictionary<int, Customer>();
        public Dictionary<int, Customer> customersWithCustomerNumber = new Dictionary<int, Customer>();
        public Dictionary<int, Account> accountWithAccountNumber = new Dictionary<int, Account>();
        public Dictionary<Customer, List<Account>> accountIsTheAccountOwner = new Dictionary<Customer, List<Account>>();

        public SaveBankDetails()
        { }

        public static void SerializeABank(string filename, SaveBankDetails bankCreated)
        {
            XmlSerializer myXmlSerializer = new XmlSerializer(typeof(SaveBankDetails));
            using (Stream file = new FileStream(filename, FileMode.Create))
            {
                myXmlSerializer.Serialize(file, bankCreated);
            }
        }

        public static SaveBankDetails DeserializeABank(string fileName)
        {
            SaveBankDetails bankOpenFile;
            XmlSerializer myXmlSerializer = new XmlSerializer(typeof(SaveBankDetails));
            using (Stream file = new FileStream(fileName, FileMode.Open))
            {
                bankOpenFile = myXmlSerializer.Deserialize(file) as SaveBankDetails;
            }
            return bankOpenFile;
        }

    }

}
