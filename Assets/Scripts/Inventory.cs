using System.Collections.Generic;
using TMPro;
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

	[SerializeField] private TextMeshProUGUI currencyText;
	[SerializeField] private TextMeshProUGUI tomatoSeedText;
	[SerializeField] private TextMeshProUGUI sunflowerSeedText;
	[SerializeField] private TextMeshProUGUI potText;

	private void Start()
	{
		currencyText.text = "Fruits: " + currency;
		potText.text = pots.ToString();
		tomatoSeedText.text = tomatoSeeds.ToString();
		sunflowerSeedText.text = sunflowerSeeds.ToString();
	}

	private int currency = 0; // current currency amount
	public int Currency
	{
		get { return currency; }
		set { currency = value; currencyText.text = "Fruits: " + currency; }
	}

	private int pots = 1; // current number of pots available
	public int Pots
	{
		get { return pots; }
		set { pots = value; potText.text = pots.ToString(); }
	}

	private int fertilizer = 0; // current amount of fertilizer available
	public int Fertilizer
	{
		get { return fertilizer; }
		set { fertilizer = value; }
	}

	private int tomatoSeeds = 1;
	public int TomatoSeeds
	{
		get { return tomatoSeeds; }
		set { tomatoSeeds = value; tomatoSeedText.text = tomatoSeeds.ToString(); }
	}	

	private int sunflowerSeeds = 0;
	public int SunflowerSeeds
	{
		get { return sunflowerSeeds; }
		set { sunflowerSeeds = value; sunflowerSeedText.text = sunflowerSeeds.ToString(); }
	}
}
