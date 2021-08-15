namespace DarkBattle.Services.Models
{

    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;


    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels;
    using DarkBattle.Services.ServiceModels.Merchants;

    public class MerchantService : IMerchantService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;
        private readonly IChampionService championService;
        public MerchantService(ApplicationDbContext data, IMapper mapper, IChampionService championService)
        {
            this.data = data;
            this.mapper = mapper;
            this.championService = championService;
        }

        public void Add(MerchantServiceModel model)
        {
            var merchant = this.mapper.Map<Merchant>(model);
            this.data.Merchants.Add(merchant);
            this.data.SaveChanges();
        }


        public void Edit(MerchantServiceModel model)
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

        public MerchantServiceModel GetMerchant(string id)
                => this.mapper
                        .Map<MerchantServiceModel>
                        (this.GetMerchantById(id));


        public ICollection<MerchantServiceListModel> MerchantsCollection()
                    =>this.data
                            .Merchants
                            .Include(x=>x.Items)
                            .Include(x=>x.Consumables)
                            .ProjectTo<MerchantServiceListModel>(mapper.ConfigurationProvider)
                            .ToList();


        public string MerchantName(string merchantId)
            => this.data.Merchants.FirstOrDefault(x => x.Id == merchantId).Name;


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

        public MerchantMarketViewModel AllMerchants(string championId, string playerId)
        {
            var champion = this.championService.ChampionBar(championId, playerId);
            if (champion == null)
            {
                return null;
            }
            var merchants = this.data
                                .Merchants
                                .Include(x => x.Items)
                                .Include(x => x.Consumables)
                                .Select(this.mapper.Map<MerchantChampionViewModel>)
                                .ToList();
            ;
            return new MerchantMarketViewModel
            {
                Champion = champion,
                Merchants = merchants
            };
        }

        private Merchant GetMerchantById(string id)
                        => this.data
                            .Merchants
                            .FirstOrDefault(x => x.Id == id);
    }
}
