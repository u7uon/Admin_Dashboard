using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace Admin_Dashboard.Service
{
    public class HandleRequest : DelegatingHandler 
    {
        private readonly IJSRuntime _js;
        private readonly NavigationManager nav;

        public HandleRequest(IJSRuntime js, NavigationManager nav)
        {
            _js = js;
            this.nav = nav;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "token");

            if(string.IsNullOrEmpty(token))
            {
                nav.NavigateTo("/login", true);
            }    

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                nav.NavigateTo("/login", true);
            }


            return response;
        }


    }
}
