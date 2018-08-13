using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Entidades.Seeds
{
    public class EstadoCivilSeed
    {
        public static EstadoCivilModel Solteiro { get { return new EstadoCivilModel() { IdEstadoCivil = 1, Descricao = "Solteiro(a)" }; } }
        public static EstadoCivilModel Casado { get { return new EstadoCivilModel() { IdEstadoCivil = 2, Descricao = "Casado(a)" }; } }
        public static EstadoCivilModel Viuvo { get { return new EstadoCivilModel() { IdEstadoCivil = 3, Descricao = "Viúvo(a)" }; } }
        public static EstadoCivilModel Divorciado { get { return new EstadoCivilModel() { IdEstadoCivil = 4, Descricao = "Divorciado(a)" }; } }
        public static EstadoCivilModel Separado { get { return new EstadoCivilModel() { IdEstadoCivil = 5, Descricao = "Separado(a)" }; } }

        public static List<EstadoCivilModel> Seeds
        {
            get
            {
                return new List<EstadoCivilModel>()
                {
                    Solteiro,
                    Casado,
                    Viuvo,
                    Divorciado,
                    Separado
                };
            }
        }
    }
}
