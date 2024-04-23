namespace ARC_InternetBanking.Core.Application.Dtos.Account
{
    public class ForgotPasswordResponse
    {
        #region Error

        public bool HasError { get; set; }
        public string? Error { get; set; }

        #endregion
    }
}
