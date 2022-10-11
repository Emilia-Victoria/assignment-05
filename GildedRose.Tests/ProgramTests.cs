namespace GildedRose.Tests;
using GildedRose;

public class ProgramTests
{
    private IList<Item> Items;
    public ProgramTests()
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
                    Quality = 40
                },
                new BackstageItem
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 40
                },
				
				new ConjuredItem { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                                          };
    }

    [Fact]
    public void UpdateQualityAgedBrie()
    {
        // Arrange
        var entity = Items.Where(i => i.Name == "Aged Brie").FirstOrDefault();
        var qualityBefore = entity!.Quality;

        // Act
        Program.UpdateQuality(Items);
        
        // Assert
        entity.Quality.Should().BeGreaterThan(qualityBefore);
    }

    [Fact]
    public void UpdateQualityBackstagePassMoreThan10Days()
    {
        // Arrange
        var entity = Items.Where(i => i.Name == "Backstage passes to a TAFKAL80ETC concert" && i.SellIn == 15).FirstOrDefault();
        var qualityBefore = entity!.Quality;

        // Act
        Program.UpdateQuality(Items);

        // Assert
        entity.Quality.Should().Be(qualityBefore + 1);
    }
    
    [Fact]
    public void UpdateQualityBackstagePass10daysOrLess()
    {
        // Arrange
        var entity = Items.Where(i => i.Name == "Backstage passes to a TAFKAL80ETC concert" && i.SellIn == 10).FirstOrDefault();
        var qualityBefore = entity!.Quality;

        // Act
        Program.UpdateQuality(Items);
        
        // Assert
        entity.Quality.Should().Be(qualityBefore + 2);
    }

    [Fact]
    public void UpdateQualityBackstagePass5daysOrLess()
    {
        // Arrange
        var entity = Items.Where(i => i.Name == "Backstage passes to a TAFKAL80ETC concert" && i.SellIn == 5).FirstOrDefault();
        var qualityBefore = entity!.Quality;

        // Act
        Program.UpdateQuality(Items);
        
        // Assert
        entity.Quality.Should().Be(qualityBefore + 3);
    }

    [Fact]
    public void UpdateQualityBackstagePassAfterConcert()
    {
        // Arrange
        var entity = Items.Where(i => i.Name == "Backstage passes to a TAFKAL80ETC concert" && i.SellIn == 5).FirstOrDefault();

        // Act
        while (entity!.SellIn >= 0) {
            Program.UpdateQuality(Items);
        }

        // Assert
        entity.Quality.Should().Be(0);
    }

    [Fact]
    public void UpdateQualitySulfuras()
    {
        // Act
        Program.UpdateQuality(Items);

        // Assert
        Items.Where(i => i.Name!.StartsWith("Sulfuras")).FirstOrDefault()!.Quality.Should().Be(80);
    }

    [Fact]
    public void UpdateQualityNeverNegative()
    {
        // Arrange
        var Items2 = new List<Item> {new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 0}};

        // Act
        Program.UpdateQuality(Items2);

        // Assert
        Items2[0].Quality.Should().Be(0);
    }

    [Fact]
    public void UpdateQualityNeverOver50()
    {
        //Arrange
        var Items2 = new List<Item> {new AgedBrieItem { Name = "Aged Brie", SellIn = 2, Quality = 50 }};
        
        // Act
        Program.UpdateQuality(Items2);

        //Assert
        Items2[0].Quality.Should().Be(50);
    }

    [Fact]
    public void UpdateQualityQualityDropsBy2ForGenericItemsWithSellInLessThan0()
    {
        // Arrange
        var item = new Item{Name = "+5 Dexterity Vest", SellIn = -1, Quality = 10};
        var items2 = new List<Item>{item};
        var qualityBefore = item.Quality;

        // Act
        Program.UpdateQuality(items2);

        // Assert
        item.Quality.Should().Be(qualityBefore-2);
    }

    [Fact]
    public void runProgram () {
        // Arrange
        using var writer = new StringWriter();
        Console.SetOut(writer);
        var expected = System.IO.File.ReadAllText("../../../GildedRose.txt");

        // Act
        Program.Main(Array.Empty<string>());

        // Assert
        writer.ToString().Should().Be(expected);
    }

    [Fact]
    public void UpdateQualityConjuredItemDecreases2SellInOver0()
    {
        // Arrange
        var entity = Items.Where(i => i.Name == "Conjured Mana Cake").FirstOrDefault();
        var qualityBefore = entity!.Quality;

        // Act
        Program.UpdateQuality(Items);

        // Assert
        entity.Quality.Should().Be(qualityBefore-2);
    }

    [Fact]
    public void UpdateQualityConjuredItemDecreases4SellInLessThan0()
    {
        // Arrange
        var entity = new ConjuredItem{Name = "Conjured Mana Cake", SellIn = 0, Quality = 10};
        var Items2 = new List<Item>{entity};
        var qualityBefore = entity.Quality;

        // Act
        Program.UpdateQuality(Items2);

        // Assert
        entity.Quality.Should().Be(qualityBefore-4);
    }
}