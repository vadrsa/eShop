using Flurl.Http;
using Flurl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopUI.Infrastructure.Interfaces;
using System.Threading;
using System.Net.Http;

namespace eShopUI.Infrastructure.Api
{
    public abstract class RestConsumingServiceBase
    {
        private string _controllerName;

        public RestConsumingServiceBase(string controllerName)
        {
            _controllerName = controllerName;
        }

        protected string ControllerName
        {
            get
            {
                return _controllerName;
            }
        }

        protected string ControllerPath
        {
            get
            {
                return ApiConfig.BaseUrl + ControllerName + "/";
            }
        }
    }
    public class RestInfoConsumingService<TInfo> : RestConsumingServiceBase, IRestInfoConsumingService<TInfo>
    {
        public RestInfoConsumingService(string controllerName) : base(controllerName)
        {
        }

        public async Task<List<TInfo>> Get(CancellationToken token = new CancellationToken())
        {
            List<TInfo> all = await ControllerPath.GetJsonAsync<List<TInfo>>(token).ConfigureAwait(false);
            return all;
            //return all.OfType<T>().ToList();
        }

        public async Task<bool> Delete(TInfo info, CancellationToken token = new CancellationToken())
        {
            return await ControllerPath.SendJsonAsync(HttpMethod.Delete, info, token).ReceiveJson<bool>();
        }

    }

    public class RestDetailConsumingService<TDetail> : RestConsumingServiceBase, IRestDetailConsumingService<TDetail>
    {
        public RestDetailConsumingService(string controllerName) : base(controllerName)
        {
        }

        public async Task<TDetail> GetByID(int id, CancellationToken token = new CancellationToken())
        {

            TDetail prod = await (ControllerPath + id).GetJsonAsync<TDetail>(token).ConfigureAwait(false);
            return prod;
            //return all.OfType<T>().ToList();
        }

        public async Task<TDetail> Add(TDetail details, CancellationToken token = new CancellationToken())
        {
            var response = await (ControllerPath).PostJsonAsync(details, token).ReceiveJson<TDetail>().ConfigureAwait(false);
            return response;
        }

        public async Task<bool> Update(TDetail details, CancellationToken token = new CancellationToken())
        {
            var response = await (ControllerPath).PutJsonAsync(details, token).ConfigureAwait(false);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }

    public class RestConsumingService<T> : RestConsumingServiceBase, IRestDetailConsumingService<T>, IRestInfoConsumingService<T>
    {
        private RestInfoConsumingService<T> _infoService;
        private RestDetailConsumingService<T> _detailService;
        public RestConsumingService(string controllerName) : base(controllerName)
        {
            _infoService = new RestInfoConsumingService<T>(controllerName);
            _detailService = new RestDetailConsumingService<T>(controllerName);
        }

        public Task<T> Add(T details, CancellationToken token = default(CancellationToken))
        {
            return _detailService.Add(details, token);
        }

        public Task<bool> Delete(T obj, CancellationToken token = default(CancellationToken))
        {
            return _infoService.Delete(obj, token);
        }

        public Task<List<T>> Get(CancellationToken token = default(CancellationToken))
        {
            return _infoService.Get(token);
        }

        public Task<T> GetByID(int id, CancellationToken token = default(CancellationToken))
        {
            return _detailService.GetByID(id, token);
        }

        public Task<bool> Update(T details, CancellationToken token = default(CancellationToken))
        {
            return _detailService.Update(details, token);

        }
    }
}
