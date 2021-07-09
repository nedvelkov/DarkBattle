

namespace DarkBattle.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.ViewModels.Merchants;
    public  class MerchantService : IMerchantService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public MerchantService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(MerchantViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            var merchant = this.mapper.Map<Merchant>(model);
            this.data.Merchants.Add(merchant);
            this.data.SaveChanges();
        }


        public void Edit(MerchantViewModel model)
        {
            var merchant = this.data.Merchants.Single(x => x.Id == model.Id);

            var properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == "Id")
                {
                    continue;
                }
                var value = prop.GetValue(model);
                var property = merchant.GetType().GetProperty(prop.Name);
                property.SetValue(merchant, value);
            }

            this.data.SaveChanges();
        }

        public MerchantViewModel GetMerchant(string id)
        {
            var creature = this.mapper
                        .Map<MerchantViewModel>
                        (this.GetMerchantById(id));

            return creature;
        }

        public ICollection<MerchantListViewModel> MerchantsCollection()
        {
            var merchants = this.data
                            .Merchants
                            .ProjectTo<MerchantListViewModel>(mapper.ConfigurationProvider)
                            .ToList();

            return merchants;
        }

        public bool Delete(string id)
        {
            var merchant = GetMerchantById(id);
            if (merchant == null)
            {
                return false;
            }

            this.data.Merchants.Remove(merchant);

            this.data.SaveChanges();

            return true;
        }

        private Merchant GetMerchantById(string id)
                        => this.data
                            .Merchants
                            .FirstOrDefault(x => x.Id == id);
    }
}
