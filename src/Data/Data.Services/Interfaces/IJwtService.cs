namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels.Jwt;

    public interface IJwtService
    {
        public string GenerateUserToken(RequestTokenModel request);
    }
}
