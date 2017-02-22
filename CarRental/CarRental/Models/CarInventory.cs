using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class CarInventory
    {
        private DateTime _createdOn = DateTime.Now;
        private DateTime _modifiedOn = DateTime.Now;

        [Key]
        public int Id { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
        [ForeignKey("Agency")]
        public int AgencyId { get; set; }
        public virtual Agency Agency { get; set; }
        public string SerialNumber { get; set; }
        public int Odometer { get; set; }
        public int year { get; set; }
        public bool AirConditionner { get; set; }
        public bool DVDplayer { get; set; }
        public bool MP3player { get; set; }
        public bool NavigationSystem { get; set; }
        public bool IsReserved { get; set; }
        public float DailyRate { get; set; }
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
}