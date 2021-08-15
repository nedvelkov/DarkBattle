namespace DarkBattle.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels;
    using Microsoft.Extensions.Configuration;
    using DarkBattle.Services.ServiceModels.Creatures;

    public class BattleService : IBattleService
    {
        private readonly ApplicationDbContext data;
        private readonly IChampionService championService;
        private readonly IAreaCreatureService areaCreatureService;
        private readonly IMapper mapper;
        private readonly IConfiguration cofing;
        private readonly ICreatureService creatureService;
        private readonly IAreaService areaService;


        public BattleService(ApplicationDbContext data,
                            IChampionService championService,
                            IMapper mapper,
                            IAreaCreatureService areaCreatureService,
                            IConfiguration cofing,
                            ICreatureService creatureService,
                            IAreaService areaService)
        {
            this.data = data;
            this.championService = championService;
            this.mapper = mapper;
            this.areaCreatureService = areaCreatureService;
            this.cofing = cofing;
            this.creatureService = creatureService;
            this.areaService = areaService;
        }

        public BattleViewModel FightWithCreature(string championId, string areaId, string playerId, bool dungeon = false)
        {
            var champion = this.championService.Details(championId, playerId);
            var creatureList = this.creatureService.CreatureInArea(areaId);
            var area = this.areaService.AreaForCreatures(areaId);

            var areaImg = dungeon ? @"https://ashesofcreation.wiki/images/b/b9/dungeons-leak-2.jpg" : this.data.Areas.First(x => x.Id == areaId).ImageUrl;

            var critAtRound = LuckyRounds(champion.CritChanse);
            var blockAtRound = LuckyRounds(champion.Block);

            var maxLevel = dungeon ? area.MaxLevel : area.MaxLevel - 1;
            var creatures = dungeon ? SortList(x => x.Level == maxLevel, creatureList) : SortList(x => x.Level < maxLevel, creatureList);
            var rand = new Random();
            var creatureIndex = rand.Next(0, creatures.Count);
            var creatureId = creatures[creatureIndex].Id;
            var creature = this.data.Creatures
                .Where(x => x.Id == creatureId)
                .Select(this.mapper.Map<CreatureServiceModel>)
                .FirstOrDefault();
            var creatureBlockAtRound = LuckyRounds(creature.Block);


            var result = new BattleViewModel
            {
                ChampionImg = champion.Champion.ImageUrl,
                CreatureName = creature.Name,
                CreatureImg = creature.ImageUrl,
                AreaName = area.Name,
                AreaId = area.Id,
                AreaImg = areaImg
            };

            var battleReport = new List<string>();
            for (int i = 0; i <= 9; i++)
            {
                battleReport.Add($"Round {i + 1}:");

                int championDmg = 0;

                if (creatureBlockAtRound.Contains(i))
                {
                    battleReport.Add($"   {creature.Name} block attack from {champion.Champion.Name}!");
                    continue;
                }

                championDmg = (int)Math.Floor(champion.Attack - creature.Defense < 0 ? 0 : champion.Attack - creature.Defense);

                if (critAtRound.Contains(i))
                {
                    championDmg = (int)Math.Floor(champion.Attack * 2 - creature.Defense < 0 ? 0 : champion.Attack * 2 - creature.Defense);
                    battleReport.Add($"    Lucky shot for {champion.Champion.Name}!");
                }

                battleReport.Add($"   {champion.Champion.Name} strikes {creature.Name} with {championDmg} damage!");

                creature.Health -= championDmg;

                if (creature.Health <= 0)
                {
                    battleReport.Add($"{champion.Champion.Name} defeats {creature.Name}");
                    break;
                }

                if (blockAtRound.Contains(i))
                {
                    battleReport.Add($"   {champion.Champion.Name} block attack from {creature.Name}!");
                    continue;
                }

                int creatureDmg = 0;

                creatureDmg = (int)Math.Floor(creature.Attack - champion.Defense * 0.5 < 0 ? 0 : creature.Attack - champion.Defense);

                battleReport.Add($"   {creature.Name} strikes {champion.Champion.Name} with {creatureDmg} damage!");

                champion.CurrentHealth -= creatureDmg;

                if (champion.CurrentHealth <= 0)
                {
                    battleReport.Add($"{champion.Champion.Name} run away from {creature.Name} defeated");
                    champion.CurrentHealth = 1;

                    ChangeCurrentHealt(championId, champion.CurrentHealth);

                    return BattleModel(result, championId, playerId, battleReport);
                }
            }

            if (champion.CurrentHealth > 0 && creature.Health > 0)
            {
                battleReport.Add($"No one wins today.Train your champion and come back.");

                ChangeCurrentHealt(championId, champion.CurrentHealth);

                return BattleModel(result, championId, playerId, battleReport);
            }

            if (champion.CurrentHealth > 1 && creature.Health <= 0)
            {
                var items = ObtainItems(creatureId, championId);
                var consumables = ObtainConsumables(creatureId, championId);
                var totalGold = creature.Gold + items.Item2 + consumables.Item2;
                var expirience = champion.Champion.Level < area.MaxLevel ? creature.Expirience : creature.Expirience / 2;

                battleReport.Add($"{champion.Champion.Name} wins :");
                battleReport.Add($"   {expirience} expirience");
                battleReport.Add($"   {totalGold} gold");
                if (items.Item1.Count > 0 || consumables.Item1.Count > 0)
                {
                    battleReport.Add($"{champion.Champion.Name} get ");
                    var itemsToReport = items.Item1.Count > 0 ? string.Join(", ", items.Item1) : "0";
                    var consumablesToReport = consumables.Item1.Count > 0 ? string.Join(", ", consumables.Item1) : "0";
                    battleReport.Add($"{itemsToReport} items and {consumablesToReport} consumables.");
                }
                AddExp(championId, expirience);
                AddGold(championId, totalGold);
                AddItems(championId, creatureId);
                AddConsumables(championId, creatureId);
            }
            ChangeCurrentHealt(championId, champion.CurrentHealth);
            LevelUp(championId);
            return BattleModel(result, championId, playerId, battleReport);
        }

        public BattleViewModel TrainChampion(string championId, string playerId, string goldSpent)
        {
            var championGold = this.data.Champions.FirstOrDefault(x => x.Id == championId).Gold;
            if (goldSpent == null || int.Parse(goldSpent)>championGold)
            {
                return new BattleViewModel
                {
                    Champion = this.championService.ChampionBar(championId, playerId),
                    AreaName = "Training Grounds",
                    AreaImg = @"https://gamepedia.cursecdn.com/wowpedia/f/f6/Training_Grounds.jpg",
                };
            }
            var champion = this.championService.Details(championId, playerId);

            var critAtRound = LuckyRounds(champion.CritChanse);

            var battleReport = new List<string>();

            for (int i = 0; i <= 9; i++)
            {
                battleReport.Add($"Round {i + 1}:");

                var championDmg = champion.Attack;

                if (critAtRound.Contains(i))
                {
                    championDmg *= 2;
                    battleReport.Add($"    Lucky shot for {champion.Champion.Name}!");
                }

                battleReport.Add($"   {champion.Champion.Name} strikes dummy with {(int)championDmg} damage!");
            }

            var exp = int.Parse(goldSpent);

            battleReport.Add($"{champion.Champion.Name} wins :");
            battleReport.Add($"   {exp} expirience");

            AddExp(championId, exp);
            PayTraining(championId, exp);
            LevelUp(championId);

            return new BattleViewModel
            {
                Champion = this.championService.ChampionBar(championId, playerId),
                ChampionImg = champion.Champion.ImageUrl,
                CreatureName = "Dummy",
                CreatureImg = @"http://1.bp.blogspot.com/-z_DsO-LXaa8/UJcP14gakgI/AAAAAAAAAFc/Y9dDnClBEjw/s320/Training_Dummy_500.jpg",
                AreaName = "Training Grounds",
                AreaImg = @"https://gamepedia.cursecdn.com/wowpedia/f/f6/Training_Grounds.jpg",
                BattleLog = battleReport

            };
        }


        private void AddItems(string championId, string creatureId)
        {
            var champion = Champion(championId);
            var listItems = ListOfItems(creatureId);
            listItems = listItems.Where(x =>
            {
                var championList = x.Champions;
                if (championList.Any(z => z.Id == championId))
                {
                    return false;
                }
                return true;
            }).ToList();
            foreach (var item in listItems)
            {
                champion.Items.Add(item);
            }
            this.data.SaveChanges();
        }

        private void AddConsumables(string championId, string creatureId)
        {
            var champion = Champion(championId);
            var consumables = ListOfConsumables(creatureId);
            consumables = consumables.Where(x => x.Champions.Any(x => x.Id != championId)).ToList();
            foreach (var item in consumables)
            {
                champion.ChampionConsumables.Add(item);
            }
            this.data.SaveChanges();
        }

        private void AddGold(string championId, int gold)
        {
            var champion = Champion(championId);
            champion.Gold += gold;
            this.data.SaveChanges();
        }

        private void AddExp(string championId, int exp)
        {
            var champion = Champion(championId);
            champion.Experience += exp;
            this.data.SaveChanges();
        }

        public void PayTraining(string championId, int gold)
        {
            var champion = Champion(championId);
            champion.Gold -=gold;
            this.data.SaveChanges();
        }

        private void ChangeCurrentHealt(string championId, int currentHealt)
        {
            var champion = Champion(championId);
            champion.CurrentHealth = currentHealt;
            this.data.SaveChanges();
        }

        private Champion Champion(string championId)
            => this.data
                   .Champions
                    .Include(x => x.ChampionClass)
                   .FirstOrDefault(x => x.Id == championId);

        private BattleViewModel BattleModel(BattleViewModel model, string championId, string playerId, List<string> battleLog)
        {
            model.Champion = this.championService.ChampionBar(championId, playerId);
            model.BattleLog = battleLog;
            return model;
        }

        private void LevelUp(string championId)
        {
            var champion = Champion(championId);
            while (true)
            {
                var maxLevel = int.Parse(this.cofing.GetSection("GameSettings:MaxLevel").Value);
                if (champion.Level == maxLevel)
                {
                    break;
                }
                var levelIncrement = this.cofing.GetSection("GameSettings:LevelIncrement")
                                 .GetChildren()
                                 .Select(x => int.Parse(x.Value))
                                 .ToList();


                var index = champion.Level == levelIncrement.Count ? levelIncrement.Count - 1 : champion.Level - 1;
                var expForLevelUp = levelIncrement[index];
                if (champion.Experience >= expForLevelUp)
                {
                    champion.Level++;
                    champion.CurrentHealth = champion.Level * champion.ChampionClass.Health;
                    this.data.SaveChanges();

                }
                else
                {
                    break;
                }
            }
        }

        private List<int> LuckyRounds(double chanse)
        {
            while (chanse > 1)
            {
                chanse /= 10;
            }
            var random = new Random();
            var luckyRounds = new List<int>();
            var luck = Math.Ceiling(chanse);
            while (luck > 0)
            {
                var round = random.Next(0, 9);
                if (luckyRounds.Contains(round))
                {
                    continue;
                }
                luckyRounds.Add(round);
                luck--;
            }
            return luckyRounds.OrderBy(x => x).ToList();
        }

        private List<T> SortList<T>(Func<T, bool> func, ICollection<T> collection)
                 => collection.Where(func).ToList();

        private Tuple<List<string>, int> ObtainItems(string creatureId, string championId)
        {
            var items = ListOfItems(creatureId);
            var obtainItems = items.Where(x =>
            {
                var championList = x.Champions;
                if (championList.Any(z => z.Id == championId))
                {
                    return false;
                }
                return true;
            }).Select(x => x.Name).ToList();
            var obtainGold = items.Where(x =>
            {
                var championList = x.Champions;
                if (championList.Any(z => z.Id == championId))
                {
                    return false;
                }
                return true;
            }).Select(x => x.Value).Sum();
            return new Tuple<List<string>, int>(obtainItems, obtainGold);
        }
        private Tuple<List<string>, int> ObtainConsumables(string creatureId, string championId)
        {
            var consumables = ListOfConsumables(creatureId);
            var obtainItems = consumables.Where(x => x.Champions.Any(x => x.Id != championId)).Select(x => x.Name).ToList();
            var obtainGold = consumables.Where(x => x.Champions.Any(x => x.Id == championId)).Select(x => x.Value).Sum();
            return new Tuple<List<string>, int>(obtainItems, obtainGold);
        }
        private List<Item> ListOfItems(string creatureId)
            => this.data.Items
                        .Include(x => x.Champions)
                        .Where(x => x.CreatureId == creatureId)
                         .ToList();
        private List<Consumable> ListOfConsumables(string creatureId)
            => this.data.Consumables
                        .Include(x => x.Champions)
                        .Where(x => x.CreatureId == creatureId)
                         .ToList();

    }
}
