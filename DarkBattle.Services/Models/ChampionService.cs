namespace DarkBattle.Services.Models
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using AutoMapper;
    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.Champions;
    using DarkBattle.Services.ServiceModels.ChampionClass;
    using DarkBattle.Services.ServiceModels.Consumables;
    using DarkBattle.Services.ServiceModels.Items;

    public class ChampionService : IChampionService
    {
        private readonly IConfiguration cofing;
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;


        public ChampionService(IConfiguration cofing, ApplicationDbContext data, IMapper mapper)
        {
            this.cofing = cofing;
            this.data = data;
            this.mapper = mapper;
        }

        public ICollection<ChampionClassServiceListToChampionModel> GetChampionClasses()
            => this.data.ChampionClasses.Select(this.mapper.Map<ChampionClassServiceListToChampionModel>).ToList();


        public bool CreateChampion(string name, string championClassId, string playerId)
        {
            if (this.data.ChampionClasses.Any(x => x.Id == championClassId) == false)
            {
                return false;
            }

            var player = GetPlayer(playerId);
            if (player == null)
            {
                return false;
            }

            var initialGold = int.Parse(this.cofing.GetSection("GameSettings").GetSection("InnitialGold").Value);
            var initialLevel = int.Parse(this.cofing.GetSection("GameSettings").GetSection("InnitialLevel").Value);
            var initialExpirience = int.Parse(this.cofing.GetSection("GameSettings").GetSection("InnitialExpirience").Value);

            var baseChampionClass = GetChampionClass(championClassId);
            var initialHealt = baseChampionClass.Health * initialLevel;


            player.Champions.Add(new Champion
            {
                Name = name,
                Experience = initialExpirience,
                Gold = initialGold,
                Level = initialLevel,
                PlayerId = playerId,
                CurrentHealth = initialHealt,
                ChampionClassId = championClassId
            });



            this.data.SaveChanges();

            return true;
        }

        public ICollection<ChampionServiceModel> ChampionCollection(string playerId)
                     => this.data
                            .Users
                            .Include(x => x.Champions)
                            .ThenInclude(y => y.ChampionClass)
                            .FirstOrDefault(x => x.Id == playerId)
                            .Champions
                            .Select(this.mapper.Map<ChampionServiceModel>)
                            .ToList();

        public bool EquipItem(string championId, string itemId)
        {
            var champion = GetChampion(championId);
            var item = GetItem(itemId);

            if (ItemBelongToChampion(championId, item) == false)
            {
                return false;
            }

            if (CanEquipItem(champion.ChampionClass.Name, item.ObtainBy) == false)
            {
                return false;
            }

            var gear = champion.Gear.Items;

            if (gear.Any(x => x.Type == item.Type))
            {
                var equipedItem = gear.First(x => x.Type == item.Type);
                gear.Remove(equipedItem);
                equipedItem.Equipped = false;
            }

            gear.Add(item);
            item.Equipped = true;

            this.data.SaveChanges();

            return true;
        }

        public bool RemoveItem(string championId, string itemId)
        {
            var champion = GetChampion(championId);
            var item = GetItem(itemId);

            if (ItemBelongToChampion(championId, item) == false)
            {
                return false;
            }

            var gear = champion.Gear.Items;
            if (gear.Any(x => x.Id == itemId))
            {
                gear.Remove(item);
                item.Equipped = false;

            }

            this.data.SaveChanges();

            return true;
        }
        public bool SellItem(string championId, string itemId)
        {
            var champion = GetChampion(championId);
            var item = GetItem(itemId);

            if (ItemBelongToChampion(championId, item) == false)
            {
                return false;
            }
            if (item.Equipped)
            {
                return false;
            }


            item.Champions.Remove(champion);

            champion.Gold += item.Value;

            this.data.SaveChanges();

            return true;
        }

        public bool DeleteChampion(string championId, string playerId)
        {
            var player = GetPlayer(playerId);
            if (player == null)
            {
                return false;
            }

            var champion = GetChampion(championId);

            if (champion == null)
            {
                return false;
            }


            player.Champions.Remove(champion);

            this.data.Champions.Remove(champion);
            this.data.SaveChanges();

            return true;
        }

        public bool Consume(string championId, string consumableId)
        {
            var champion = GetChampion(championId);
            var consumable = this.data.Consumables.Include(x => x.Champions).FirstOrDefault(x => x.Id == consumableId);
            if (consumable.Champions.Any(x => x.Id == championId) == false)
            {
                return false;
            }
            var health = consumable.RestoreHealth;
            var maxHealth = champion.ChampionClass.Health * champion.Level;
            if (champion.CurrentHealth + health > maxHealth)
            {
                champion.CurrentHealth = maxHealth;
            }
            else
            {
                champion.CurrentHealth += health;
            }
            consumable.Champions.Remove(champion);
            this.data.SaveChanges();
            return true;
        }


        public ChampionDetailServiceModel Details(string championId, string playerId)
        {
            var champion = Champion(championId, playerId);
            if (champion == null)
            {
                return null;
            }
            var baseChampionClass = GetChampionClass(champion.ChampionClassId);
            var playerChampion = GetChampion(championId);

            var gear = GetGear(championId);

            var items = ItemInInventory(gear, ItemCollection(championId));

            var consumables = ConsumablesInInventory(championId);

            var details = new ChampionDetailServiceModel
            {
                Champion = champion,
                Strenght = baseChampionClass.Strenght * champion.Level,
                CurrentHealth = playerChampion.CurrentHealth,
                MaxHealth = baseChampionClass.Health * champion.Level,
                Agility = baseChampionClass.Agility * champion.Level,
                SpellPower = baseChampionClass.SpellPower * champion.Level,
                Description = baseChampionClass.Description,
                Items = items,
                Consumables = consumables,
                Gear = gear
            };

            return details;
        }

        public ChampionBarServiceModel ChampionBar(string championId, string playerId)
        {

            var champion = this.data.Champions
                .Include(x => x.ChampionClass)
                .Where(x => x.Id == championId && x.PlayerId == playerId)
                .Select(this.mapper.Map<ChampionBarServiceModel>)
                .FirstOrDefault();

            var levelIncrement = this.cofing.GetSection("GameSettings:LevelIncrement")
                                         .GetChildren()
                                         .Select(x => int.Parse(x.Value))
                                         .ToList();


            var index = champion.Level == levelIncrement.Count ? levelIncrement.Count - 1 : champion.Level - 1;
            champion.ExperienceForLevelUp = levelIncrement[index];
            champion.MaxHealth = champion.BaseHealth * champion.Level;

            return champion;
        }

        private ChampionServiceModel Champion(string championId, string playerId = null)
        {
            if (playerId == null)
            {
                return null;
            }
            else
            {
                return this.data
                            .Users
                            .Include(x => x.Champions)
                            .ThenInclude(y => y.ChampionClass)
                            .FirstOrDefault(x => x.Id == playerId)
                            .Champions
                            .Select(this.mapper.Map<ChampionServiceModel>)
                            .FirstOrDefault(x => x.ChampionId == championId);
            }
        }

        private bool ItemBelongToChampion(string championId, Item item)
        {
            if (item.Champions.Any(x => x.Id == championId) == false)
            {
                return false;
            }

            return true;
        }

        private bool CanEquipItem(string className, string obtainBy)
        {
            if (obtainBy != className)
            {
                return false;
            }

            return true;
        }

        private Player GetPlayer(string playerId)
           => this.data.Users.FirstOrDefault(x => x.Id == playerId);

        private Champion GetChampion(string championId)
           => this.data
                  .Champions
                  .Include(x => x.ChampionClass)
                  .Include(x => x.Gear)
                   .ThenInclude(x => x.Items)
                  .FirstOrDefault(x => x.Id == championId);
        private Item GetItem(string itemId)
           => this.data
                  .Items
                  .Include(x => x.Champions)
                  .FirstOrDefault(x => x.Id == itemId);

        private void AddGear(string championId)
        {
            var gear = new Gear { ChampionId = championId };
            this.data.Gears.Add(gear);
            this.data.SaveChanges();
        }

        private ICollection<ItemViewServiceModel> ItemInInventory(GearServiceModel gearService, ICollection<ItemViewServiceModel> items)
        {
            var result = items;
            foreach (var gear in gearService.EquipedItems)
            {
                if (result.Any(x => x.Id == gear.Id))
                {
                    var item = result.First(x => x.Id == gear.Id);
                    result.Remove(item);
                }
            }
            return result;
        }

        private ICollection<ConsumableViewServiceModel> ConsumablesInInventory(string championId)
            => this.data
                    .Consumables
                    .Include(x => x.Champions)
                    .Where(x => x.Champions.Any(x => x.Id == championId))
                     .Select(this.mapper.Map<ConsumableViewServiceModel>)
                    .ToList();

        private GearServiceModel GetGear(string championId)
        {

            var gear = this.data
                            .Gears
                            .Where(x => x.ChampionId == championId)
                            .Select(this.mapper.Map<GearServiceModel>)
                            .FirstOrDefault();

            if (gear == null)
            {
                AddGear(championId);
                gear = GetGear(championId);
            }
            var items = this.data.Gears
                                    .Include(x => x.Items)
                                    .FirstOrDefault(x => x.Id == gear.GearId)
                                    .Items
                                    .Select(this.mapper.Map<ItemViewServiceModel>).ToList();
            gear.EquipedItems = items;
            return gear;

        }

        private ChampionClass GetChampionClass(string championClassId)
           => this.data.ChampionClasses.FirstOrDefault(x => x.Id == championClassId);

        private ICollection<ItemViewServiceModel> ItemCollection(string championId)
            => this.data.Champions
                        .Include(x => x.Items)
                        .FirstOrDefault(x => x.Id == championId)
                        .Items
                        .Select(this.mapper.Map<ItemViewServiceModel>)
                        .ToList();
    }
}
