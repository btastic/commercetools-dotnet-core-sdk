namespace commercetools.Sdk.HttpApi.Tests
{
    using commercetools.Sdk.HttpApi;
    using commercetools.Sdk.HttpApi.Domain;
    using commercetools.Sdk.Serialization;
    using commercetools.Sdk.Test.Helpers;
    using System.Net.Http;
    using Xunit;

    // TODO Thing of better names for tests
    // These are not real unit tests, but something like "integration tests" and a way to test code in a simple way
    public class TokenIntegrationTests : IClassFixture<ClientFixture>
    {
        private readonly ClientFixture clientFixture;

        public TokenIntegrationTests(ClientFixture clientFixture)
        {
            this.clientFixture = clientFixture;
        }

        [Fact]
        public void GetClientCredentialsToken()
        {
            IClientConfiguration clientConfiguration = TestUtils.GetClientConfiguration("Client");
            // Resetting scope to an empty string for testing purposes
            clientConfiguration.Scope = "";
            ITokenStoreManager tokenStoreManager = new InMemoryTokenStoreManager();
            ITokenProvider tokenProvider = new ClientCredentialsTokenProvider(
                this.clientFixture.GetService<IHttpClientFactory>(), 
                clientConfiguration, 
                tokenStoreManager,
                this.clientFixture.GetService<ISerializerService>());
            Token token = tokenProvider.Token;
            Assert.NotNull(token.AccessToken);
        }

        [Fact]
        public void GetClientCredentialsTokenWithScope()
        {
            IClientConfiguration clientConfiguration = TestUtils.GetClientConfiguration("ClientWithSmallerScope");
            IHttpClientFactory httpClientFactory = new MockHttpClientFactory(null, null, null);
            ITokenStoreManager tokenStoreManager = new InMemoryTokenStoreManager();
            ITokenProvider tokenProvider = new ClientCredentialsTokenProvider(
                this.clientFixture.GetService<IHttpClientFactory>(), 
                clientConfiguration, 
                tokenStoreManager,
                this.clientFixture.GetService<ISerializerService>());
            Token token = tokenProvider.Token;
            Assert.NotNull(token.AccessToken);
            Assert.Equal(clientConfiguration.Scope, token.Scope);
        }

        [Fact]
        public void GetPasswordToken()
        {
            IClientConfiguration clientConfiguration = TestUtils.GetClientConfiguration("ClientWithSmallerScope");
            IUserCredentialsStoreManager userCredentialsStoreManager = new InMemoryUserCredentialsStoreManager();
            userCredentialsStoreManager.Username = "mick.jagger@commercetools.com";
            userCredentialsStoreManager.Password = "st54e9m4";
            ITokenProvider tokenProvider = new PasswordTokenProvider(
                this.clientFixture.GetService<IHttpClientFactory>(), 
                clientConfiguration, 
                userCredentialsStoreManager, 
                this.clientFixture.GetService<ISerializerService>());
            Token token = tokenProvider.Token;
            Assert.NotNull(token.AccessToken);
        }

        [Fact]
        public void GetAnonymousTokenNoIdProvided()
        {
            IClientConfiguration clientConfiguration = TestUtils.GetClientConfiguration("ClientWithAnonymousScope");
            IAnonymousCredentialsStoreManager anonymousStoreManager = new InMemoryAnonymousCredentialsStoreManager();
            ITokenProvider tokenProvider = new AnonymousSessionTokenProvider(
                this.clientFixture.GetService<IHttpClientFactory>(), 
                clientConfiguration, 
                anonymousStoreManager,
                this.clientFixture.GetService<ISerializerService>());
            Token token = tokenProvider.Token;
            Assert.NotNull(token.AccessToken);
        }

        [Fact]
        public void GetAnonymousTokenIdProvided()
        {
            IClientConfiguration clientConfiguration = TestUtils.GetClientConfiguration("ClientWithAnonymousScope");
            IAnonymousCredentialsStoreManager anonymousStoreManager = new InMemoryAnonymousCredentialsStoreManager();
            anonymousStoreManager.AnonymousId = TestUtils.RandomString(10);
            ITokenProvider tokenProvider = new AnonymousSessionTokenProvider(
                this.clientFixture.GetService<IHttpClientFactory>(),
                clientConfiguration, 
                anonymousStoreManager, 
                this.clientFixture.GetService<ISerializerService>());
            Token token = tokenProvider.Token;
            Assert.NotNull(token.AccessToken);
        }

        [Fact]
        public void RefreshTokenPasswordFlow()
        {
            IClientConfiguration clientConfiguration = TestUtils.GetClientConfiguration("ClientWithSmallerScope");
            IUserCredentialsStoreManager userCredentialsStoreManager = new InMemoryUserCredentialsStoreManager();
            userCredentialsStoreManager.Username = "mick.jagger@commercetools.com";
            userCredentialsStoreManager.Password = "st54e9m4";
            ITokenProvider tokenProvider = new PasswordTokenProvider(
                this.clientFixture.GetService<IHttpClientFactory>(), 
                clientConfiguration, 
                userCredentialsStoreManager, 
                this.clientFixture.GetService<ISerializerService>());
            Token token = tokenProvider.Token;
            string initialAccessToken = token.AccessToken;
            // TODO Find a better way to test this (with mock objects perhaps)
            token.ExpiresIn = 0;
            token = tokenProvider.Token;
            Assert.NotEqual(token.AccessToken, initialAccessToken);
        }
    }
}