namespace DarkBattle.Services.Models
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using DarkBattle.Data;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.Home;
    using System.Collections.Generic;
    using System;

    public class PlayerService : IPlayerService
    {

        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;
        private readonly IStatisticService statisticService;

        public PlayerService(ApplicationDbContext data, IMapper mapper, IStatisticService statisticService)
        {
            this.data = data;
            this.mapper = mapper;
            this.statisticService = statisticService;
        }

        public void LogIn(string email)
        {
            var player = this.data.Users
                          .Single(x => x.Email == email);
            player.IsOnline = true;

            this.data.SaveChanges();
        }


        public void LogOut(string playerId)
        {
            var player = this.data.Users
                          .Single(x => x.Id == playerId);
            player.IsOnline = false;
            this.data.SaveChanges();
        }

        public AdminServiceControl AdminView()
        {
            var players = this.data
                             .Users
                             .Include(x => x.Champions)
                             .Select(this.mapper.Map<PlayerServiceModel>)
                             .ToList();

            var statistics = new Dictionary<string, int>();
            var totalPlayers = this.statisticService.TotalPlayers();
            statistics.Add("Total players", totalPlayers);

            var totalOnlinePlayers = this.statisticService.TotalOnlinePlayers();
            statistics.Add("Total online players", totalOnlinePlayers);

            var totalBanPlayers = this.statisticService.TotalBanPlayers();
            statistics.Add("Total ban players", totalBanPlayers);
            return new AdminServiceControl
            {
                Statistics = statistics,
                Players = players
            };

        }
        public void BanPlayer(string playerId)
        {
            var player = this.data.Users.FirstOrDefault(x => x.Id == playerId);
            player.IsBanned = true;
            this.data.SaveChanges();
        }

        public void RemoveBan(string playerId)
        {
            var player = this.data.Users.FirstOrDefault(x => x.Id == playerId);
            player.IsBanned = false;
            this.data.SaveChanges();
        }

        public bool IsBanned(string playerId)
        {
            var player = this.data.Users.FirstOrDefault(x => x.Id == playerId);

            return player.IsBanned;
        }

    }
}
