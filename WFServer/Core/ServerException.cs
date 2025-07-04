using System;
using System.Collections.Generic;
using System.Text;

namespace WFServer.Core
{
    public class ServerException : Exception
    {
        public ServerException(string message)
            : base (message)
        {

        }
    }
}
