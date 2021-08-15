namespace DarkBattle.Services.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using AutoMapper;


    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.ViewModels.Areas;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels;

    public class AreaService : IAreaService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public AreaService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(AreaViewModel model)
        {
            var area = this.mapper.Map<Area>(model);

            this.data.Areas.Add(area);
            this.data.SaveChanges();
        }

        public void Edit(AreaViewModel model)
        {
            var area = this.data.Areas.Single(x => x.Id == model.Id);

            var properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == "Id")
                {
                    continue;
                }
                var value = prop.GetValue(model);
                var property = area.GetType().GetProperty(prop.Name);
                property.SetValue(area, value);
            }

            this.data.SaveChanges();
        }


        public AreaViewModel GetArea(string id)
        {
            var area = this.mapper
                .Map<AreaViewModel>
                (this.GetAreaById(id));
            return area;
        }

        public ICollection<AreasListViewModel> AreasCollection()
            => GetAreaCollection<AreasListViewModel>();


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
