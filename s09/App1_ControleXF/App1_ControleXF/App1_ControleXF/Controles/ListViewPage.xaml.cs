using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1_ControleXF.Modelo;

namespace App1_ControleXF.Controles
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListViewPage : ContentPage
	{
		public ListViewPage ()
		{
			InitializeComponent ();

            List<Pessoa> lista = new List<Pessoa>();
            lista.Add(new Pessoa { Nome = "José", Idade = "20" });
            lista.Add(new Pessoa { Nome = "José", Idade = "20" });
            lista.Add(new Pessoa { Nome = "José", Idade = "20" });
            lista.Add(new Pessoa { Nome = "José", Idade = "20" });
            lista.Add(new Pessoa { Nome = "José", Idade = "20" });

            ListaPessoas.ItemsSource = lista;

        }
	}
}