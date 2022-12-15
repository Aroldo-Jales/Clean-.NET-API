namespace Prova1.Infrastructure.Database.Utils
{
    public static class AppDirectories
    {
        public static string getDatabasePath
        {
            get
            {
                string path = Directory.GetCurrentDirectory();
                return path.Substring(0, path.LastIndexOf("Prova1.Api")) + @"Prova1.Infrastructure\Database\SQLite\Database.db";
            }
        }
    }
}