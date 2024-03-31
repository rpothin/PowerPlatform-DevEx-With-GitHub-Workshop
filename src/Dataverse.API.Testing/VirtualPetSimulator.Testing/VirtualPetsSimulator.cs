using Xunit;
using Microsoft.PowerPlatform.Dataverse.Client;
using System;

namespace Dataverse.API.Testing
{
    public class DataverseAPITests
    {
        private ServiceClient _serviceClient;

        public DataverseAPITests()
        {
            // Get environment variables
            var url = Environment.GetEnvironmentVariable("DATASERVICE_URL");
            var clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            var clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

            // Create connection string
            var connectionString = $"AuthType=ClientSecret;Url={url};ClientId={clientId};ClientSecret={clientSecret}";

            // Initialize service client
            _serviceClient = new ServiceClient(connectionString);
        }

        [Fact]
        public void TestConnection()
        {
            // Check if the connection is ready
            Assert.True(_serviceClient.IsReady);
        }

        // Add more tests here...
    }
}