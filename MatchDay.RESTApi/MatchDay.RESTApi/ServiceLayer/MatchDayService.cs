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

        public async Task<Result> CreateTeam(TeamModel teamModel)
        {
            // Make sure we don't already have a team with this name
            var team = await this.repository.GetTeam(teamModel.Name);
            if (team != null)
            {
                return Result.Failure(TeamError.TeamAlreadyExists);
            }

            var teamEntity = ModelToEntity.ToEntity(teamModel);
            var teamId = await this.repository.AddTeam(teamEntity);
            
            if (teamId == null || teamId == 0)
            {
                return Result.Failure(TeamError.TeamCreationError);
            }

            return Result.Success(teamId);
        }
    }
}
