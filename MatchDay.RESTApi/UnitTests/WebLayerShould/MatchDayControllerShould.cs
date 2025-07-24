using MatchDay.RESTApi.ServiceLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Models;
using MatchDay.RESTApi.ServiceLayer.Results;
using MatchDay.RESTApi.WebLayer;
using MatchDay.RESTApi.WebLayer.DTOs;
using MatchDay.RESTApi.WebLayer.Validators;
using Microsoft.AspNetCore.Http;
using Moq;
using HttpResults = Microsoft.AspNetCore.Http.HttpResults;

namespace MatchDay.RESTApi.UnitTests.WebLayer
{
    public class MatchDayControllerShould
    {
        private readonly Mock<IMatchDayService> mockService;
        private MatchDayController controller;

        public MatchDayControllerShould()
        {
            this.mockService = new Mock<IMatchDayService>();
            this.controller = new MatchDayController(mockService.Object);
        }

        #region HttpGet /MatchDay/Teams

        [Fact]
        public async Task ShouldGetTeams()
        {
            // Arrange
            var teamModels = this.GenerateListOfRandomTeamModels(5);
            var expected = Result.Success(teamModels);
            this.mockService
                .Setup(x => x.GetTeams())
                .ReturnsAsync(expected);

            // Act
            var response = await this.controller.GetTeams();
            var actualResult = response as HttpResults.Ok<List<GetTeamResponseDto>>;

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.NotNull(actualResult.Value);
            AssertListOfTeamsAreEqual(teamModels, actualResult.Value);
        }

        [Fact]
        public async Task ShouldNotGetTeams()
        {
            // Arrange
            var expected = Result.Success(new List<TeamModel>());
            this.mockService
                .Setup(x => x.GetTeams())
                .ReturnsAsync(expected);

            // Act
            var response = await this.controller.GetTeams();
            var actualResult = response as HttpResults.Ok<List<GetTeamResponseDto>>;

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.NotNull(actualResult.Value);
            Assert.Empty(actualResult.Value);
        }

        #endregion

        #region HttpGet /MatchDay/Teams/{teamId}

        [Fact]
        public async Task ShouldGetTeamById()
        {
            // Arrange
            var teamModel = this.GenerateRandomTeamModel();
            var expected = Result.Success(teamModel);
            this.mockService
                .Setup(x => x.GetTeam(teamModel.Id.Value))
                .ReturnsAsync(expected);

            // Act
            var response = await this.controller.GetTeam(teamModel.Id.Value, new GetTeamDtoValidator());
            var actualResult = response as HttpResults.Ok<GetTeamResponseDto>;

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.NotNull(actualResult.Value);
            AssertTeamModelAndTeamDtoAreEqual(teamModel, actualResult.Value);
        }

        [Fact]
        public async Task ShouldNotGetTeamById_ValidationErrors()
        {
            // Arrange
            var teamModel = this.GenerateRandomTeamModel();
            var expected = Result.Success(teamModel);
            this.mockService
                .Setup(x => x.GetTeam(teamModel.Id.Value))
                .ReturnsAsync(expected);

            // Act
            var response = await this.controller.GetTeam(0, new GetTeamDtoValidator());
            var actualResult = response as HttpResults.ProblemHttpResult;

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.NotNull(actualResult.ProblemDetails);
            
            var validationProblemDetails = (HttpValidationProblemDetails)actualResult.ProblemDetails;
            Assert.Equal(StatusCodes.Status400BadRequest, validationProblemDetails.Status);
            Assert.Equal("Bad Request", validationProblemDetails.Title);
            Assert.Equal("One or more validation errors occured.", validationProblemDetails.Detail);
            
            var errors = validationProblemDetails.Errors.FirstOrDefault().Value;
            Assert.Equal(2, errors.Length);
            Assert.Contains("TeamId is required.", errors);
            Assert.Contains("TeamId must be a positive number.", errors);
        }

        [Fact]
        public async Task ShouldNotGetTeamById_NotFound()
        {
            // Arrange
            var expected = Result.Failure(TeamError.TeamNotFound);
            this.mockService
                .Setup(x => x.GetTeam(It.IsAny<int>()))
                .ReturnsAsync(expected);

            // Act
            var response = await this.controller.GetTeam(100, new GetTeamDtoValidator());
            var actualResult = response as HttpResults.ProblemHttpResult;

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.NotNull(actualResult.ProblemDetails);
            Assert.Equal(StatusCodes.Status404NotFound, actualResult.ProblemDetails.Status);
            Assert.Equal("Not Found", actualResult.ProblemDetails.Title);
            Assert.NotNull(actualResult.ProblemDetails.Extensions);

            actualResult.ProblemDetails.Extensions.TryGetValue("errors", out var errorObj);
            Assert.NotNull(errorObj);
            var error = errorObj as Error;
            Assert.NotNull(error);
            Assert.Equal("TeamNotFound", error.Code);
            Assert.Equal("Team not found.", error.Message);
            Assert.Equal(ErrorType.NotFound, error.Type);            
        }

        #endregion

        #region HttpPost /MatchDay/Teams

        [Fact]
        public async Task ShouldCreateTeam()
        {
            // Arrange
            int newTeamId = 100;
            var newTeam = new CreateTeamDto()
            {
                Name = "Test Team Name",
                Coach = new CreateCoachDto()
                {
                    FirstName = "Test Coach First Name",
                    LastName = "Test Coach Last Name",
                },
                Roster = new List<CreatePlayerDto>()
                {
                    new CreatePlayerDto()
                    {
                        FirstName = "Test Player First Name",
                        LastName = "Test Player Last Name",
                    }
                }
            };
            var expected = Result.Success(newTeamId);
            this.mockService
                .Setup(x => x.CreateTeam(It.IsAny<TeamModel>()))
                .ReturnsAsync(expected);

            // Act
            var response = await this.controller.PostTeam(newTeam, new CreateTeamDtoValidator());
            var actualResult = response as HttpResults.Created<int>;

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(201, actualResult.StatusCode);
            Assert.Equal(newTeamId, actualResult.Value);
        }

        [Fact]
        public async Task ShouldNotCreateTeam_ValidationErrors_NullRequestBody()
        {          
            // Act
            var response = await this.controller.PostTeam(null, new CreateTeamDtoValidator());            
            var nullBodyResult = response as HttpResults.BadRequest<string>;

            // Assert
            Assert.NotNull(nullBodyResult);
            Assert.Equal(400, nullBodyResult.StatusCode);
            Assert.Equal("Request body cannot be null.", nullBodyResult.Value);
        }

        [Fact]
        public async Task ShouldNotCreateTeam_ValidationErrors_EmptyRequestBody()
        {
            // Arrange
            var newTeam = new CreateTeamDto();

            // Act
            var response = await this.controller.PostTeam(newTeam, new CreateTeamDtoValidator());
            var emptyBodyResult = response as HttpResults.ProblemHttpResult;

            // Assert
            Assert.NotNull(emptyBodyResult);
            Assert.Equal(400, emptyBodyResult.StatusCode);
            Assert.NotNull(emptyBodyResult.ProblemDetails);

            var validationProblemDetails = (HttpValidationProblemDetails)emptyBodyResult.ProblemDetails;
            Assert.Equal(StatusCodes.Status400BadRequest, validationProblemDetails.Status);
            Assert.Equal("Bad Request", validationProblemDetails.Title);
            Assert.Equal("One or more validation errors occured.", validationProblemDetails.Detail);

            var errors = validationProblemDetails.Errors;
            Assert.Equal(3, errors.Count);
            errors.TryGetValue("Name", out var nameErrors);
            errors.TryGetValue("Roster", out var rosterErrors);
            errors.TryGetValue("Coach", out var coachErrors);

            Assert.NotNull(nameErrors);
            Assert.NotNull(rosterErrors);
            Assert.NotNull(coachErrors);

            Assert.Equal("Team Name is required.", nameErrors[0]);
            Assert.Equal("A team must have players.", rosterErrors[0]);
            Assert.Equal("A team must have a coach.", coachErrors[0]);
        }

        [Fact]
        public async Task ShouldNotCreateTeam_ValidationErrors_NullRosterAndCoach()
        {
            // Arrange
            var newTeam = new CreateTeamDto()
            {
                Name = "Test",
                Roster = null,
                Coach = null,
            };
            var response = await this.controller.PostTeam(newTeam, new CreateTeamDtoValidator());
            var emptyBodyResult = response as HttpResults.ProblemHttpResult;

            // Assert
            Assert.NotNull(emptyBodyResult);
            Assert.Equal(400, emptyBodyResult.StatusCode);
            Assert.NotNull(emptyBodyResult.ProblemDetails);

            var validationProblemDetails = (HttpValidationProblemDetails)emptyBodyResult.ProblemDetails;
            Assert.Equal(StatusCodes.Status400BadRequest, validationProblemDetails.Status);
            Assert.Equal("Bad Request", validationProblemDetails.Title);
            Assert.Equal("One or more validation errors occured.", validationProblemDetails.Detail);

            var errors = validationProblemDetails.Errors;
            Assert.Equal(2, errors.Count);
            errors.TryGetValue("Roster", out var rosterErrors);
            errors.TryGetValue("Coach", out var coachErrors);

            Assert.NotNull(rosterErrors);
            Assert.NotNull(coachErrors);

            Assert.Equal("A team must have players.", rosterErrors[0]);
            Assert.Equal("A team must have a coach.", coachErrors[0]);
        }

        [Fact]
        public async Task ShouldNotCreateTeam_ValidationErrors_EmptyRosterAndCoach()
        {
            // Arrange
            var newTeam = new CreateTeamDto()
            {
                Name = "Test",
                Roster = new List<CreatePlayerDto>() 
                { 
                    new CreatePlayerDto() { FirstName = "Test", LastName = "Test2" },
                    new CreatePlayerDto()
                },
                Coach = new CreateCoachDto(),
            };
            var response = await this.controller.PostTeam(newTeam, new CreateTeamDtoValidator());
            var emptyBodyResult = response as HttpResults.ProblemHttpResult;

            // Assert
            Assert.NotNull(emptyBodyResult);
            Assert.Equal(400, emptyBodyResult.StatusCode);
            Assert.NotNull(emptyBodyResult.ProblemDetails);

            var validationProblemDetails = (HttpValidationProblemDetails)emptyBodyResult.ProblemDetails;
            Assert.Equal(StatusCodes.Status400BadRequest, validationProblemDetails.Status);
            Assert.Equal("Bad Request", validationProblemDetails.Title);
            Assert.Equal("One or more validation errors occured.", validationProblemDetails.Detail);

            var errors = validationProblemDetails.Errors;
            Assert.Equal(4, errors.Count);
            errors.TryGetValue("Roster[1].FirstName", out var playerFirstNameErrors);
            errors.TryGetValue("Roster[1].LastName", out var playerLastNameErrors);
            errors.TryGetValue("Coach.FirstName", out var coachFirstNameErrors);
            errors.TryGetValue("Coach.LastName", out var coachLastNameErrors);

            Assert.NotNull(playerFirstNameErrors);
            Assert.NotNull(playerLastNameErrors);
            Assert.NotNull(coachFirstNameErrors);
            Assert.NotNull(coachLastNameErrors);

            Assert.Equal("Player's first name is required.", playerFirstNameErrors[0]);
            Assert.Equal("Player's last name is required.", playerLastNameErrors[0]);
            Assert.Equal("Coach's first name is required.", coachFirstNameErrors[0]);
            Assert.Equal("Coach's last name is required.", coachLastNameErrors[0]);
        }

        #endregion

        #region Helper Functions

        private IList<TeamModel> GenerateListOfRandomTeamModels(int count)
        {
            var teams = new List<TeamModel>();

            while (count > 0)
            {
                teams.Add(GenerateRandomTeamModel());
                count--;
            }

            return teams;
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

        private void AssertListOfTeamsAreEqual(IList<TeamModel>? models, IList<GetTeamResponseDto> dtos)
        {
            Assert.NotNull(models);
            Assert.NotNull(dtos);
            Assert.Equal(models.Count, dtos.Count);

            foreach (var model in models)
            {
                var dto = dtos.FirstOrDefault(x => x.TeamName == model.Name);
                Assert.NotNull(dto);
                AssertTeamModelAndTeamDtoAreEqual(model, dto);
            }
        }

        private void AssertTeamModelAndTeamDtoAreEqual(TeamModel model, GetTeamResponseDto dto)
        {
            // Validate Team
            Assert.NotNull(model);
            Assert.NotNull(dto);
            Assert.Equal(model.Name, dto.TeamName);

            // Validate Players
            Assert.NotNull(model.Players);
            Assert.NotNull(dto.Roster);

            Assert.Equal(model.Players.Count, dto.Roster.Count);

            foreach (var player in model.Players)
            {
                var playerName = GetFullName(player.FirstName, player.LastName);

                Assert.Contains(playerName, dto.Roster);
            }

            // Validate Coach
            Assert.NotNull(model.Coach);
            Assert.Equal(GetFullName(model.Coach.FirstName, model.Coach.LastName), dto.CoachName);
        }

        private string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }

        #endregion
    }
}
