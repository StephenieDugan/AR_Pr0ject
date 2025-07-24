using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    [SerializeField] private float age;
    [SerializeField] private float water;
    [SerializeField] private float consumtionRate;
	[SerializeField] private int growthStage;
    // probably have some value here the determines what plant stage you will be
    [SerializeField] private PlantDataSO myPlantData;
    [SerializeField] private Transform[] produceGrowLocations;
    [SerializeField] private List<ProduceScript> produces;

	public void PlantUpdate()
    {
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
}
