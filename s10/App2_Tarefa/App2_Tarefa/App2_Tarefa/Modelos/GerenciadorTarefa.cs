using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App2_Tarefa.Modelos
{
    public class GerenciadorTarefa
    {
        private List<Tarefa> Lista { get; set; }

        public void Salvar(Tarefa _tarefa)
        {
            Lista = Listagem();
            Lista.Add(_tarefa);
            SalvarNoProperties(Lista);
        }
        public void Deletar(int _index)
        {
            Lista = Listagem();
            Lista.RemoveAt(_index);
            SalvarNoProperties(Lista);
        }
        public void Finalizar(int _index, Tarefa _tarefa)
        {
            Lista = Listagem();
            Lista.RemoveAt(_index);

            _tarefa.DataFinalizacao = DateTime.Now;
            Lista.Add(_tarefa);
            SalvarNoProperties(Lista);
        }
        public List<Tarefa> Listagem()
        {
            return ListagemNoProperties();
        }
        private void SalvarNoProperties(List<Tarefa> _lista)
        {
            if (App.Current.Properties.ContainsKey("Tarefas"))
            {
                App.Current.Properties.Remove("Tarefas");
            }

            // Serializa a lista para poder salvar
            string jsonVal = JsonConvert.SerializeObject(Lista);

            App.Current.Properties.Add("Tarefas", jsonVal);
        }
        private List<Tarefa> ListagemNoProperties()
        {
            if (App.Current.Properties.ContainsKey("Tarefas"))
            {
                string jsonVal = (string)App.Current.Properties["Tarefas"];
                List<Tarefa> Lista = JsonConvert.DeserializeObject<List<Tarefa>>(jsonVal);
                return Lista;
                //return (List<Tarefa>)App.Current.Properties["Tarefas"];
            }


            return new List<Tarefa>();
        }

    }
}
