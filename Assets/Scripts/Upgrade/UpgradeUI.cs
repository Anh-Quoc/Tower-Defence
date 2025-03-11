using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Image upgradeIcon;
    public Text upgradeTitle;
    public Text upgradeDescription;

    public void SetupUpgrade(UpgradeData upgrade)
    {
        upgradeIcon.sprite = upgrade.upgradeSprite;
        upgradeTitle.text = upgrade.upgradeName;
        upgradeDescription.text = upgrade.description + " (" + upgrade.effectValue + "%)";
    }
}
