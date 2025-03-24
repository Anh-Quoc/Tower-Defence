using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject GunbasePrefab;
    private GameObject Gunbase;
    private Camera mainCamera;
    private bool canPlace = false;

    [Header("Placement Settings")]
    [SerializeField] private LayerMask gunbaseLayer;
    [SerializeField] private float placementOffset = 0.1f;

    [Header("Visual Feedback")]
    [SerializeField] private Color validPlacementColor = new Color(0, 1, 0, 0.14f);
    [SerializeField] private Color invalidPlacementColor = new Color(1, 0, 0, 0.14f);

    public TextMeshProUGUI MoneyText; // Use TextMeshPro for UI text

    public bool isActivated = false;

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
        if(isActivated == false)
        {
            return;
        }
        
        if (GunbasePrefab == null)
        {
            Debug.LogError("GunbasePrefab is not assigned!");
            return;
        }

        Gunbase = Instantiate(GunbasePrefab, GetWorldPosition(), Quaternion.identity);

        if (Gunbase.TryGetComponent(out GunController controller))
        {
            controller.Deactivate();
        }
        else
        {
            Debug.LogWarning("GunController component not found on prefab!");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Gunbase == null) return;

        Vector2 worldPos = GetWorldPosition();
        Gunbase.transform.position = worldPos;

        canPlace = IsValidPlacement(worldPos);
        UpdatePlacementIndicator(canPlace);
    }

public void OnEndDrag(PointerEventData eventData)
{
    if (Gunbase == null) return;

    if (canPlace)
    {
        Vector2 worldPos = GetWorldPosition();
        Collider2D placeableArea = GetPlaceableArea(worldPos);

        if (placeableArea != null)
        {
            Bounds bounds = placeableArea.bounds;
            Vector2 clampedPosition = new Vector2(
                Mathf.Clamp(worldPos.x, bounds.min.x, bounds.max.x),
                Mathf.Clamp(worldPos.y, bounds.min.y, bounds.max.y)
            );

            Gunbase.transform.position = clampedPosition;

            if (Gunbase.TryGetComponent(out GunController controller))
            {
                controller.Activate();
            }

            // Destroy the DropArea after placement
            placeableArea.gameObject.SetActive(false);
            Gunbase.GetComponent<GunController>().detectionZone.GetComponent<SpriteRenderer>().enabled = false;

            // Deduct the price from the money
            MoneyText.text = (int.Parse(MoneyText.text) - Gunbase.GetComponent<GunController>().price).ToString();
        }
    }
    else
    {
        Destroy(Gunbase);
    }

    Gunbase = null;
}


    private Vector2 GetWorldPosition()
    {
        return mainCamera != null ? (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) : Vector2.zero;
    }

    private Collider2D GetPlaceableArea(Vector2 position)
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(position);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("DropArea"))
            {
                return hit;
            }
        }
        return null;
    }

    private bool IsValidPlacement(Vector2 position)
    {
        Collider2D placeableArea = GetPlaceableArea(position);
        return placeableArea != null && !IsOverlappingWithGunbase(position);
    }

    private bool IsOverlappingWithGunbase(Vector2 position)
    {
        if (Gunbase == null) return false;

        Collider2D gunbaseCollider = Gunbase.GetComponent<Collider2D>();
        if (gunbaseCollider == null) return false;

        float radius = Mathf.Max(gunbaseCollider.bounds.extents.x, gunbaseCollider.bounds.extents.y) - placementOffset;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, gunbaseLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != Gunbase && collider.CompareTag("Gunbase"))
            {
                return true;
            }
        }
        return false;
    }

    private void UpdatePlacementIndicator(bool isValid)
    {
        if (Gunbase == null) return;

        if (Gunbase.TryGetComponent(out GunController controller) && controller.detectionZone != null)
        {
            if (controller.detectionZone.TryGetComponent(out SpriteRenderer renderer))
            {
                renderer.color = isValid ? validPlacementColor : invalidPlacementColor;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Gunbase != null)
        {
            Gizmos.color = canPlace ? Color.green : Color.red;
            Gizmos.DrawWireSphere(Gunbase.transform.position, 0.5f);
        }
    }

}