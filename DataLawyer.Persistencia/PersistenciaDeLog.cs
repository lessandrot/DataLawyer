using DataLawyer.Nucleo;

namespace DataLawyer.Persistencia
{
    public class PersistenciaDeLog
    {
        private static PersistenciaDeLog _instancia = null;
        public static PersistenciaDeLog Instancia => _instancia ?? new PersistenciaDeLog();
        private PersistenciaDeLog() { }        

        public void Registre(LogDeErro log)
        {
            if (log is null) return;

            using (var contexto = new Contexto())
            {
                contexto.LogDeErro.Add(log);
                contexto.SaveChanges();
            }
        }        
    }
}