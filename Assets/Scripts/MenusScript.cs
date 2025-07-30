using UnityEngine;

public class MenusScript : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject inventoryUI;
 
    public void OnShopButtonClick()
    {
		if (inventoryUI.activeSelf) inventoryUI.SetActive(false);
		if (shopUI.activeSelf) shopUI.SetActive(false);
        else shopUI.SetActive(true);
	}

    public void OnInventoryButtonClick()
    {
        if (shopUI.activeSelf) shopUI.SetActive(false);
        if (inventoryUI.activeSelf) inventoryUI.SetActive(false);
        else inventoryUI.SetActive(true);
	}
}
