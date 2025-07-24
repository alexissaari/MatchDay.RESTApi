using MatchDay.RESTApi.DatabaseLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Mappers;
using MatchDay.RESTApi.ServiceLayer.Models;
using MatchDay.RESTApi.ServiceLayer.Results;

namespace MatchDay.RESTApi.ServiceLayer
{
    /*
     * The Service Layer is where we do all our business logic.
     * 
     * You'll notice each of these functions return a Result object. But WHY?
     * 
     * There's two trains of thought;
     * if our database layer returns something not in-line with a happy path result,
     * we can either throw an exception or we can return a failure result.
     * 
     * Throwing exceptions can be incredibly usefull when you really want to look into
     * the inner exception to see what went wrong.
     * 
     * However, exceptions are cumbersome for common issues that don't need further investigation.
     * 
     * So, I've chosen to return failure result objects for common issues, 
     * such as NotFound or AlreadyExists
     */
    public class MatchDayService : IMatchDayService
    {
        IMatchDayRepository repository;

        public MatchDayService(IMatchDayRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> GetTeams()
        {
            var entities = await this.repository.GetTeams();

            if (entities == null)
            {
                // If there are no teams, let's fail gracefully and
                // return a Success Result with an empty list of models
                return Result.Success(new List<TeamModel>());
            }

            IList<TeamModel> models = entities.Select(EntityToModel.ToModel).ToList();
            
            return Result.Success(models);
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
