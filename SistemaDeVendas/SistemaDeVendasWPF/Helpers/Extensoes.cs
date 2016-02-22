namespace SistemaDeVendasWPF.Helpers
{
    public static class Extensoes
    {
        public static string paraValorReal(this decimal valor)
        {
            string strValor = valor.ToString();
            string[] array = strValor.Split(',');

            string casaDecimal = string.Empty;
            if (array.Length == 1)
                casaDecimal = "00";
            else
                casaDecimal = array[1].Length == 2 ? array[1] : array[1] + "0";

            string tempInteiros = array[0];
            string inteiros = "";

            int indice = 0;
            for (int i = tempInteiros.Length - 1; i >= 0; i--)
            {
                if (indice == 3)
                {
                    inteiros = tempInteiros[i] + "." + inteiros;
                    indice = 0;
                }
                else
                {
                    inteiros = tempInteiros[i] + inteiros;
                }
                indice++;
            }
            return "R$ " + inteiros + "," + casaDecimal.Substring(0,2);
        }
    }
}
