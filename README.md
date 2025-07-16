# MatchDay.RESTApi

This `ASP.NET Core Web Api` is a `Minimum Viable Product` designed to show my skillset in:
- RESTApi Design
- Object-Oriented Design
- SQL Data Structures

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
