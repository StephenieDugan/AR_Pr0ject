using UnityEngine;

public class PlantInteracterScript : MonoBehaviour
{
	[SerializeField] private LayerMask whoIHit;

	private void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch t = Input.GetTouch(0);
			if (t.phase == TouchPhase.Began)
			{
				RaycastHit hit;
				Ray raypos = Camera.main.ScreenPointToRay(t.position);
				Physics.Raycast(raypos, out hit, 1000.0f, whoIHit);
				Debug.DrawRay(raypos.origin, raypos.direction, Color.aliceBlue, 5.0f);


				if (hit.rigidbody)
				{
					ProduceScript produceHit = hit.rigidbody.GetComponent<ProduceScript>();

					produceHit.TryHarvest();
				}
			}
		}
	}
}
