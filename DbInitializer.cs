namespace Tms.Backend;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();
    }
}