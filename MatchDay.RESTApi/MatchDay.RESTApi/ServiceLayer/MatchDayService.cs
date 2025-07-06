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

        public PlayerModel GetPlayer(int id)
        {
            var entity = this.repository.GetPlayer(id);
            return EntityToModel.ToModel(entity);
        }

        public CoachModel GetCoach(int id)
        {
            var entity = this.repository.GetCoach(id);
            return EntityToModel.ToModel(entity);
        }

        public TeamModel GetTeam(int id)
        {
            var entity = this.repository.GetTeam(id);
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

        public void AddTeam(TeamModel team)
        {
            var entity = ModelToEntity.ToEntity(team);
            this.repository.AddTeam(entity);
        }
    }
}
