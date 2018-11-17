using Microsoft.EntityFrameworkCore;
using Resc.Src.Enums;
using System.Collections.Generic;

namespace Resc.Src
{
    public class RescContext : DbContext
    {
        public DbSet<ActivePosition> ActivePositions { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<FirstResponder> FirstResponders { get; set; }
        public DbSet<FirstResponderIntervention> FirstResponderInterventions { get; set; }
        public DbSet<Station> Stations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=resc.db");
        }

        public class ActivePosition
        {
            public int Id { get; set; }
            public int FirstResponderId { get; set; }
            public FirstResponder FirstResponder { get; set; }
            public double Lat { get; set; }
            public double Lng { get; set; }
        }

        public class Intervention
        {
            public int Id { get; set; }
            public double Lat { get; set; }
            public double Lng { get; set; }
            public string Overview { get; set; }
            public string Detail { get; set; }
            public IntervationState State { get; set; }
        }

        public class FirstResponder
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string PushEndpoint { get; set; }
            public string PushAuth { get; set; }
            public string PushKey { get; set; }
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

        public class Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public double Lat { get; set; }
            public double Lng { get; set; }
        }
    }
}