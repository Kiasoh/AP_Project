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
        public Customer customer;
        public int id;
        public TypeOfPackage typeOfPackage;
        public TypeOfDelivery typeOfDelivery;
        public Status status;
        public string addressReciever;
        public string addressSender;
        public string? phoneNumber = null;
        public bool IsExpensive;
        public double weight, FinalPrice;
        public string comment ="";
        public Package(Customer customer , TypeOfPackage typeOfPackage , TypeOfDelivery typeOfDelivery , Status status , string addr , string adds , bool b , double weight , string? phonenumber = null )
        {
            id = packages.Count;
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
        public override string ToString ()
        {
            if (phoneNumber==null) phoneNumber = "";
            return $"{customer.ssn} ; {typeOfPackage.ToString()} ; {typeOfDelivery.ToString()} ; {status.ToString()} ; {addressReciever} ; {addressSender} ; {IsExpensive.ToString()} ; {weight.ToString()} ; {phoneNumber} ; {comment}";
        }
        public double CalculateCost()
        {
            double cost = 10000;
            if ( typeOfPackage == TypeOfPackage.Breakable ) cost *= 2;
            else if ( typeOfPackage == TypeOfPackage.Document ) cost *= 1.5;
            if ( IsExpensive ) cost *= 2;
            double w = weight;
            for ( w -= 0.5; w > 0; w -= 0.5 ) cost *= 1.2;
            if ( typeOfDelivery == TypeOfDelivery.Special ) cost *= 1.5;
            return cost;
        }
    }
}
