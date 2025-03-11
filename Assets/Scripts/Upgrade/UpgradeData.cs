using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade System/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public UpgradeTier tier;
    public UpgradeEffect effect;
    public float effectValue;
    public Sprite upgradeSprite;
}

public enum UpgradeTier { Silver, Gold, Diamond }
public enum UpgradeEffect { IncreaseDamage, IncreaseAttackSpeed, IncreaseExp, IncreaseGold, IncreaseBaseHealth }
