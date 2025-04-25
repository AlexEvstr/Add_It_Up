using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchieveController : MonoBehaviour
{
    [SerializeField] private Button[] _achieveButtons;
    [SerializeField] private GameObject _circleImage;
    [SerializeField] private TMP_Text _achievesNumberText;
    [SerializeField] private TMP_Text _totalCoinsText;
    private int _activeCount;

    private void Start()
    {
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        _activeCount = 0;

        for (int i = 0; i < _achieveButtons.Length; i++)
        {
            bool isActive = PlayerPrefs.GetInt($"Achieve_{i}", 0) == 1;
            _achieveButtons[i].interactable = isActive;
            _achieveButtons[i].GetComponent<ButtonScaleEffect>().enabled = isActive;

            if (isActive)
            {
                _activeCount++;
            }

            
        }
        CheckRedCircle();

    }


    public void ResetAchievements()
    {
        PlayerPrefs.SetInt("GamesPlayed", 0);
        PlayerPrefs.SetInt("WinsCount", 0);
        PlayerPrefs.SetInt("TakenAchievs", 0);
        for (int i = 0; i < _achieveButtons.Length; i++)
        {
            PlayerPrefs.SetInt($"Achieve_{i}", 0);
        }
        PlayerPrefs.Save();
        UpdateButtonStates();
    }

    public void AddCoinsForAchieve(int coinsToAdd)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += coinsToAdd;
        _totalCoinsText.text = totalCoins.ToString();
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        int takenaAchievs = PlayerPrefs.GetInt("TakenAchievs", 0);
        takenaAchievs++;
        PlayerPrefs.SetInt("TakenAchievs", takenaAchievs);
        CheckRedCircle();
    }

    private void CheckRedCircle()
    {
        int takenaAchievs = PlayerPrefs.GetInt("TakenAchievs", 0);
        _activeCount -= takenaAchievs;
        if (_activeCount > 0)
        {
            _circleImage.SetActive(true);
            _achievesNumberText.text = _activeCount.ToString();
        }
        else
        {
            _circleImage.SetActive(false);
            _achievesNumberText.text = "";
        }
    }
}
