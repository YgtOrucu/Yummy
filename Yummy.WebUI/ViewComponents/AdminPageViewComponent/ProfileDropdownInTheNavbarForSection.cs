using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.ForProfileInTheAdminPageDto;

namespace Yummy.WebUI.ViewComponents.AdminPageViewComponent
{
    public class ProfileDropdownInTheNavbarForSection : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileDropdownInTheNavbarForSection(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var username = User.Identity.Name;
            var client = _httpClientFactory.CreateClient("YummyClient");

            var response = await client.GetAsync($"Auths/GetProfileNavbarSection?username={username}");

            if (response.IsSuccessStatusCode)
            {
                var values = await response.Content.ReadFromJsonAsync<ProfileDto>();
                return View("~/Views/Shared/Components/AdminPageViewComponent/ProfileDropdownInTheNavbarForSection.cshtml", values);
            }
            return View("~/Views/Shared/Components/AdminPageViewComponent/ProfileDropdownInTheNavbarForSection.cshtml", new ProfileDto());
        }
    }
}
