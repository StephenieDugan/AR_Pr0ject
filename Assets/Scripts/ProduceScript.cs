using System;
using System.Collections.Generic;
using UnityEngine;

public class ProduceScript : MonoBehaviour
{
	[SerializeField] private float age;
	[SerializeField] private int growthStage; // serialized for testing, will not be serialized in final version
	[SerializeField] private float growthRate; // base rate at which the produce grows
	[SerializeField] private int[] growthAges; // the age at which the produce will grow to the next stage
	private bool maxAge; // produce has reached its max age, so it will not grow anymore, now produce is ready to be harvested
	[NonSerialized] public bool harvested;
	[SerializeField] public bool harvest = false; // serialized for testing, will not be serialized in final version
	[SerializeField] private float maxGrowthRate; // max rate at which the produce grows
	[NonSerialized] public float water; // current water level, will be set by the plant script
	[SerializeField] private int produceValue; // value of the produce when harvested

	[NonSerialized] public int locationIndex; // index of the produce location in the plant script, will be set by the plant script

	[SerializeField] private ProduceDataSO myProduceData;

	public void ProduceUpdate()
	{
		AgeUp();

		if (maxAge && harvest)
		{
			// produce is ready to be harvested, do something here 
			Harvest();
			return;
		}
		else if (age >= growthAges[growthStage])
		{
			Grow();
		}
		// update produce stuff
	}

	public void SetupProduceData(ProduceDataSO plantData)
	{
		// do stuff for making the produce
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
			GetComponent<MeshFilter>().mesh = myProduceData.getProduceMesh(growthStage);
		}
		else GetComponent<MeshFilter>().mesh = myProduceData.getProduceMesh(growthStage - 1);
	}

	private void Harvest()
	{
		Inventory.Instance.Currency += produceValue; // add the produce value to the inventory currency
		harvested = true; // mark the produce as harvested
	}
}
