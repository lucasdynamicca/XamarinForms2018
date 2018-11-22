using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using App01_ConsultarCEP.Servico.Modelo;
using Newtonsoft.Json;

namespace App01_ConsultarCEP.Servico.Modelo
{
    public class ViaCEPServico
    {
        private static string EnderecoURL = "http://viacep.com.br/ws/{0}/json";

        public static Endereco BuscarEnderecoViaCEP(string _cep)
        {
            // Formata a página a ser acessada
            string novoEnderecoURL = string.Format(EnderecoURL, _cep);

            // Acessa a página e retorna o conteúdo
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(novoEnderecoURL);

            // Desserializa o conteúdo web na classe Endereco
            Endereco end = JsonConvert.DeserializeObject<Endereco>(conteudo);
            if (end.cep == null) return null;

            return end;
        }
    }
}
