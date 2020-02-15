using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
   public enum Gender
    {
        Both,Girls,Boys
    }

    public class Item:BaseEntity
    {
        [MaxLength(100)]
        public string IdentificationKod { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
       
        public string Description { get; set; }

        public int AgeId { get; set; }

        public virtual Age Age { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public Gender Gender { get; set; }

        public ICollection<AvailableItem> AvailableItems { get; set; }
        

    }
}
