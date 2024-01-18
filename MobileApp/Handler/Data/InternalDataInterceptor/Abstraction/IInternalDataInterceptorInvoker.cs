﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handler.Data.InternalDataInterceptor.Abstraction
{
    public interface IInternalDataInterceptorInvoker
    {

        Task Invoke(params object[] data);
    }
}
