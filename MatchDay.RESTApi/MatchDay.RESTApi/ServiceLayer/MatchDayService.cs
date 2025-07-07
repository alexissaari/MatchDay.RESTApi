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

        public PlayerModel? GetPlayer(int id)
        {
            var entity = this.repository.GetPlayer(id);
            if (entity == null) { return null; }

            return EntityToModel.ToModel(entity);
        }

        public CoachModel? GetCoach(int id)
        {
            var entity = this.repository.GetCoach(id);
            if (entity == null) { return null; }

            return EntityToModel.ToModel(entity);
        }

        public TeamModel? GetTeam(int id)
        {
            var entity = this.repository.GetTeam(id);
            if (entity == null) { return null; }

            return EntityToModel.ToModel(entity);
        }

        public void AddPlayer(PlayerModel player)
        {
            var entity = ModelToEntity.ToEntity(player);
            this.repository.AddPlayer(entity);
        }

        public void AddCoach(CoachModel coach)
        {
            var entity = ModelToEntity.ToEntity(coach);
            this.repository.AddCoach(entity);
        }

        public void CreateTeam(TeamModel team)
        {
            var teamEntity = ModelToEntity.ToEntity(team);

            if (teamEntity != null)
            {
                this.repository.AddTeam(teamEntity);
            }
        }
    }
}
