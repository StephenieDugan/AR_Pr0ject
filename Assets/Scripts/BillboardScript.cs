using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(transform.position + (transform.position - Camera.main.transform.position));
    }
}
