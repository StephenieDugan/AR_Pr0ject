using System.Collections.Generic;
using UnityEngine;

public class ProduceScript : MonoBehaviour
{
	[SerializeField] private float age;
	[SerializeField] private int growthStage;
	// probably have some value here the determines what plant stage you will be
	[SerializeField] private ProduceDataSO myPlantData;

	public void ProduceUpdate()
	{
		// update produce stuff
	}

	public void SetupProduceData(ProduceDataSO plantData)
	{
		// do stuff for making the produce
	}
}
