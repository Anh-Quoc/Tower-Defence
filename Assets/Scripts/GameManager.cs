using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int currentWave = 0;

    GameState currentState;

    public event Action<GameState> OnStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }


    // Test code to change state
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(GameState.CardSelection);
            currentWave++;
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

