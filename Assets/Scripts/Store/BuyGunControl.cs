using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyGunControl : MonoBehaviour
{

    public Text MoneyText; // Use TextMeshPro for UI text

    public GameObject[] StoreItems;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       foreach (GameObject obj in StoreItems)
        {
            DragDrop drag = obj.GetComponent<DragDrop>();
            Image img = obj.GetComponent<Image>();

            if (drag == null || img == null || drag.GunbasePrefab == null) continue;

            GunController gun = drag.GunbasePrefab.GetComponent<GunController>();
            if (gun == null) continue;

            bool isAffordable = int.Parse(MoneyText.text) >= gun.price;
            drag.isActivated = isAffordable;

            // Correct color values (0f to 1f range)
            img.color = isAffordable ? new Color(1f, 1f, 1f, 1f) : new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }
}
