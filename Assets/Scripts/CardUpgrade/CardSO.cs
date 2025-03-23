using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public Sprite cardImage;
    public string cardText;
    public Sprite cardBackGround;

    public CardEffect effectType;
    public float effectValue;
    public bool isUnique;
    public int unlockWave;
}

public enum CardEffect
{
    HealIncrease,
    DamageIncrease,
    AttackSpeedIncrease,
}