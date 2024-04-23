namespace ARC_InternetBanking.Core.Application.Dtos.Account
{
    public class RegisterResponse
    {
        public string? UserId { get; set; }

        #region Error

        public bool HasError { get; set; }
        public string? Error { get; set; }

        #endregion

    }
}
