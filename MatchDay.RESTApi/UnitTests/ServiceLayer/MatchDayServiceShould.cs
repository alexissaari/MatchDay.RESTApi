using MatchDay.RESTApi.DatabaseLayer.Entities;
using MatchDay.RESTApi.DatabaseLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer;
using MatchDay.RESTApi.ServiceLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Mappers;
using MatchDay.RESTApi.ServiceLayer.Models;
using MatchDay.RESTApi.ServiceLayer.Results;
using Moq;

namespace UnitTests.ServiceLayer
{
    public class MatchDayServiceShould
    {
        private readonly Mock<IMatchDayRepository> mockRepository;
        private IMatchDayService service;

        public MatchDayServiceShould()
        {
            this.mockRepository = new Mock<IMatchDayRepository>();
            this.service = new MatchDayService(this.mockRepository.Object);
        }

        [Fact]
        public async Task ShouldGetTeams()
        {
            // Arrange
            var teamEntities = GenerateListOfRandomTeamEntities(5);
            this.mockRepository
                .Setup(x => x.GetTeams())
                .ReturnsAsync(teamEntities);

            // Act
            var result = await this.service.GetTeams();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.SuccessResult);
            Assert.Null(result.ErrorResult);

            var teamModels = (IList<TeamModel>)result.SuccessResult;
            AssertListOfTeamsAreEqual(teamEntities, teamModels);
        }

        [Fact]
        public async Task ShouldNotGetTeamsWhenNoTeamsSaved()
        {
            // Arrange
            this.mockRepository
                .Setup(x => x.GetTeams());

            // Act
            var result = await this.service.GetTeams();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.SuccessResult);
            Assert.Null(result.ErrorResult);
            Assert.Empty((IList<TeamModel>)result.SuccessResult);
        }

        [Fact]
        public async Task ShouldGetTeamById()
        {
            // Arrange
            var teamEntity = GenerateRandomTeamEntity();
            this.mockRepository
                .Setup(x => x.GetTeam(It.IsAny<int>()))
                .ReturnsAsync(teamEntity);

            // Act
            var result = await this.service.GetTeam(1);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.SuccessResult);
            Assert.Null(result.ErrorResult);

            var teamModel = (TeamModel)result.SuccessResult;
            Assert.NotNull(teamModel);
            AssertTeamsAreEqual(teamEntity, teamModel);
        }

        [Fact]
        public async Task ShouldNotGetTeamById()
        {
            // Arrange
            this.mockRepository
                .Setup(x => x.GetTeam(It.IsAny<int>()));

            // Act
            var result = await this.service.GetTeam(1);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Null(result.SuccessResult);
            Assert.NotNull(result.ErrorResult);

            var error = (Error)result.ErrorResult;
            Assert.NotNull(error);
            Assert.Equal(ErrorCodes.TeamNotFound, error.Code);
            Assert.Equal(ErrorType.NotFound, error.Type);
            Assert.Equal("Team not found.", error.Message);
        }

        [Fact]
        public async Task ShouldCreateTeam()
        {
            // Arrange
            var model = this.GenerateRandomTeamModel();
            var entity = ModelToEntity.ToEntity(model);

            this.mockRepository
                .Setup(x => x.GetTeam(It.IsAny<string>()));
            this.mockRepository
                .Setup(x => x.AddTeam(It.IsAny<TeamEntity>()))
                .ReturnsAsync(model.Id);

            // Act
            var result = await this.service.CreateTeam(model);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.SuccessResult);
            Assert.Null(result.ErrorResult);
            Assert.Equal(model.Id, (int)result.SuccessResult);
        }

        [Fact]
        public async Task ShouldNotCreateTeam_AlreadyExists()
        {
            // Arrange
            var model = this.GenerateRandomTeamModel();
            var entity = ModelToEntity.ToEntity(model);

            this.mockRepository
                .Setup(x => x.GetTeam(It.IsAny<string>()))
                .ReturnsAsync(entity);
            this.mockRepository
                .Setup(x => x.AddTeam(It.IsAny<TeamEntity>()))
                .ReturnsAsync(model.Id);

            // Act
            var result = await this.service.CreateTeam(model);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.ErrorResult);

            var error = result.ErrorResult;
            Assert.Equal(ErrorCodes.TeamAlreadyExists, error.Code);
            Assert.Equal(ErrorType.Conflict, error.Type);
            Assert.Equal("This team already exists and cannot be added.", error.Message);
        }

        [Fact]
        public async Task ShouldNotCreateTeam_TeamCreationError()
        {
            // Arrange
            var model = this.GenerateRandomTeamModel();

            this.mockRepository
                .Setup(x => x.GetTeam(It.IsAny<string>()));
            this.mockRepository
                .Setup(x => x.AddTeam(It.IsAny<TeamEntity>()))
                .ReturnsAsync(0);

            // Act
            var result = await this.service.CreateTeam(model);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.ErrorResult);

            var error = result.ErrorResult;
            Assert.Equal(ErrorCodes.TeamCreationError, error.Code);
            Assert.Equal(ErrorType.Conflict, error.Type);
            Assert.Equal("Error occured when creating new team.", error.Message);
        }

        private IList<TeamEntity> GenerateListOfRandomTeamEntities(int count)
        {
            var teams = new List<TeamEntity>();

            while (count > 0)
            {
                teams.Add(GenerateRandomTeamEntity());
                count--;
            }

            return teams;
        }

        private TeamEntity GenerateRandomTeamEntity()
        {
            Random random = new Random();
            var teamId = random.Next(0, 1000000);

            return new TeamEntity
            {
                Id = teamId,
                Name = Guid.NewGuid().ToString(),
                Players = new List<PlayerEntity>
                { 
                    new PlayerEntity
                    {
                        Id = random.Next(100, 200),
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        TeamId = teamId
                    },
                    new PlayerEntity
                    {
                        Id = random.Next(200, 300),
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        TeamId = teamId
                    }
                },
                Coach = new CoachEntity 
                { 
                    Id = random.Next(900, 1000),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    TeamId = teamId
                }
            };
        }

        private TeamModel GenerateRandomTeamModel()
        {
            Random random = new Random();
            var teamId = random.Next(0, 1000000);

            return new TeamModel
            {
                Id = teamId,
                Name = Guid.NewGuid().ToString(),
                Players = new List<PlayerModel>
                {
                    new PlayerModel
                    {
                        Id = random.Next(100, 200),
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        TeamId = teamId
                    },
                    new PlayerModel
                    {
                        Id = random.Next(200, 300),
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        TeamId = teamId
                    }
                },
                Coach = new CoachModel
                {
                    Id = random.Next(900, 1000),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    TeamId = teamId
                }
            };
        }

        private void AssertListOfTeamsAreEqual(IList<TeamEntity> expected, IList<TeamModel> actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);

            foreach (var actualTeam in actual)
            {
                var expectedTeam = expected.FirstOrDefault(e => e.Id == actualTeam.Id);
                Assert.NotNull(expectedTeam);
                AssertTeamsAreEqual(expectedTeam, actualTeam);
            }
        }

        private void AssertTeamsAreEqual(TeamEntity expected, TeamModel actual)
        {
            // Validate Team
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);

            // Validate Players
            Assert.NotNull(expected.Players);
            Assert.NotNull(actual.Players);

            Assert.Equal(expected.Players.Count, actual.Players.Count);

            foreach (var actualPlayer in actual.Players)
            {
                var expectedPlayer = expected.Players.FirstOrDefault(p => p.Id == actualPlayer.Id && p.FirstName == actualPlayer.FirstName && p.LastName == actualPlayer.LastName);
                Assert.NotNull(expectedPlayer);
                Assert.Equal(expectedPlayer.TeamId, actualPlayer.TeamId);
            }

            // Validate Coach
            Assert.NotNull(expected.Coach);
            Assert.NotNull(actual.Coach);
            Assert.Equal(expected.Coach.Id, actual.Coach.Id);
            Assert.Equal(expected.Coach.FirstName, actual.Coach.FirstName);
            Assert.Equal(expected.Coach.LastName, actual.Coach.LastName);
        }
    }
}
