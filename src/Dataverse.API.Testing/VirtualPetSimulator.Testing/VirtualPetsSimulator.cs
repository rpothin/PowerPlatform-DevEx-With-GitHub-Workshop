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

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                throw new InvalidOperationException("Required environment variables are not set.");
            }

            // Create connection string
            var connectionString = $"AuthType=ClientSecret;Url={url};ClientId={clientId};ClientSecret={clientSecret}";

            try
            {
                // Initialize service client
                _serviceClient = new ServiceClient(connectionString);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize ServiceClient.", ex);
            }
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