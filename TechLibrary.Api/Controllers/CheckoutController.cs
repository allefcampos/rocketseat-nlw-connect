using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.Services;
using TechLibrary.Api.UseCase.Checkout;

namespace TechLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        [HttpPost]
        [Route("{bookId}")]
        public IActionResult BookCheckout(Guid bookId)
        {
            var loggedUser = new LoggedUserService(HttpContext);

            var useCase = new RegisterBookCheckoutUseCase(loggedUser);

            useCase.Execute(bookId);

            return NoContent();
        }
    }
}
