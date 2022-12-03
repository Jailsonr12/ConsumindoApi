using Devs2Blu.ProjetosAula.CSharpMVCRevisao.Models;
using System;

namespace Devs2Blu.ProjetosAula.CSharpMVCRevisao.Services {
    public class ServiceAPI {
        private readonly HttpClient _httpClient;

        public ServiceAPI() {
            _httpClient = new HttpClient();
        }

        public async Task<List<GetListDataCardsDTO>> GetListCards() {
            return await GetList<GetListDataCardsDTO>(URL_API);
            
        }

        public async Task<List<GetListDataCardsDTO>> GetCardByName(string name) {
            var lista = await GetListCards();
            List<GetListDataCardsDTO> listReturn = new List<GetListDataCardsDTO>() ;
            foreach(var make in lista) {
                if(make.name.Contains(name)) {
                    listReturn.Add(make);
                }
            }
            return listReturn;
        }

        #region BaseMethods

        public async Task<T> Get<T>(string url) {
            var objHttp = await GetAsync(url);

            if (!objHttp.IsSuccessStatusCode)
                return (T)(object)url;

            return await objHttp.Content.ReadFromJsonAsync<T>();

        }

        public async Task<List<T>> GetList<T>(string url) {
            var listHttp = await GetAsync(url);

            if (!listHttp.IsSuccessStatusCode)
                return new List<T>();

            return await listHttp.Content.ReadFromJsonAsync<List<T>>();

        }

        public async Task<HttpResponseMessage> GetAsync(string url) {
            var getRequest = new HttpRequestMessage() {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };
            return await _httpClient.SendAsync(getRequest);
        }
        #endregion


        #region CONSTS

        private const string URL_API = "http://makeup-api.herokuapp.com/api/v1/products.json?brand=maybelline";

        #endregion
    }
}
