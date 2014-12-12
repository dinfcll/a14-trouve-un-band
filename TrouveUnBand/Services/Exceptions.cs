using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TrouveUnBand.Services
{
    static class DbUpdateConcurrencyExceptionExtensions
    {
        public static void ClientWins(this DbUpdateConcurrencyException exception)
        {
            var entry = exception.Entries.First();
            entry.OriginalValues.SetValues(entry.GetDatabaseValues());
        }

        public static void StoreWins(this DbUpdateConcurrencyException exception)
        {
            var entry = exception.Entries.First();
            entry.Reload();
        }
    }
}
