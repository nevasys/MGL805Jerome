using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Reservation
    {
        private DateTime _createdOn = DateTime.Now;
        private DateTime _modifiedOn = DateTime.Now;

        [Key]
        public int Id { get; set; }
        [ForeignKey("CarInventory")]
        public int CarInventoryId { get; set; }
        public virtual CarInventory CarInventory { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        [ForeignKey("Agency")]
        public int AgencyId { get; set; }
        public virtual Agency Agency { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int OdometerStart { get; set; }
        public int OdometerEnd { get; set; }
        public int Days { get; set; }
        public float DailyRate { get; set; }
        public float ProvincialTax { get; set; }
        public float FederalTax { get; set; }
        public float Amount { get; set; }
        public float TotalAmount { get; set; }
        public float Discount { get; set; }
        public reservationStatus ReservationStatus { get; set; }
        public bool IsActive { get; set; }
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

    public enum reservationStatus
    {
        Réservé = 1,
        SurLaRoute,
        Retourné,
        CancelléParClient,
        CancelléParAgence,
        CancelléParAdmin
    }
}