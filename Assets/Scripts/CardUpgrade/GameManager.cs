using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	private bool gameOver = false;
	[SerializeField]
	int currentWave = 0;
	int gold = 150;
	[SerializeField] float maxHealth = 1000;
	float currentHealth = 0;
	[SerializeField] int waveUnlockUpgrade = 5;
	GameState currentState;
	public HeathBar healthBar;

	public event Action<GameState> OnStateChanged;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		currentHealth = maxHealth;
	}

	public bool IsSelect { get; set; } = true;

	public int GetCurrentWave()
	{
		return currentWave;
	}
	public void NextWave()
	{
		if (currentWave % waveUnlockUpgrade == 0 && !IsSelect) IsSelect = true;
		currentWave += 1;
	}
	public int GetGold => gold;
	public bool IsGameOver => gameOver;
	public void AddGold(int gold) {
		this.gold += gold;
		if (this.gold < 0) this.gold = 0;
	}
	public float GetCurrentHealth => currentHealth;
	public void TakeDamage(float damage)
	{
		if (currentHealth <= 0)
		{
			gameOver = true;
			return;
		}
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);

	}

	public void HealthRecover(float health)
	{
		if (currentHealth >= maxHealth)
		{
			return;
		}
		currentHealth += health;
		healthBar.SetHealth(currentHealth);

	}

	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	private void Update()
	{
		if (gameOver)
		{
			// TODO: Stop the game and show game over screen
			SceneManager.LoadScene("Start");
			
			return;
		}
		if (currentState == GameState.Playing && IsSelect)
		{
			IsSelect = false;
			ChangeState(GameState.CardSelection);
			Debug.Log("Wave: " + currentWave);
		}
	}

	public void ChangeState(GameState newState)
	{
		currentState = newState;
		OnStateChanged?.Invoke(newState);
		HandleStateChanged();
	}

	private void HandleStateChanged()
	{
		switch (currentState)
		{
			case GameState.Playing:
				CardManager.Instance.HideCardSelection();
				Debug.Log("Game is playing");
				break;
			case GameState.CardSelection:
				CardManager.Instance.ShowCardSelection();
				Debug.Log("Card selection");
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public enum GameState
	{
		Playing,
		CardSelection,
	}
}

