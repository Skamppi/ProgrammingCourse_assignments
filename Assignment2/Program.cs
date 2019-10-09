using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<Player> lst = new List<Player>();


            Random rnd = new Random();
            // create 1000000 items
            for (int i = 0; i < 1000000; i++)
            {
                // create Player
                Player player = new Player();
                player.Score = rnd.Next(1, 100000); // create random integer!!
                player.Id = Guid.NewGuid();

                // add items to Player items list
                player.Items = new List<Item>();
                player.Items.Add(new Item(Guid.NewGuid(), i));
                player.Items.Add(new Item(Guid.NewGuid(), i+1000));

                lst.Add(player);
            }

            // get Player
            var p1 = lst[1000];
            //2. Extension method:  get Item with highest Level
            var max = p1.GetHighestLevelItem();


            // 3.
            var array1 = GetItems(p1);
            var array2 = GetItemsWithLinq(p1);

            // 4.
            var item1 = FirstItem(p1);
            var item2 = FirstItemWithLinq(p1);

            // 5.
            Del del = ProcessEachItem;
            del(p1, PrintItem);

            // 6.

            // 7.
            var g1 = new Game<Player>(lst);
            var top10 = g1.GetTop10Players();
        }

        static Item[] GetItems(Player p)
        {
            Item[] itm = new Item[p.Items.Count];
            for (int i = 0; i < p.Items.Count; i++)
            {
                itm[i] = p.Items[i];
            }
            return itm;
        }

        // GetItemsWithLinq
        static Item[] GetItemsWithLinq(Player p)
        {
            Item[] itm = p.Items.ToArray();
            return itm;
        }

        //FirstItem
        static Item FirstItem(Player p)
        {
            if (p.Items != null)
            {
                return p.Items[0];
            }
            return null;
        }

        // FirstItemWithLinq
        static Item FirstItemWithLinq(Player p)
        {
            if (p.Items != null)
            {
                return p.Items.FirstOrDefault();
            }
            return null;
        }

        // kohta 5
        public delegate void Del(Player player, Action<Item> process);
        // Create a method for a delegate.
        public static void ProcessEachItem(Player player, Action<Item> process)
        {
            System.Console.WriteLine(player.Id);
            foreach (var item in player.Items)
            {
                process(item);
            }           
        }
        public static void PrintItem(Item item)
        {
            System.Console.WriteLine(item.Id);
            System.Console.WriteLine(item.Level);
        }
    }


}
