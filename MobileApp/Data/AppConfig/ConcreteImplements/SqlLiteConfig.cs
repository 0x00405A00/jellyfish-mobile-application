using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Attribute;
using Presentation.Data.AppConfig.Abstraction;

namespace Presentation.Data.AppConfig.ConcreteImplements
{
    public class SqlLiteConfig : AbstractApplicationConfig
    {

        public bool SchemaCreated { get; set; }
        public string DatabasePath { get; set; }
    }
}
