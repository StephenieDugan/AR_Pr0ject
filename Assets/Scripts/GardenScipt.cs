using System.Collections.Generic;
using UnityEngine;

public class GardenScipt : MonoBehaviour
{
    [SerializeField] private List<PlantScript> plants;

	void Update()
    {
        if (plants.Count == 0)
        {
            return;
        }
        else
        {
			foreach (var plant in plants)
            {
                if (!plant.doneHarvesting)
                {
                    plant.PlantUpdate();
                }
            }

            plants.RemoveAll((PlantScript p) => p == null);
        }
        // Update the plants using the PlantUpdate method. so this object is sync in updates with its plant
    }

    public void AddPlant()
    {
        // for adding a plant to the garden plot
        // please use the BasePrefab.basePlantPrefab
    }

    public void WaterPlant()
    {
        // water the plant
    }
}
