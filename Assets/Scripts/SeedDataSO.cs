using UnityEngine;

[CreateAssetMenu(fileName = "SeedDataSO", menuName = "Scriptable Objects/SeedDataSO")]
public class SeedDataSO : ScriptableObject
{
    public enum SeedType
    {
        TOMATO, 
        SUNFLOWER
    }
    [SerializeField] private SeedType seedType; // Type of the seed, can be used to determine what plant will be spawned
	[SerializeField] private GameObject plantPrefab; // Prefab of the plant that will be spawned when the seed is planted

    public SeedType GetSeedType()
    {
        return seedType;
	}

}
