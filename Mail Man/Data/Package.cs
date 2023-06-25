using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum TypeOfPackage
    {
        Object,
        Document,
        Breakable
    }
    public enum TypeOfDelivery
    {
        Normal,
        Special
    }
    public enum Status
    {
        Registered,
        ReadyToGO,
        OnTheWay,
        Delivered
    }
    public class Package
    {
        static List<Package> packages = new List<Package>();
        Customer? customer;
        public TypeOfPackage typeOfPackage;
        public TypeOfDelivery typeOfDelivery;
        public Status status;
        public string addressReciever;
        public string addressSender;
        public string? phoneNumber = null;
        public bool IsExpensive;
        public double weight, FinalPrice;
    }
}
