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
    [SerializeField] private Transform _lifeWindow;
    [SerializeField] private GameObject _gameOverWindow;
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

        Time.timeScale = 1;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameOverWindow.activeSelf && !_retryWindow.activeSelf && !_winWindow.activeSelf)
        {
            if (_pauseWindow.activeSelf)
            {
                Time.timeScale = 1;
                _pauseWindow.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                _pauseWindow.SetActive(true);
            }
        }
    }

    public void Win()
    {
        Time.timeScale = 0;
        _winWindow.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0;

        _retryWindow.SetActive(true);
    }

    public bool IsPaused()
    {
        return Time.timeScale == 0;
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

    public void ContinueBtn_Click()
    {
        Time.timeScale = 1;
        _pauseWindow.SetActive(false);
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