using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardImageRenderer;
    [SerializeField] SpriteRenderer cardBackGroundRenderer;
    [SerializeField] TextMeshPro cardTextRenderer;

    private CardSO cardInfor;
    public void Setup(CardSO card)
    {
        cardInfor = card;
        cardImageRenderer.sprite = card.cardImage;
        cardBackGroundRenderer.sprite = card.cardBackGround;
        cardTextRenderer.text = card.cardText;
    }

    public void OnMouseDown()
    {
        CardManager.Instance.SelectCard(cardInfor);
        Debug.Log("Card clicked: " + cardInfor.cardText);
    }
}
