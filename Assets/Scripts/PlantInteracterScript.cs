using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required for IPointerDownHandler, IDragHandler, IEndDragHandler
using UnityEngine.XR.ARFoundation; // Required for ARRaycastManager
using UnityEngine.XR.ARSubsystems;

public class PlantInteracterScript : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private LayerMask whoIHit;
	[SerializeField] private Camera cam;

    public void OnPointerDown(PointerEventData eventData)
    {
        RaycastHit[] hit;
        Ray raypos = Camera.main.ScreenPointToRay(eventData.position);
        hit = Physics.RaycastAll(raypos, 1000.0f, whoIHit);
        Debug.DrawRay(raypos.origin, raypos.direction, Color.aliceBlue, 5.0f);


        foreach (var item in hit)
        {
            if (item.rigidbody)
            {
                if (item.rigidbody.GetComponent<ProduceScript>())
                {
                    item.rigidbody.GetComponent<ProduceScript>().TryHarvest();
                }
            }
        }
    }
}
