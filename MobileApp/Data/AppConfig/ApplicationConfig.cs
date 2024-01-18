using Presentation.Data.AppConfig.Abstraction;
using Presentation.Data.AppConfig.ConcreteImplements;

namespace Presentation.Data.AppConfig
{
    public class ApplicationConfig : AbstractApplicationConfig
    {
        public ChatConfig ChatConfig { get; set; }
        public NetworkConfig NetworkConfig { get; set; }
        public AccountConfig AccountConfig { get; set; }
        public NotificationConfig NotificationConfig { get; set; }  
        public SqlLiteConfig SqlLiteConfig { get; set; }

        public override void SetDefaults()
        {
            base.SetDefaults();
            ChatConfig = new ChatConfig();
            NetworkConfig = new NetworkConfig();    
            NotificationConfig = new NotificationConfig();
            SqlLiteConfig = new SqlLiteConfig();
            AccountConfig = new AccountConfig();
        }
    }
}
