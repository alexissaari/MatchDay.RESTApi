using MatchDay.RESTApi.DatabaseLayer.Context;
using MatchDay.RESTApi.DatabaseLayer.Entities;
using MatchDay.RESTApi.DatabaseLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MatchDay.RESTApi.DatabaseLayer
{
    public class MatchDayRepository : IMatchDayRepository
    {
        public PlayerEntity GetPlayer(int id)
        {
            using (var db = new SQLiteContext())
            {
                return db.Players
                    .Include(x => x.Team)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public void CreatePlayer(PlayerEntity player)
        {
            using (var db = new SQLiteContext())
            {
                db.Players.Add(player);
                db.SaveChanges();
            }
        }

        public void CreateTeam(TeamEntity team)
        {
            using (var db = new SQLiteContext())
            {
                db.Teams.Add(team);
                db.SaveChanges();
            }
        }
    }
}
