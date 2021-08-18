namespace DarkBattle.Tests.Services
{
    using System;
    using System.Linq;

    using Xunit;
    using AutoMapper;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.Services.MappingConfiguration;
    using DarkBattle.Services.ServiceModels.Consumables;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Champions;
    using Microsoft.EntityFrameworkCore;
   public class CreatureConsumableServiceTest
    {
        [Fact]
        public void TestAddConsumable()
        {
            //Arrange
            const string consumableId = "apple";
            const string creatureid = "wolf";

            using var data = DatabaseMock.Instance;

            var creature = new Creature { Id = creatureid };
            var consumable = new Consumable { Id = consumableId };
            data.Creatures.Add(creature);
            data.Consumables.Add(consumable);
            data.SaveChanges();

            var creatureConsumablesService = new CreatureConsumablesService(data);
            //Act
            var result = creatureConsumablesService.Add(consumableId, creatureid);
            var test1 = data.Creatures.Include(x=>x.Consumables).First();
            var test2 = data.Consumables.First();
            //Assert
            Assert.True(result);
            Assert.True(test1.Consumables.Contains(consumable));
            Assert.True(test2.CreatureId == creatureid);

        }

        [Fact]
        public void TestRemoveConsumable()
        {
            //Arrange
            const string consumableId = "apple";
            const string creatureid = "wolf";

            using var data = DatabaseMock.Instance;

            var creature = new Creature { Id = creatureid };
            var consumable = new Consumable { Id = consumableId,Creature=creature,CreatureId=creatureid };
            data.Consumables.Add(consumable);
            data.SaveChanges();

            var creatureConsumablesService = new CreatureConsumablesService(data);
            //Act
            var result = creatureConsumablesService.Remove(consumableId, creatureid);
            var test1 = data.Creatures.Include(x => x.Consumables).First();
            var test2 = data.Consumables.First();
            //Assert
            Assert.True(result);
            Assert.False(test1.Consumables.Contains(consumable));
            Assert.True(test2.CreatureId == null);

        }
    }
}
