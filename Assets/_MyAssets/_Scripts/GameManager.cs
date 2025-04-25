using BansheeGz.BGSpline.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _spritesSet1;
    [SerializeField] private Sprite[] _spritesSet2;
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
    private WindowAnimator _windowAnimator;

    private Sprite[] _activeSpriteSet;

    private void Awake()
    {
        Instance = this;
        BgMath = FindObjectOfType<BGCcMath>();
        MoveBallsScript = FindObjectOfType<MoveBalls>();
        int spriteSetIndex = PlayerPrefs.GetInt("SpriteSet", 0); // 1 - default
        _activeSpriteSet = spriteSetIndex == 1 ? _spritesSet2 : _spritesSet1;
        _windowAnimator = GetComponent<WindowAnimator>();
    }

    public void OpenPauseBtn()
    {
        _windowAnimator.OpenWindow(_pauseWindow);
    }

    public void ClosePauseBtn()
    {
        _windowAnimator.CloseWindow(_pauseWindow);
    }

    public void Win()
    {
        ScoreManager.Instance.AddCoins();
        _windowAnimator.OpenWindow(_winWindow);
    }

    public void Lose()
    {
        ScoreManager.Instance.AddCoins();
        _windowAnimator.OpenWindow(_retryWindow);
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
        Sprite randomSprite = _activeSpriteSet[Random.Range(0, _activeSpriteSet.Length)];
        foreach (var item in _arrowSprites)
        {
            item.sprite = randomSprite;
        }
        return randomSprite;
    }
}