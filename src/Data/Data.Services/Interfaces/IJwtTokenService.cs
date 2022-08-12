namespace Data.Services.Interfaces
{
    using Data.Models.DtoModels;

    public interface IJwtTokenService
    {
        public string GenerateUserToken(RequestTokenModel request);
    }
}
