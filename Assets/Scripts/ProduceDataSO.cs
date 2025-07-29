using UnityEngine;

[CreateAssetMenu(fileName = "ProduceDataSO", menuName = "Scriptable Objects/ProduceDataSO")]
public class ProduceDataSO : ScriptableObject
{
	[SerializeField] private string produceName = "produce";
	[SerializeField] private Mesh[] growthMeshes;

	public Mesh getProduceMesh(int index)
	{
		return growthMeshes[index];
	}
}
