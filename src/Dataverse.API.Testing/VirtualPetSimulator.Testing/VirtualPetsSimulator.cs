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

        /// <summary>
        /// Initializes a new instance of the <see cref="DataverseAPITests"/> class.
        /// </summary>
        /// <remarks>
        /// The constructor initializes the service client using the environment variables.
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
        /// The test creates a pet entity with a random name, lifepoints, and happinesspoints.
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
        }

        /// <summary>
        /// Tests the decrease of lifepoints and happinesspoints over time.
        /// </summary>
        /// <remarks>
        /// The test retrieves all the pets with lifepoints and happinesspoints greater than 100.
        /// It then waits for 3 minutes and validates that the lifepoints and happinesspoints of the pets have decreased by at least 20 points.
        /// </remarks>
        [Fact]
        public void DecreasePointsOverTime()
        {
            // Retrieve all the pets with lifepoints and happinesspoints greater than 100
            var query = new QueryExpression("rpo_pet")
            {
                ColumnSet = new ColumnSet("rpo_lifepoints", "rpo_happinesspoints"),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("rpo_lifepoints", ConditionOperator.GreaterThan, 100),
                        new ConditionExpression("rpo_happinesspoints", ConditionOperator.GreaterThan, 100)
                    }
                }
            };

            var pets = _serviceClient.RetrieveMultiple(query).Entities;

            // Wait for 3 minutes
            System.Threading.Thread.Sleep(180000);

            // Validate the lifepoints and happinesspoints of the pets
            foreach (var pet in pets)
            {
                // Get the current pet
                var updatedPet = _serviceClient.Retrieve("rpo_pet", pet.Id, new ColumnSet("rpo_lifepoints", "rpo_happinesspoints"));

                var lifepoints = pet.GetAttributeValue<int>("rpo_lifepoints");
                var happinesspoints = pet.GetAttributeValue<int>("rpo_happinesspoints");

                var updatedLifepoints = updatedPet.GetAttributeValue<int>("rpo_lifepoints");
                var updatedHappinesspoints = updatedPet.GetAttributeValue<int>("rpo_happinesspoints");

                // Check if the lifepoints and happinesspoints have decreased by at least 20 points
                Assert.True(lifepoints - updatedLifepoints >= 20);
                Assert.True(happinesspoints - updatedHappinesspoints >= 20);
            }
        }
    
        /// <summary>
        /// Tests the feeding of a pet entity.
        /// </summary>
        /// <remarks>
        /// The test retrieves a pet entity with lifepoints less than 98000.
        /// If no pet is found, a new pet entity is created with lifepoints less than 98000.
        /// The test then randomly selects a quantity of food and creates a feeding activity for the pet entity.
        /// It waits for 20 seconds and validates that the lifepoints of the pet entity have increased by the quantity of food selected.
        /// </remarks>
        [Fact]
        public void FeedPet_WithLessThan98000LifePoints()
        {
            // Initialize the pet variable
            Entity pet = new Entity("rpo_pet");

            // Retrieve pets with less than 98000 lifepoints
            var query = new QueryExpression("rpo_pet")
            {
                ColumnSet = new ColumnSet("rpo_lifepoints"),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("rpo_lifepoints", ConditionOperator.LessThan, 98000)
                    }
                }
            };

            var pets = _serviceClient.RetrieveMultiple(query).Entities;

            // Check if there are any pets with less than 98000 lifepoints
            if (pets.Count == 0)
            {
                // Create a pet, wait for 20 seconds - the time it takes for the lifepoints and happinesspoints to be initialized at 100 000
                // Then update the pet's lifepoints to less than 98 000
                pet = new Entity("rpo_pet");
                pet["rpo_name"] = "Fluffy";

                var petId = _serviceClient.Create(pet);

                System.Threading.Thread.Sleep(20000);

                pet["rpo_lifepoints"] = 97000;

                _serviceClient.Update(pet);
            } else {
                // Get the first pet with less than 98 000 lifepoints
                pet = pets[0];
            }

            // Randomly select a quantity of food
            var random = new Random();
            var foodOptions = new Dictionary<int, int>
            {
                { 913610000, 10 },
                { 913610001, 50 },
                { 913610002, 100 },
                { 913610003, 1000 }
            };
            var selectedFoodOption = foodOptions.ElementAt(random.Next(foodOptions.Count));

            // Create a feeding activity for a pet with a quantity of food less than 98 000
            Entity feedingActivity = new Entity("rpo_feeding");
            feedingActivity["rpo_quantity"] = new OptionSetValue(selectedFoodOption.Key);
            feedingActivity["regardingobjectid"] = new EntityReference("rpo_pet", pet.Id);

            // Act
            var feedingActivityId = _serviceClient.Create(feedingActivity);

            // Assert
            Assert.NotEqual(Guid.Empty, feedingActivityId);

            // Wait for 20 seconds
            System.Threading.Thread.Sleep(20000);

            // Retrieve the pet
            var updatedPet = _serviceClient.Retrieve("rpo_pet", pet.Id, new ColumnSet("rpo_lifepoints"));

            // Validate the lifepoints of the pet
            var lifepoints = updatedPet.GetAttributeValue<int>("rpo_lifepoints");

            // Check if the lifepoints have increased by the quantity of food selected
            Assert.Equal(pet.GetAttributeValue<int>("rpo_lifepoints") + selectedFoodOption.Value, lifepoints);
        }
    
        /// <summary>
        /// Tests the feeding of a pet entity.
        /// </summary>
        /// <remarks>
        /// The test retrieves a pet entity with lifepoints greater than 98000.
        /// If no pet is found, a new pet entity is created with lifepoints greater than 98000.
        /// The test then randomly selects a quantity of food that will go over the initial lifepoints of the pet entity.
        /// It creates a feeding activity for the pet entity and waits for 20 seconds.
        /// The test then validates that the lifepoints of the pet entity have reached the initial lifepoints - 100 000.
        /// </remarks>
        [Fact]
        public void FeedPet_WithQuantityToGoOverInitialPoints()
        {
            // Initialize the pet variable
            Entity pet = new Entity("rpo_pet");

            // Retrieve pets with less than 98000 lifepoints
            var query = new QueryExpression("rpo_pet")
            {
                ColumnSet = new ColumnSet("rpo_lifepoints"),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("rpo_lifepoints", ConditionOperator.GreaterThan, 99000)
                    }
                }
            };

            var pets = _serviceClient.RetrieveMultiple(query).Entities;

            // Check if there are any pets with less than 98000 lifepoints
            if (pets.Count == 0)
            {
                // Create a pet, wait for 20 seconds - the time it takes for the lifepoints and happinesspoints to be initialized at 100 000
                // Then update the pet's lifepoints to less than 98 000
                pet = new Entity("rpo_pet");
                pet["rpo_name"] = "Fluffy";

                var petId = _serviceClient.Create(pet);

                System.Threading.Thread.Sleep(20000);

                pet["rpo_lifepoints"] = 99990;

                _serviceClient.Update(pet);
            } else {
                // Get the first pet with less than 98 000 lifepoints
                pet = pets[0];
            }

            // Randomly select a quantity of food
            var random = new Random();
            var foodOptions = new Dictionary<int, int>
            {
                { 913610000, 10 },
                { 913610001, 50 },
                { 913610002, 100 },
                { 913610003, 1000 }
            };

            // Select a quantity of food that will go over the initial lifepoints
            var quantityToGoOverInitialPoints = 100000 - pet.GetAttributeValue<int>("rpo_lifepoints") + 1;
            var selectedFoodOption = foodOptions.Where(x => x.Value >= quantityToGoOverInitialPoints).First();

            // Create a feeding activity for a pet with a quantity of food less than 98 000
            Entity feedingActivity = new Entity("rpo_feeding");
            feedingActivity["rpo_quantity"] = new OptionSetValue(selectedFoodOption.Key);
            feedingActivity["regardingobjectid"] = new EntityReference("rpo_pet", pet.Id);

            // Act
            var feedingActivityId = _serviceClient.Create(feedingActivity);

            // Assert
            Assert.NotEqual(Guid.Empty, feedingActivityId);

            // Wait for 20 seconds
            System.Threading.Thread.Sleep(20000);

            // Retrieve the pet
            var updatedPet = _serviceClient.Retrieve("rpo_pet", pet.Id, new ColumnSet("rpo_lifepoints"));

            // Validate the lifepoints of the pet
            var lifepoints = updatedPet.GetAttributeValue<int>("rpo_lifepoints");

            // Check if the lifepoints have increased by the quantity of food selected
            Assert.Equal(100000, lifepoints);
        }
    }
}