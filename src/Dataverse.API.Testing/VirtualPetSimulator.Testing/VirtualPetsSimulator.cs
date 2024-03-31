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
        
        /// <summary>
        /// Tests the creation of a pet entity.
        /// </summary>
        /// <remarks>
        /// The test creates a pet entity with a random name and validates the lifepoints and happinesspoints of the pet.
        /// </remarks>
        [Fact]
        public void CreatePet()
        {
            // Create a pet entity with the following attributes
            List<string> petNames = new List<string> { "Fluffy", "Sparky", "Rover", "Bella", "Max", "Lucy", "Charlie", "Molly", "Buddy", "Daisy" };

            Random random = new Random();
            int index = random.Next(petNames.Count);

            string randomPetName = petNames[index];

            Entity pet = new Entity("rpo_pet");
            pet["rpo_name"] = randomPetName;

            // Act
            var petId = _serviceClient.Create(pet);

            // Assert
            Assert.NotNull(petId);

            // Wait for 15 seconds
            System.Threading.Thread.Sleep(15000);

            // Retrieve the created pet
            var createdPet = _serviceClient.Retrieve("rpo_pet", petId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));

            // Validate the lifepoints and happinesspoints of the pet
            var lifepoints = createdPet.GetAttributeValue<int>("rpo_lifepoints");
            var happinesspoints = createdPet.GetAttributeValue<int>("rpo_happinesspoints");

            Assert.Equal(100000, lifepoints);
            Assert.Equal(100000, happinesspoints);
        }
    }
}