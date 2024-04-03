using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using VirtualPetsSimulator.Helpers;

namespace Dataverse.API.Testing
{
    /// <summary>
    /// Represents a class containing tests for the Dataverse API.
    /// </summary>
    public class DataverseAPITests
    {
        private ServiceClient _serviceClient;
        private Guid _petId;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataverseAPITests"/> class.
        /// </summary>
        /// <remarks>
        /// The constructor initializes the service client.
        /// </remarks>
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
        /// Tests the creation of a pet entity.
        /// </summary>
        /// <remarks>
        /// The test creates a pet entity with a random name.
        /// </remarks>
        [Fact]
        public void CreatePet_SetOnlyName()
        {
            // Act
            _petId = PetHelper.CreateRandomPet(_serviceClient);

            // Assert
            Assert.NotEqual(Guid.Empty, _petId);

            // Wait for 20 seconds
            System.Threading.Thread.Sleep(20000);

            // Assert that the life points and the happiness points are set to 100000 or 99990
            Assert.True(PetHelper.ArePetLifeAndHappinessPointsCorrectlyInitialized(_serviceClient, _petId));
        }

        /// <summary>
        /// Tests the creation of a pet entity.
        /// </summary>
        /// <remarks>
        /// The test creates a pet entity with a random name and the specified life and happiness points.
        /// </remarks>
        [Fact]
        public void CreatePet_SetNameAndPoints()
        {
            // Act
            var petId = PetHelper.CreateRandomPetWithLifeAndHappinessPoints(_serviceClient, 50000, 60000);

            // Assert
            Assert.NotEqual(Guid.Empty, petId);

            // Wait for 20 seconds
            System.Threading.Thread.Sleep(20000);

            // Assert that the life points and the happiness points are set to 100000 or 99990
            Assert.True(PetHelper.ArePetLifeAndHappinessPointsCorrectlyInitialized(_serviceClient, petId));

            // Delete the pet
            //PetHelper.DeletePet(_serviceClient, petId);
        }

/*
        /// <summary>
        /// Tests the decrease of pet points over time.
        /// </summary>
        /// <remarks>
        /// The test retrieves a pet entity and waits for 3 minutes.
        /// It then retrieves the pet entity again and validates that the lifepoints and happinesspoints have decreased by at least 20 points.
        /// </remarks>
        [Fact]
        public void PetPoints_DecreaseOverTime()
        {
            // Retrieve pet in initial state
            Entity petInInitialState = _serviceClient.Retrieve("rpo_pet", _petId, new ColumnSet("rpo_lifepoints", "rpo_happinesspoints"));

            // Wait for 3 minutes
            System.Threading.Thread.Sleep(180000);

            // Retrieve the pet
            var pet = _serviceClient.Retrieve("rpo_pet", _petId, new ColumnSet("rpo_lifepoints", "rpo_happinesspoints"));

            // Validate the lifepoints and happinesspoints of the pet
            var initialLifePoints = petInInitialState.GetAttributeValue<int>("rpo_lifepoints");
            var initialHappinessPoints = petInInitialState.GetAttributeValue<int>("rpo_happinesspoints");
            var lifePoints = pet.GetAttributeValue<int>("rpo_lifepoints");
            var happinessPoints = pet.GetAttributeValue<int>("rpo_happinesspoints");

            // Check if the lifepoints and happinesspoints have decreased by at least 20 points
            Assert.True(initialLifePoints - lifePoints >= 20);
            Assert.True(initialHappinessPoints - happinessPoints >= 20);
        }
    
        /// <summary>
        /// Tests the feeding of a pet entity.
        /// </summary>
        /// <remarks>
        /// The test update the life points of the pet entity to a random value between 10000 and 90000.
        /// It then create a feeding activity for the pet entity with a random quantity of food between 10, 50, 100, 1000.
        /// The test waits for 20 seconds and validates that the lifepoints of the pet entity have increased by the quantity of food selected.
        /// </remarks>
        [Fact]
        public void FeedPet_NotReachingInitialLifePoints()
        {
            // Update the life points of the pet to a random value between 10000 and 90000
            var random = new Random();
            var lifePoints = random.Next(10000, 90000);
            PetHelper.UpdatePetLifePoints(_serviceClient, _petId, lifePoints);

            // Retrieve the pet
            var petBeforeFeeding = _serviceClient.Retrieve("rpo_pet", _petId, new ColumnSet("rpo_lifepoints"));
            int initialLifePoints = petBeforeFeeding.GetAttributeValue<int>("rpo_lifepoints");

            // Randomly select a quantity of food between the following options: 10, 50, 100, 1000
            random = new Random();
            var foodQuantityOptions = new List<int> { 10, 50, 100, 1000 };
            var selectedFoodQuantity = foodQuantityOptions[random.Next(foodQuantityOptions.Count)];

            // Create a feeding activity
            var feedingActivityId = PetHelper.CreateFeedingActivity(_serviceClient, _petId, selectedFoodQuantity);

            // Wait for 20 seconds
            System.Threading.Thread.Sleep(20000);

            // Assert
            Assert.True(PetHelper.ArePetLifePointsCorrectlyUpdatedAfterFeedingActivity(_serviceClient, _petId, initialLifePoints, selectedFoodQuantity));
        }
    
        /// <summary>
        /// Tests the feeding of a pet entity.
        /// </summary>
        /// <remarks>
        /// The test update the life points of the pet entity to 99990.
        /// It then create a feeding activity for the pet entity with a random quantity of food between 100, 1000.
        /// The test waits for 20 seconds and validates that the lifepoints have increased without going over the initial life points value.
        /// </remarks>
        [Fact]
        public void FeedPet_ReachingInitialLifePoints()
        {
            // Update the life points of the pet to 99990
            PetHelper.UpdatePetLifePoints(_serviceClient, _petId, 99990);

            // Retrieve the pet
            var petBeforeFeeding = _serviceClient.Retrieve("rpo_pet", _petId, new ColumnSet("rpo_lifepoints"));
            int initialLifePoints = petBeforeFeeding.GetAttributeValue<int>("rpo_lifepoints");

            // Randomly select a quantity of food between the following options: 100, 1000
            var random = new Random();
            var foodQuantityOptions = new List<int> { 100, 1000 };
            var selectedFoodQuantity = foodQuantityOptions[random.Next(foodQuantityOptions.Count)];

            // Create a feeding activity
            var feedingActivityId = PetHelper.CreateFeedingActivity(_serviceClient, _petId, selectedFoodQuantity);

            // Wait for 20 seconds
            System.Threading.Thread.Sleep(20000);

            // Assert
            Assert.True(PetHelper.ArePetLifePointsCorrectlyUpdatedAfterFeedingActivity(_serviceClient, _petId, initialLifePoints, selectedFoodQuantity));
        }
    */

        /// <summary>
        /// Disposes the resources used by the <see cref="DataverseAPITests"/> class.
        /// </summary>
        /// <remarks>
        /// The method calls the <see cref="Dispose(bool)"/> method.
        /// </remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the resources used by the <see cref="DataverseAPITests"/> class.
        /// </summary>
        /// <param name="disposing">A value indicating whether the method is called from the <see cref="Dispose()"/> method.</param>
        /// <remarks>
        /// The method disposes the managed resources and optionally the unmanaged resources.
        /// </remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    if (_petId != Guid.Empty)
                    {
                        PetHelper.DeletePet(_serviceClient, _petId);
                        _petId = Guid.Empty;
                    }

                    if(_serviceClient != null)
                    {
                        _serviceClient.Dispose();
                        _serviceClient = null;
                    }
                }

                // Dispose unmanaged resources.

                _disposed = true;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DataverseAPITests"/> class.
        /// </summary>
        /// <remarks>
        /// The finalizer calls the <see cref="Dispose(bool)"/> method.
        /// </remarks>
        ~DataverseAPITests()
        {
            Dispose(false);
        }
    }
}