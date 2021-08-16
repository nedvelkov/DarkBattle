namespace DarkBattle.Services.Models
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.ChampionClass;

    public class ChampionClassService : IChampionClassService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public ChampionClassService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(ChampionClassServiceModel model)
        {
            var championClass = this.mapper.Map<ChampionClass>(model);

            this.data.ChampionClasses.Add(championClass);
            this.data.SaveChanges();
        }

        public void Edit(ChampionClassServiceModel model)
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

        public ChampionClassServiceModel GetClass(string id)
                      => this.mapper
                             .Map<ChampionClassServiceModel>
                             (this.GetChampionClassById(id));


        public ICollection<ChampionClassServiceListModel> ClassCollection()
                        => this.data
                            .ChampionClasses
                            .ProjectTo<ChampionClassServiceListModel>(mapper.ConfigurationProvider)
                            .ToList();


        public ICollection<string> ChampionClassCollection()
            => this.data.ChampionClasses.Select(x => x.Name).ToList();


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
