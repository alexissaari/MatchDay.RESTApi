using MatchDay.RESTApi.DatabaseLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Mappers;
using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.ServiceLayer
{
    public class MatchDayService : IMatchDayService
    {
        IMatchDayRepository repository;

        public MatchDayService(IMatchDayRepository repository)
        {
            this.repository = repository;
        }

        public async Task<TeamModel?> GetTeam(int id)
        {
            var entity = await this.repository.GetTeam(id);
            if (entity == null) { return null; }

            return EntityToModel.ToModel(entity);
        }

        public async Task<int> CreateTeam(TeamModel team)
        {
            var teamEntity = ModelToEntity.ToEntity(team);

            if (teamEntity != null)
            {
                var teamId = await this.repository.AddTeam(teamEntity);
                team.Id = teamId;
            }

            if (team.Id != null)
            {
                return team.Id.Value;
            }

            return 0;
        }
    }
}
