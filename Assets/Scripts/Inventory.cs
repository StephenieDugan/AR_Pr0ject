using UnityEngine;

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


}
