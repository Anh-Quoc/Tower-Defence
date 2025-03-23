using UnityEngine;
using UnityEngine.EventSystems;

public class PlaneScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject PlanePrefab;
    private GameObject Plane;
    private Camera mainCamera;
    private bool isMovingLeft = false; // Track movement state
    private float moveSpeed = 4f; // Speed of movement to the left

    private void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (PlanePrefab == null)
        {
            Debug.LogError("PlanePrefab is not assigned!");
            return;
        }

        Plane = Instantiate(PlanePrefab, GetWorldPosition(), Quaternion.identity);
        Debug.Log("Plane instantiated at position: " + Plane.transform.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Plane == null) return;

        Vector2 worldPos = GetWorldPosition();
        Plane.transform.position = worldPos;
        Debug.Log("Plane being dragged to position: " + worldPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Plane == null)
        {
            Debug.LogWarning("Plane is null on EndDrag!");
            return;
        }

        // Keep the object at its position and start moving it to the left
        isMovingLeft = true;
        Debug.Log("Plane drag ended. Position: " + Plane.transform.position);
    }

    private Vector2 GetWorldPosition()
    {
        return mainCamera != null ? (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) : Vector2.zero;
    }

    private void Update()
    {
        // Move the object to the left if the flag is set
        if (Plane != null && isMovingLeft)
        {
            Plane.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            Debug.Log("Plane moving to the left. Position: " + Plane.transform.position);
        }
    }
}