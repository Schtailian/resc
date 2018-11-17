using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resc.Src;
using Resc.Src.Helper;
using Resc.Src.Models.Request;
using static Resc.Src.RescContext;
using WebPush;


namespace Resc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PushController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post(PushModel data)
        {
            UserMocker.Mock(data.Id);

            using (var db = new RescContext())
            {
                var firstResponder = db.FirstResponders.SingleOrDefault(p => p.Id == data.Id);
                if (firstResponder == null)
                    return Ok("Fail");

                firstResponder.PushEndpoint = data.Endpoint;
                var tmp = data.Keys.FirstOrDefault();
                firstResponder.PushAuth = tmp.Value;
                tmp = data.Keys.Skip(1).FirstOrDefault();
                firstResponder.PushKey = tmp.Value;

                db.FirstResponders.Update(firstResponder);

                db.SaveChanges();
            }

            return Ok();
        }
    }

    public class PushControllerExtension
    {
        public void SendNotification(FirstResponder responder, Intervention intervention)
        {
            var pushEndpoint = responder.PushEndpoint;
            var p256dh = responder.PushKey;
            var auth = responder.PushAuth;

            var subject = @"mailto:example@example.com";
            var publicKey = @"BOYIC7mz-ejuM6Nq4AFlZn3lYfA4NpxHI62RwubkdOEcY1qysVLGvtWXXKdoZYg6mMVujYYNfvMvGr7P8ChtII4";
            var privateKey = @"hgg0q6HVAtKwb1AKeYJlIkyQbqRfxujp6hULhflh6f8";

            var subscription = new PushSubscription(pushEndpoint, p256dh, auth);
            var vapidDetails = new VapidDetails(subject, publicKey, privateKey);

            var webPushClient = new WebPushClient();
            try
            {
                webPushClient.SendNotification(subscription, "payload", vapidDetails);
            }
            catch (WebPushException exception)
            {
                Console.WriteLine("Http STATUS code" + exception.StatusCode);
            }
        }
    }
}