using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2_Tarefa.Modelos;
using System.Globalization;

namespace App2_Tarefa.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();

            CultureInfo culture = new CultureInfo("pt-BR");
            string Data = DateTime.Now.ToString("dddd, dd {0} MMMM {0} yyyy", culture);
            DataHoje.Text = string.Format(Data, "de");

            CarregarTarefas();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cadastro());
        }

        private void CarregarTarefas()
        {
            // Limpa todos os elementos filhos
            SLTarefas.Children.Clear();

            List<Tarefa> Lista = new GerenciadorTarefa().Listagem();
            int i = 0;
            foreach (Tarefa tarefa in Lista)
            {
                LinhaStackLayout(tarefa, i);
                i++;
            }
        }

        public void LinhaStackLayout(Tarefa _tarefa, int _index)
        {
            View StackCentral = null;

            // Label
            if (_tarefa.DataFinalizacao == null)
            {
                StackCentral = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, Text = _tarefa.Nome };
            }
            else
            {
                StackCentral = new StackLayout() { VerticalOptions = LayoutOptions.Center, Spacing = 0, HorizontalOptions = LayoutOptions.FillAndExpand };
                ((StackLayout)StackCentral).Children.Add(new Label() { Text = _tarefa.Nome, TextColor = Color.Gray });
                ((StackLayout)StackCentral).Children.Add(new Label() { Text = "Finalizado em " + _tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy - hh:mm") + "h", TextColor = Color.Gray, FontSize = 10 });
            }

            // Imagem Delete
            Image imgDelete = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("Delete.png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                imgDelete.Source = ImageSource.FromFile("Resources/Delete.png");
            }
            TapGestureRecognizer DeleteTap = new TapGestureRecognizer();
            DeleteTap.Tapped += delegate
            {
                new GerenciadorTarefa().Deletar(_index);
                CarregarTarefas();
            };
            imgDelete.GestureRecognizers.Add(DeleteTap);
            
            // Imagem Prioridade
            Image imgPrioridade = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("p" + _tarefa.Prioridade + ".png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                imgPrioridade.Source = ImageSource.FromFile("Resources/p" + _tarefa.Prioridade + ".png");
            }

            // Imagem Check
            Image Check = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("CheckOff.png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                Check.Source = ImageSource.FromFile("Resources/CheckOff.png");
            }
            if (_tarefa.DataFinalizacao != null)
            {
                Check.Source = ImageSource.FromFile("CheckOn.png");
                if (Device.RuntimePlatform == Device.UWP)
                {
                    Check.Source = ImageSource.FromFile("Resources/CheckOn.png");
                }
            }
            TapGestureRecognizer CheckTap = new TapGestureRecognizer();
            CheckTap.Tapped += delegate
            {
                new GerenciadorTarefa().Finalizar(_index, _tarefa);
                CarregarTarefas();
            };
            Check.GestureRecognizers.Add(CheckTap);

            StackLayout Linha = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 15 };
            Linha.Children.Add(Check);
            Linha.Children.Add(StackCentral);
            Linha.Children.Add(imgPrioridade);
            Linha.Children.Add(imgDelete);

            SLTarefas.Children.Add(Linha);
        }
    }
}