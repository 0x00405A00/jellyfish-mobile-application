﻿using Infrastructure.Handler.Data.InternalDataInterceptor.Invoker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if ANDROID
using Infrastructure.Handler.Data.InternalDataInterceptor.Invoker.Notification.Android;
#elif IOS
using Infrastructure.Handler.Data.InternalDataInterceptor.Invoker.Notification.iOS;
#endif

namespace Infrastructure.Handler.Data.InternalDataInterceptor
{
    public class InitDataInterceptorApplicationModel
    {
        private readonly Infrastructure.Handler.Data.InternalDataInterceptor.InternalDataInterceptorApplication _internalDataInterceptorApplication;
        private readonly JellyfishWebApiRestClientInvoker _jellyfishWebApiRestClientInvoker;
        private readonly ViewModelInvoker _viewModelInvoker;
        private readonly SqlLiteDatabaseHandlerInvoker _sqlLiteDatabaseHandlerInvoker;
        private readonly NotificationInvoker _notificationInvoker;
        public InitDataInterceptorApplicationModel(
            Infrastructure.Handler.Data.InternalDataInterceptor.InternalDataInterceptorApplication internalDataInterceptorApplication,
            JellyfishWebApiRestClientInvoker jellyfishWebApiRestClientInvoker,
            ViewModelInvoker viewModelInvoker,
            SqlLiteDatabaseHandlerInvoker sqlLiteDatabaseHandlerInvoker,
            NotificationInvoker notificationInvoker)
        {

            _internalDataInterceptorApplication = internalDataInterceptorApplication;
            _internalDataInterceptorApplication.Add(jellyfishWebApiRestClientInvoker);
            _internalDataInterceptorApplication.Add(notificationInvoker);
            _internalDataInterceptorApplication.Add(sqlLiteDatabaseHandlerInvoker);
            _internalDataInterceptorApplication.Add(viewModelInvoker);

        }
    }
}
