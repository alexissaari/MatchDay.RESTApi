using MatchDay.RESTApi.DatabaseLayer.Context;
using MatchDay.RESTApi.DatabaseLayer.Entities;
using MatchDay.RESTApi.DatabaseLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MatchDay.RESTApi.DatabaseLayer
{
    public class MatchDayRepository : IMatchDayRepository
    {
        public PlayerEntity? GetPlayer(int id)
        {
            using (var db = new SQLiteContext())
            {
                return db.Players.Include(x => x.Team).FirstOrDefault(x => x.Id == id);
            }
        }

        public CoachEntity? GetCoach(int id)
        {
            using (var db = new SQLiteContext())
            {
                return db.Coaches.Include(x => x.Team).FirstOrDefault(x => x.Id == id);
            }
        }

        public TeamEntity? GetTeam(int id)
        {
            using (var db = new SQLiteContext())
            {
                return db.Teams.Include(x => x.Players).Include(x => x.Coach).FirstOrDefault(x => x.Id == id);
            }
        }

        public void AddPlayer(PlayerEntity player)
        {
            using (var db = new SQLiteContext())
            {
                db.Players.Add(player);
                db.SaveChanges();
            }
        }

        public void AddCoach(CoachEntity coach)
        {
            using (var db = new SQLiteContext())
            {
                db.Coaches.Add(coach);
                db.SaveChanges();
            }
        }

        public void AddTeam(TeamEntity team)
        {
            using (var db = new SQLiteContext())
            {
                db.Teams.Add(team);
                db.SaveChanges();
            }
        }
    }
}
