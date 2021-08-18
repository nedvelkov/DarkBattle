namespace DarkBattle.Services.MappingConfiguration
{
    using AutoMapper;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.ServiceModels.Areas;
    using DarkBattle.Services.ServiceModels.ChampionClass;
    using DarkBattle.Services.ServiceModels.Champions;
    using DarkBattle.Services.ServiceModels.Consumables;
    using DarkBattle.Services.ServiceModels.Creatures;
    using DarkBattle.Services.ServiceModels.Items;
    using DarkBattle.Services.ServiceModels.Merchants;

    public class DarkBattleProfile:Profile
    {
        public DarkBattleProfile()
        {
            //Creature
            this.CreateMap<CreatureServiceModel, Creature>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            this.CreateMap<Creature, CreatureServiceModel>();


            this.CreateMap<Creature, CreatureServiceListModel>()
                .ForMember(x => x.Area, y => y.MapFrom(a => a.Area.Name));

            //Area
            this.CreateMap<Area, AreaServiceViewModel>();

            this.CreateMap<AreaServiceViewModel, Area>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            this.CreateMap<Area, AreaServiceViewModel>();
            this.CreateMap<AreaServiceViewModel, Area>();

            this.CreateMap<Area, AreaServiceListModelExtention>()
                .ForMember(x=>x.MaxLevel,y=>y.MapFrom(i=>i.MaxLevelCreatures))
                .ForMember(x=>x.MinLevel,y=>y.MapFrom(i=>i.MinLevelEnterence))
                .ForMember(x => x.CreaturesCount, y => y.MapFrom(i => i.Creatures.Count));

            this.CreateMap<AreaServiceListModelExtention, Area>()
                .ForMember(x => x.MinLevelEnterence, y => y.MapFrom(l => l.MinLevel))
                .ForMember(x => x.MaxLevelCreatures, y => y.MapFrom(l => l.MaxLevel))
                .ForMember(x => x.Creatures, opt => opt.Ignore());


            //Items
            this.CreateMap<ItemServiceModel, Item>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            this.CreateMap<Item, ItemServiceModel>();

            this.CreateMap<Item, ItemViewServiceModel>();

            //Gear

            this.CreateMap<Gear, GearServiceModel>()
                .ForMember(x => x.GearId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.ChampionId, y => y.MapFrom(x => x.ChampionId))
                .ForMember(x => x.EquipedItems, opt => opt.Ignore());



            //Consumable
            this.CreateMap<ConsumableViewServiceModel, Consumable>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            this.CreateMap<Consumable, ConsumableViewServiceModel>();


            //Merchant
            this.CreateMap<MerchantServiceModel, Merchant>()
                .ForMember(x => x.Id, opt => opt.Ignore());


            this.CreateMap<Merchant, MerchantServiceModel>();

            this.CreateMap<Merchant, MerchantChampionViewModel>();


            this.CreateMap<Merchant, MerchantServiceListModel>()
                .ForMember(x => x.ConsumableCount, y=>y.MapFrom(i=>i.Consumables.Count))
                .ForMember(x => x.ItemCount, y => y.MapFrom(i => i.Items.Count));

            //ChampionClass
            this.CreateMap<ChampionClassServiceModel, ChampionClass>()
                            .ForMember(x => x.Id, opt => opt.Ignore());

            this.CreateMap<ChampionClass, ChampionClassServiceModel>();


            this.CreateMap<ChampionClass, ChampionClassServiceListModel>();
            this.CreateMap<ChampionClassServiceListModel, ChampionClass>();

            this.CreateMap<ChampionClass, ChampionClassServiceListToChampionModel>();

            //AreaCreatures

            this.CreateMap<Creature, CreateureInAreaServiceModel>();

            this.CreateMap<Area, AreaServiceListModel>()
                .ForMember(x => x.MinLevel, y => y.MapFrom(i => i.MinLevelEnterence))
                .ForMember(x => x.MaxLevel, y => y.MapFrom(i => i.MaxLevelCreatures));


            //MerchantItems

            this.CreateMap<Item, ItemServiceListModel>();
            this.CreateMap<ItemServiceListModel, Item>();


            //Champions

            this.CreateMap<Champion, ChampionServiceModel>()
                .ForMember(x => x.ChampionId, y => y.MapFrom(i => i.Id))
                .ForMember(x => x.ChampionClassId, y => y.MapFrom(i => i.ChampionClassId))
                .ForMember(x => x.ChampionClass, y => y.MapFrom(i => i.ChampionClass.Name))
                .ForMember(x => x.ImageUrl, y => y.MapFrom(i => i.ChampionClass.ImageUrl));

            this.CreateMap<Champion, ChampionBarServiceModel>()
                .ForMember(x => x.ChampionId, y => y.MapFrom(i => i.Id))
                .ForMember(x => x.BaseHealth, y => y.MapFrom(i => i.ChampionClass.Health));
        }
    }
}