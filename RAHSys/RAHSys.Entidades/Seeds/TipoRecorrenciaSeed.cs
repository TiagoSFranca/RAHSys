using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Entidades.Seeds
{
    public class TipoRecorrenciaSeed
    {
        public static TipoRecorrenciaModel Dias { get { return new TipoRecorrenciaModel() { IdTipoRecorrencia = 1, Descricao = "Dias" }; } }
        public static TipoRecorrenciaModel Semanas { get { return new TipoRecorrenciaModel() { IdTipoRecorrencia = 2, Descricao = "Semanas" }; } }
        public static TipoRecorrenciaModel Meses { get { return new TipoRecorrenciaModel() { IdTipoRecorrencia = 3, Descricao = "Meses" }; } }
        public static TipoRecorrenciaModel Anos { get { return new TipoRecorrenciaModel() { IdTipoRecorrencia = 4, Descricao = "Anos" }; } }

        public static List<TipoRecorrenciaModel> Seeds
        {
            get
            {
                return new List<TipoRecorrenciaModel>()
                {
                    Dias,
                    Semanas,
                    Meses,
                    Anos
                };
            }
        }
    }
}
