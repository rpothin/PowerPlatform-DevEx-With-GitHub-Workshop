using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace VirtualPetsSimulator.Helpers
{
    public class PetHelper
    {
        private static int _initialLifePoints = 100000;
        private static int _initialHappinessPoints = 100000;
        private static int _cuddleHappinessPoints = 1000;

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
        /// Update the happiness points of a pet
        /// </summary>
        /// <param name="serviceClient">The service client</param>
        /// <param name="petId">The id of the pet</param>
        /// <param name="happinessPoints">The new happiness points</param>
        /// <remarks>
        /// This method updates the happiness points of the pet
        /// </remarks>
        public static void UpdatePetHappinessPoints(ServiceClient serviceClient, Guid petId, int happinessPoints)
        {
            var pet = new Entity("rpo_pet");
            pet.Id = petId;
            pet["rpo_happinesspoints"] = happinessPoints;
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
                { 10, 913610000 },
                { 50, 913610001 },
                { 100, 913610002 },
                { 1000, 913610003 }
            };

            // Get the code corresponding to the requested food quantity
            var selectedFoodOption = foodOptions[foodQuantity];

            // Create the feeding activity
            Entity feedingActivity = new Entity("rpo_feeding");
            feedingActivity["rpo_quantity"] = new OptionSetValue(selectedFoodOption);
            feedingActivity["regardingobjectid"] = new EntityReference("rpo_pet", petId);

            return serviceClient.Create(feedingActivity);
        }

        /// <summary>
        /// Check if the life points are correctly updated after a feeding activity
        /// </summary>
        /// <param name="serviceClient">The service client</param>
        /// <param name="petId">The id of the pet</param>
        /// <param name="lifePointsBeforeFeeding">The life points of the pet before the feeding activity</param>
        /// <param name="foodQuantity">The quantity of food</param>
        /// <returns>True if the life points are correctly updated, false otherwise</returns>
        /// <remarks>
        /// This method checks if the life points of the pet are correctly updated after a feeding activity
        /// </remarks>
        public static bool ArePetLifePointsCorrectlyUpdatedAfterFeedingActivity(ServiceClient serviceClient, Guid petId, int lifePointsBeforeFeeding, int foodQuantity)
        {
            // Retrieve the pet
            var pet = serviceClient.Retrieve("rpo_pet", petId, new ColumnSet("rpo_lifepoints"));

            // Get the life points
            var lifePoints = pet.GetAttributeValue<int>("rpo_lifepoints");

            // Check if the life points are correctly updated
            if (lifePointsBeforeFeeding + foodQuantity >= _initialLifePoints) {
                return lifePoints == _initialLifePoints;
            } else {
                // Consider the option that the life points already decreased by 10
                return lifePoints == lifePointsBeforeFeeding + foodQuantity || lifePoints == lifePointsBeforeFeeding + foodQuantity - 10;
            }
        }

        /// <summary>
        /// Create a cuddle activity
        /// </summary>
        /// <param name="serviceClient">The service client</param>
        /// <param name="petId">The id of the pet</param>
        /// <returns>The id of the created cuddle activity</returns>
        /// <remarks>
        /// This method creates a new cuddle activity for the pet
        /// </remarks>
        public static Guid CreateCuddleActivity(ServiceClient serviceClient, Guid petId)
        {
            Entity cuddleActivity = new Entity("rpo_cuddle");
            cuddleActivity["regardingobjectid"] = new EntityReference("rpo_pet", petId);

            return serviceClient.Create(cuddleActivity);
        }

        /// <summary>
        /// Check if the happiness points are correctly updated after a cuddle activity
        /// </summary>
        /// <param name="serviceClient">The service client</param>
        /// <param name="petId">The id of the pet</param>
        /// <param name="happinessPointsBeforeCuddle">The happiness points of the pet before the cuddle activity</param>
        /// <returns>True if the happiness points are correctly updated, false otherwise</returns>
        /// <remarks>
        /// This method checks if the happiness points of the pet are correctly updated after a cuddle activity
        /// </remarks>
        public static bool ArePetHapppinessPointsCorrectlyUpdatedAfterCuddleActivity(ServiceClient serviceClient, Guid petId, int happinessPointsBeforeCuddle)
        {
            // Retrieve the pet
            var pet = serviceClient.Retrieve("rpo_pet", petId, new ColumnSet("rpo_happinesspoints"));

            // Get the happiness points
            var happinessPoints = pet.GetAttributeValue<int>("rpo_happinesspoints");

            Console.WriteLine($"happinessPointsBeforeCuddle: {happinessPointsBeforeCuddle}");
            Console.WriteLine($"happinessPoints: {happinessPoints}");
            Console.WriteLine($"_initialHappinessPoints: {_initialHappinessPoints}");
            Console.WriteLine($"_cuddleHappinessPoints: {_cuddleHappinessPoints}");

            // Check if the happiness points are correctly updated
            if (happinessPointsBeforeCuddle + _cuddleHappinessPoints >= _initialHappinessPoints) {
                Console.WriteLine("1");
                return happinessPoints == _initialHappinessPoints;
            } else {
                Console.WriteLine("2");
                // Consider the option that the happiness points already decreased by 10
                return happinessPoints == happinessPointsBeforeCuddle + _cuddleHappinessPoints || happinessPoints == happinessPointsBeforeCuddle + _cuddleHappinessPoints - 10;
            }
        }

        /// <summary>
        /// Delete a pet
        /// </summary>
        /// <param name="serviceClient">The service client</param>
        /// <param name="petId">The id of the pet</param>
        /// <remarks>
        /// This method deletes a pet
        /// </remarks>
        public static void DeletePet(ServiceClient serviceClient, Guid petId)
        {
            serviceClient.Delete("rpo_pet", petId);
        }
    }
}