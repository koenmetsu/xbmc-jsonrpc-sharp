﻿using System;

namespace XBMC.JsonRpc
{
    public class UnknownJsonRpcErrorException : Exception
    {
        internal UnknownJsonRpcErrorException()
            : base()
        { }

        internal UnknownJsonRpcErrorException(string message)
            : base(message)
        { }

        internal UnknownJsonRpcErrorException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
