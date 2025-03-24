using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
	public static UpgradeStats Instance;

	public float damageMultiplier = 1f;
	public float attackSpeedMultiplier = 1f;
	public float healBonus = 0f;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void ApplyCardEffect(CardSO card)
	{
		switch (card.effectType)
		{
			case CardEffect.DamageIncrease:
				damageMultiplier *= card.effectValue;
				break;
			case CardEffect.AttackSpeedIncrease:
				attackSpeedMultiplier *= card.effectValue;
				break;
			case CardEffect.HealIncrease:
				healBonus += card.effectValue;
				break;
		}

		Debug.Log($"Buff Applied: {card.effectType} +{card.effectValue}");
		Debug.Log($"Total Buffs -> Damage x{damageMultiplier}, Speed x{attackSpeedMultiplier}, Heal +{healBonus}");
	}
}
