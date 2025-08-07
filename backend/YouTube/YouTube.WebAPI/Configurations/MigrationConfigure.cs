using YouTube.Persistence.MigrationTools;

namespace YouTube.WebAPI.Configurations;

public static class MigrationConfigure
{
    public static async Task UseAutomaticMigrations(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var migrator = scope.ServiceProvider.GetRequiredService<Migrator>();

            await migrator.MigrateAsync();
        }
    }
}