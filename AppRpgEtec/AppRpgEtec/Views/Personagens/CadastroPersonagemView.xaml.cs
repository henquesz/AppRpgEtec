using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppRpgEtec.Models;

namespace AppRpgEtec.Views.Personagens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPersonagemView : ContentPage
    {
        private CadastroPersonagemViewModel cadViewModel;
        public CadastroPersonagemView()
        {
            InitializeComponent();

            cadViewModel = new CadastroPersonagemViewModel();
            BindingContext = cadViewModel;
            Title = "Novo Personagem";
        }

        public CadastroPersonagemView(Personagem p)
        {
            InitializeComponent();

            cadViewModel = new CadastroPersonagemViewModel();
            BindingContext = cadViewModel;
            Title = $"Editar {p.Nome}";

            _ = cadViewModel.CarregarPersonagem(p);
        }
    }
}