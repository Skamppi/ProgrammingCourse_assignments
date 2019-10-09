using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public static class PlayerExtensions
    {
        public static Item GetHighestLevelItem(this IPlayer p)
        {
            // loop items
            Player pp = p as Player;
            Item max = null;
            foreach (var item in pp.Items)
            {
                if (max == null)
                {
                    max = item;
                }
                else
                {
                    if (item.Level > max.Level)
                    {
                        max = item;
                    }
                }
            }
            return max;
        }
    }
}
