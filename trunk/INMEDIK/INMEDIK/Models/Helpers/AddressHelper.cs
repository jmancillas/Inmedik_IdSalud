using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class AddressAux
    {
        public int id { get; set; }
        public CountyAux countyAux { get; set; }
        public string addressLine { get; set; }
        public string postalCode { get; set; }
        public string fullAddress { get {
                return String.Format("{0} {1} {2} {3}",addressLine,countyAux.name,countyAux.cityAux.name, countyAux.cityAux.stateAux.name );
            }
        }

        public AddressAux()
        {
            countyAux = new CountyAux();
        }

        public void fillDB(ref Address address)
        {
            address.AddressLine = this.addressLine;
            address.PostalCode = this.postalCode;
            address.CountyId = this.countyAux.id;
        }
    }
    class AddressHelper
    {
    }
}
