﻿using AndreasReitberger.API.Enums;
using AndreasReitberger.API.Models.REST;
using AndreasReitberger.API.Models.REST.Events;
using AndreasReitberger.API.Models.REST.Respones;
using AndreasReitberger.API.Structs;
using AndreasReitberger.Core.Interfaces;
using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AndreasReitberger.API
{
    // Documentation: https://docs.lemon.markets/
    public partial class LemonMarketsClient : BaseModel
    {
        #region Instance
        static LemonMarketsClient _instance = null;
        static readonly object Lock = new();
        public static LemonMarketsClient Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                        _instance = new LemonMarketsClient();
                }
                return _instance;
            }
            set
            {
                if (_instance == value) return;
                lock (Lock)
                {
                    _instance = value;
                }
            }
        }

        #endregion

        #region Variables
        RestClient restClient;
        HttpClient httpClient;
        int _retries = 0;
        #endregion

        #region Properties
        [JsonProperty(nameof(ApiKey))]
        string apiKey = "";
        [JsonIgnore]
        public string ApiKey
        {
            get => apiKey;
            set
            {
                if (apiKey == value)
                    return;
                apiKey = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(Address))]
        string address = "https://data.lemon.markets/v1/";
        [JsonIgnore]
        public string Address
        {
            get => address;
            set
            {
                if (address == value)
                    return;
                address = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(DefaultTimeout))]
        bool isOnline = false;
        [JsonIgnore]
        public bool IsOnline
        {
            get => isOnline;
            set
            {
                if (isOnline != value)
                {
                    isOnline = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonIgnore, XmlIgnore]
        bool _isConnecting = false;
        [JsonIgnore, XmlIgnore]
        public bool IsConnecting
        {
            get => _isConnecting;
            set
            {
                if (_isConnecting == value) return;
                _isConnecting = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(DefaultTimeout))]
        int defaultTimeout = 10000;
        [JsonIgnore]
        public int DefaultTimeout
        {
            get => defaultTimeout;
            set
            {
                if (defaultTimeout != value)
                {
                    defaultTimeout = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonProperty(nameof(RetriesWhenOffline))]
        [XmlAttribute(nameof(RetriesWhenOffline))]
        int _retriesWhenOffline = 2;
        [JsonIgnore, XmlIgnore]
        public int RetriesWhenOffline
        {
            get => _retriesWhenOffline;
            set
            {
                if (_retriesWhenOffline == value) return;
                _retriesWhenOffline = value;
                OnPropertyChanged();
            }
        }

        #region Proxy
        [JsonProperty(nameof(EnableProxy))]
        [XmlAttribute(nameof(EnableProxy))]
        bool _enableProxy = false;
        [JsonIgnore, XmlIgnore]
        public bool EnableProxy
        {
            get => _enableProxy;
            set
            {
                if (_enableProxy == value) return;
                _enableProxy = value;
                OnPropertyChanged();
                UpdateRestClientInstance();
            }
        }

        [JsonProperty(nameof(ProxyUseDefaultCredentials))]
        [XmlAttribute(nameof(ProxyUseDefaultCredentials))]
        bool _proxyUseDefaultCredentials = true;
        [JsonIgnore, XmlIgnore]
        public bool ProxyUseDefaultCredentials
        {
            get => _proxyUseDefaultCredentials;
            set
            {
                if (_proxyUseDefaultCredentials == value) return;
                _proxyUseDefaultCredentials = value;
                OnPropertyChanged();
                UpdateRestClientInstance();
            }
        }

        [JsonProperty(nameof(SecureProxyConnection))]
        [XmlAttribute(nameof(SecureProxyConnection))]
        bool _secureProxyConnection = true;
        [JsonIgnore, XmlIgnore]
        public bool SecureProxyConnection
        {
            get => _secureProxyConnection;
            private set
            {
                if (_secureProxyConnection == value) return;
                _secureProxyConnection = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(ProxyAddress))]
        [XmlAttribute(nameof(ProxyAddress))]
        string _proxyAddress = string.Empty;
        [JsonIgnore, XmlIgnore]
        public string ProxyAddress
        {
            get => _proxyAddress;
            private set
            {
                if (_proxyAddress == value) return;
                _proxyAddress = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(ProxyPort))]
        [XmlAttribute(nameof(ProxyPort))]
        int _proxyPort = 443;
        [JsonIgnore, XmlIgnore]
        public int ProxyPort
        {
            get => _proxyPort;
            private set
            {
                if (_proxyPort == value) return;
                _proxyPort = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(ProxyUser))]
        [XmlAttribute(nameof(ProxyUser))]
        string _proxyUser = string.Empty;
        [JsonIgnore, XmlIgnore]
        public string ProxyUser
        {
            get => _proxyUser;
            private set
            {
                if (_proxyUser == value) return;
                _proxyUser = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(ProxyPassword))]
        [XmlAttribute(nameof(ProxyPassword))]
        SecureString _proxyPassword;
        [JsonIgnore, XmlIgnore]
        public SecureString ProxyPassword
        {
            get => _proxyPassword;
            private set
            {
                if (_proxyPassword == value) return;
                _proxyPassword = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #endregion

        #region Constructor
        public LemonMarketsClient() { }

        public LemonMarketsClient(string apiKey)
        {
            ApiKey = apiKey;
        }

        public LemonMarketsClient(string webAddress, string apiKey)
        {
            Address = webAddress;
            ApiKey = apiKey;
        }
        #endregion

        #region EventHandlers
        public event EventHandler Error;
        protected virtual void OnError()
        {
            Error?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        protected virtual void OnError(UnhandledExceptionEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        protected virtual void OnError(LemonMarketsJsonConvertEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        public event EventHandler<LemonMarketsRestEventArgs> RestApiError;
        protected virtual void OnRestApiError(LemonMarketsRestEventArgs e)
        {
            RestApiError?.Invoke(this, e);
        }

        public event EventHandler<LemonMarketsJsonConvertEventArgs> RestJsonConvertError;
        protected virtual void OnRestJsonConvertError(LemonMarketsJsonConvertEventArgs e)
        {
            RestJsonConvertError?.Invoke(this, e);
        }
        #endregion

        #region Methods

        #region Proxy
        Uri GetProxyUri()
        {
            return ProxyAddress.StartsWith("http://") || ProxyAddress.StartsWith("https://") ? new Uri($"{ProxyAddress}:{ProxyPort}") : new Uri($"{(SecureProxyConnection ? "https" : "http")}://{ProxyAddress}:{ProxyPort}");
        }

        WebProxy GetCurrentProxy()
        {
            WebProxy proxy = new()
            {
                Address = GetProxyUri(),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = ProxyUseDefaultCredentials,
            };
            if (ProxyUseDefaultCredentials && !string.IsNullOrEmpty(ProxyUser))
            {
                proxy.Credentials = new NetworkCredential(ProxyUser, ProxyPassword);
            }
            else
            {
                proxy.UseDefaultCredentials = ProxyUseDefaultCredentials;
            }
            return proxy;
        }
        void UpdateRestClientInstance()
        {
            if (string.IsNullOrEmpty(Address))
            {
                return;
            }
            if (EnableProxy && !string.IsNullOrEmpty(ProxyAddress))
            {
                RestClientOptions options = new(Address)
                {
                    ThrowOnAnyError = true,
                    MaxTimeout = 10000,
                };
                HttpClientHandler httpHandler = new()
                {
                    UseProxy = true,
                    Proxy = GetCurrentProxy(),
                    AllowAutoRedirect = true,
                };

                httpClient = new(handler: httpHandler, disposeHandler: true);
                restClient = new(httpClient: httpClient, options: options);
            }
            else
            {
                httpClient = null;
                restClient = new(baseUrl: Address);
            }
        }
        #endregion

        #region OnlineCheck

        public async Task CheckOnlineAsync(int timeout = 10000)
        {
            CancellationTokenSource cts = new(timeout);
            await CheckOnlineAsync(cts).ConfigureAwait(false);
        }

        public async Task CheckOnlineAsync(CancellationTokenSource cts)
        {
            if (IsConnecting) return; // Avoid multiple calls
            IsConnecting = true;
            bool isReachable = false;
            try
            {
                string uriString = Address;
                try
                {
                    // Send a blank api request in order to check if the server is reachable
                    LemonMarketsApiRequestRespone respone = await SendOnlineCheckRestApiRequestAsync(
                       function: LemonMarketsEndpoints.instruments,
                       additionalParameters: new Dictionary<string, string>() { { "isin", LemonMarketsSymbols.BASF } },
                       cts: cts)
                    .ConfigureAwait(false);

                    isReachable = respone?.IsOnline == true;
                }
                catch (InvalidOperationException iexc)
                {
                    OnError(new UnhandledExceptionEventArgs(iexc, false));
                }
                catch (HttpRequestException rexc)
                {
                    OnError(new UnhandledExceptionEventArgs(rexc, false));
                }
                catch (TaskCanceledException)
                {
                    // Throws an exception on timeout, not actually an error
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            IsConnecting = false;
            // Avoid offline message for short connection loss
            if (!IsOnline || isReachable || _retries > RetriesWhenOffline)
            {
                // Do not check if the previous state was already offline
                _retries = 0;
                IsOnline = isReachable;
            }
            else
            {
                // Retry with shorter timeout to see if the connection loss is real
                _retries++;
                cts = new(3500);
                await CheckOnlineAsync(cts).ConfigureAwait(false);
            }
        }

        #endregion

        #region REST
        async Task<LemonMarketsApiRequestRespone> SendRestApiRequestAsync(
            LemonMarketsEndpoints function,
            string command = "",
            Dictionary<string, string> additionalParameters = null,
            object jsonData = null,
            Method method = Method.Get,
            CancellationTokenSource cts = default,
            string requestTargetUri = "")
        {
            LemonMarketsApiRequestRespone apiRsponeResult = new() { IsOnline = IsOnline };
            if (!IsOnline) return apiRsponeResult;
            try
            {
                if (cts == default)
                {
                    cts = new(DefaultTimeout);
                }
                if (restClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new(
                    string.IsNullOrEmpty(requestTargetUri) ?
                    string.IsNullOrEmpty(command) ? function.ToString() : $"{function}/{command}" :
                    requestTargetUri)
                {
                    RequestFormat = DataFormat.Json,
                    Method = method
                };

                if (!string.IsNullOrEmpty(ApiKey))
                {
                    request.AddHeader("Authorization", $"Bearer {ApiKey}");
                }

                if (additionalParameters?.Count > 0)
                {
                    foreach(KeyValuePair<string, string> paramter in additionalParameters)
                    {
                        request.AddParameter(paramter.Key, paramter.Value, ParameterType.QueryString);
                    }
                }

                string jsonDataString = "";
                if (jsonData is string jsonString)
                {
                    jsonDataString = jsonString;
                }
                else if (jsonData is object jsonObject)
                {
                    jsonDataString = JsonConvert.SerializeObject(jsonObject);
                }
                if(!string.IsNullOrEmpty(jsonDataString))
                {
                    _ = request.AddParameter("data", jsonDataString, ParameterType.QueryString);
                }

                Uri fullUri = restClient.BuildUri(request);
                try
                {
                    RestResponse respone = await restClient.ExecuteAsync(request, cts.Token).ConfigureAwait(false);
                    apiRsponeResult = ValidateRespone(respone, fullUri);
                }
                catch (TaskCanceledException texp)
                {
                    if (!IsOnline)
                    {
                        OnError(new UnhandledExceptionEventArgs(texp, false));
                    }
                    // Throws exception on timeout, not actually an error but indicates if the server is reachable.
                }
                catch (HttpRequestException hexp)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is not reachable.
                    if (!IsOnline)
                    {
                        OnError(new UnhandledExceptionEventArgs(hexp, false));
                    }
                }
                catch (TimeoutException toexp)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is not reachable.
                    if (!IsOnline)
                    {
                        OnError(new UnhandledExceptionEventArgs(toexp, false));
                    }
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return apiRsponeResult;
        }

        async Task<LemonMarketsApiRequestRespone> SendOnlineCheckRestApiRequestAsync(
            LemonMarketsEndpoints function,
            string command = "",
            Dictionary<string, string> additionalParameters = null,
            Method method = Method.Get,
            CancellationTokenSource cts = default,
            string requestTargetUri = ""
            )
        {
            LemonMarketsApiRequestRespone apiRsponeResult = new() { IsOnline = false };
            try
            {
                if (restClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new(
                    string.IsNullOrEmpty(requestTargetUri) ?
                    string.IsNullOrEmpty(command) ? function.ToString() : $"{function}/{command}" :
                    requestTargetUri)
                {
                    RequestFormat = DataFormat.Json,
                    Method = method
                };

                if (!string.IsNullOrEmpty(ApiKey))
                {
                    request.AddHeader("Authorization", $"Bearer {ApiKey}");
                }
                /*
                request.AddParameter(
                    "function",
                    string.IsNullOrEmpty(command) ? function.ToString() : $"{function}/{command}",
                    ParameterType.QueryString
                    );
                */
                if (additionalParameters?.Count > 0)
                {
                    foreach (KeyValuePair<string, string> paramter in additionalParameters)
                    {
                        request.AddParameter(paramter.Key, paramter.Value, ParameterType.QueryString);
                    }
                }

                Uri fullUri = restClient.BuildUri(request);
                try
                {
                    RestResponse respone = await restClient.ExecuteAsync(request, cts.Token).ConfigureAwait(false);
                    apiRsponeResult = ValidateRespone(respone, fullUri);
                }
                catch (TaskCanceledException)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is not reachable.
                }
                catch (HttpRequestException)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is not reachable.
                }
                catch (TimeoutException)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is not reachable.
                }

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return apiRsponeResult;
        }

        LemonMarketsApiRequestRespone ValidateRespone(RestResponse respone, Uri targetUri)
        {
            LemonMarketsApiRequestRespone apiRsponeResult = new() { IsOnline = IsOnline };
            try
            {
                if ((
                    respone.StatusCode == HttpStatusCode.OK || respone.StatusCode == HttpStatusCode.NoContent) &&
                    respone.ResponseStatus == ResponseStatus.Completed)
                {
                    apiRsponeResult.IsOnline = true;
                    //AuthenticationFailed = false;
                    apiRsponeResult.Result = respone.Content;
                    apiRsponeResult.Succeeded = true;
                    apiRsponeResult.EventArgs = new LemonMarketsRestEventArgs()
                    {
                        Status = respone.ResponseStatus.ToString(),
                        Exception = respone.ErrorException,
                        Message = respone.ErrorMessage,
                        Uri = targetUri,
                    };
                }
                else if (respone.StatusCode == HttpStatusCode.NonAuthoritativeInformation
                    || respone.StatusCode == HttpStatusCode.Forbidden
                    || respone.StatusCode == HttpStatusCode.Unauthorized
                    )
                {
                    apiRsponeResult.IsOnline = true;
                    apiRsponeResult.HasAuthenticationError = true;
                    apiRsponeResult.EventArgs = new LemonMarketsRestEventArgs()
                    {
                        Status = respone.ResponseStatus.ToString(),
                        Exception = respone.ErrorException,
                        Message = respone.ErrorMessage,
                        Uri = targetUri,
                    };
                }
                else if (respone.StatusCode == HttpStatusCode.Conflict)
                {
                    apiRsponeResult.IsOnline = true;
                    apiRsponeResult.HasAuthenticationError = false;
                    apiRsponeResult.EventArgs = new LemonMarketsRestEventArgs()
                    {
                        Status = respone.ResponseStatus.ToString(),
                        Exception = respone.ErrorException,
                        Message = respone.ErrorMessage,
                        Uri = targetUri,
                    };
                }
                else
                {
                    OnRestApiError(new LemonMarketsRestEventArgs()
                    {
                        Status = respone.ResponseStatus.ToString(),
                        Exception = respone.ErrorException,
                        Message = respone.ErrorMessage,
                        Uri = targetUri,
                    });
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return apiRsponeResult;
        }
        #endregion

        #region Public

        #region Trading API

        #endregion

        #region Marketing API
        public async Task<LemonMarketsInstrumentsRespone> GetInstrumentsAsync(string isin = "", string search = "", string type = "")
        {
            LemonMarketsInstrumentsRespone returnValue = new();
            LemonMarketsApiRequestRespone result = new();
            try
            {
                // Always seems to be a CSV
                Dictionary<string, string> parameters = new();

                if (!string.IsNullOrEmpty(isin)) parameters.Add("isin", isin);
                if (!string.IsNullOrEmpty(search)) parameters.Add("search", isin);
                if (!string.IsNullOrEmpty(type)) parameters.Add("type", isin);

                result = await SendRestApiRequestAsync(
                   function: LemonMarketsEndpoints.instruments,
                   additionalParameters: parameters
                   )
                    .ConfigureAwait(false);

                LemonMarketsInstrumentsRespone info = JsonConvert.DeserializeObject<LemonMarketsInstrumentsRespone>(result.Result);
                return info;
            }
            catch (JsonException jecx)
            {
                OnError(new LemonMarketsJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
                    TargetType = nameof(IsOnline),
                    Message = jecx.Message,
                });
                return returnValue;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return returnValue;
            }
        }
        public async Task<LemonMarketsInstrumentsRespone> GetInstrumentsAsync(List<string> isins)
        {
            return await GetInstrumentsAsync(isin: string.Join(",", isins)).ConfigureAwait(false);
        }

        public async Task<LemonMarketsVenuesRespone> GetVenuesAsync(string mic = "", int limit = 100, int page = 0)
        {
            LemonMarketsVenuesRespone returnValue = new();
            LemonMarketsApiRequestRespone result = new();
            try
            {
                // Always seems to be a CSV
                Dictionary<string, string> parameters = new();

                if (!string.IsNullOrEmpty(mic)) parameters.Add("mic", mic);
                if (limit != 100)  parameters.Add("limit", limit.ToString());
                if (page > 0)  parameters.Add("page", page.ToString());

                result = await SendRestApiRequestAsync(
                   function: LemonMarketsEndpoints.venues,
                   additionalParameters: parameters
                   )
                    .ConfigureAwait(false);

                LemonMarketsVenuesRespone info = JsonConvert.DeserializeObject<LemonMarketsVenuesRespone>(result.Result);
                return info;
            }
            catch (JsonException jecx)
            {
                OnError(new LemonMarketsJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
                    TargetType = nameof(IsOnline),
                    Message = jecx.Message,
                });
                return returnValue;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return returnValue;
            }
        }

        public async Task<LemonMarketsQuotesRespone> GetQuotesAsync(string isin, string mic = "", bool decimals = true, bool epoch = false, bool latest = true)
        {
            LemonMarketsQuotesRespone returnValue = new();
            LemonMarketsApiRequestRespone result = new();
            try
            {
                // Always seems to be a CSV
                Dictionary<string, string> parameters = new();
                parameters.Add("isin", isin);
                parameters.Add("decimals", decimals.ToString());
                parameters.Add("epoch", epoch.ToString());
                if(!string.IsNullOrEmpty(mic)) parameters.Add("mic", mic);

                result = await SendRestApiRequestAsync(
                   function: LemonMarketsEndpoints.quotes,
                   command: latest ? "latest" : "",
                   additionalParameters: parameters
                   )
                    .ConfigureAwait(false);

                LemonMarketsQuotesRespone info = JsonConvert.DeserializeObject<LemonMarketsQuotesRespone>(result.Result);
                return info;
            }
            catch (JsonException jecx)
            {
                OnError(new LemonMarketsJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
                    TargetType = nameof(IsOnline),
                    Message = jecx.Message,
                });
                return returnValue;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return returnValue;
            }
        }

        #endregion

        #endregion

        #endregion
    }
}