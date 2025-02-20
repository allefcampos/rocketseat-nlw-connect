using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Infraestructure.Secutiry.Cryptography;
using TechLibrary.Api.Infraestructure.Secutiry.Tokens;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCase.Login
{
    public class DoLoginUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestLoginJson request)
        {
            var dbContext = new TechLibraryDbContext();

            var entity = dbContext
                            .Users
                            .FirstOrDefault(user => user.Email.Equals(request.Email));

            if (entity is null)
                throw new InvalidLoginException();

            var cryptography = new BCryptAlgorithm();
            var passwordIsValid = cryptography.Verify(request.Password, entity);

            if (!passwordIsValid)
                throw new InvalidLoginException();

            var tokenGenerator = new JwtTokenGenerator();

            return new ResponseRegisteredUserJson
            {
                Name = entity.Name,
                AccessToken = tokenGenerator.Generate(entity)
            };
        }
    }
}
