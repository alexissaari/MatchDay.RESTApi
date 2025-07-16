using MatchDay.RESTApi.DatabaseLayer.Context;
using MatchDay.RESTApi.DatabaseLayer.Entities;
using MatchDay.RESTApi.DatabaseLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MatchDay.RESTApi.DatabaseLayer
{
    public class MatchDayRepository : IMatchDayRepository
    {
        public async Task<PlayerEntity?> GetPlayer(int id)
        {
            await using (var db = new SQLiteContext())
            {
                return await db.Players
                    .Include(x => x.Team)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<CoachEntity?> GetCoach(int id)
        {
            await using (var db = new SQLiteContext())
            {
                return await db.Coaches
                    .Include(x => x.Team)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<TeamEntity?> GetTeam(int id)
        {
            await using (var db = new SQLiteContext())
            {
                return await db.Teams
                    .Include(x => x.Players)
                    .Include(x => x.Coach)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<TeamEntity?> GetTeam(string name)
        {
            await using (var db = new SQLiteContext())
            {
                return await db.Teams
                    .Include(x => x.Players)
                    .Include(x => x.Coach)
                    .FirstOrDefaultAsync(x => x.Name == name);
            }
        }

        public async Task AddPlayer(PlayerEntity player)
        {
            await using (var db = new SQLiteContext())
            {
                await db.Players.AddAsync(player);
                await db.SaveChangesAsync();
            }
        }

        public async Task AddCoach(CoachEntity coach)
        {
            await using (var db = new SQLiteContext())
            {
                await db.Coaches.AddAsync(coach);
                await db.SaveChangesAsync();
            }
        }

        public async Task<int?> AddTeam(TeamEntity team)
        {
            await using (var db = new SQLiteContext())
            {
                await db.Teams.AddAsync(team);
                await db.SaveChangesAsync();

                return team.Id;
            }
        }
    }
}
