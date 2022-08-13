namespace Data.Services.JWT.Interfaces
{
    using Data.Services.DtoModels.Jwt;

    public interface IJwtService
    {
        public string GenerateUserToken(RequestTokenModel request);
    }
}
