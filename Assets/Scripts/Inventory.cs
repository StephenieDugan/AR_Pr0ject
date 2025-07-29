using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Inventory : MonoBehaviour
{
	#region Singleton Iementation
	private static Inventory _instance; // Singleton instance
	private static readonly object _lock = new object(); // Lock for thread safety
	public static Inventory Instance
	{
		get
		{
			lock (_lock)
			{
				if (_instance == null)
				{
					_instance = FindFirstObjectByType<Inventory>();
					if (_instance == null)
					{
						GameObject inventoryObject = new GameObject("Inventory");
						_instance = inventoryObject.AddComponent<Inventory>();
					}
				}
				return _instance;
			}
		}
	}
	#endregion
	// use Inventory.Instance to access the singleton instance

	private int currency = 0; // current currency amount
	public int Currency
	{
		get { return currency; }
		set { currency = value; }
	}

	private int pots = 1; // current number of pots available
	public int Pots
	{
		get { return pots; }
		set { pots = value; }
	}

	private int fertilizer = 0; // current amount of fertilizer available
	public int Fertilizer
	{
		get { return fertilizer; }
		set { fertilizer = value; }
	}

	private List<SeedScript> seeds = new List<SeedScript>(); // list of seeds in the inventory
	private List<SeedScript> tomatoSeeds = new List<SeedScript>(); // list of tomato seeds in the inventory
	public SeedScript GetTomatoSeed
	{
		get { return tomatoSeeds.Count > 0 ? tomatoSeeds[0] : null; } // returns the first tomato seed if available
	}
	public int TomatoSeedCount
	{
		get { return tomatoSeeds.Count; } // returns the count of tomato seeds
	}
	private List<SeedScript> sunflowerSeeds = new List<SeedScript>(); // list of tomato seeds in the inventory
	public SeedScript GetSunflowerSeed
	{
		get { return sunflowerSeeds.Count > 0 ? sunflowerSeeds[0] : null; } // returns the first sunflower seed if available
	}
	public int SunflowerSeedCount
	{
		get { return sunflowerSeeds.Count; } // returns the count of sunflower seeds
	}

	public void AddSeed(SeedScript seed)
	{
		seeds.Add(seed); // add seed to the inventory
	}

	private List<SeedScript> GetSeedsOfType(SeedDataSO.SeedType seedType)
	{
		return seeds.FindAll(seed => seed.GetSeedData().GetSeedType() == seedType); // find the first seed of the specified type
	}

	private void FixedUpdate()
	{
		tomatoSeeds = GetSeedsOfType(SeedDataSO.SeedType.TOMATO); // update the list of tomato seeds
		sunflowerSeeds = GetSeedsOfType(SeedDataSO.SeedType.SUNFLOWER); // update the list of sunflower seeds
	}
}
