using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace myMenuJournal.Entities
{
    public class UserProperties
    {
        [Key]
        public virtual Guid UserPropertyId { get; set; }

        public virtual User User { get; set; }

        public virtual string FacebookToken { get; set; }
    }
}