using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemDataSO", menuName = "Scriptable Objects/ShopItemDataSO")]
public class ShopItemDataSO : ScriptableObject
{
	[SerializeField] private string itemName;
	private enum ItemType
	{
		TOMATO_SEED,
		SUNFLOWER_SEED,
		POT,
		FERTILIZER
	}
	[SerializeField] private ItemType itemType;

	public string ItemName
	{
		get { return itemName; }
		set { itemName = value; }
	}
	[SerializeField] private int itemPrice;
	public int ItemPrice
	{
		get { return itemPrice; }
		set { itemPrice = value; }
	}
	[SerializeField] private Mesh itemMesh;
	public Mesh ItemMesh
	{
		get { return itemMesh; }
	}
	[SerializeField] private Material itemMaterial;
	public Material ItemMaterial
	{
		get { return itemMaterial; }
	}
	[SerializeField] private float scale;
	public float Scale
	{
		get { return scale; }
	}

	public void BuyShopItem()
	{
		Inventory.Instance.Currency -= itemPrice;
		switch (itemType)
		{
			case ItemType.TOMATO_SEED:
				Inventory.Instance.TomatoSeeds++;
				break;
			case ItemType.SUNFLOWER_SEED:
				Inventory.Instance.SunflowerSeeds++;
				break;
			case ItemType.POT:
				Inventory.Instance.Pots++;
				break;
			case ItemType.FERTILIZER:
				Inventory.Instance.Fertilizer++;
				break;
		}
	}
}
