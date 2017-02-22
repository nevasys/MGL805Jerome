using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
         public class Agency
        {
        private DateTime _createdOn = DateTime.Now;
        private DateTime _modifiedOn = DateTime.Now;

        [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Division { get; set; }
            public string AffiliatedWith { get; set; }
            public int CivicNumber { get; set; }
            public string StreetName { get; set; }
            public string Suite { get; set; }
            public string City { get; set; }
            public province Province {get; set;}
            public state State { get; set; }
            public string PostalCode { get; set; }
            public string TelephoneNumber { get; set; }
            public string FaxNumber { get; set; }
            public string Email { get; set; }
            public float Latitude { get; set; }
            public float Longitude { get; set; }
            public Boolean IsActive { get; set; }
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
        public enum province
        {
            Quebec = 1,
            Ontario,
            Alberta,
            BritishColumbia,
            PrinceEdwardIsland,
            Manitoba,
            NewBrunswick,
            NovaScotia,
            Saskatchewan,
            NewfoundlandandLabrador,
            Nunavut,
            NorthwestTerritories,
            Yukon
        }

        public enum state
        {
            Canada = 1,
            UnitedStates,
            Mexico
        }
    
}