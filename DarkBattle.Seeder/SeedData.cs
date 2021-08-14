namespace DarkBattle.Seeder
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class SeedData
    {
        private readonly ApplicationDbContext data;
        private readonly Random rand;

        public SeedData(ApplicationDbContext data)
        {
            this.data = data;
            this.rand = new Random();
        }

        public void Seed()
        {
            this.data.Database.EnsureCreated();

            this.SeedChampionClass();
            this.SeedAreas();
            this.SeedMerchant();
            this.SeedCreatures();
            this.SeedItems();
            this.SeedConsumables();
        }

        private void SeedChampionClass()
        {
            if (this.data.ChampionClasses.Any())
            {
                return;
            }

            var warrior = new ChampionClass
            {
                Name = "Warrior",
                Strenght = 13,
                Agility = 8,
                Health = 10,
                SpellPower = 5,
                ImageUrl = @"https://guides.gamepressure.com/guildwars2/gfx/word/978182984.jpg",
                Description = "Fearless warrior, with massive physical damage."
            };
            var mage = new ChampionClass
            {
                Name = "Mage",
                Strenght = 5,
                Agility = 8,
                Health = 7,
                SpellPower = 15,
                ImageUrl = @"https://rpgvaliant.com/wp-content/uploads/2017/02/Final-Fantasy-Explorers-Force-Black-Mage.jpg",
                Description = "Wise wizard with powers over nature."
            };
            var rouge = new ChampionClass
            {
                Name = "Rouge",
                Strenght = 6,
                Agility = 14,
                Health = 6,
                SpellPower = 9,
                ImageUrl = @"https://i.pinimg.com/originals/7b/7f/1f/7b7f1fcdcaf7453614dfdf448ff284ce.jpg",
                Description = "Stealth and silent, rouge attack from shadows."
            };
            var archer = new ChampionClass
            {
                Name = "Archer",
                Strenght = 10,
                Agility = 14,
                Health = 8,
                SpellPower = 9,
                ImageUrl = @"https://vignette.wikia.nocookie.net/raiderz-game/images/2/29/Archer.png",
                Description = "Agile, quick and silent archer always find his prey."
            };
            var classes = new List<ChampionClass>()
            {
                warrior,mage,rouge,archer
            };

            this.data.ChampionClasses.AddRange(classes);
            this.data.SaveChanges();
        }

        private void SeedAreas()
        {
            if (this.data.Areas.Any())
            {
                return;
            }
            var valey = new Area
            {
                Name = "Valey",
                MinLevelEnterence = 1,
                MaxLevelCreatures = 10,
                Description = "Initial area",
                ImageUrl = @"https://jooinn.com/images/beautiful-valley-30.jpg"
            };
            var winterLands = new Area
            {
                Name = "Winter lands",
                MinLevelEnterence = 11,
                MaxLevelCreatures = 20,
                Description = "Fight in ice and snow",
                ImageUrl = @"https://wallpapercave.com/wp/wp2063495.jpg"
            };
            var scourgeLands = new Area
            {
                Name = "Scourge lands",
                MinLevelEnterence = 21,
                MaxLevelCreatures = 30,
                Description = "Land of fire and battles",
                ImageUrl = @"https://i2-prod.scunthorpetelegraph.co.uk/incoming/article1808998.ece/ALTERNATES/s1200/0_IMG-9529.jpg"
            };
            var heaven = new Area
            {
                Name = "Heaven",
                MinLevelEnterence = 31,
                MaxLevelCreatures = 40,
                Description = "Land of angles and dreams",
                ImageUrl = @"https://wallup.net/wp-content/uploads/2018/09/27/27382-fantasy-dream-art-cg-digital-art-manipulation-magic-clouds-sky-vehicles-ship-boat-waterfall-islands-tower-island-stars-moon.jpg"
            };

            var areas = new List<Area>()
            {
                valey,winterLands,scourgeLands,heaven
            };

            this.data.Areas.AddRange(areas);
            this.data.SaveChanges();
        }

        private void SeedMerchant()
        {
            if (this.data.Merchants.Any())
            {
                return;
            }
            var vulcan = new Merchant
            {
                Name = "Vulcan",
                Description = "Blacksmith",
                ImageUrl = @"https://vignette2.wikia.nocookie.net/godsofrome/images/1/13/Vulcan.full.jpg",
            };

            var hephaestus = new Merchant
            {
                Name = "Hephaestus",
                Description = "Blacksmith",
                ImageUrl = @"https://www.znanje.org/i/i2014/2014iii09/2014iii0917/slike/hefest4.jpg",
            };

            var goibhniu = new Merchant
            {
                Name = "Goibhniu",
                Description = "Blacksmith",
                ImageUrl = @"https://workingtheflame.com/wp-content/uploads/2019/11/goibniu-blacksmith.jpg",
            };

            var butcher = new Merchant
            {
                Name = "Butcher",
                Description = "Meal vendor",
                ImageUrl = @"https://logos.co/1024/royalty-free-clip-art-vector-logo-of-a-woodcut-red-butcher-holding-a-cleaver-knife-over-meats-by-patrimonio-9301.jpg",
            };

            var fisherman = new Merchant
            {
                Name = "Fisherman",
                Description = "Fish vendor",
                ImageUrl = @"https://i.pinimg.com/originals/18/ec/39/18ec399178b542ece92c94b70141dcf4.png",
            };

            var baker = new Merchant
            {
                Name = "Baker",
                Description = "Bread vendor",
                ImageUrl = @"https://i.pinimg.com/originals/97/52/f5/9752f5c25d609b2047f71ac2371f110f.jpg",
            };

            var merchants = new List<Merchant>()
            {
                vulcan,hephaestus,goibhniu,butcher,fisherman,baker
            };

            this.data.Merchants.AddRange(merchants);
            this.data.SaveChanges();
        }

        private void SeedCreatures()
        {
            if (this.data.Creatures.Any())
            {
                return;
            }

            //Valey
            var bear = new Creature
            {
                Name = "Bear",
                ImageUrl = "https://vignette2.wikia.nocookie.net/leagueoflegends/images/d/d7/Volibear_OriginalLoading.jpg",
                Attack = 5,
                Defense = 10,
                Block = 5,
                Health = 20,
                Gold = 10,
                Expirience = 15,
                Level = 1,
            };

            var wolf = new Creature
            {
                Name = "Wolf",
                ImageUrl = "https://pm1.narvii.com/7289/5aa40cd3abf3b6e889c4d8265a003609055c6b63r1-474-474v2_hq.jpg",
                Attack = 7,
                Defense = 13,
                Block = 6,
                Health = 20,
                Gold = 13,
                Expirience = 16,
                Level = 2,
            };

            var warewolf = new Creature
            {
                Name = "Warewolf",
                ImageUrl = "http://2.bp.blogspot.com/_rAJe_CypsZs/TGzLZf4Xw9I/AAAAAAAAAj8/hWP6UywMPPE/w1200-h630-p-k-no-nu/werewolf6.jpg",
                Attack = 12,
                Defense = 10,
                Block = 5,
                Health = 22,
                Gold = 15,
                Expirience = 17,
                Level = 3,
            };

            var scarabaeus = new Creature
            {
                Name = "Scarabaeus",
                ImageUrl = "https://gamepedia.cursecdn.com/wowpedia/a/a5/Silithid_Scarab.png",
                Attack = 9,
                Defense = 20,
                Block = 8,
                Health = 30,
                Gold = 17,
                Expirience = 18,
                Level = 5,
            };

            var darkSliser = new Creature
            {
                Name = "DarkSliser",
                ImageUrl = "https://i.pinimg.com/736x/37/e3/e6/37e3e67f1e0c114cb7031c23504edce8.jpg",
                Attack = 50,
                Defense = 60,
                Block = 25,
                Health = 80,
                Gold = 80,
                Expirience = 30,
                Level = 10,
            };

            //Winter

            var whiteBear = new Creature
            {
                Name = "Snow bear",
                ImageUrl = "http://pngimg.com/uploads/polar_bear/polar_bear_PNG30.png",
                Attack = 35,
                Defense = 40,
                Block = 15,
                Health = 80,
                Gold = 20,
                Expirience = 20,
                Level = 11,
            };

            var iceWolf = new Creature
            {
                Name = "Ice wolf",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2Foriginals%2Ffe%2F19%2F3e%2Ffe193e77a3f190aa7da2a73772aca0a2.png",
                Attack = 50,
                Defense = 45,
                Block = 10,
                Health = 90,
                Gold = 22,
                Expirience = 21,
                Level = 12,
            };
            var whiteWarewolf = new Creature
            {
                Name = "White warewolf",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2Foriginals%2F04%2F42%2Fa4%2F0442a43de50c95ac5e34f645b964e871.jpg",
                Attack = 70,
                Defense = 60,
                Block = 25,
                Health = 90,
                Gold = 25,
                Expirience = 20,
                Level = 13,
            };

            var iceSnake = new Creature
            {
                Name = "Ice snake",
                ImageUrl = "http://curseddice.weebly.com/uploads/3/2/8/5/3285221/1475095.jpg?412",
                Attack = 108,
                Defense = 130,
                Block = 9,
                Health = 150,
                Gold = 35,
                Expirience = 35,
                Level = 20,
            };

            //Scorge

            var demon = new Creature
            {
                Name = "Demon",
                ImageUrl = "https://vignette3.wikia.nocookie.net/dragons-crown/images/0/0e/Arch_Demon.png",
                Attack = 110,
                Defense = 150,
                Block = 30,
                Health = 200,
                Gold = 80,
                Expirience = 30,
                Level = 21,
            };

            var shadow = new Creature
            {
                Name = "Shadow",
                ImageUrl = "https://i.pinimg.com/564x/d4/e0/e0/d4e0e0c7f5d871841136d8b6f79c4ba8.jpg",
                Attack = 115,
                Defense = 170,
                Block = 35,
                Health = 208,
                Gold = 75,
                Expirience = 35,
                Level = 22,
            };

            var choGa = new Creature
            {
                Name = "ChoGA",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fd181w3hxxigzvh.cloudfront.net%2Fwp-content%2Fuploads%2F2017%2F09%2FChoGath_Splash_Tile_0.jpg",
                Attack = 180,
                Defense = 150,
                Block = 48,
                Health = 220,
                Gold = 74,
                Expirience = 32,
                Level = 23,
            };

            var darkVigius = new Creature
            {
                Name = "Dark Vigius",
                ImageUrl = "https://i.pinimg.com/564x/51/ce/c5/51cec5fdb7a6effa89c3ed09c00baf42.jpg",
                Attack = 200,
                Defense = 250,
                Block = 80,
                Health = 400,
                Gold = 120,
                Expirience = 50,
                Level = 30,
            };

            //Heaven

            var archanngel = new Creature
            {
                Name = "Archanngel",
                ImageUrl = "external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fsfwallpaper.com%2Fimages%2Farchangel-wallpaper-10.jpg",
                Attack = 215,
                Defense = 280,
                Block = 95,
                Health = 380,
                Gold = 100,
                Expirience = 55,
                Level = 31,
            };
            var grifone = new Creature
            {
                Name = "Bear",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fs-media-cache-ak0.pinimg.com%2F736x%2Fdc%2Fda%2Fbd%2Fdcdabd8666bf44dd62875bc819af0432--mythical-birds-fantasy-creatures.jpg",
                Attack = 225,
                Defense = 250,
                Block = 90,
                Health = 380,
                Gold = 110,
                Expirience = 60,
                Level = 32,
            };
            var vigeos = new Creature
            {
                Name = "Vigeos",
                ImageUrl = @"https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fvignette4.wikia.nocookie.net%2Fdragons-world%2Fimages%2F0%2F0d%2FLightDragonBaby.png",
                Attack = 240,
                Defense = 280,
                Block = 95,
                Health = 400,
                Gold = 150,
                Expirience = 70,
                Level = 35,
            };
            var skyScraper = new Creature
            {
                Name = "Skyscraper",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fvignette.wikia.nocookie.net%2Fcharacter-stats-and-profiles%2Fimages%2F2%2F2c%2FBlue_eyes_ultimate_dragon.pngg",
                Attack = 300,
                Defense = 350,
                Block = 110,
                Health = 500,
                Gold = 200,
                Expirience = 80,
                Level = 40,
            };


            var creatures = new List<Creature>()
            {
                bear,wolf,warewolf,scarabaeus,darkSliser,whiteBear,whiteWarewolf,iceWolf,iceSnake,demon,shadow,choGa,darkVigius,archanngel,grifone,vigeos,skyScraper
            };

            foreach (var area in this.data.Areas.ToList())
            {
                var creaturesToArea = creatures.Where(x => x.Level >= area.MinLevelEnterence && x.Level <= area.MaxLevelCreatures).ToList();
                AddCreaturesToArea(creaturesToArea, area);
            }

            this.data.Creatures.AddRange(creatures);
            this.data.SaveChanges();
        }

        private void SeedItems()
        {
            if (this.data.Items.Any())
            {
                return;
            }
            //Chestplate basic
            var basicArmorWarrior = new Item
            {
                Name = "Basic armor",
                ImageUrl = "https://ashesofcreation.wiki/images/3/32/dcb18dd8-0ab8-45d6-ae4a-9f07a7d33bd3-0.png",
                Attack = 1,
                Defense = 1,
                Equipped = false,
                ObtainBy = "Warrior",
                Type = "Chestplate",
                Value = 5,
            };
            var basicArmorMage = new Item
            {
                Name = "Basic armor",
                ImageUrl = "https://ashesofcreation.wiki/images/3/32/dcb18dd8-0ab8-45d6-ae4a-9f07a7d33bd3-0.png",
                Attack = 1,
                Defense = 1,
                Equipped = false,
                ObtainBy = "Mage",
                Type = "Chestplate",
                Value = 5,
            };
            var basicArmorRouge = new Item
            {
                Name = "Basic armor",
                ImageUrl = "https://ashesofcreation.wiki/images/3/32/dcb18dd8-0ab8-45d6-ae4a-9f07a7d33bd3-0.png",
                Attack = 1,
                Defense = 1,
                Equipped = false,
                ObtainBy = "Rouge",
                Type = "Chestplate",
                Value = 5,
            };
            var basicArmorArcher = new Item
            {
                Name = "Basic armor",
                ImageUrl = "https://ashesofcreation.wiki/images/3/32/dcb18dd8-0ab8-45d6-ae4a-9f07a7d33bd3-0.png",
                Attack = 1,
                Defense = 1,
                Equipped = false,
                ObtainBy = "Archer",
                Type = "Chestplate",
                Value = 5,
            };
            //Helm basic
            var basicHelmWarrior = new Item
            {
                Name = "Basic helm",
                ImageUrl = "https://ashesofcreation.wiki/images/1/19/EpicClothHelmIcon.png",
                Attack = 1,
                Defense = 3,
                Equipped = false,
                ObtainBy = "Warrior",
                Type = "Helm",
                Value = 5,
            };
            var basicHelmMage = new Item
            {
                Name = "Basic helm",
                ImageUrl = "https://ashesofcreation.wiki/images/1/19/EpicClothHelmIcon.png",
                Attack = 1,
                Defense = 3,
                Equipped = false,
                ObtainBy = "Mage",
                Type = "Helm",
                Value = 5,
            };
            var basicHelmRouge = new Item
            {
                Name = "Basic helm",
                ImageUrl = "https://ashesofcreation.wiki/images/1/19/EpicClothHelmIcon.png",
                Attack = 1,
                Defense = 3,
                Equipped = false,
                ObtainBy = "Rouge",
                Type = "Helm",
                Value = 5,
            };
            var basicHelmArcher = new Item
            {
                Name = "Basic helm",
                ImageUrl = "https://ashesofcreation.wiki/images/1/19/EpicClothHelmIcon.png",
                Attack = 1,
                Defense = 3,
                Equipped = false,
                ObtainBy = "Archer",
                Type = "Helm",
                Value = 5,
            };
            //Gloves basic
            var basicGlovesWarrior = new Item
            {
                Name = "Basic gloves",
                ImageUrl = "https://ashesofcreation.wiki/images/c/cf/f6bab455-aaf6-43c2-8de3-298312e484b1-0.png",
                Attack = 1,
                Defense = 2,
                Equipped = false,
                ObtainBy = "Warrior",
                Type = "Gloves",
                Value = 5,
            };
            var basicGlovesMage = new Item
            {
                Name = "Basic gloves",
                ImageUrl = "https://ashesofcreation.wiki/images/c/cf/f6bab455-aaf6-43c2-8de3-298312e484b1-0.png",
                Attack = 1,
                Defense = 2,
                Equipped = false,
                ObtainBy = "Mage",
                Type = "Gloves",
                Value = 5,
            };
            var basicGlovesRouge = new Item
            {
                Name = "Basic gloves",
                ImageUrl = "https://ashesofcreation.wiki/images/c/cf/f6bab455-aaf6-43c2-8de3-298312e484b1-0.png",
                Attack = 1,
                Defense = 2,
                Equipped = false,
                ObtainBy = "Rouge",
                Type = "Gloves",
                Value = 5,
            };
            var basicGlovesArcher = new Item
            {
                Name = "Basic gloves",
                ImageUrl = "https://ashesofcreation.wiki/images/c/cf/f6bab455-aaf6-43c2-8de3-298312e484b1-0.png",
                Attack = 1,
                Defense = 2,
                Equipped = false,
                ObtainBy = "Archer",
                Type = "Gloves",
                Value = 5,
            };
            //Pants basic
            var basicPantsWarrior = new Item
            {
                Name = "Basic pants",
                ImageUrl = "https://ashesofcreation.wiki/images/2/29/PlatePantsIcon.png",
                Attack = 1,
                Defense = 4,
                Equipped = false,
                ObtainBy = "Warrior",
                Type = "Pants",
                Value = 5,
            };
            var basicPantsMage = new Item
            {
                Name = "Basic pants",
                ImageUrl = "https://ashesofcreation.wiki/images/2/29/PlatePantsIcon.png",
                Attack = 1,
                Defense = 4,
                Equipped = false,
                ObtainBy = "Mage",
                Type = "Pants",
                Value = 5,
            };
            var basicPantsRouge = new Item
            {
                Name = "Basic pants",
                ImageUrl = "https://ashesofcreation.wiki/images/2/29/PlatePantsIcon.png",
                Attack = 1,
                Defense = 4,
                Equipped = false,
                ObtainBy = "Rouge",
                Type = "Pants",
                Value = 5,
            };
            var basicPantsArcher = new Item
            {
                Name = "Basic pants",
                ImageUrl = "https://ashesofcreation.wiki/images/2/29/PlatePantsIcon.png",
                Attack = 1,
                Defense = 4,
                Equipped = false,
                ObtainBy = "Archer",
                Type = "Pants",
                Value = 5,
            };
            //Boots basic
            var basicBootsWarrior = new Item
            {
                Name = "Basic boots",
                ImageUrl = "https://ashesofcreation.wiki/images/8/8a/469bb455-f97b-4fa3-83fc-d0872cef848e-0.png",
                Attack = 2,
                Defense = 2,
                Equipped = false,
                ObtainBy = "Warrior",
                Type = "Boots",
                Value = 5,
            };
            var basicBootsMage = new Item
            {
                Name = "Basic boots",
                ImageUrl = "https://ashesofcreation.wiki/images/8/8a/469bb455-f97b-4fa3-83fc-d0872cef848e-0.png",
                Attack = 2,
                Defense = 2,
                Equipped = false,
                ObtainBy = "Mage",
                Type = "Boots",
                Value = 5,
            };
            var basicBootsRouge = new Item
            {
                Name = "Basic boots",
                ImageUrl = "https://ashesofcreation.wiki/images/8/8a/469bb455-f97b-4fa3-83fc-d0872cef848e-0.png",
                Attack = 2,
                Defense = 2,
                Equipped = false,
                ObtainBy = "Rouge",
                Type = "Boots",
                Value = 5,
            };
            var basicBootsArcher = new Item
            {
                Name = "Basic boots",
                ImageUrl = "https://ashesofcreation.wiki/images/8/8a/469bb455-f97b-4fa3-83fc-d0872cef848e-0.png",
                Attack = 2,
                Defense = 2,
                Equipped = false,
                ObtainBy = "Archer",
                Type = "Boots",
                Value = 5,
            };

            //CheatWeapon
            var cheatWeaponWarrior = new Item
            {
                Name = "Cheat",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.pngpix.com%2Fwp-content%2Fuploads%2F2016%2F10%2FPNGPIX-COM-Bitcoin-PNG-Image.png&f=1&nofb=1",
                Attack = 999,
                Defense = 99,
                Equipped = false,
                ObtainBy = "Warrior",
                Type = "Weapon",
                Value = 99,
            };
            var cheatWeaponMage = new Item
            {
                Name = "Cheat",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.pngpix.com%2Fwp-content%2Fuploads%2F2016%2F10%2FPNGPIX-COM-Bitcoin-PNG-Image.png&f=1&nofb=1",
                Attack = 999,
                Defense = 99,
                Equipped = false,
                ObtainBy = "Mage",
                Type = "Weapon",
                Value = 99,
            };
            var cheatWeaponRouge = new Item
            {
                Name = "Cheat",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.pngpix.com%2Fwp-content%2Fuploads%2F2016%2F10%2FPNGPIX-COM-Bitcoin-PNG-Image.png&f=1&nofb=1",
                Attack = 999,
                Defense = 99,
                Equipped = false,
                ObtainBy = "Rouge",
                Type = "Weapon",
                Value = 99,
            };
            var cheatWeaponArcher = new Item
            {
                Name = "Cheat",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.pngpix.com%2Fwp-content%2Fuploads%2F2016%2F10%2FPNGPIX-COM-Bitcoin-PNG-Image.png&f=1&nofb=1",
                Attack = 999,
                Defense = 99,
                Equipped = false,
                ObtainBy = "Archer",
                Type = "Weapon",
                Value = 99,
            };


            var basicItemToMerchant = new List<Item>
            {
                basicArmorWarrior,basicArmorArcher,basicArmorMage,basicArmorRouge,
                basicBootsArcher,basicBootsMage,basicBootsRouge,basicBootsWarrior,
                basicGlovesArcher,basicGlovesMage,basicGlovesRouge,basicGlovesWarrior,
                basicHelmArcher,basicHelmMage,basicHelmRouge,basicHelmWarrior,basicPantsArcher,
                basicPantsMage,basicPantsRouge,basicPantsWarrior,
                cheatWeaponArcher,cheatWeaponMage,cheatWeaponRouge,cheatWeaponWarrior
            };
            foreach (var item in basicItemToMerchant)
            {
                item.RequiredLevel = 1;
            }
            var blacksmiths = this.data.Merchants.Where(x => x.Description == "Blacksmith").ToList();

            var basicItemsCount = basicItemToMerchant.Count;
            var blacksmitsCount = blacksmiths.Count;
            var perItems = basicItemsCount / blacksmitsCount;
            int notEqualItems = basicItemsCount % blacksmitsCount != 0 ? basicItemsCount % blacksmitsCount : 0;
            for (int i = 0; i < blacksmitsCount; i++)
            {
                var merchant = blacksmiths[i];
                var skip = i * perItems;
                if (i == blacksmitsCount - 1)
                {
                    perItems += notEqualItems;
                }
                var itemsToMerchant = basicItemToMerchant.Skip(skip).Take(perItems).ToList();
                AddItemsToMerchant(itemsToMerchant, merchant);
            }

            //Shild
            var shieldWarrior = new Item
            {
                Name = "Steel shield",
                ImageUrl = "https://ashesofcreation.wiki/images/c/c8/ShieldIcon.png",
                Attack = 1,
                Defense = 6,
                Equipped = false,
                ObtainBy = "Warrior",
                Type = "Shield",
                Value = 10,
                RequiredLevel = 3,
            };
            var shieldMage = new Item
            {
                Name = "Proto shield",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwww.worldmuonline.com%2Fwp-content%2Fuploads%2F2020%2F11%2Felhazshield.jpg",
                Attack = 1,
                Defense = 4,
                Equipped = false,
                ObtainBy = "Mage",
                Type = "Shield",
                Value = 10,
                RequiredLevel = 3,
            };
            //Weapons-basic
            var staf = new Item
            {
                Name = "Simple staf",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fdb4sgowjqfwig.cloudfront.net%2Fimages%2F959144%2Fwalking_stick_5.jpg&f=1&nofb=1",
                Attack = 3,
                Defense = 1,
                Equipped = false,
                ObtainBy = "Mage",
                Type = "Weapon",
                Value = 12,
                RequiredLevel = 1,
            };

            var mace = new Item
            {
                Name = "Simple mace",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwww.swordsandarmor.com%2Fimages%2FMACE_Spiked_926795_small.Jpg&f=1&nofb=1",
                Attack = 3,
                Defense = 1,
                Equipped = false,
                ObtainBy = "Rouge",
                Type = "Weapon",
                Value = 12,
                RequiredLevel = 1,
            };

            var axe = new Item
            {
                Name = "Axe",
                ImageUrl = "https://ashesofcreation.wiki/images/5/5d/AxeIcon.png",
                Attack = 3,
                Defense = 1,
                Equipped = false,
                ObtainBy = "Warrior",
                Type = "Weapon",
                Value = 12,
                RequiredLevel = 1,
            };

            var bow = new Item
            {
                Name = "Simple mace",
                ImageUrl = "https://i.pinimg.com/564x/30/19/2f/30192f889a4b2e5dc130e8d1fd322a0c.jpg",
                Attack = 3,
                Defense = 1,
                Equipped = false,
                ObtainBy = "Archer",
                Type = "Weapon",
                Value = 12,
                RequiredLevel = 1,
            };

            var itemsToCreaturesValey = new List<Item>
            {
                shieldMage,shieldWarrior,staf,mace,axe,bow,staf
            };

            var maxLevel = this.data.Areas.FirstOrDefault(x => x.Name == "Valey").MaxLevelCreatures;

            var creatures = this.data
                              .Areas
                              .Include(x => x.Creatures)
                              .FirstOrDefault(x => x.Name == "Valey")
                              .Creatures
                              .Where(x => x.Level != maxLevel)
                              .ToList();
            AddItemToCreatures(itemsToCreaturesValey, creatures, 2);

            //Weapons-dungeonDrop
            var aArchangel = new Item
            {
                Name = "AArchangel",
                ImageUrl = "https://www.guiamuonline.com/imagenes/comojugar/archangel/staff.jpg",
                Attack = 15,
                Defense = 5,
                Equipped = false,
                ObtainBy = "Mage",
                Type = "Weapon",
                Value = 25,
                RequiredLevel = 10,
            };

            var huracane = new Item
            {
                Name = "Huracane",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fmu-warzone.webcindario.com%2Fimages%2Fweapons%2Fsm%2Fimperial%2520staff.jpg&f=1&nofb=1",
                Attack = 15,
                Defense = 7,
                Equipped = false,
                ObtainBy = "Rouge",
                Type = "Weapon",
                Value = 25,
                RequiredLevel = 10,
            };

            var aaBlade = new Item
            {
                Name = "AABlade",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi1.wp.com%2Fguiasmu.com%2Fwp-content%2Fuploads%2F2018%2F09%2FHolyangel-Staff.jpg%3Ffit%3D181%252C280%26ssl%3D1&f=1&nofb=1",
                Attack = 20,
                Defense = 15,
                Equipped = false,
                ObtainBy = "Warrior",
                Type = "Weapon",
                Value = 35,
                RequiredLevel = 10,
            };

            var heartBreaker = new Item
            {
                Name = "Hearth breaker",
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fupload.webzen.com%2FFiles%2FClara%2Fportal-inquiry-notice%2F2018%2F08%2F21%2F--636704240462552810.jpg&f=1&nofb=1",
                Attack = 18,
                Defense = 7,
                Equipped = false,
                ObtainBy = "Archer",
                Type = "Weapon",
                Value = 30,
                RequiredLevel = 10,
            };

            var itemToDungeonValey = new List<Item>
            {
                aArchangel,aaBlade,huracane,heartBreaker
            };
            var creaturesInDungeon = this.data
                              .Areas
                              .Include(x => x.Creatures)
                              .FirstOrDefault(x => x.Name == "Valey")
                              .Creatures
                              .Where(x => x.Level == maxLevel)
                              .ToList();
            AddItemToCreatures(itemToDungeonValey, creaturesInDungeon, 8);

            this.data.Items.AddRange(basicItemToMerchant);
            this.data.Items.AddRange(itemsToCreaturesValey);
            this.data.Items.AddRange(itemToDungeonValey);

            this.data.SaveChanges();
        }

        private void SeedConsumables()
        {
            if (this.data.Consumables.Any())
            {
                return;
            }
            var bread = new Consumable
            {
                Name = "Bread",
                RestoreHealth = 5,
                Value = 2,
                ImageUrl = "http://www.clker.com/cliparts/m/C/s/s/k/6/bread-md.png"
            };
            var bread2 = new Consumable
            {
                Name = "Bread",
                RestoreHealth = 15,
                Value = 5,
                ImageUrl = "http://www.clker.com/cliparts/m/C/s/s/k/6/bread-md.png"
            };
            var bread3 = new Consumable
            {
                Name = "Bread",
                RestoreHealth = 25,
                Value = 10,
                ImageUrl = "http://www.clker.com/cliparts/m/C/s/s/k/6/bread-md.png"
            };
            var cake = new Consumable
            {
                Name = "Cake",
                RestoreHealth = 7,
                Value = 5,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fstatic.abeka.com%2FShared%2FABeka%2FProductImages%2FClipArt%2F250589%2F150x150y160fx160fh-w%2F250589-Pink-Bithday-Cake-with-six-candles-line-pdf.png&f=1&nofb=1"
            };
            var cake2 = new Consumable
            {
                Name = "Cake",
                RestoreHealth = 18,
                Value = 10,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fstatic.abeka.com%2FShared%2FABeka%2FProductImages%2FClipArt%2F250589%2F150x150y160fx160fh-w%2F250589-Pink-Bithday-Cake-with-six-candles-line-pdf.png&f=1&nofb=1"
            };
            var cake3 = new Consumable
            {
                Name = "CheatCake",
                RestoreHealth = 999,
                Value = 99,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fstatic.abeka.com%2FShared%2FABeka%2FProductImages%2FClipArt%2F250589%2F150x150y160fx160fh-w%2F250589-Pink-Bithday-Cake-with-six-candles-line-pdf.png&f=1&nofb=1"
            };
            var baked = new List<Consumable>
            {
                bread,bread2,bread3,cake,cake2,cake3
            };
            var baker = this.data.Merchants.First(X => X.Description == "Bread vendor");

            AddConsumablesToMerchant(baked, baker);

            var fish = new Consumable
            {
                Name = "Fish",
                RestoreHealth = 5,
                Value = 2,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fstatic.abeka.com%2FShared%2FABeka%2FProductImages%2FClipArt%2F212288%2F150x150y160fx160fh-w%2F212288-Goldfish-smiling-line-png.png&f=1&nofb=1"
            };

            var fish2 = new Consumable
            {
                Name = "Fish",
                RestoreHealth = 15,
                Value = 7,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fstatic.abeka.com%2FShared%2FABeka%2FProductImages%2FClipArt%2F212288%2F150x150y160fx160fh-w%2F212288-Goldfish-smiling-line-png.png&f=1&nofb=1"
            };

            var fish3 = new Consumable
            {
                Name = "Fish",
                RestoreHealth = 25,
                Value = 12,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fstatic.abeka.com%2FShared%2FABeka%2FProductImages%2FClipArt%2F212288%2F150x150y160fx160fh-w%2F212288-Goldfish-smiling-line-png.png&f=1&nofb=1"
            };

            var tuna= new Consumable
            {
                Name = "Tuna",
                RestoreHealth = 7,
                Value = 7,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.identicards.com%2Fproductcart%2Fpc%2Fcatalog%2Fimages%2FEYP%2Fdecals%2F512-1019b-150.jpg&f=1&nofb=1"
            };
            var tuna2 = new Consumable
            {
                Name = "Tuna",
                RestoreHealth = 27,
                Value = 15,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.identicards.com%2Fproductcart%2Fpc%2Fcatalog%2Fimages%2FEYP%2Fdecals%2F512-1019b-150.jpg&f=1&nofb=1"
            };
            var tuna3 = new Consumable
            {
                Name = "CheatTuna",
                RestoreHealth = 999,
                Value = 99,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.identicards.com%2Fproductcart%2Fpc%2Fcatalog%2Fimages%2FEYP%2Fdecals%2F512-1019b-150.jpg&f=1&nofb=1"
            };
            var seaFood = new List<Consumable>
            {
                fish,fish2,fish3,tuna,tuna2,tuna3
            };
            var fisheman= this.data.Merchants.First(X => X.Description == "Fish vendor");

            AddConsumablesToMerchant(seaFood, fisheman);

            var meal= new Consumable
            {
                Name = "Steak",
                RestoreHealth = 5,
                Value = 2,
                ImageUrl = "https://cdn.shopify.com/s/files/1/1968/5479/products/CowboySteakForSite_1024x1024.jpg"
            };
            
            var meal2 = new Consumable
            {
                Name = "Steak",
                RestoreHealth = 12,
                Value = 4,
                ImageUrl = "https://cdn.shopify.com/s/files/1/1968/5479/products/CowboySteakForSite_1024x1024.jpg"
            };

            var meal3 = new Consumable
            {
                Name = "Steak",
                RestoreHealth = 24,
                Value = 8,
                ImageUrl = "https://cdn.shopify.com/s/files/1/1968/5479/products/CowboySteakForSite_1024x1024.jpg"
            };

            var chicken= new Consumable
            {
                Name = "Chicken",
                RestoreHealth = 10,
                Value = 7,
                ImageUrl = "https://www.aldi.us/fileadmin/_processed_/7/7/csm_090711_S_40526_KW_FreshWholeChicken_Hero_D_bde3dad430.jpg"
            };

            var chicken2 = new Consumable
            {
                Name = "Chicken",
                RestoreHealth = 20,
                Value = 10,
                ImageUrl = "https://www.aldi.us/fileadmin/_processed_/7/7/csm_090711_S_40526_KW_FreshWholeChicken_Hero_D_bde3dad430.jpg"
            };
            var chicken3 = new Consumable
            {
                Name = "CheatChicken",
                RestoreHealth = 999,
                Value = 99,
                ImageUrl = "https://www.aldi.us/fileadmin/_processed_/7/7/csm_090711_S_40526_KW_FreshWholeChicken_Hero_D_bde3dad430.jpg"
            };

            var mealFood = new List<Consumable>
            {
                meal,meal2,meal3,chicken,chicken2,chicken3
            };

            var butcher = this.data.Merchants.First(X => X.Description == "Meal vendor");

            AddConsumablesToMerchant(mealFood, butcher);

            this.data.Consumables.AddRange(baked);
            this.data.Consumables.AddRange(seaFood);
            this.data.Consumables.AddRange(mealFood);

            this.data.SaveChanges();
        }

        private void AddCreaturesToArea(IEnumerable<Creature> creatures, Area area)
        {
            foreach (var creature in creatures)
            {
                area.Creatures.Add(creature);
            }
        }
        private void AddItemsToMerchant(IEnumerable<Item> items, Merchant merchant)
        {
            foreach (var item in items)
            {
                merchant.Items.Add(item);
            }
        }

        private void AddConsumablesToMerchant(IEnumerable<Consumable> consumables, Merchant merchant)
        {
            foreach (var consumable in consumables)
            {
                merchant.Consumables.Add(consumable);
            }
        }

        private void AddItemToCreatures(IEnumerable<Item> items, ICollection<Creature> creatures, int itemsPerCreature)
        {
            foreach (var creature in creatures)
            {
                var itemsNotAssign = items.Where(x => x.CreatureId == null).ToList();
                if (itemsNotAssign.Count < itemsPerCreature)
                {
                    foreach (var item in itemsNotAssign)
                    {
                        creature.Items.Add(item);
                        return;
                    }
                }

                var tmp = RandNums(itemsPerCreature, itemsNotAssign.Count);

                for (int i = 0; i < itemsPerCreature; i++)
                {
                    var index = tmp[i];
                    var item = itemsNotAssign[index];
                    creature.Items.Add(item);
                }
            }
        }

        private List<int> RandNums(int count, int range)
        {
            var result = new List<int>();
            while (true)
            {
                var num = this.rand.Next(0, range);
                if (result.Contains(num))
                {
                    continue;
                }
                result.Add(num);
                if (result.Count == count)
                {
                    return result;
                }
            }
        }
    }
}
