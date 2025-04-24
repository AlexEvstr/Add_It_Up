using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int _score;
    private int _totalCoins;
    private int _coins;

    [SerializeField] private TMP_Text _scoreGameText;
    [SerializeField] private TMP_Text _scoreWinText;
    [SerializeField] private TMP_Text _scoreLoseText;
    [SerializeField] private TMP_Text _winCoinsText;
    [SerializeField] private TMP_Text _loseCoinsText;
    [SerializeField] private TMP_Text _plusScoreText;
    [SerializeField] private GameObject _plusScoreObject;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _totalCoins = PlayerPrefs.GetInt("TotalCoins");
        _score = 0;
        _coins = 0;
        _scoreGameText.text = _score.ToString();
    }

    public void AddScore(int index)
    {
        _score += index * 10;
        _scoreGameText.text = _score.ToString();
        _scoreWinText.text = _score.ToString();
        _scoreLoseText.text = _score.ToString();
        _plusScoreText.text = $"+{index * 10}";

        StartCoroutine(ShowAndHidePlusScore());
    }

    private IEnumerator ShowAndHidePlusScore()
    {
        _plusScoreObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        _plusScoreObject.SetActive(false);
    }

    public void AddCoins()
    {
        _coins = _score / 5;
        _totalCoins += _coins;
        PlayerPrefs.SetInt("TotalCoins", _totalCoins);
        _winCoinsText.text = _coins.ToString();
        _loseCoinsText.text = _coins.ToString();
    }
}