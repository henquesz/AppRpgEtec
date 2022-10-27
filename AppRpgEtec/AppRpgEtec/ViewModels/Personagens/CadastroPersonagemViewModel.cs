using System;
using AppRpgEtec.Services.Personagens;
using Xamarin.Forms;
using AppRpgEtec.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppRpgEtec.ViewModels;
using System.Windows.Input;
using System.Linq;

public class CadastroPersonagemViewModel : BaseViewModel
{
    private PersonagemService pService;
    public ICommand SalvarCommand { get; set; }
    public ICommand CancelarCommand { get; set; }
    public CadastroPersonagemViewModel()
    {
        string token = "";
        pService = new PersonagemService(token);

        _ = ObterClasses();
        SalvarCommand = new Command(async () => await SalvarPersongem());
        CancelarCommand = new Command(async => CancelarCadastro());
    }

    private TipoClasse tipoClasseSelecionado;
    public TipoClasse TipoClasseSelecionado
    {
        get { return tipoClasseSelecionado; }
        set
        {
            if (value != null)
            {
                tipoClasseSelecionado = value;
                OnPropertyChanged();
            }
        }
    }

    private int id;
    private string nome;
    private int pontosVida;
    private int forca;
    private int defesa;
    private int inteligencia;
    private int disputas;
    private int vitorias;
    private int derrotas;

    public int Id
    {
        get => id;
        set
        {
            id = value;
            OnPropertyChanged();
        }
    }
    public String Nome
    {
        get => nome;
        set
        {
            nome = value;
            OnPropertyChanged();
        }
    }
    public int PontosVida
    {
        get => pontosVida;
        set 
        {
            pontosVida = value;
            OnPropertyChanged();
        }
    }
    public int Forca
    {
        get => forca;
        set
        {
            forca = value;
            OnPropertyChanged();
        }
    }
    public int Defesa
    {
        get => defesa;
        set
        {
            defesa = value;
            OnPropertyChanged();
        }
    }
    public int Inteligencia
    {
        get => inteligencia;
        set
        {
            inteligencia = value;
            OnPropertyChanged();
        }
    }
    public int Disputas
    {
        get => disputas;
        set
        {
            disputas = value;
            OnPropertyChanged();
        }
    }
    public int Vitorias
    {
        get => vitorias;
        set
        {
            vitorias = value;
            OnPropertyChanged();
        }
    }
    public int Derrotas
    {
        get => derrotas;
        set
        {
            derrotas = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<TipoClasse> listaTiposClasse;
    public ObservableCollection<TipoClasse> ListaTiposClasse
    {
        get { return listaTiposClasse; }
        set
        {
            if (value != null)
            {
                listaTiposClasse = value;
                OnPropertyChanged();
            }
        }
    }

    public async Task ObterClasses()
    {
        try
        {
            ListaTiposClasse = new ObservableCollection<TipoClasse>();
            ListaTiposClasse.Add(new TipoClasse() { Id = 1, Descricao = "Cavaleiro" });
            ListaTiposClasse.Add(new TipoClasse() { Id = 2, Descricao = "Mago" });
            ListaTiposClasse.Add(new TipoClasse() { Id = 3, Descricao = "Clerigo" });
            OnPropertyChanged(nameof(ListaTiposClasse));
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
        }
    }

    public async Task SalvarPersongem()
    {
        try
        {
            Personagem model = new Personagem()
            {
                Nome = this.nome,
                PontosVida = this.pontosVida,
                Defesa = this.defesa,
                Derrotas = this.derrotas,
                Disputas = this.disputas,
                Forca = this.forca,
                Inteligencia = this.inteligencia,
                Vitorias = this.vitorias,
                Id = this.id,
                Classe = (ClasseEnum)tipoClasseSelecionado.Id
            };
            if (model.Id == 0)
                await pService.PostPersonagemAsync(model);
            else
                await pService.PutPersonagemAsync(model);

            await Application.Current.MainPage
                .DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");

            await Application.Current.MainPage.Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
}
    }

    public async Task CarregarPersonagem(Personagem p)
    {
        try
        {
            Nome = p.Nome;
            PontosVida = p.PontosVida;
            Defesa = p.Defesa;
            Derrotas = p.Derrotas;
            Disputas = p.Disputas;
            Forca = p.Forca;
            Inteligencia = p.Inteligencia;
            Vitorias = p.Vitorias;
            Id = p.Id;
            TipoClasseSelecionado = this.listaTiposClasse.FirstOrDefault(busca => busca.Id == (int)p.Classe);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
        } 
    }

    public async void CancelarCadastro()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}
