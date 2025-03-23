using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool gameOver = false;
    int currentWave = 0;
    int gold = 150;
    [SerializeField] float maxHealth = 1000;
    float currentHealth = 0;
    [SerializeField] int waveUnlockUpgrade = 5;
    GameState currentState;

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

    public int GetCurrentWave()
    {
        return currentWave;
    }
    public void NextWave()
    {
        currentWave += 1;
    }
    public int GetGold => gold;
    public bool IsGameOver => gameOver;
    public void AddGold(int gold) => this.gold += gold;
    public float GetCurrentHealth => currentHealth;
    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
        {
            gameOver = true;
            return;
        }
        currentHealth -= damage;
    }


    private void Update()
    {
        if (gameOver)
        {
            // TODO: Stop the game and show game over screen
            return;
        }
        // if (currentState == GameState.Playing && (currentWave) % waveUnlockUpgrade == 0)
        // {
        //     ChangeState(GameState.CardSelection);
        //     Debug.Log("Wave: " + currentWave);
        // }
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

