using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace VirtualPetsSimulator.Helpers
{
    public class PetHelper
    {
        private static int _initialLifePoints = 100000;
        private static int _initialHappinessPoints = 100000;

        /// <summary>
        /// Create a random pet
        /// </summary>
        /// <param name="serviceClient">The service client</param>
        /// <returns>The id of the created pet</returns>
        /// <remarks>
        /// This method creates a new pet with a random name
        /// </remarks>
        public static Guid CreateRandomPet(ServiceClient serviceClient)
        {
            var pet = new Entity("rpo_pet");
            pet["rpo_name"] = "Pet" + Guid.NewGuid().ToString();
            return serviceClient.Create(pet);
        }

        /// <summary>
        /// Create a random pet with life and happiness points
        /// </summary>
        /// <param name="serviceClient"></param>
        /// <param name="lifePoints"></param>
        /// <param name="happinessPoints"></param>
        /// <returns>The id of the created pet</returns>
        /// <remarks>
        /// This method creates a new pet with a random name and the specified life and happiness points
        /// </remarks>
        public static Guid CreateRandomPetWithLifeAndHappinessPoints(ServiceClient serviceClient, int lifePoints, int happinessPoints)
        {
            var pet = new Entity("rpo_pet");
            pet["rpo_name"] = "Pet" + Guid.NewGuid().ToString();
            pet["rpo_lifepoints"] = lifePoints;
            pet["rpo_happinesspoints"] = happinessPoints;
            return serviceClient.Create(pet);
        }

        /// <summary>
        /// Check if the life points and the happiness points are correctly initialized
        /// </summary>
        /// <param name="serviceClient">The service client</param>
        /// <param name="petId">The id of the pet</param>
        /// <returns>True if the life points and the happiness points are correctly initialized, false otherwise</returns>
        /// <remarks>
        /// This method checks if the life points and the happiness points of the pet are correctly initialized
        /// </remarks>
        public static bool ArePetLifeAndHappinessPointsCorrectlyInitialized(ServiceClient serviceClient, Guid petId)
        {
            // Retrieve the created pet
            var pet = serviceClient.Retrieve("rpo_pet", petId, new ColumnSet("rpo_lifepoints", "rpo_happinesspoints"));

            // Get the life points and the happiness points
            var lifePoints = pet.GetAttributeValue<int>("rpo_lifepoints");
            var happinessPoints = pet.GetAttributeValue<int>("rpo_happinesspoints");

            // Assert that the life points and the happiness points are set to their initial values
            // If it is not the case, assert that the life points and the happiness points are set to their initial values minus 10
            Console.WriteLine(lifePoints);
            Console.WriteLine(happinessPoints);

            Console.WriteLine(_initialLifePoints);
            Console.WriteLine(_initialHappinessPoints);

            Console.WriteLine(lifePoints == _initialLifePoints);
            Console.WriteLine(happinessPoints == _initialHappinessPoints);

            Console.WriteLine(lifePoints == _initialLifePoints - 10);
            Console.WriteLine(happinessPoints == _initialHappinessPoints - 10);

            return (lifePoints == _initialLifePoints && happinessPoints == _initialHappinessPoints) || (lifePoints == _initialLifePoints - 10 && happinessPoints == _initialHappinessPoints - 10);
        }

        /// <summary>
        /// Update the life points of a pet
        /// </summary>
        /// <param name="serviceClient">The service client</param>
        /// <param name="petId">The id of the pet</param>
        /// <param name="lifePoints">The new life points</param>
        /// <remarks>
        /// This method updates the life points of the pet
        /// </remarks>
        public static void UpdatePetLifePoints(ServiceClient serviceClient, Guid petId, int lifePoints)
        {
            var pet = new Entity("rpo_pet");
            pet.Id = petId;
            pet["rpo_lifepoints"] = lifePoints;
            serviceClient.Update(pet);
        }

        /// <summary>
        /// Create a feeding activity
        /// </summary>
        /// <param name="serviceClient">The service client</param>
        /// <param name="petId">The id of the pet</param>
        /// <param name="foodQuantity">The quantity of food</param>
        /// <returns>The id of the created feeding activity</returns>
        /// <remarks>
        /// This method creates a new feeding activity for the pet
        /// </remarks>
        public static Guid CreateFeedingActivity(ServiceClient serviceClient, Guid petId, int foodQuantity)
        {
            var foodOptions = new Dictionary<int, int>
            {
                { 913610000, 10 },
                { 913610001, 50 },
                { 913610002, 100 },
                { 913610003, 1000 }
            };

            // Get the code corresponding to the requested food quantity
            var selectedFoodOption = foodOptions.ElementAt(foodQuantity);

            // Create the feeding activity
            Entity feedingActivity = new Entity("rpo_feeding");
            feedingActivity["rpo_quantity"] = new OptionSetValue(selectedFoodOption.Key);
            feedingActivity["regardingobjectid"] = new EntityReference("rpo_pet", petId);

            return serviceClient.Create(feedingActivity);
        }

        public static bool ArePetLifePointsCorrectlyUpdatedAfterFeedingActivity(ServiceClient serviceClient, Guid petId, int lifePointsBeforeFeeding, int foodQuantity)
        {
            // Retrieve the pet
            var pet = serviceClient.Retrieve("rpo_pet", petId, new ColumnSet("rpo_lifepoints"));

            // Get the life points
            var lifePoints = pet.GetAttributeValue<int>("rpo_lifepoints");

            // Check if the life points are correctly updated
            if (lifePointsBeforeFeeding + foodQuantity == _initialLifePoints) {
                return lifePoints == _initialLifePoints;
            } else {
                // Consider the option that the life points already decreased by 10
                return lifePoints == lifePointsBeforeFeeding + foodQuantity || lifePoints == lifePointsBeforeFeeding + foodQuantity - 10;
            }
        }
    }
}