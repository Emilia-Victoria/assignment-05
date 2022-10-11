using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class Program
    {
        IList<Item> Items;
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
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public virtual void UpdateQuality()
        {
            SellIn--;
            if (Quality > 0)
            {
                Quality--;

                if (SellIn < 0 && Quality > 0) Quality--;
            }
        }
    }

    public class AgedBrieItem : Item {
        public override void UpdateQuality() {
            SellIn--;
            if (Quality < 50)
            {
                Quality++;
                if (SellIn < 0 && Quality < 50){
                    Quality++;
                }
            }
        }
    }

    public class BackstageItem : Item {
        public override void UpdateQuality()
        {
            SellIn--;
            switch (SellIn)
            {
                case < 0:
                    Quality = 0;
                break;
                case <= 5:
                    Quality += 3;
                break;
                case <= 10:
                    Quality += 2;
                break;
                case > 10:
                    Quality += 1;
                break;
            }

            if (Quality > 50) Quality = 50;
        }
    }

    public class LegendaryItem : Item {
        public override void UpdateQuality(){}
    }

    public class ConjuredItem : Item {
        public override void UpdateQuality(){
            SellIn--;
            if (Quality > 0){
                Quality--;
                if(Quality > 0){
                    Quality--;
                }
            }
            if (SellIn < 0 && Quality > 0) {
                Quality--;
                if(Quality > 0){
                    Quality--;
                }
            }
        }
    }
}