using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public class Item
    {
        public Item(Guid id, int level)
        {
            Id = id;
            Level = level;
        }

        public Guid Id { get; set; }
        public int Level { get; set; }
    }
}
