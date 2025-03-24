using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunUIController : MonoBehaviour
{

    private GameObject currentGun; // Assign in the Inspector

    public GameObject panelGunInfo; // Assign in the Inspector

    public Text MoneyText; // Use TextMeshPro for UI text

    public GameObject DropAreaPrefab;

    public void DisplayPanelGunInfo(GameObject gun)
    {
        SetCurrentGun(gun);

        if (panelGunInfo != null)
            panelGunInfo.SetActive(true);
    }

    public void HidePanelGunInfo()
    {
        if (panelGunInfo != null)
            panelGunInfo.SetActive(false);
    }

    public void SetCurrentGun(GameObject gun)
    {
        currentGun = gun;
    }

    public void SellGun()
    {
        if (currentGun != null)
        {
            // Ensure the GunController component exists before accessing it
            GunController gunController = currentGun.GetComponent<GunController>();
            if (gunController == null) return;

            int gunPrice = gunController.price;

            // Instantiate DropAreaPrefab at the currentGun's position
            if (DropAreaPrefab != null)
            {
                Instantiate(DropAreaPrefab, currentGun.transform.position, Quaternion.identity);
            }

            // Remove the gun
            Destroy(currentGun);
            HidePanelGunInfo();

            // Safely parse MoneyText and update the value
            if (int.TryParse(MoneyText.text, out int currentMoney))
            {
                GameManager.Instance.AddGold(gunPrice / 2);
            }
        }
    }


}
