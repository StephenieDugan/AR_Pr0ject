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
	[SerializeField] private ProduceLocation[] produceGrowLocations;
	[SerializeField] private List<ProduceScript> produces; // this will not be serialized, but for testing, it is serialized for now
	[SerializeField] private GameObject producePrefab; // prefab for the produce that will be spawned
	[SerializeField] private int maxProducesCount;
	[SerializeField] private float produceMinTimeBetweenGrowth = 1; // minimum time between produce growth in seconds
	[SerializeField] private float produceMaxTimeBetweenGrowth = 10; // maximum time between produce growth in seconds
	private bool addingProduce = false; // flag to check if we are currently adding produce
	[SerializeField] private int maxHarvestAmount; // maximum amount of produce that can be harvested before plant is destroyed
	private int harvestCount = 0; // count of how many produce have been harvested

	public void PlantUpdate()
	{
		AgeUp();

		if (maxAge && produces.Count < maxProducesCount && !addingProduce) { Invoke("AddProduce", Random.Range(produceMinTimeBetweenGrowth, produceMaxTimeBetweenGrowth)); addingProduce = true;  }
		else if (age >= growthAges[growthStage])
		{
			Grow();
		}
		// increase age or something
		// call update on produce
		if (produces.Count > 0)
		{
			foreach (var produce in produces)
			{
				if (produce.harvested)
				{
					// if the produce has been harvested, remove it from the list and destroy it
					harvestCount++; // increment the harvest count
					produceGrowLocations[produce.locationIndex].isOccupied = false; // mark the location as not occupied
					produces.Remove(produce);
					Destroy(produce.gameObject);
					break; // skip to the next produce
				}
				else
				{
					produce.water = water; // set the water level for the produce
					produce.ProduceUpdate();
				}
			}
		}

		if (harvestCount >= maxHarvestAmount)
		{
			// destroy the plant if the harvest count exceeds the maximum harvest amount
			
			Destroy(gameObject);
			return;
		}
	}

	public void AddProduce()
	{
		foreach (var growLocation in produceGrowLocations)
		{
			if (Random.Range(0, 1) == 1) continue; // 50% chance to skip this location
			else if (growLocation.isOccupied) continue; // if the location is occupied, skip it
			else
			{
				// spawn produce at this location
				GameObject newProduce = Instantiate(producePrefab, growLocation.transform.position, Quaternion.identity, growLocation.transform);
				newProduce.GetComponent<ProduceScript>().locationIndex = System.Array.IndexOf(produceGrowLocations, growLocation); // set the location index for the produce
				ProduceScript produceScript = newProduce.GetComponent<ProduceScript>();
				produces.Add(produceScript); // add the produce to the list
				growLocation.isOccupied = true; // mark the location as occupied
				addingProduce = false; // reset the adding produce flag
				break; // exit the loop after adding one produce
			}
		}
		// add produce when conditions arise. 
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
			GetComponent<MeshFilter>().mesh = myPlantData.getPlantMesh(growthStage);
		}
		else GetComponent<MeshFilter>().mesh = myPlantData.getPlantMesh(growthStage - 1);
	}
}
