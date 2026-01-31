using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTest : MonoBehaviour
{
    ObjectPooler<BulletTest> nuggetPooler;

    [SerializeField] BulletTest bulletPrefab;
    [SerializeField] float timeShoot;
    [SerializeField] Transform shootPivot;
    [SerializeField] float shootForce;


    private void Awake()
    {
        nuggetPooler = new ObjectPooler<BulletTest>(bulletPrefab);
    }
    private void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeShoot);
            Shoot();
        }
    }

    public void Shoot()
    {
        //qui � dove prendo
        BulletTest spawnedBullet = nuggetPooler.Get();

        if (!spawnedBullet.gameObject.activeSelf)
        {
            spawnedBullet.gameObject.SetActive(true);
        }
        else
        {
            spawnedBullet.onCollisionEnter += () =>
            {
                nuggetPooler.Set(spawnedBullet);
            };
        }

        //qui � dove setto l'oggetto dall'object pool a 0 con le cose di base
        spawnedBullet.transform.SetPositionAndRotation(shootPivot.position, Quaternion.identity);
        spawnedBullet.rb.linearVelocity = Vector3.zero;
        spawnedBullet.rb.AddForce(shootPivot.forward * shootForce, ForceMode.Impulse);
    }
}
