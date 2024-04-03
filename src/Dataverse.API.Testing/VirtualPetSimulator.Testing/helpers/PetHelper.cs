using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace VirtualPetsSimulator.Helpers
{
    public class PetHelper
    {
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
            var pet = new Entity("pet");
            pet["name"] = "Pet" + Guid.NewGuid().ToString();
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
            var pet = serviceClient.Retrieve("pet", petId, new Microsoft.Xrm.Sdk.Query.ColumnSet("life_points", "happiness_points"));

            // Get the life points and the happiness points
            var lifePoints = pet.GetAttributeValue<int>("life_points");
            var happinessPoints = pet.GetAttributeValue<int>("happiness_points");

            // Assert that the life points and the happiness points are set to 100000
            // If it is not the case, assert that the life points and the happiness points are set to 99990
            return (lifePoints == 100000 && happinessPoints == 100000) || (lifePoints == 99990 && happinessPoints == 99990);
        }
    }
}