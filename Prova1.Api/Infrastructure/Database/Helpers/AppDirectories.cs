
namespace Prova1.Api.Infrastructure.Database.Helpers
{
    public class AppDirectories
    {
        public static string getDatabasePath
        {
            get
            {
                string path = Directory.GetCurrentDirectory();    

                // CORRIGIR DEBUGANDO PARA VERIFICAR PATH;
                return path+@"\Infrastructure\Database\SQLite\AppDatabase.db";              
            }            
        }
    }
}