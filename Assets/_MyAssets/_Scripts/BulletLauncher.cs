using System.Collections;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint; // точка выстрела
    [SerializeField] private Transform _aimArrow;  // стрелка-прицел
    [SerializeField] private GameObject _boomPrefab;

    private Bullet _bullet;
    private bool _isAiming;

    private void Start()
    {
        InstantiateBullet();
        _aimArrow.gameObject.SetActive(false); // скрываем при старте
    }

    private void Update()
    {
        if (GameManager.Instance.IsAnyWindowOpen()) return;

#if UNITY_EDITOR
        var isTouching = Input.GetMouseButton(0);
        var touchPos = Input.mousePosition;
        var isTouchDown = Input.GetMouseButtonDown(0);
        var isTouchUp = Input.GetMouseButtonUp(0);
#else
        var isTouching = Input.touchCount > 0;
        var touch = isTouching ? Input.GetTouch(0) : default;
        var touchPos = touch.position;
        var isTouchDown = isTouching && touch.phase == TouchPhase.Began;
        var isTouchUp = isTouching && touch.phase == TouchPhase.Ended;
#endif

        if (isTouchDown)
        {
            _aimArrow.gameObject.SetActive(true);
            _isAiming = true;
        }

        if (_isAiming)
        {
            Vector3 worldTouch = Camera.main.ScreenToWorldPoint(touchPos);
            worldTouch.z = 0;
            Vector3 direction = (worldTouch - _firePoint.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _aimArrow.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (isTouchUp && _bullet)
        {
            _aimArrow.gameObject.SetActive(false);
            _isAiming = false;

            Vector3 worldTouch = Camera.main.ScreenToWorldPoint(touchPos);
            worldTouch.z = 0;
            Vector3 direction = (worldTouch - _firePoint.position).normalized;

            _bullet.Fire();
            _bullet = null;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(GameManager.Instance.BulletLoadTime);
        InstantiateBullet();
    }

    private void InstantiateBullet()
    {
        _bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity, transform).GetComponent<Bullet>();
        _bullet.SetSprite(GameManager.Instance.GetRandomSprite());
    }

    public void CreateBoom(Vector2 position)
    {
        GameObject boom = Instantiate(_boomPrefab);
        boom.transform.position = position;
        Destroy(boom, 2.0f);
    }
}