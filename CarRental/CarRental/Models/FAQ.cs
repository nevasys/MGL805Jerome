using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class FAQ
    {
        private DateTime _createdOn = DateTime.Now;
        private DateTime _modifiedOn = DateTime.Now;

        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreatedOn { get { return _createdOn; }
                                    set { _createdOn = value; } }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn {
                                    get { return _modifiedOn; }
                                    set { _modifiedOn = value; }
        }
        public string ModifiedBy { get; set; }
    }
}