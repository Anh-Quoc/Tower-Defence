using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject GunbasePrefab;
    private GameObject Gunbase;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Gunbase = Instantiate(GunbasePrefab, GetWorldPosition(), Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        Gunbase.transform.position = GetWorldPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Ensure Gunbase snaps to a valid position on the map
        Gunbase.transform.position = GetValidMapPosition(Gunbase.transform.position);
        Destroy(gameObject);
    }

    private Vector3 GetWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Set an appropriate Z depth for conversion
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    private Vector3 GetValidMapPosition(Vector3 position)
    {
        // Implement logic to snap to grid or restrict within bounds
        float x = Mathf.Round(position.x);
        float y = Mathf.Round(position.y);
        return new Vector3(x, y, position.z);
    }
}