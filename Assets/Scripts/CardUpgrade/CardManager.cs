using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] Transform cardPositionOne;
    [SerializeField] Transform cardPositionTwo;
    [SerializeField] Transform cardPositionThree;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject cardSelectionUI;
    [SerializeField] List<CardSO> deck;


    List<CardSO> alreadySelectedCards = new List<CardSO>();

    //Currently randomizes the cards
    GameObject card1, card2, card3;

    public static CardManager Instance;

    private void Awake()
    {
        Instance = this;
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged += GetHandleGameStateChanged;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= GetHandleGameStateChanged;
    }

    private void GetHandleGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.CardSelection)
        {
            RandomizeNewCards();
        }
    }

    void RandomizeNewCards()
    {
        if (card1 != null) Destroy(card1);
        if (card2 != null) Destroy(card2);
        if (card3 != null) Destroy(card3);

        List<CardSO> randomizedCards = new List<CardSO>();

        List<CardSO> availableCards = new List<CardSO>(deck);

        availableCards.RemoveAll(card =>
            card.isUnique && alreadySelectedCards.Contains(card)
            || card.unlockWave > GameManager.Instance.GetCurrentWave()
        );

        if (availableCards.Count < 3)
        {
            Debug.Log("Not enough available cards");
            return;
        }

        while (randomizedCards.Count < 3)
        {
            CardSO randomCard = availableCards[Random.Range(0, availableCards.Count)];
            if (!randomizedCards.Contains(randomCard))
            {
                randomizedCards.Add(randomCard);
            }
        }

        card1 = InstantiateCard(randomizedCards[0], cardPositionOne);
        card2 = InstantiateCard(randomizedCards[1], cardPositionTwo);
        card3 = InstantiateCard(randomizedCards[2], cardPositionThree);

        GameObject InstantiateCard(CardSO cardSO, Transform position)
        {
            GameObject cardGo = Instantiate(cardPrefab, position.position, Quaternion.identity, position);
            Card card = cardGo.GetComponent<Card>();
            card.Setup(cardSO);
            return cardGo;
        }

    }
    public void SelectCard(CardSO selectedCard)
    {
        if (!alreadySelectedCards.Contains(selectedCard))
        {
            alreadySelectedCards.Add(selectedCard);
        }

		UpgradeStats.Instance.ApplyCardEffect(selectedCard);

		GameManager.Instance.ChangeState(GameManager.GameState.Playing);
    }

    public void ShowCardSelection() { 
        Time.timeScale = 0;
		cardSelectionUI.SetActive(true);
    }

    public void HideCardSelection() {
		Time.timeScale = 1;
		cardSelectionUI.SetActive(false);
    }
}
