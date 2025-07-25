# MatchDay.RESTApi
This ASP.NET Core Web Api is a Minimum Viable Product designed to show my ability to create a RESTful Api. This project also includes fundimentals in Object-Oriented Design, Domain-Driven Design, and a dash of SQL Database Design.

**Use Case:** You are a sports coordinator and need a way to keep track of teams, their players, and their coach.

### Running Locally
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/), built on Windows 11
- To run Acceptance Tests, I had two instances of VS pulled up, one running the main project on a localhost and the other running the acceptance tests calling the main project's localhost
- [DB Browser for SQLite](https://sqlitebrowser.org/) for viewing the SQLite database
- [Postman](https://www.postman.com/) and built in Swagger for calling the HTTP endpoints

#### TODOs
- Add other CRUD routes
  - PATCH 
  - PUT
  - DELETE
  - For teams, but also players/coaches?
  - Ex: PATCH /Teams/{teamId}/Coach/{firstName}/{lastName} update this team's coach (what happens to previous coach? do we allow orphans?)
- Hook this up to an actual SQL Server DB
  - Use SSMS
  - CRUD updates through simple Stored Procedures
  - Add more tables to show my understanding of database design? -> Referees, Matches (w/ two teams, a referee, a date, and a score)
- Continue to improve documentation, maybe a diagram of the layers?
- Try to break the Failure Results the service layer is returning. Think: what else could go wrong?
  - Player / Coach already exists? how would we validate this? it's not uncommon for two people to have the same first and last name

### Endpoints
> ```http 
> GET /MatchDay/Teams
> ```
> ```http 
> GET /MatchDay/Teams/{teamId}
> ```
> ```http 
> POST /MatchDay/Teams
> {
>   "name": "string",
>   "roster": [
>     {
>       "firstName": "string",
>       "lastName": "string"
>     }
>   ],
>   "coach": {
>     "firstName": "string",
>     "lastName": "string"
>   }
> }
> ```


#### SQLite Database
<img width="726" height="497" alt="matchday_db_diagram" src="https://github.com/user-attachments/assets/a140a0d2-2ffe-4429-966e-517de1ed0d4a" />

_Diagram created via [dbdiagram.io](https://dbdiagram.io/d)_
