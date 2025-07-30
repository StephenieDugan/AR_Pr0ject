using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private ShopItemDataSO[] shopItems;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameObject itemModel;
    private int currentItemIndex = 0;

	private void Start()
	{
	    ChangeItem();
	}

	public void SwitchShopItem(int i)
    {
        if (i == 0)
        {
            if (currentItemIndex >= shopItems.Length - 1)
            {
                currentItemIndex = 0;
            }
            else currentItemIndex++;
        } else if (i == 1)
		{
			if (currentItemIndex == 0)
            {
               currentItemIndex = shopItems.Length - 1;
            }
            else currentItemIndex--;
		}
        ChangeItem();
	}

    public void BuyItem()
    {
        if (Inventory.Instance.Currency < shopItems[currentItemIndex].ItemPrice)
        {
            return;
        } else shopItems[currentItemIndex].BuyShopItem();
    }
    
	private void ChangeItem()
	{
		nameText.text = shopItems[currentItemIndex].ItemName;
        priceText.text = "Cost: " + shopItems[currentItemIndex].ItemPrice.ToString();
        itemModel.GetComponent<MeshFilter>().mesh = shopItems[currentItemIndex].ItemMesh;
        itemModel.GetComponent<MeshRenderer>().material = shopItems[currentItemIndex].ItemMaterial;
        itemModel.transform.localScale = Vector3.one * shopItems[currentItemIndex].Scale;
	}

}
