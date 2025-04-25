using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShopItem : MonoBehaviour
{
    public enum ItemType { Background, ElementSet }

    [Header("References")]
    [SerializeField] private ItemType itemType;
    public ItemType Type => itemType;
    [SerializeField] private int itemIndex;
    [SerializeField] private int price;
    [SerializeField] private GameObject priceGO;
    private TMP_Text priceText;
    [SerializeField] private GameObject statusGO;
    private TMP_Text statusText;
    [SerializeField] private GameObject notEnoughGO;
    [SerializeField] private Button itemButton;

    private bool isPurchased;

    private const string BackgroundKey = "SelectedBackground";
    private const string ElementSetKey = "SpriteSet";
    private const string CoinsKey = "TotalCoins";

    private string PurchasedKey => $"Purchased_{itemType}_{itemIndex}";

    private void Start()
    {
        priceText = priceGO.GetComponent<TMP_Text>();
        statusText = statusGO.GetComponent<TMP_Text>();
        priceText.text = price.ToString();

        isPurchased = PlayerPrefs.GetInt(PurchasedKey, itemIndex == 0 ? 1 : 0) == 1;

        if (itemType == ItemType.Background && PlayerPrefs.GetInt(BackgroundKey, 0) == itemIndex ||
            itemType == ItemType.ElementSet && PlayerPrefs.GetInt(ElementSetKey, 0) == itemIndex)
        {
            SetEquippedState();
        }
        else if (isPurchased)
        {
            SetEquipState();
        }
        else
        {
            SetPriceState();
        }

        itemButton.onClick.AddListener(OnItemClick);
    }

    private void OnItemClick()
    {
        int coins = PlayerPrefs.GetInt(CoinsKey, 0);

        if (isPurchased)
        {
            Equip();
        }
        else if (coins >= price)
        {
            coins -= price;
            PlayerPrefs.SetInt(CoinsKey, coins);
            PlayerPrefs.SetInt(PurchasedKey, 1);
            isPurchased = true;
            Equip();
            UIShopManager.Instance.UpdateCoinText();
        }
        else
        {
            StartCoroutine(ShowNotEnough());
        }
    }

    private void Equip()
    {
        if (itemType == ItemType.Background)
        {
            PlayerPrefs.SetInt(BackgroundKey, itemIndex);
            UIShopManager.Instance.DeselectAllBackgrounds();
            UIShopManager.Instance.UpdateBG(itemIndex);
        }
        else
        {
            PlayerPrefs.SetInt(ElementSetKey, itemIndex);
            UIShopManager.Instance.DeselectAllProjectiles();
        }
        SetEquippedState();
    }

    public void SetEquipState()
    {
        priceGO.SetActive(false);
        statusGO.SetActive(true);
        statusText.text = "EQUIP";
        itemButton.image.color = Color.white;
    }

    private void SetEquippedState()
    {
        priceGO.SetActive(false);
        statusGO.SetActive(true);
        statusText.text = "EQUIPPED";
        itemButton.image.color = Color.gray;
    }

    private void SetPriceState()
    {
        priceGO.SetActive(true);
        statusGO.SetActive(false);
        itemButton.image.color = Color.white;
    }

    private IEnumerator ShowNotEnough()
    {
        notEnoughGO.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        notEnoughGO.SetActive(false);
    }

    public void Deselect()
    {
        if (isPurchased)
        {
            priceGO.SetActive(false);
            statusGO.SetActive(true);
            statusText.text = "EQUIP";
            itemButton.image.color = Color.white;
        }
        else
        {
            priceGO.SetActive(true);
            statusGO.SetActive(false);
            itemButton.image.color = Color.white;
        }
    }


}
