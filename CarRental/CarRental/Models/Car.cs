using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Car
    {
        private DateTime _createdOn = DateTime.Now;
        private DateTime _modifiedOn = DateTime.Now;

        [Key]
        public int Id { get; set; }
        public string Brand { get; set;  }
        public string model { get; set;  }
        public string Description { get; set; }
        public carType Type { get; set; }
        public int HorsePower { get; set; }
        public numberOfDoor NumberOfDoors { get; set; }
        public float DailyRate { get; set;  }
        public Boolean IsActive { get; set; }
        public numberOfPassenger NumberOfPassenger { get; set; }
        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }
        public string ModifiedBy { get; set; }
    }

    public enum carType
    {
        SousCompacte = 1,
        Compacte,
        Moyenne,
        Grande,
        ToutTerrains,
        Luxe
    }

    public enum numberOfDoor
    {
        Une=1,
        Deux,
        Trois,
        Quatre,
        Cinq,
        Six
    }

    public enum numberOfPassenger
    {
        Un = 1,
        Deux,
        Trois,
        Quatre,
        Cinq,
        Six,
        Sept
    }
}