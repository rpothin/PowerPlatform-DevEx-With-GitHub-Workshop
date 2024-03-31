using Xunit;
using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using Microsoft.Xrm.Sdk;

namespace Dataverse.API.Testing
{
    /// <summary>
    /// Represents a class containing tests for the Dataverse API.
    /// </summary>
    public class DataverseAPITests
    {
        private ServiceClient _serviceClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataverseAPITests"/> class.
        /// </summary>
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

        /// <summary>
        /// Tests the connection to the Dataverse API.
        /// </summary>
        [Fact]
        public void TestConnection()
        {
            // Check if the connection is ready
            Assert.True(_serviceClient.IsReady);
        }

        // Create an account by passing a name and a phone number
        // And validate that the owner is automatically set to the current user
        [Fact]
        public void CreateAccount()
        {
            // Create an account entity with the following attributes
            var accountName = "Contoso";
            var phoneNumber = "123-456-7890";

            Entity account = new Entity("account");
            account["name"] = accountName;
            account["telephone1"] = phoneNumber;

            // Act
            var accountId = _serviceClient.Create(account);

            // Assert
            Assert.NotNull(accountId);

            // Retrieve the created account
            var createdAccount = _serviceClient.Retrieve("account", accountId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));

            // Validate the owner of the account
            var owner = createdAccount.GetAttributeValue<EntityReference>("ownerid");
            Assert.NotNull(owner);
        }
    }
}