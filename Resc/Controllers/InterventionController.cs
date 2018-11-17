using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Resc.Src;
using Resc.Src.Enums;
using Resc.Src.Models.Request;
using Resc.Src.Helper;
using Microsoft.EntityFrameworkCore;
using static Resc.Src.RescContext;

namespace Resc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InterventionController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post(InterventionModel data)
        {
            using (var db = new RescContext())
            {
                var intervention = new Intervention {
                    Detail = data.Detail,
                    Overview = data.Overview,
                    Lat = data.Lat,
                    Lng = data.Lng,
                    State = IntervationState.Open
                };
                
                db.Interventions.Add(intervention);
                db.SaveChanges();
            }

            using (var db = new RescContext())
            {
                var intervention = db.Interventions.Where(i => i.State == IntervationState.Open).LastOrDefault();
                if (intervention != null)
                {
                    InterventionHelper.Notify(intervention);
                }
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class InterventionHelper
    {
        public static void Notify(Intervention intervention)
        {
            var radius = GetNearestStation(intervention.Lat, intervention.Lng);

            var responders = GetRespondersInRadius(radius, intervention.Lat, intervention.Lng);
            var contr = new PushControllerExtension();
            foreach(var responder in responders)
            {
                contr.SendNotification(responder, intervention);
            }
        }

        public static double GetNearestStation(double Lat, double Lng)
        {
            Station nearestStation = null;
            double nearest = 150;


            using (var db = new RescContext())
            {
                var stations = db.Stations.ToList();

                foreach (var station in stations)
                {
                    var distance = GeoCoordinates.DistanceTo(Lat, Lng, station.Lat, station.Lng);

                    if (distance < nearest)
                    {
                        nearestStation = station;
                        nearest = distance;
                    }
                }
            }

            return nearest;
        }

        public static List<FirstResponder> GetRespondersInRadius(double radius, double Lat, double Lng)
        {
            var responders = new List<FirstResponder>();

            using (var db = new RescContext())
            {
                var positions = db.ActivePositions.Include(p => p.FirstResponder).ToList();

                foreach (var position in positions)
                {
                    var distance = GeoCoordinates.DistanceTo(Lat, Lng, position.Lat, position.Lng);

                    if (distance < radius)
                    {
                        responders.Add(position.FirstResponder);
                    }
                }
            }

            return responders;
        }
    }
}
