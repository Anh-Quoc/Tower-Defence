using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public static UIController instance;

	public Text goldText;
	public Text waveText;

	private void Awake()
	{
		instance = this;
	}

	void Start()
	{

	}
	// Update is called once per frame
	void Update()
    {
		goldText.text = GameManager.Instance.GetGold.ToString();
		waveText.text = "Wave: " + GameManager.Instance.GetCurrentWave().ToString();
	}
}
