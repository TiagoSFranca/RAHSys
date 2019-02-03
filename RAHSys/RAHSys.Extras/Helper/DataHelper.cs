using System;

namespace RAHSys.Extras.Helper
{
    public static class DataHelper
    {
        public static DateTime ConverterStringParaData(string stringData)
        {
            DateTime data;

            if (!DateTime.TryParse(stringData,
                System.Globalization.CultureInfo.GetCultureInfo("pt-BR"),
                System.Globalization.DateTimeStyles.None, out data))
                throw new Exception("Não foi possível converter a data");

            return data;
        }
    }
}
