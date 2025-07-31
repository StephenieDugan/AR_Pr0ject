using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required for IPointerDownHandler, IDragHandler, IEndDragHandler
using UnityEngine.XR.ARFoundation; // Required for ARRaycastManager
using UnityEngine.XR.ARSubsystems;

public class PlantInteracterScript : MonoBehaviour
{
	[SerializeField] private LayerMask whoIHit;
	[SerializeField] private Camera cam;

    public void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit[] hit;
			Ray raypos = Camera.main.ScreenPointToRay(Input.mousePosition);
			hit = Physics.RaycastAll(raypos, 1000.0f, whoIHit);

			Debug.Log(raypos.origin);
			Debug.DrawRay(raypos.origin, raypos.direction * 1000.0f, Color.aliceBlue, 5.0f);


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


        if (Input.touchCount > 0)
        {
			var t = Input.GetTouch(0);

			if (t.phase == TouchPhase.Began)
			{
				RaycastHit[] hit;
				Ray raypos = Camera.main.ScreenPointToRay(t.position);
				hit = Physics.RaycastAll(raypos, 1000.0f, whoIHit);

				Debug.Log(raypos.origin);
				Debug.DrawRay(raypos.origin, raypos.direction * 1000.0f, Color.aliceBlue, 5.0f);


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
    }
}
