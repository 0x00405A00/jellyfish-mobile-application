using Presentation.Data.AppConfig.Abstraction;
using Presentation.Model;

namespace Presentation.Data.AppConfig.ConcreteImplements
{
    public class AccountConfig : AbstractApplicationConfig
    {
        public User User { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
        public string RegisterBase64Token { get; set; }
        public AccountConfig() 
        {

        }
        public override void SetDefaults()
        {

            UserName = String.Empty;
            Password = String.Empty;
            RemeberMe = false;
            RegisterBase64Token = null;
            base.SetDefaults();
        }
    }
}
