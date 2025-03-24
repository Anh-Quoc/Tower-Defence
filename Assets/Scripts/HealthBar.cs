using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
	public Slider slider;
	public GameManager gameManager;

	public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

	public void SetHealth(float health)
	{
		slider.value = health;
	}
}
