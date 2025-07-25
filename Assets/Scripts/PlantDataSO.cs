using UnityEngine;

[CreateAssetMenu(fileName = "New PlantDataSO", menuName = "Scriptable Objects/PlantDataSO")]
public class PlantDataSO : ScriptableObject
{
    [SerializeField] private string plantName = "plant";
    [SerializeField] private Mesh[] growthMeshes;

    public ProduceDataSO myProduceData;

    public Mesh getPlantMesh(int index)
    {
        return growthMeshes[index];
    }
}
