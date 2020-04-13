using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Thaumatec.Core.Database.Settings
{
    public class DatabaseHandler : IDisposable
    {
        private DatabaseContext _context;
        private IClientSessionHandle _session;
        private DateTime _transactionStartDateTime;

        public DatabaseHandler()
        {
            _context = new DatabaseContext(DatabaseConnection.db, DatabaseConnection.Client);
        }

        public async Task StartTransaction()
        {
            _transactionStartDateTime = DateTime.Now;

            _session = await DatabaseConnection.Client.StartSessionAsync();
            _session.StartTransaction();
        }

        public async Task CommitTransaction()
        {
            await _session.CommitTransactionAsync();
        }

        public async Task AbortTransaction()
        {
            await _session.AbortTransactionAsync();
        }

        public DatabaseContext db
        {
            get { return _context; }
        }

        public DateTime TransactionStartDateTime
        {
            get { return _transactionStartDateTime; }
        }

        public void CloseConnection()
        {
            _context = null;
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
