using UnityEngine;
using System;
using TMPro;

public class DailyBonusManager : MonoBehaviour
{
    [SerializeField] private GameObject dailyBonusWindow;
    private WindowAnimator _windowAnimator;
    private int _totalCoins;
    [SerializeField] private TMP_Text _coinsText;

    private const string LastClaimKey = "LastDailyBonusClaim";

    private void Start()
    {
        _windowAnimator = GetComponent<WindowAnimator>();
        if (ShouldShowBonus())
        {
            _windowAnimator.OpenWindow(dailyBonusWindow);
            AudioController.Instance.PlayButtonSound();
        }
        _totalCoins = PlayerPrefs.GetInt("TotalCoins");
        _coinsText.text = _totalCoins.ToString();
    }

    public void OnTakeBonus()
    {
        _totalCoins += 250;
        PlayerPrefs.SetInt("TotalCoins", _totalCoins);
        _coinsText.text = _totalCoins.ToString();
        PlayerPrefs.SetString(LastClaimKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
        _windowAnimator.CloseWindow(dailyBonusWindow);
    }

    private bool ShouldShowBonus()
    {
        if (!PlayerPrefs.HasKey(LastClaimKey)) return true;

        DateTime lastClaim = DateTime.Parse(PlayerPrefs.GetString(LastClaimKey));
        TimeSpan difference = DateTime.Now - lastClaim;

        return difference.TotalHours >= 24;
    }
}
