using Microsoft.EntityFrameworkCore;
using Resc.Src.Enums;
using System.Collections.Generic;

namespace Resc.Src
{
    public class RescContext : DbContext
    {
        public DbSet<ActivePosition> ActivePositions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=resc.db");
        }

        public class ActivePosition
        {
            public int Id { get; set; }
            public int FirstResponderId { get; set; }
            public FirstResponder FirstResponder { get; set; }
            public decimal Lat { get; set; }
            public decimal Lng { get; set; }
        }

        public class Intervention
        {
            public int Id { get; set; }
            public decimal Lat { get; set; }
            public decimal Lng { get; set; }
            public string Overview { get; set; }
            public string Detail { get; set; }
            public IntervationState State { get; set; }
        }

        public class FirstResponder
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class FirstResponderIntervention
        {
            public int Id { get; set; }
            public int FirstResponderId { get; set; }
            public FirstResponder FirstResponder { get; set; }
            public int InterventionId { get; set; }
            public Intervention Intervention { get; set; }
            public FirstResponderIntervationState State { get; set; }
        }
    }
}