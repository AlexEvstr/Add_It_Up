using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private float _distance;

    public Sprite Sprite
    {
        get => _renderer.sprite;
        set => _renderer.sprite = value;
    }
    public float Distance
    {
        get => _distance;
        set
        {
            var pathLength = GameManager.Instance.BgMath.GetDistance();
            _distance = value % pathLength;

            if (_distance < 0) _distance += pathLength;

            transform.position = GameManager.Instance.BgMath.CalcPositionByDistance(_distance);
        }
    }


    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
}