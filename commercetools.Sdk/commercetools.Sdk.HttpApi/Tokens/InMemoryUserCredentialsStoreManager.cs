﻿namespace commercetools.Sdk.HttpApi.Tokens
{
    public class InMemoryUserCredentialsStoreManager : InMemoryTokenStoreManager, IUserCredentialsStoreManager
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
