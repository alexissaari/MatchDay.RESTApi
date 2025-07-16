using MatchDay.RESTApi.DatabaseLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Mappers;
using MatchDay.RESTApi.ServiceLayer.Models;
using MatchDay.RESTApi.ServiceLayer.Results;

namespace MatchDay.RESTApi.ServiceLayer
{
    public class MatchDayService : IMatchDayService
    {
        IMatchDayRepository repository;

        public MatchDayService(IMatchDayRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> GetTeam(int id)
        {
            var entity = await this.repository.GetTeam(id);
            if (entity == null) 
            { 
                return Result.Failure(TeamError.TeamNotFound); 
            }

            var model = EntityToModel.ToModel(entity);

            return Result.Success(model);
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
