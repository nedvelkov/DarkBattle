namespace DarkBattle.MappingConfiguration
{
    using AutoMapper;
    using DarkBattle.Data.Models;
    using DarkBattle.ViewModels.Creatures;
    using DarkBattle.ViewModels.Areas;
    using DarkBattle.ViewModels.Items;
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

            this.CreateMap<AreaViewModel, Area>();

            this.CreateMap<Area, AreaViewModel>();



            //this.CreateMap<Area, AreasListViewModel>()
            //    .ForMember(x => x.MinLevel, y => y.MapFrom(l => l.MinLevelEnterence))
            //    .ForMember(x => x.MaxLevel, y => y.MapFrom(l => l.MaxLevelCreatures));
            ////.ReverseMap()
            ////.ForPath(x => x.Creatures.Count, y => y.MapFrom(l => l.CreaturesCount)); 

            this.CreateMap<AreasListViewModel, Area>()
                .ForMember(x => x.MinLevelEnterence, y => y.MapFrom(l => l.MinLevel))
                .ForMember(x => x.MaxLevelCreatures, y => y.MapFrom(l => l.MaxLevel))
                .ReverseMap()
                .ForPath(x => x.CreaturesCount, y => y.MapFrom(l => l.Creatures.Count));
            //.ForMember(x => x.Creatures.Count, y => y.MapFrom(l => l.CreaturesCount));


            //Items
            this.CreateMap<ItemViewModel, Item>();

            this.CreateMap<Item, ItemViewModel>();


            this.CreateMap<Item, ItemListViewModel>();

            this.CreateMap<ItemListViewModel, Item>();
        }
    }
}