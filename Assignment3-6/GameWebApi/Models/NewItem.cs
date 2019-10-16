using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Models
{
    public class NewItem
    {
        public Guid Id { get; set; }

        [Range(1, 99, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Level { get; set; }

        public ItemType Type { get; set; }
        //public string Type { get; set; }

        [CreationDate]
        //[DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
    }
}
