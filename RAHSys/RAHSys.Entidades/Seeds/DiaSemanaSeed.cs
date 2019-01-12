using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Entidades.Seeds
{
    public class DiaSemanaSeed
    {
        public static DiaSemanaModel Domingo { get { return new DiaSemanaModel() { IdDiaSemana = 1, Descricao = "Domingo" }; } }
        public static DiaSemanaModel Segunda { get { return new DiaSemanaModel() { IdDiaSemana = 2, Descricao = "Segunda" }; } }
        public static DiaSemanaModel Terca { get { return new DiaSemanaModel() { IdDiaSemana = 3, Descricao = "Terça" }; } }
        public static DiaSemanaModel Quarta { get { return new DiaSemanaModel() { IdDiaSemana = 4, Descricao = "Quarta" }; } }
        public static DiaSemanaModel Quinta { get { return new DiaSemanaModel() { IdDiaSemana = 5, Descricao = "Quinta" }; } }
        public static DiaSemanaModel Sexta { get { return new DiaSemanaModel() { IdDiaSemana = 6, Descricao = "Sexta" }; } }
        public static DiaSemanaModel Sabado { get { return new DiaSemanaModel() { IdDiaSemana = 7, Descricao = "Sábado" }; } }

        public static List<DiaSemanaModel> Seeds
        {
            get
            {
                return new List<DiaSemanaModel>()
                {
                    Domingo,
                    Segunda,
                    Terca,
                    Quarta,
                    Quinta,
                    Sexta,
                    Sabado
                };
            }
        }
    }
}
