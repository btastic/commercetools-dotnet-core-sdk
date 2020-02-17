﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace commercetools.Sdk.HttpApi.Tokens
{
    internal class TokenFlowMapper : ITokenFlowMapper
    {
        public TokenFlowMapper()
        {
            this.Clients = new ConcurrentDictionary<string, ITokenFlowRegister>();
        }

        public IDictionary<string, ITokenFlowRegister> Clients { get; private set; }

        public ITokenFlowRegister TokenFlowRegister => this.Clients.FirstOrDefault().Value;

        public ITokenFlowRegister GetTokenFlowRegisterForClient(string name)
        {
            if (this.Clients.ContainsKey(name))
            {
                return this.Clients[name];
            }

            throw new ArgumentException($"Client with name {name} not exists", nameof(name));
        }
    }
}
