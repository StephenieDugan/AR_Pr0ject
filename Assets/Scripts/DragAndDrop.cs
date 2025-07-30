using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required for IPointerDownHandler, IDragHandler, IEndDragHandler
using UnityEngine.XR.ARFoundation; // Required for ARRaycastManager
using UnityEngine.XR.ARSubsystems; // Required for TrackableType

public class DragUIAndPlaceAR : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    [Header("AR Setup")]
    public ARRaycastManager arRaycastManager;
    public GameObject arPrefabToPlace; // The 3D AR Prefab to instantiate
    private Vector2 originalAnchoredPosition;

    public string targetTagName = "Pot"; // Tag of the specific GameObject to hit
    // OR: public LayerMask targetLayer; // Layer of the specific GameObject to hit

    [Header("Inventory")]
    public Inventory inv;

    [Header("UI Element")]
    public RectTransform draggableRectTransform; // The RectTransform of the UI element being dragged
    public Canvas parentCanvas; // The Canvas containing this UI element

    private GameObject currentDraggedPrefabInstance; // Instance of the AR prefab currently being dragged
    private static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    [SerializeField] private LayerMask whoIHit;

    void Start()
    {
        if (draggableRectTransform == null)
        {
            draggableRectTransform = GetComponent<RectTransform>();
        }
        if (parentCanvas == null)
        {
            parentCanvas = GetComponentInParent<Canvas>();
        }

        // Ensure ARRaycastManager is assigned
        if (arRaycastManager == null)
        {
            Debug.LogError("ARRaycastManager not assigned. Please assign it in the Inspector.");
            enabled = false; // Disable script if essential components are missing
        }

        originalAnchoredPosition = draggableRectTransform.anchoredPosition; // ensure that the UI snaps back on release

        // Ensure Canvas is in World Space if you're directly manipulating its position in World Space
        // Otherwise, if you're only using it for input and converting to world, it can be Screen Space Overlay
        // For this example, assuming conversion from screen to world for interaction
        // If your Canvas is World Space, you might adjust position slightly differently
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("UI Element: OnPointerDown");
        // Instantiate the AR prefab as a temporary preview, if needed
        // For this example, we'll instantiate on release (OnEndDrag)
        // You could instantiate a "ghost" object here and move it with OnDrag

        RaycastHit hit;
        Ray raypos = Camera.main.ScreenPointToRay(eventData.position);
        Physics.Raycast(raypos, out hit, 1000.0f, whoIHit);
        Debug.DrawRay(raypos.origin, raypos.direction, Color.aliceBlue, 5.0f);


        if (hit.rigidbody)
        {
            if (hit.rigidbody.GetComponent<ProduceScript>())
            {
                hit.rigidbody.GetComponent<ProduceScript>().TryHarvest();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("UI Element: OnDrag - Delta: " + eventData.delta);

        // Move the UI element visually
        draggableRectTransform.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;

        // If you want to show a "ghost" object during drag, instantiate it in OnPointerDown
        // and update its position here based on the raycast hit, or simply follow the UI element.
        // For simplicity, this example only places on drop.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("UI Element: OnEndDrag");

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;

        bool isPot = arPrefabToPlace.CompareTag("Pot");

        // 🌱 Inventory Check: Do we have this item?
        if (isPot && inv.Pots <= 0)
        {
            Debug.LogWarning("No pots left in inventory.");
            draggableRectTransform.anchoredPosition = originalAnchoredPosition;
            return;
        }
        else if (arPrefabToPlace.CompareTag("SunFlowerSeeds") && inv.SunflowerSeeds <= 0)
        {
            Debug.LogWarning("No sunflower seeds left.");
            draggableRectTransform.anchoredPosition = originalAnchoredPosition;
            return;
        }
        else if (arPrefabToPlace.CompareTag("TomatoSeeds") && inv.TomatoSeeds <= 0)
        {
            Debug.LogWarning("No tomato seeds left.");
            draggableRectTransform.anchoredPosition = originalAnchoredPosition;
            return;
        }

        // 🌿 Pot placement (on AR plane)
        if (isPot && arRaycastManager.Raycast(eventData.position, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = s_Hits[0].pose;

            Instantiate(arPrefabToPlace, hitPose.position, hitPose.rotation);
            inv.Pots--; // 🌱 Subtract pot from inventory
            Debug.Log("Pot placed and removed from inventory.");
        }

        // 🌸 Seed/plant placement (on a pot)
        else if (!isPot && Physics.Raycast(ray, out hit))
        {
            // Traverse up to find Pot tag
            Transform current = hit.collider.transform;
            while (current != null && !current.CompareTag("Pot"))
            {
                current = current.parent;
            }

            if (current != null)
            {
                GameObject hitObject = current.gameObject;
                Transform targetSlot = hitObject.transform.Find("Visuals/plantTransform");

                if (targetSlot != null)
                {
                    GameObject clone = Instantiate(arPrefabToPlace, targetSlot.position, Quaternion.identity);
                    clone.transform.SetParent(targetSlot, worldPositionStays: true);

                    PlantScript plantScript = clone.GetComponent<PlantScript>();
                    GardenScipt gardenScript = hitObject.GetComponent<GardenScipt>();

                    if (plantScript != null && gardenScript != null && !gardenScript.plants.Contains(plantScript))
                    {
                        gardenScript.AddPlant(plantScript);
                        Debug.Log("Plant added to garden.");

                        // 🌻 Inventory update based on seed type
                        if (arPrefabToPlace.CompareTag("SunFlowerSeeds")) inv.SunflowerSeeds--;
                        else if (arPrefabToPlace.CompareTag("TomatoSeeds")) inv.TomatoSeeds--;
                    }
                }
                else
                {
                    Debug.LogWarning("No 'plantTransform' under pot.");
                }
            }
            else
            {
                Debug.Log("Dropped on non-pot object.");
            }
        }
        else
        {
            Debug.Log("No valid surface or object detected.");
        }

        draggableRectTransform.anchoredPosition = originalAnchoredPosition;
    }

}
