using FluentValidation.Results;
using TechLibrary.Api.Domain;
using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCase.Users.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestUserJson request)
        {
            Validate(request);

            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            var dbContext = new TechLibraryDbContext();
            dbContext.Users.Add(entity);
            dbContext.SaveChanges();

            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
                AccessToken = "akakka"
            };
        }

        private void Validate(RequestUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
