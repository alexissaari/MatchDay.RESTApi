using MatchDay.RESTApi.DatabaseLayer.Context;
using MatchDay.RESTApi.DatabaseLayer.Entities;
using MatchDay.RESTApi.DatabaseLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MatchDay.RESTApi.DatabaseLayer
{
    /*
     * The Database Layer is the connection to the database.
     * 
     * Here, we use Entities which are the C# object versions of our SQLite tables.
     */
    public class MatchDayRepository : IMatchDayRepository
    {
        public async Task<IList<TeamEntity>> GetTeams()
        {
            await using (var db = new SQLiteContext())
            {
                return await db.Teams
                    .Include(x => x.Players)
                    .Include(x => x.Coach)
                    .ToListAsync();
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

        public async Task<int?> AddTeam(TeamEntity team)
        {
            await using (var db = new SQLiteContext())
            {
                await db.Teams.AddAsync(team);
                await db.SaveChangesAsync(); // on success, updates team.Id with Id used in SQLite table

                return team.Id;
            }
        }
    }
}
