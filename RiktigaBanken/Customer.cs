using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RiktigaBanken
{
     class Customer //customer class med olika constructors och en ToString overload för att få tillbaka flera props på en gång
    {

        public string customerSureName { get; set; }
        public string customerLastName { get; set; }
        public long customerPNR { get; set; }

        public List<SavingsAccount> accounts = new List<SavingsAccount>();


        public Customer(string _customerSureName, string _customerLastName, long _customerPNR, List<SavingsAccount> _accounts)
        {
            this.customerSureName = _customerSureName;
            this.customerPNR = _customerPNR;
            this.customerLastName = _customerLastName;
            this.accounts = _accounts;
        }

        public Customer(string _customerFirstName, string _customerLastName, long _customerPNR)
        {
            this.customerSureName = _customerFirstName;
            this.customerPNR = _customerPNR;
            this.customerLastName = _customerLastName;
        }

        public override string ToString()
        {
            string retur = $"{this.customerPNR}  {this.customerSureName}  {this.customerLastName}";
            return retur.ToString();
        }
        public string WriteToString() //metod för att skriva ut customer till textil så att den kan sparas och läsas in igen senare
        {
            string accountString = "";
            foreach (var item in accounts)
            {
                if(item != accounts.Last())
                {
                    accountString += $"{item.getBalance()}###{item.accountNumber}###";
                } else
                {
                    accountString += $"{item.getBalance()}###{item.accountNumber}";
                }
                
            }
            string retur = $"{this.customerSureName}###{this.customerLastName}###{this.customerPNR}###{accountString}";
            return retur.ToString();
        }






    }
}
