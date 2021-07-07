namespace DarkBattle.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.ViewModels.Areas;
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

        public void AddAreas(AreaViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            var area = this.mapper.Map<Area>(model);

            this.data.Areas.Add(area);
            this.data.SaveChanges();
        }

        public void EditArea(AreaViewModel model)
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
        {
            var areas = this.data
                            .Areas
                            .Include(x => x.Creatures)
                            .Select(x => this.mapper.Map<AreasListViewModel>(x))
                            // .ProjectTo<AreasListViewModel>(mapper.ConfigurationProvider)
                            .ToList();
            return areas;
        }

        public bool DeleteArea(string id)
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
    }
}
