using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static  class AuditoriaExtensao
    {
        public static AuditoriaModel MapearParaDominio(this AuditoriaAppModel obj)
        {
            return AutoMapper.Mapper.Map<AuditoriaModel>(obj);
        }

        public static AuditoriaAppModel MapearParaAplicacao(this AuditoriaModel obj)
        {
            return AutoMapper.Mapper.Map<AuditoriaAppModel>(obj);
        }
    }
}
