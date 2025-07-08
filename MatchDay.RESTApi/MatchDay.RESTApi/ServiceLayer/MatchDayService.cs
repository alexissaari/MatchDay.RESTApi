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

        public async Task<PlayerModel?> GetPlayer(int id)
        {
            var entity = await this.repository.GetPlayer(id);
            if (entity == null) { return null; }

            return EntityToModel.ToModel(entity);
        }

        public async Task<CoachModel?> GetCoach(int id)
        {
            var entity = await this.repository.GetCoach(id);
            if (entity == null) { return null; }

            return EntityToModel.ToModel(entity);
        }

        public async Task<TeamModel?> GetTeam(int id)
        {
            var entity = await this.repository.GetTeam(id);
            if (entity == null) { return null; }

            return EntityToModel.ToModel(entity);
        }

        public async Task AddPlayer(PlayerModel player)
        {
            var entity = ModelToEntity.ToEntity(player);
            await this.repository.AddPlayer(entity);
        }

        public async Task AddCoach(CoachModel coach)
        {
            var entity = ModelToEntity.ToEntity(coach);
            await this.repository.AddCoach(entity);
        }

        public async Task CreateTeam(TeamModel team)
        {
            var teamEntity = ModelToEntity.ToEntity(team);

            if (teamEntity != null)
            {
                await this.repository.AddTeam(teamEntity);
            }
        }
    }
}
