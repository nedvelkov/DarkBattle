namespace DarkBattle.Services.Interface
{
    using DarkBattle.Services.ServiceModels;
    public interface IBattleService
    {
        public BattleViewModel FightWithCreature(string championId, string areaId,string playerId,bool dungeon=false);
        public BattleViewModel TrainChampion(string championId, string playerId, string goldSpent);
    }
}
