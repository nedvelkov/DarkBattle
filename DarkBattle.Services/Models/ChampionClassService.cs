namespace DarkBattle.Services.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.ChampionClasses;

    public class ChampionClassService : IChampionClassService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public ChampionClassService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(ChampionClassViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            var championClass = this.mapper.Map<ChampionClass>(model);

            this.data.ChampionClasses.Add(championClass);
            this.data.SaveChanges();
        }

        public void Edit(ChampionClassViewModel model)
        {
            var championClass = this.data.ChampionClasses.Single(x => x.Id == model.Id);

            var properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == "Id")
                {
                    continue;
                }
                var value = prop.GetValue(model);
                var property = championClass.GetType().GetProperty(prop.Name);
                property.SetValue(championClass, value);
            }

            this.data.SaveChanges();
        }

        public ChampionClassViewModel GetClass(string id)
        {
            var area = this.mapper
                             .Map<ChampionClassViewModel>
                             (this.GetChampionClassById(id));

            return area;
        }

        public ICollection<ChampionClassListViewModel> ClassCollection()
        {
            var championClasses = this.data
                            .ChampionClasses
                            .ProjectTo<ChampionClassListViewModel>(mapper.ConfigurationProvider)
                            .ToList();

            return championClasses;
        }


        public bool Delete(string id)
        {
            var championClass = GetChampionClassById(id);
            if (championClass == null)
            {
                return false;
            }

            this.data.ChampionClasses.Remove(championClass);

            this.data.SaveChanges();

            return true;
        }

        private ChampionClass GetChampionClassById(string id)
        => this.data
            .ChampionClasses
            .FirstOrDefault(x => x.Id == id);
    }
}
