using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MvcCoreNacbarAplicacao
{
    public static class NacbarAplicacao
    {
        public static int IntIdEmpresa { get; set; } = 1;
        public static string StrNomeEmpresa { get; set; }
        public static bool BolRedirect { get; set; } = false;

        public enum EnumFormato
        {
            CEP,
            TELEFONE
        }

        /// <summary>
        /// Utilize RemoveFormato() antes de utilizar essa função para que funcione corretamente
        /// Formate o texto de acordo com o formato desejado
        /// </summary>
        /// <param name="strTexto"></param>
        /// <param name="enumFormato">Formato do texto</param>
        /// <returns></returns>
        public static string FormataTexto(this string strTexto, EnumFormato? enumFormato = null)
        {
            if (string.IsNullOrEmpty(strTexto) || (strTexto.Length != 10 || strTexto.Length != 11 || strTexto.Length != 8))
                return null;

            if (enumFormato == EnumFormato.TELEFONE)
            {
                if (strTexto.Length == 10)
                    strTexto = "(" + strTexto.Substring(0, 2) + ")" + strTexto.Substring(2, 4) + "-" + strTexto.Substring(6, 4);
                else if (strTexto.Length == 11)
                    strTexto = "(" + strTexto.Substring(0, 2) + ")" + strTexto.Substring(2, 5) + "-" + strTexto.Substring(7, 4);
            }
            else if (enumFormato == EnumFormato.CEP)
            {
                strTexto = strTexto.Substring(0, 5) + "-" + strTexto.Substring(5, 3);
            }
            return strTexto;
        }

        /// <summary>
        /// Remove caractéres que não seja números no texto
        /// </summary>
        /// <param name="strTexto">Texto a ser formatado somento com números</param>
        /// <returns></returns>
        public static string RemoveFormato(this string strTexto)
        {
            if (!string.IsNullOrEmpty(strTexto))
            {
                foreach (char item in strTexto)
                {
                    if (!char.IsNumber(item))
                        strTexto = strTexto.Replace(item.ToString(), "");
                }
            }

            return strTexto;
        }

        /// <summary>
        /// Remove caractéres especiais do texto
        /// </summary>
        /// <param name="strTexto">Texto a ser formatado sem caractéres especiais</param>
        /// <returns></returns>
        public static string RemoveAcento(this string strTexto)
        {
            if (!string.IsNullOrEmpty(strTexto))
            {
                StringBuilder strRetorno = new StringBuilder();
                var arrayText = strTexto.Normalize(NormalizationForm.FormD).ToCharArray();
                foreach (char letter in arrayText)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                        strRetorno.Append(letter);
                }
                return strRetorno.ToString();
            }
            return strTexto;
        }
    }

    public class BuscaCnpj
    {
        public System.Collections.Generic.List<AtividadePrincipal> atividade_principal { get; set; }
        public string data_situacao { get; set; }
        public string nome { get; set; }
        public string uf { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public System.Collections.Generic.List<AtividadesSecundarias> atividades_secundarias { get; set; }
        public string situacao { get; set; }
        public string bairro { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string cep { get; set; }
        public string municipio { get; set; }
        public string abertura { get; set; }
        public string natureza_juridica { get; set; }
        public string cnpj { get; set; }
        public string ultima_atualizacao { get; set; }
        public string status { get; set; }
        public string tipo { get; set; }
        public string fantasia { get; set; }
        public string complemento { get; set; }
        public string efr { get; set; }
        public string motivo_situacao { get; set; }
        public string situacao_especial { get; set; }
        public string data_situacao_especial { get; set; }
        public int idEstado { get; set; }
        public int idMunicipio { get; set; }
        public int idBairro { get; set; }
        public string strObservacao { get; set; }
    }
    public class AtividadePrincipal
    {
        public string text { get; set; }

        public string code { get; set; }
    }
    public class AtividadesSecundarias
    {
        public string text { get; set; }

        public string code { get; set; }
    }

}
