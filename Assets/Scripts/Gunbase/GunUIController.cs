using TMPro;
using UnityEngine;

public class GunUIController : MonoBehaviour
{

    private GameObject currentGun; // Assign in the Inspector

    public GameObject panelGunInfo; // Assign in the Inspector

    public TextMeshProUGUI MoneyText; // Use TextMeshPro for UI text

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
            // Sell the gun
            Destroy(currentGun);
            HidePanelGunInfo();
            int gunPrice = currentGun.GetComponent<GunController>().price;
            MoneyText.text = (int.Parse(MoneyText.text) + currentGun.GetComponent<GunController>().price / 2).ToString();
        }
    }

}
