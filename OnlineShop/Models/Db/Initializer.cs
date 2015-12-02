using System.Data.Entity;

namespace OnlineShop.Models.Db
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
    }
}