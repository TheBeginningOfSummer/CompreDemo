using SQLite;
using System.Linq.Expressions;

namespace Services
{
    public class SQLiteService
    {
        public readonly string DatabaseName;
        public readonly string DatabasePath;
        private SQLiteAsyncConnection? sqlconnection;
        public SQLiteAsyncConnection SQL => sqlconnection ?? (sqlconnection = new SQLiteAsyncConnection(DatabasePath));

        public SQLiteService(string databaseName = "Data.db", string databasePath = "")
        {
            this.DatabaseName = databaseName;
            if (databasePath == "")
                DatabasePath = databaseName;
            else
                DatabasePath = Path.Combine(databasePath, databaseName);
        }

        public async void InitializeTableAsync<T>() where T : new()
        {
            try
            {
                await SQL.Table<T>().ToListAsync();
            }
            catch (Exception e)
            {
                if (e.Message == $"no such table: {typeof(T).ToString().Split('.').LastOrDefault()}")
                    await SQL.CreateTableAsync<T>();
            }
        }

        public async Task InsertOrUpdateAsync<T>(Expression<Func<T, bool>> predicate, T storedData) where T : ISQLData, new()
        {
            T data = await SQL.FindAsync(predicate);
            if (data == null)
            {
                await SQL.InsertAsync(storedData);
            }
            else
            {
                storedData.Id = data.Id;
                await SQL.UpdateAsync(storedData);
            }
        }

        public async Task<List<T>> Inquire<T>(string min, string max, string name = "Date") where T : new()
        {
            var dataSource = await SQL.QueryAsync<T>
            ($"select * from {nameof(T)} where {name} >='{min}' and {name} <='{max}'");
            return dataSource;
        }

        
    }

    public interface ISQLData
    {
        int Id { get; set; }
    }
}
