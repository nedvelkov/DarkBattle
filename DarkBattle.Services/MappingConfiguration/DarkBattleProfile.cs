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

    public class DarkBattleProfile:Profile
    {
        public DarkBattleProfile()
        {
            //Creature
            this.CreateMap<CreatureViewModel, Creature>()
                .ForMember(x => x.ImageUrl, y => y.MapFrom(i => i.ImageUrl));


            this.CreateMap<Creature, CreatureViewModel>()
                .ForMember(x => x.ImageUrl, y => y.MapFrom(i => i.ImageUrl));


            this.CreateMap<Creature, CreatureListViewModel>()
                .ForMember(x => x.Area, y => y.MapFrom(a => a.Area.Name));

            //Area


            this.CreateMap<Area, AreaViewModel>();
            this.CreateMap<AreaViewModel, Area>();



            //this.CreateMap<Area, AreasListViewModel>()
            //    .ForMember(x => x.MinLevel, y => y.MapFrom(l => l.MinLevelEnterence))
            //    .ForMember(x => x.MaxLevel, y => y.MapFrom(l => l.MaxLevelCreatures));
            ////.ReverseMap()
            ////.ForPath(x => x.Creatures.Count, y => y.MapFrom(l => l.CreaturesCount)); 
            ///
            this.CreateMap<Area, AreasListViewModel>()
                .ForMember(x=>x.MaxLevel,y=>y.MapFrom(i=>i.MaxLevelCreatures))
                .ForMember(x=>x.MinLevel,y=>y.MapFrom(i=>i.MinLevelEnterence))
                .ForMember(x => x.CreaturesCount, y => y.MapFrom(i => i.Creatures.Count));

            this.CreateMap<AreasListViewModel, Area>()
                .ForMember(x => x.MinLevelEnterence, y => y.MapFrom(l => l.MinLevel))
                .ForMember(x => x.MaxLevelCreatures, y => y.MapFrom(l => l.MaxLevel))
                .ForMember(x => x.Creatures, opt => opt.Ignore());
            //.ForMember(x => x.Creatures.Count, y => y.MapFrom(l => l.CreaturesCount));


            //Items
            this.CreateMap<ItemViewModel, Item>();

            this.CreateMap<Item, ItemViewModel>();


            this.CreateMap<Item, ItemListViewModel>();

            this.CreateMap<ItemListViewModel, Item>();

            //Consumable
            this.CreateMap<ConsumableViewModel, Consumable>();

            this.CreateMap<Consumable,ConsumableViewModel>();


            this.CreateMap<Consumable, ConsumableListViewModel>();

            this.CreateMap<ConsumableListViewModel, Consumable>();

            //Merchant
            this.CreateMap<MerchantViewModel, Merchant>();

            this.CreateMap<Merchant, MerchantViewModel>();


            this.CreateMap<Merchant, MerchantListViewModel>();

            this.CreateMap<MerchantListViewModel, Merchant>();

            //ChampionClass
            this.CreateMap<ChampionClassViewModel, ChampionClass>();

            this.CreateMap<ChampionClass, ChampionClassViewModel>();


            this.CreateMap<ChampionClass, ChampionClassListViewModel>();

            this.CreateMap<ChampionClassListViewModel, ChampionClass>();
        }
    }
}