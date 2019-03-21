using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2_Tarefa.Modelos;

namespace App2_Tarefa.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cadastro : ContentPage
	{
        private byte Prioridade { get; set; }
		public Cadastro ()
		{
			InitializeComponent ();
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var Stacks = SLPrioridades.Children;
            foreach(var Linha in Stacks)
            {
                Label LblPrioridade = ((StackLayout)Linha).Children[1] as Label;
                LblPrioridade.TextColor = Color.Gray;
            }
            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            FileImageSource fisSource = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;
            string strPrioridade = fisSource.File.ToString().Replace("Resources/", "").Replace(".png", "").Replace("p", "");
            this.Prioridade = byte.Parse(strPrioridade);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            bool ErroExiste = false;
            if (string.IsNullOrWhiteSpace(TxtNome.Text))
            {
                // Erro
                ErroExiste = true;
                DisplayAlert("Erro", "Nome não preenchido!", "Ok");
            }
            if (this.Prioridade <= 0)
            {
                // Erro
                ErroExiste = true;
                DisplayAlert("Erro", "Prioridade não informada!", "Ok");
            }

            if (!ErroExiste)
            {
                // Salva dados
                Tarefa tarefa = new Tarefa
                {
                    Nome = TxtNome.Text.Trim(),
                    Prioridade = this.Prioridade
                };

                new GerenciadorTarefa().Salvar(tarefa);

                // Voltar pag listagem
                App.Current.MainPage = new NavigationPage(new Inicio());
            }
        }
    }
}