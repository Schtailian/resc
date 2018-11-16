using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resc.Src;
using Resc.Src.Models.Request;
using static Resc.Src.RescContext;

namespace Resc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        [HttpPost]
        public void Post(PositionModel data)
        {
            using (var db = new RescContext())
            {
                var position = db.ActivePositions.SingleOrDefault(p => p.FirstResponderId == data.Id);
                if (position == null)
                    position = new ActivePosition { FirstResponderId = data.Id };

                position.Lat = data.Lat;
                position.Lng = data.Lng;

                if (position.Id != 0)
                    db.ActivePositions.Update(position);
                else
                    db.ActivePositions.Add(position);

                db.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult Get(string a)
        {
            return Ok(a);
        }
    }
}