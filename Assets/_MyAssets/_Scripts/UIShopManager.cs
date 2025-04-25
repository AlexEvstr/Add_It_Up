using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShopManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> shopCards; // все карточки
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private TMP_Text coinText;

    private int currentIndex;

    public static UIShopManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentIndex = 0;
        UpdateCardVisibility();
        UpdateArrowState();
    }

    public void ShowNext()
    {
        if (currentIndex < shopCards.Count - 1)
        {
            currentIndex++;
            UpdateCardVisibility();
            UpdateArrowState();
        }
    }

    public void ShowPrevious()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateCardVisibility();
            UpdateArrowState();
        }
    }

    private void UpdateCardVisibility()
    {
        for (int i = 0; i < shopCards.Count; i++)
        {
            shopCards[i].SetActive(i == currentIndex);
        }
    }

    private void UpdateArrowState()
    {
        leftButton.interactable = currentIndex > 0;
        rightButton.interactable = currentIndex < shopCards.Count - 1;
    }

    public void UpdateCoinText()
    {
        int coins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinText.text = coins.ToString();
    }

    public void DeselectAllBackgrounds()
    {
        foreach (var card in shopCards)
        {
            var item = card.GetComponent<ShopItem>();
            if (item != null && item.Type == ShopItem.ItemType.Background)
            {
                item.Deselect();
            }
        }
    }

    public void DeselectAllProjectiles()
    {
        foreach (var card in shopCards)
        {
            var item = card.GetComponent<ShopItem>();
            if (item != null && item.Type == ShopItem.ItemType.ElementSet)
            {
                item.Deselect();
            }
        }
    }

    public void IncreaseCoinsForTest()
    {
        int coins = PlayerPrefs.GetInt("TotalCoins", 0);
        coins += 100;
        PlayerPrefs.SetInt("TotalCoins", coins);
        coinText.text = coins.ToString();
    }

}
