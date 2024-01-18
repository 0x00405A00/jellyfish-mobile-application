using System;
using System.ComponentModel;

namespace Presentation.Data.AppConfig.Abstraction
{
    [Serializable]
    public abstract class AbstractApplicationConfig : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected AbstractApplicationConfig()
        {
            SetDefaults();
        }
        public virtual void SetDefaults()
        {

        }

        
    }
}
