using Microsoft.EntityFrameworkCore;
using TradingCompany.DALEF.Data;

namespace TradingCompany.DALEF.Concrete.Context
{
    public class TradingCompanyContext : TradingCompanyContextOriginal
    {
        private readonly string _connectionString;
        public TradingCompanyContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseSqlServer(_connectionString);

    }
}
