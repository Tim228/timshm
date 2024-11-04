using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WebPlan.Domain.Entity;
using WebPlan.Service.Interfaces;


namespace WebPlan.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDataRecordService _dataRecordService;

        public LoginController(IDataRecordService dataRecordService)
        {
            _dataRecordService = dataRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task Login() 
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
        }

        [Route("Login/GoogleResponse")]
        public async Task<IActionResult> GoogleRespose() //переименовать и вынести в отдельный контроллер
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "email"); //Получаем google gmail
            var email = emailClaim?.Value;

            if (email == null)  //если email не установлен, посылаем статусный код ошибки 400
                return RedirectToAction("Privacy", "Home");

            var adminRole = new Role("admin");
            var userRole = new Role("user");

            IEnumerable<DataRecord> recordIEnum = _dataRecordService.GoogleResposeService();

            DataRecord? dataRecord = recordIEnum.FirstOrDefault(p => p.Email == email);           

            if (email == "arturtimur201998@gmail.com") //список админов по gmail, можно писать сюда или передавать извне списком
                dataRecord.Role = adminRole;
            else
                dataRecord.Role = userRole;

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, dataRecord.Role.Name)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, //Установка аутентификационных куки
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddYears(1),
                    IsPersistent = true
                });
            
            return RedirectToAction("Index", "Home");
        }

        //выйти
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
