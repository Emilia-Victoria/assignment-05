using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class Program
    {
        IList<Item>? Items;
        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new AgedBrieItem { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                new BackstageItem
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new BackstageItem
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new BackstageItem
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
				new ConjuredItem { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                                          }

                          };

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < app.Items.Count; j++)
                {
                    Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
                }
                Console.WriteLine("");
                UpdateQuality(app.Items);
            }

        }

        public static void UpdateQuality(IList<Item> Items)
        {
            foreach (Item item in Items)
            {
                item.UpdateQuality();
            }
        }

    }

    public class Item
    {
        public string? Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public virtual void UpdateQuality()
        {
            SellIn--;
            Quality -= SellIn < 0 ? 2 : 1;
            if (Quality < 0) Quality = 0;
        }
    }

    public class AgedBrieItem : Item {
        public override void UpdateQuality() {
            SellIn--;
            Quality += SellIn < 0 ? 2 : 1;
            if (Quality > 50) Quality = 50;
        }
    }

    public class BackstageItem : Item {
        public override void UpdateQuality()
        {
            SellIn--;

            Quality += SellIn switch
            {
                < 0 => -Quality,
                <= 5 => 3,
                <= 10 => 2,
                > 10 => 1
            };

            if (Quality > 50) Quality = 50;
        }
    }

    public class LegendaryItem : Item {
        public override void UpdateQuality(){}
    }

    public class ConjuredItem : Item {
        public override void UpdateQuality(){
            SellIn--;
            Quality -= SellIn < 0 ? 4 : 2;
            if (Quality < 0) Quality = 0;
        }
    }
}