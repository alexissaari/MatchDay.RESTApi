﻿using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.DatabaseLayer.Entities
{
    public class TeamEntity
    {
        public int? Id { get; set; } // Not required because on creation this won't have an Id yet
        public required string Name { get; set; }

        public required ICollection<PlayerEntity> Players { get; set; }
        public required CoachEntity Coach { get; set; }
    }
}
