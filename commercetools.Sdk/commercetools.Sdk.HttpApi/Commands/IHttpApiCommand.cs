﻿using commercetools.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace commercetools.Sdk.HttpApi
{
    public interface IHttpApiCommand : ICommand
    {
        HttpMethod HttpMethod { get; }
        // TODO Think of a better name
        string RequestUriEnd { get; }
    }
}