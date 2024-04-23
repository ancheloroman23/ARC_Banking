namespace ARC_InternetBanking.Core.Application.Dtos.Account
{
    public class ResetPasswordResponse
    {
        #region Error

        public bool HasError { get; set; }
        public string? Error { get; set; }

        #endregion
    }
}
