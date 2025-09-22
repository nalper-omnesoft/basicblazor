using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevOpsAssignment.ApiService;

public class DBService(NpgsqlDataSource dataSource)
{
    private bool _tableInitialized = false;

    // Ensures the greetings table exists and has initial data
    private async Task EnsureTableExistsAsync()
    {
        if (_tableInitialized)
            return;

        const string createTableQuery = """
            CREATE TABLE IF NOT EXISTS greetings (
                id SERIAL PRIMARY KEY,
                message TEXT NOT NULL
            );

            INSERT INTO greetings (message)
            SELECT 'Hello World'
            WHERE NOT EXISTS (SELECT 1 FROM greetings);
            """;

        await using var command = dataSource.CreateCommand(createTableQuery);
        await command.ExecuteNonQueryAsync();

        _tableInitialized = true; // Prevent repeated execution
    }

    public async Task<string?> GetGreetingMessageAsync()
    {
        await EnsureTableExistsAsync(); // Ensure table exists before querying

        const string query = "SELECT message FROM greetings WHERE id = 1";

        await using var command = dataSource.CreateCommand(query);
        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return reader.GetString(0);
        }

        return null;
    }

    public async Task<List<(int Id, string Message)>> GetAllGreetingsAsync()
    {
        await EnsureTableExistsAsync(); // Ensure table exists before querying

        const string query = "SELECT id, message FROM greetings ORDER BY id";

        var greetings = new List<(int, string)>();

        await using var command = dataSource.CreateCommand(query);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var id = reader.GetInt32(0);
            var message = reader.GetString(1);
            greetings.Add((id, message));
        }

        return greetings;
    }
}