using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // To use Button

public class PlaneScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject PlanePrefab;
    public Button yourButton; // Reference to the button you want to disable
    private GameObject Plane;
    private Camera mainCamera;
    private bool isMovingLeft = false; // Track movement state
    private float moveSpeed = 4f; // Speed of movement to the left
    private bool isPlaneCreated = false; // Track whether the plane has been created

    private void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }

        // Ensure the button is assigned
        if (yourButton == null)
        {
            Debug.LogError("Button not assigned!");
        }
    }

    // This method gets triggered when the user begins dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Prevent plane instantiation and dragging if the button is deactivated (interactable = false)
        if (yourButton == null || !yourButton.interactable)
        {
            Debug.Log("Button is deactivated. Cannot drag or instantiate a plane.");
            return;
        }

        // If the plane has not been created yet, we create it
        if (!isPlaneCreated)
        {
            if (PlanePrefab == null)
            {
                Debug.LogError("PlanePrefab is not assigned!");
                return;
            }

            // Instantiate the plane at the mouse position
            Plane = Instantiate(PlanePrefab, GetWorldPosition(), Quaternion.identity);
            isPlaneCreated = true; // Mark plane as created
            Debug.Log("Plane instantiated at position: " + Plane.transform.position);
        }
    }

    // This method is called while the user is dragging the plane
    public void OnDrag(PointerEventData eventData)
    {
        if (Plane == null || !yourButton.interactable) return; // If no plane or button is deactivated, exit

        // Update the position of the plane to follow the mouse
        Vector2 worldPos = GetWorldPosition();
        Plane.transform.position = worldPos;
        Debug.Log("Plane being dragged to position: " + worldPos);
    }

    // This method is called when the drag ends
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Plane == null || !yourButton.interactable)
        {
            Debug.LogWarning("Plane is null or button is deactivated on EndDrag!");
            return;
        }

        // Start moving the plane to the left after drag ends
        isMovingLeft = true;
        Debug.Log("Plane drag ended. Position: " + Plane.transform.position);

        // Deactivate the button interactability after dropping the plane
        if (yourButton != null)
        {
            yourButton.interactable = false; // Disable the button interactability
            Debug.Log("Button deactivated.");
        }
    }

    // Convert mouse position to world space position
    private Vector2 GetWorldPosition()
    {
        return mainCamera != null ? (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) : Vector2.zero;
    }

    // Update method to move the plane to the left
    private void Update()
    {
        // If plane is created and the flag is set, move the plane to the left
        if (Plane != null && isMovingLeft)
        {
            Plane.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            Debug.Log("Plane moving to the left. Position: " + Plane.transform.position);
        }
    }

    // Method to reactivate the button if needed
    public void ReactivateButton()
    {
        if (yourButton != null)
        {
            yourButton.interactable = true; // Reactivate the button interactability
            Debug.Log("Button reactivated.");
        }
    }
}
