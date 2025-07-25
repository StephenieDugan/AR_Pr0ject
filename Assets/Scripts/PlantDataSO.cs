using UnityEngine;

[CreateAssetMenu(fileName = "New PlantDataSO", menuName = "Scriptable Objects/PlantDataSO")]
public class PlantDataSO : ScriptableObject
{
    [SerializeField] private string plantName = "plant";
    [SerializeField] private GameObject[] growthModels;

    public ProduceDataSO myProduceData;

    public GameObject getPlantModel(int index)
    {
        return growthModels[index];
    }
}
