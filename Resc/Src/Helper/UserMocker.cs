using System.Linq;
using static Resc.Src.RescContext;

namespace Resc.Src.Helper
{
    public class UserMocker
    {
        public static void Mock(int id)
        {
            using (var db = new RescContext())
            {
                var user = db.FirstResponders.SingleOrDefault(r => r.Id == id);
                if (user != null) return;

                user = new FirstResponder { Id = id };
                user.Name = "TestName" + id;
                user.Description = "TestDescription" + id;

                db.FirstResponders.Add(user);

                db.SaveChanges();
            }
        }
    }
}
