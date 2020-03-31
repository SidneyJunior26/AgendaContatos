using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos
{
    public class ManipuladorDeArquivos
    {
        private static string EnderecoDoArquivo = AppDomain.CurrentDomain.BaseDirectory + "Contatos.txt";
        public static List<Contato> LerArquivo()
        {
            List<Contato> contatosList = new List<Contato>();
            if (File.Exists(@EnderecoDoArquivo))
            {
                using (StreamReader sr = File.OpenText(@EnderecoDoArquivo))
                {
                    while (sr.Peek() >= 0) // Verifica quantidade de caracteres
                    {
                        string linha = sr.ReadLine();
                        string[] linhaComSplit = linha.Split(';'); // Separa por ;
                        if (linhaComSplit.Count() == 3)
                        {
                            Contato contato = new Contato();
                            contato.Nome = linhaComSplit[0];
                            contato.Email = linhaComSplit[1];
                            contato.NumeroTelefone = linhaComSplit[2];
                            contatosList.Add(contato);
                        }
                    }
                }
            }
            return contatosList;
        }

        public static void EscreverArquivo(List<Contato> contatosList)
        {
            using (StreamWriter sw = new StreamWriter(EnderecoDoArquivo, false))
            {
                foreach (Contato contato in contatosList)
                {
                    string linha = string.Format("{0};{1};{2}", contato.Nome, contato.Email, contato.NumeroTelefone);
                    sw.WriteLine(linha);
                }
                sw.Flush(); // Limpar Buffer
            }
        }
    }
}
