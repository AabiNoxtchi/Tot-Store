using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Age:BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
