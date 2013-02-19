using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace myMenuJournal.Entities
{
    public class UserDay
    {
        [Key]
        public virtual Guid UserDayID { get; set; }
        
        public DateTime DateCreated { get; set; }
        public int DOYIntake { get; set; }

        public Guid UserID { get; set; }
    }
}