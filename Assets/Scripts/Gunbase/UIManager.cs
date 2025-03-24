using UnityEngine;

public class UIManager : MonoBehaviour
{

    private GameObject GunUIController;
    private void Start()
    {
        GunUIController  = GameObject.Find("GunUIControl");
       
    }

    public void DisplayPanelGunInfo()
    {
        GunUIController.GetComponent<GunUIController>().DisplayPanelGunInfo(gameObject);
   
    }

    public void HidePanelGunInfo()
    {
        GunUIController.GetComponent<GunUIController>().HidePanelGunInfo();
    }
}
