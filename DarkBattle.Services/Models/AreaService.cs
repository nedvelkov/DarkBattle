namespace DarkBattle.Services.Models
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using AutoMapper;


    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.Areas;
    using System;

    public class AreaService : IAreaService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public AreaService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(AreaServiceViewModel model)
        {
            var area = this.mapper.Map<Area>(model);
            area.Id = Guid.NewGuid().ToString();
            this.data.Areas.Add(area);
            this.data.SaveChanges();
        }

        public void Edit(AreaServiceViewModel model)
        {
            var area = this.data.Areas.Single(x => x.Id == model.Id);
            var newArea = this.mapper.Map<Area>(model);

            var properties = newArea.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == "Id")
                {
                    continue;
                }
                var value = prop.GetValue(newArea);
                var property = area.GetType().GetProperty(prop.Name);
                property.SetValue(area, value);
            }

            this.data.SaveChanges();
        }


        public AreaServiceViewModel GetArea(string id)
             => this.mapper
                    .Map<AreaServiceViewModel>
                    (this.GetAreaById(id));
 

        public AreaServiceListModel AreaForCreatures(string areaId)
            => this.mapper
                .Map<AreaServiceListModel>
                (this.GetAreaById(areaId));

        public ICollection<AreaServiceListModelExtention> AreasCollection()
            => GetAreaCollection<AreaServiceListModelExtention>();


        public ICollection<AreaServiceViewModel> AreaServiceCollection()
                => GetAreaCollection<AreaServiceViewModel>();


        public bool Delete(string id)
        {
            var area = GetAreaById(id);
            if (area == null)
            {
                return false;
            }

            this.data.Areas.Remove(area);

            this.data.SaveChanges();

            return true;
        }

        private Area GetAreaById(string id)
                => this.data
                    .Areas
                    .FirstOrDefault(x => x.Id == id);

        private ICollection<T> GetAreaCollection<T>()
            => this.data.Areas
                        .Include(x => x.Creatures)
                        .Select(this.mapper.Map<T>)
                        .ToList();
    }
}
