using UnityEngine;

public class BasePrefabs : MonoBehaviour
{
    public static BasePrefabs Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	// put this on some type of game manager object and make sure it is set
	public PlantScript basePlantPrefab;
	public ProduceScript baseProducePrefab;
}
