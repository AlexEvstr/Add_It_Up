using BansheeGz.BGSpline.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private int _ballCount;
    [SerializeField] private float _ballSpeed;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLoadTime;
    [SerializeField, Range(1, 5)] private int _lifeCount;

    [Header("Windows")]
    [SerializeField] private GameObject _retryWindow;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _pauseWindow;
    [SerializeField] private SpriteRenderer[] _arrowSprites;

    public static GameManager Instance { get; private set; }
    public BGCcMath BgMath { get; private set; }
    public MoveBalls MoveBallsScript { get; private set; }
    public int BallCount => _ballCount;
    public float BallSpeed => _ballSpeed;
    public float BulletSpeed => _bulletSpeed;
    public float BulletLoadTime => _bulletLoadTime;

    private void Awake()
    {
        Instance = this;
        BgMath = FindObjectOfType<BGCcMath>();
        MoveBallsScript = FindObjectOfType<MoveBalls>();
    }

    public void OpenPauseBtn()
    {
        _pauseWindow.SetActive(true);
    }

    public void ClosePauseBtn()
    {
        _pauseWindow.SetActive(false);
    }

    public void Win()
    {
        ScoreManager.Instance.AddCoins();
        _winWindow.SetActive(true);
    }

    public void Lose()
    {
        ScoreManager.Instance.AddCoins();
        _retryWindow.SetActive(true);
    }

    public bool IsAnyWindowOpen()
    {
        return _retryWindow.activeSelf || _winWindow.activeSelf || _pauseWindow.activeSelf;
    }


    public void MainMenuBtn_Click()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void RetryBtn_Click()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void NextLevelBtn_Click()
    {
        SceneManager.LoadScene("GameScene");
    }

    public Sprite GetRandomSprite()
    {
        Sprite randomSprite = _sprites[Random.Range(0, _sprites.Length)];
        foreach (var item in _arrowSprites)
        {
            item.sprite = randomSprite;
        }
        return randomSprite;
    }
}