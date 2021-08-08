namespace DarkBattle.MappingConfiguration
{
    using AutoMapper;
    using DarkBattle.Data.Models;
    using DarkBattle.ViewModels.Creatures;
    using DarkBattle.ViewModels.Areas;
    using DarkBattle.ViewModels.Items;
    using DarkBattle.ViewModels.Consumables;
    using DarkBattle.ViewModels.Merchants;
    using DarkBattle.ViewModels.ChampionClasses;
    using DarkBattle.Services.ServiceModels;

    public class DarkBattleProfile:Profile
    {
        public DarkBattleProfile()
        {
            //Creature
            this.CreateMap<CreatureViewModel, Creature>()
                .ForMember(x => x.ImageUrl, y => y.MapFrom(i => i.ImageUrl));


            this.CreateMap<Creature, CreatureViewModel>()
                .ForMember(x => x.ImageUrl, y => y.MapFrom(i => i.ImageUrl));
            this.CreateMap<Creature, CreatureServiceModel>();


            this.CreateMap<Creature, CreatureListViewModel>()
                .ForMember(x => x.Area, y => y.MapFrom(a => a.Area.Name));

            //Area
            this.CreateMap<Area, AreaViewModel>();
            this.CreateMap<AreaViewModel, Area>();

            this.CreateMap<Area, AreaServiceViewModel>();
            this.CreateMap<AreaServiceViewModel, Area>();

            this.CreateMap<Area, AreasListViewModel>()
                .ForMember(x=>x.MaxLevel,y=>y.MapFrom(i=>i.MaxLevelCreatures))
                .ForMember(x=>x.MinLevel,y=>y.MapFrom(i=>i.MinLevelEnterence))
                .ForMember(x => x.CreaturesCount, y => y.MapFrom(i => i.Creatures.Count));

            this.CreateMap<AreasListViewModel, Area>()
                .ForMember(x => x.MinLevelEnterence, y => y.MapFrom(l => l.MinLevel))
                .ForMember(x => x.MaxLevelCreatures, y => y.MapFrom(l => l.MaxLevel))
                .ForMember(x => x.Creatures, opt => opt.Ignore());


            //Items
            this.CreateMap<ItemViewModel, Item>();

            this.CreateMap<Item, ItemViewModel>();


            this.CreateMap<Item, ItemListViewModel>();

            this.CreateMap<ItemListViewModel, Item>();

            this.CreateMap<Item, ItemViewServiceModel>();


            //Consumable
            this.CreateMap<ConsumableViewModel, Consumable>();

            this.CreateMap<Consumable,ConsumableViewModel>();

            this.CreateMap<Consumable, ConsumableViewServiceModel>();


            this.CreateMap<Consumable, ConsumableListViewModel>();

            this.CreateMap<ConsumableListViewModel, Consumable>();

            //Merchant
            this.CreateMap<MerchantViewModel, Merchant>();

            this.CreateMap<Merchant, MerchantViewModel>();

            this.CreateMap<Merchant, MerchantServiceModel>();


            this.CreateMap<Merchant, MerchantListViewModel>()
                .ForMember(x => x.ConsumableCount, y=>y.MapFrom(i=>i.Consumables.Count))
                .ForMember(x => x.ItemCount, y => y.MapFrom(i => i.Items.Count));

            //ChampionClass
            this.CreateMap<ChampionClassViewModel, ChampionClass>();
            this.CreateMap<ChampionClass, ChampionClassViewModel>();


            this.CreateMap<ChampionClass, ChampionClassListViewModel>();
            this.CreateMap<ChampionClassListViewModel, ChampionClass>();

            this.CreateMap<ChampionClassPresentationModel, ChampionClass>();
            this.CreateMap<ChampionClass, ChampionClassPresentationModel>();

            //AreaCreatures

            this.CreateMap<Creature, CreatureAreaViewModel>();
            this.CreateMap<Area, AreaCreatureViewModel>();

            //MerchantItems

            this.CreateMap<Item, ItemsListView>();
            this.CreateMap<ItemsListView, Item>();


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