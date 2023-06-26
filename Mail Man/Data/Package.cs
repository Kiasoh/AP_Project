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
        public static List<Package> packages = new List<Package>();
        Customer customer;
        public TypeOfPackage typeOfPackage;
        public TypeOfDelivery typeOfDelivery;
        public Status status;
        public string addressReciever;
        public string addressSender;
        public string? phoneNumber = null;
        public bool IsExpensive;
        public double weight, FinalPrice;
        public Package(Customer customer , TypeOfPackage typeOfPackage , TypeOfDelivery typeOfDelivery , Status status , string addr , string adds , bool b , double weight , string? phonenumber = null )
        {
            this.customer = customer;
            this.typeOfPackage = typeOfPackage;
            this.typeOfDelivery = typeOfDelivery;
            this.status = status;
            addressReciever = addr;
            this.addressSender = adds;
            this.phoneNumber = phonenumber;
            this.weight = weight;
            IsExpensive = b;
            packages.Add(this);
        }
    }
}
