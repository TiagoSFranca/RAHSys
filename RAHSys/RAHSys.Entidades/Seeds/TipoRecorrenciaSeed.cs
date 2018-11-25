using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Entidades.Seeds
{
    public class TipoRecorrenciaSeed
    {
        public static TipoRecorrenciaModel Diaria { get { return new TipoRecorrenciaModel() { IdTipoRecorrencia = 1, Descricao = "Diária" }; } }
        public static TipoRecorrenciaModel Semanal { get { return new TipoRecorrenciaModel() { IdTipoRecorrencia = 2, Descricao = "Semanal" }; } }
        public static TipoRecorrenciaModel Mensal { get { return new TipoRecorrenciaModel() { IdTipoRecorrencia = 3, Descricao = "Mensal" }; } }

        public static List<TipoRecorrenciaModel> Seeds
        {
            get
            {
                return new List<TipoRecorrenciaModel>()
                {
                    Diaria,
                    Semanal,
                    Mensal
                };
            }
        }
    }
}
