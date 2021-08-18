namespace DarkBattle.Tests.Services
{
    using System.Linq;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;

   public class CreatureItemsServiceTest
    {
        [Fact]
        public void TestAddItem()
        {
            //Arrange
            const string itemId = "apple";
            const string creatureid = "wolf";

            using var data = DatabaseMock.Instance;

            var creature = new Creature { Id = creatureid };
            var item = new Item { Id = itemId };
            data.Creatures.Add(creature);
            data.Items.Add(item);
            data.SaveChanges();

            var creatureItemsService = new CreatureItemsService(data);
            //Act
            var result = creatureItemsService.Add(itemId, creatureid);
            var test1 = data.Creatures.Include(x => x.Items).First();
            var test2 = data.Items.First();
            //Assert
            Assert.True(result);
            Assert.True(test1.Items.Contains(item));
            Assert.True(test2.CreatureId == creatureid);

        }

        [Fact]
        public void TestRemoveConsumable()
        {
            //Arrange
            const string itemId = "apple";
            const string creatureid = "wolf";

            using var data = DatabaseMock.Instance;

            var creature = new Creature { Id = creatureid };
            var item = new Item { Id = itemId, Creature = creature, CreatureId = creatureid };
            data.Items.Add(item);
            data.SaveChanges();

            var creatureItemsService = new CreatureItemsService(data);

            //Act
            var result = creatureItemsService.Remove(itemId, creatureid);
            var test1 = data.Creatures.Include(x => x.Items).First();
            var test2 = data.Items.First();
            //Assert
            Assert.True(result);
            Assert.False(test1.Items.Contains(item));
            Assert.True(test2.CreatureId == null);

        }
    }
}
