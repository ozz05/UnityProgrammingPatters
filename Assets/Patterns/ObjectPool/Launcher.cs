using System;
using UnityEngine;
using UnityEngine.Pool;

public class Launcher : MonoBehaviour {
    [SerializeField] Bullet bulletPrefab;
    private IObjectPool<Bullet> _bulletPool;

    private void Awake() {
        _bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGet,
            OnRelease,
            OnDestroyBullet,
            defaultCapacity:15,
            maxSize:3


        );
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }


    private void OnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }


    private void OnGet(Bullet bullet)
    {
        bullet.gameObject.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
    }


    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.SetPool(_bulletPool);
        return bullet;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bulletPool.Get();
        }
    }
}