using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Data.AppConfig.Abstraction;
using Presentation.ViewModel.SettingsSubPage;

namespace Presentation.Data.AppConfig.ConcreteImplements
{
    public class NotificationConfigViewModel : AbstractConfigViewModel<NotificationConfig>
    {
        public NotificationConfigViewModel(NotificationConfig config) : base(config)
        {
        }

        public override void AddValidations()
        {

        }

        public override void MapConfigDataWithDisplayData()
        {
        }

        public override void Safe()
        {

        }
    }
}
