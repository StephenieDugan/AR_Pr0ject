using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
	[SerializeField] private float age = 0; // this will not be serialized, but for testing, it is serialized for now
	[SerializeField] private float water = 0; // this will not be serialized, but for testing, it is serialized for now
	[SerializeField] private float consumptionRate;
	[SerializeField] private int[] growthAges; // the age at which the plant will grow to the next stage
	[SerializeField] private int growthStage = 0; // this will not be serialized, but for testing, it is serialized for now
	[SerializeField] private float growthRate; // base rate at which the plant grows at without water
	[SerializeField] private float maxGrowthRate; // max rate at which the plant grows
	bool maxAge; // plant has reached its max age, so it will not grow anymore, now produce will start to grow
	// probably have some value here the determines what plant stage you will be
	[SerializeField] private PlantDataSO myPlantData;
	[SerializeField] private Transform[] produceGrowLocations;
	[SerializeField] private List<ProduceScript> produces;
	[SerializeField] private int maxProducesCount;

	public void PlantUpdate()
	{
		AgeUp();

		if (maxAge && produces.Count < maxProducesCount) AddProduce();
		else if (age >= growthAges[growthStage])
		{
			Grow();
		}
		// increase age or something
		// call update on produce
	}

	public void AddProduce()
	{
		// add produce when conditions arrise.
		// please use BasePrefabs.baseProducePrefab
	}

	public void SetupPlantData(PlantDataSO plantData)
	{
		// do stuff for making the plant
	}

	private void AgeUp()
	{
		float gr;
		if (water <= 0)
		{
			gr = growthRate * Time.deltaTime; // if no water, grow slow
		}
		else
		{
			water -= consumptionRate * Time.deltaTime;
			gr = Mathf.Min(growthRate * water, maxGrowthRate) * Time.deltaTime;  // if water grow at water * base rate, unless water * base rate > maxGrowthRate
		}
		age += gr * Time.deltaTime;
	}

	private void Grow()
	{
		growthStage++;
		if (growthStage >= growthAges.Length)
		{
			growthStage = growthAges.Length - 1; // cap at max stage
			maxAge = true;
		}
		GetComponent<MeshFilter>().mesh = myPlantData.getPlantMesh(growthStage - 1);
	}
}
