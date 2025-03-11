using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeData> allUpgrades;

    public UpgradeData GetRandomUpgrade(UpgradeTier tier)
    {
        List<UpgradeData> filteredUpgrades = allUpgrades.FindAll(upg => upg.tier == tier);
        return filteredUpgrades[Random.Range(0, filteredUpgrades.Count)];
    }
}
