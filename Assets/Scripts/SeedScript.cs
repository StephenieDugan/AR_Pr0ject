using UnityEngine;

public class SeedScript : MonoBehaviour
{
	[SerializeField] private SeedDataSO seedDataSO; // Reference to the Seed ScriptableObject

	public SeedDataSO GetSeedData()
	{
		return seedDataSO; // Returns the SeedDataSO associated with this seed
	}
}
