using Conta.Mobile.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Conta.Mobile
{
    public static class Api
    {

        public static bool IsUserLoggedIn
        {
            get => GetValueOrDefault("IsLoggedIn", false);
            set => AddOrUpdateValue("IsLoggedIn", value);
        }

        public static string AuthAccessToken
        {
            get => GetValueOrDefault("access_token", string.Empty);
            set => AddOrUpdateValue("access_token", value);
        }

        private const string ApiUrl = "http://api.tecnofit.com.br";


        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public async static Task<ApiResult<bool>> CriarConta(CreateAccount account)
        {
            var result = new ApiResult<bool> { Response = true };
            
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders
                        .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders
                        .Authorization = new AuthenticationHeaderValue("Bearer", AuthAccessToken);

                    //http://api.tecnofit.com.br/financeiro/conta-pr/cadastro

                    var url = $"{ApiUrl}/financeiro/conta-pr/cadastro";

                    var accoutnString = JsonConvert.SerializeObject(account, SerializerSettings);
                    var content = new StringContent(accoutnString);
                    var res = await httpClient.PostAsync(url, content);
                    var strContent = await res.Content.ReadAsStringAsync();
                    result.Response = res.IsSuccessStatusCode;
                }

                return result;
            }
            catch (Exception e)
            {
                result.Ok = false;
                result.Message = e.Message;
            }
            return result;
        }

        public async static Task<ApiResult<bool>> Login(User user)
        {
            var result = new ApiResult<bool> { Ok = true, Message = "Successuful login" };
            if (IsUserLoggedIn) return result;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders
                        .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var userString = JsonConvert.SerializeObject(user, SerializerSettings);
                    var content = new StringContent(userString);
                    var res = await httpClient.PostAsync($"{ApiUrl}/auth", content);
                    var strContent = await res.Content.ReadAsStringAsync();
                    var ret = JsonConvert.DeserializeObject<ApiToken>(strContent);
                    AuthAccessToken = ret.Token;
                }

                IsUserLoggedIn = true;

                return result;
            }
            catch (Exception e)
            {
                IsUserLoggedIn = false;

                result.Ok = false;
                result.Message = e.Message;
                return result;
            }
        }

        public async static Task<ApiResult<Statement>> GetContas(DateTime? data)
        {
            var result = new ApiResult<Statement> { Response = new Statement() };
            if (!data.HasValue) return result;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders
                        .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders
                        .Authorization = new AuthenticationHeaderValue("Bearer", AuthAccessToken);

                    //http://api.tecnofit.com.br/financeiro/extrato/dia/1036/2018-10-23

                    var url = $"{ApiUrl}/financeiro/extrato/dia/1036/";
                    url += data.Value.ToString("yyyy-MM-dd");
                    var res = await httpClient.GetAsync(url);
                    var strContent = await res.Content.ReadAsStringAsync();
                    result.Response = JsonConvert.DeserializeObject<Statement>(strContent);
                }

                return result;
            }
            catch (Exception e)
            {
                result.Ok = false;
                result.Message = e.Message;
            }
            return result;
        }

        public async static Task<ApiResult<Parameter>> Cadastros()
        {

            var result = new ApiResult<Parameter> { Response = new Parameter() };

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders
                        .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders
                        .Authorization = new AuthenticationHeaderValue("Bearer", AuthAccessToken);

                    //http://api.tecnofit.com.br/financeiro/conta-pr/1036/R

                    var url = $"{ApiUrl}/financeiro/conta-pr/1036/R";
                    var res = await httpClient.GetAsync(url);
                    var strContent = await res.Content.ReadAsStringAsync();
                    result.Response = JsonConvert.DeserializeObject<Parameter>(strContent);
                }

                return result;
            }
            catch (Exception e)
            {
                result.Ok = false;
                result.Message = e.Message;
            }
            return result;
        }

        private static Task AddOrUpdateValue(string key, bool value)
        {
            return AddOrUpdateValueInternal(key, value);
        }

        private static Task AddOrUpdateValue(string key, string value)
        {
            return AddOrUpdateValueInternal(key, value);
        }

        private static bool GetValueOrDefault(string key, bool defaultValue)
        {
            return GetValueOrDefaultInternal(key, defaultValue);
        }

        private static string GetValueOrDefault(string key, string defaultValue)
        {
            return GetValueOrDefaultInternal(key, defaultValue);
        }

        private static T GetValueOrDefaultInternal<T>(string key, T defaultValue = default(T))
        {
            object value = null;
            if (Application.Current.Properties.ContainsKey(key))
            {
                value = Application.Current.Properties[key];
            }
            return null != value ? (T)value : defaultValue;
        }
        private static async Task AddOrUpdateValueInternal<T>(string key, T value)
        {
            if (value == null)
            {
                await Remove(key);
            }

            Application.Current.Properties[key] = value;
            try
            {
                await Application.Current.SavePropertiesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to save: " + key, " Message: " + ex.Message);
            }
        }

        private static async Task Remove(string key)
        {
            try
            {
                if (Application.Current.Properties[key] != null)
                {
                    Application.Current.Properties.Remove(key);
                    await Application.Current.SavePropertiesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to remove: " + key, " Message: " + ex.Message);
            }
        }
    }
}
