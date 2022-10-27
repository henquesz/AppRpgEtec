using AppRpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AppRpgEtec.Services.Personagens
{
    public class PersonagemService : Request
    {
        private readonly Request _request;
        private string _token;
        private const string ApiUrlBase = "https://bsite.net/luizfernando987/Personagens";

        public PersonagemService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<Personagem> PostPersonagemAsync(Personagem p)
        {
            return await _request.PostAsync(ApiUrlBase, p, _token);
        }
        public async Task<ObservableCollection<Personagem>> GetPersonagensAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Personagem> listaPersonagens = await
      _request.GetAsync<ObservableCollection<Models.Personagem>>(ApiUrlBase + urlComplementar, _token);
            return listaPersonagens;
        }
        public async Task<Personagem> GetPersonagemAsync(int personagemId)
        {
            string urlComplementar = string.Format("/{0}", personagemId);
            var personagem = await _request.GetAsync<Models.Personagem>(ApiUrlBase + urlComplementar, _token);
            return personagem;
        }
        public async Task<Personagem> PutPersonagemAsync(Personagem p)
        {
            var result = await _request.PutAsync(ApiUrlBase, p, _token);
            return result;
        }
        public async Task<Personagem> DeletePersonagemAsync(int personagemId)
        {
            string urlComplementar = string.Format("/{0}", personagemId);
            await _request.DeleteAsync(ApiUrlBase + urlComplementar, _token);
            return new Personagem() { Id = personagemId };
        }
    }
}
