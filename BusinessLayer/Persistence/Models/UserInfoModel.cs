namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class UserInfoModel
    {
        public string CompCode { get; set; }
        public string UserName { get; set; }
        public string UserEmailId { get; set; }
        public string UserPassword { get; set; }
        public string UserSalt { get; set; }
        public string Token { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string OrderBy { get; set; } = "userName";
    }
}
