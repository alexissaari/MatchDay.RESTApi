using System.Net.Http.Json;
using MatchDay.RESTApi.WebLayer.DTOs;

namespace MatchDay.RESTApi.AcceptanceTests.WebLayerShould
{
    public class MatchDayControllerShould
    {
        private HttpClient client;
        private static string baseUri = "https://localhost:7087/MatchDay/Teams";

        public MatchDayControllerShould()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
        }

        [Fact]
        public async Task ShouldCreateAndGetTeam()
        {
            // First, let's add a new team to our collection
            // Arrange
            var createTeam = this.CreateRandomTeam(); 
            JsonContent content = JsonContent.Create(createTeam);

            // Act
            var postResponse = await client.PostAsync(baseUri, content);

            // Assert
            Assert.NotNull(postResponse);
            var teamId = await postResponse.Content.ReadFromJsonAsync<int>();
            Assert.True(teamId > 0);

            // Second, let's make sure that new team was actually get added
            // Act
            var getResponse = await client.GetAsync(baseUri + $"/{teamId}");
            Assert.NotNull(getResponse);
            var team = await getResponse.Content.ReadFromJsonAsync<GetTeamResponseDto>();
            Assert.NotNull(team);

            // Assert
            AssertTeamsAreEqual(createTeam, team);

            // Lastly, let's grab all the teams we have and make sure our newly added one is there too
            // Act
            var getAllTeamsResponse = await client.GetAsync(baseUri);
            Assert.NotNull(getAllTeamsResponse);
            var teams = await getAllTeamsResponse.Content.ReadFromJsonAsync<IList<GetTeamResponseDto>>();
            Assert.NotNull(teams);
            Assert.True(teams.Count > 0);
            var newTeamExists = teams.Any(x => x.TeamName == team.TeamName);
            Assert.True(newTeamExists);
        }

        private CreateTeamDto CreateRandomTeam() 
        {
            var newTeam = new CreateTeamDto()
            {
                Name = Guid.NewGuid().ToString(),
                Coach = new CreateCoachDto()
                {
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                }
            };

            newTeam.Roster = new List<CreatePlayerDto>();
            for (int i = 0; i < 10; i++)
            {
                newTeam.Roster.Add(new CreatePlayerDto()
                {
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                });
            }

            return newTeam;
        }

        private void AssertTeamsAreEqual(CreateTeamDto requestObj, GetTeamResponseDto responseObj)
        {
            // Validate Team
            Assert.NotNull(requestObj);
            Assert.NotNull(responseObj);
            Assert.Equal(requestObj.Name, responseObj.TeamName);

            // Validate Roster
            Assert.NotNull(requestObj.Roster);
            Assert.NotNull(responseObj.Roster);
            Assert.Equal(requestObj.Roster.Count, responseObj.Roster.Count);

            foreach (var player in requestObj.Roster)
            {
                var playerName = GetFullName(player.FirstName, player.LastName);

                Assert.Contains(playerName, responseObj.Roster);
            }

            // Validate Coach
            Assert.NotNull(requestObj.Coach);
            Assert.Equal(GetFullName(requestObj.Coach.FirstName, requestObj.Coach.LastName), responseObj.CoachName);
        }

        private string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }
    }
}
