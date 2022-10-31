using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CustomExtension;
using static UnityEngine.GraphicsBuffer;

public class ShootingComponent : MonoBehaviour
{
    [SerializeField] public Pool myPool;
    public GameObject bullet;
    public Vector3 shootingDirection;
    public Vector3 newShootingDirection;
    public Vector3 prevShootingDirection = Vector3.zero;
    public float totalShootingDirectionValue;
    private float rateOfFire = 0.2f;
    private float lastShotTime = 0.0f;
    private float bulletSpeed = 1.0f;
    public float angle;
    const float fourer = 2.0f / Mathf.PI;
    const float antiFourer = 1.0f / fourer;
    public bool fire;
    
    public UnityEvent hasShot;
    private Vector3 weaponPosition = new Vector3(0,0.15f,0);
    private float weaponDistance = 0.2f;
    void Start()
    {
       myPool = PoolHandler.instance.GetPool(this.gameObject.name, PoolTypes.PoolType.ForcedRecycleObjectPool);
       myPool.PopulatePool(bullet, 20);
    }
    void FixedUpdate()
    {

        if (fire && lastShotTime <= Time.time)
        {

               // Analog controller forced to four directions. Should be in PlayerController
              //  angle = fourer * Mathf.Atan2(shootingDirection.x, shootingDirection.z);
              // angle = Mathf.Round(angle);
              //  angle *= antiFourer;
              //  newShootingDirection.z = Mathf.Cos(angle);
              //  newShootingDirection.x = Mathf.Sin(angle);

                lastShotTime = Time.time + rateOfFire;

                SpawnBullet();
        }



    }


    void SpawnBullet()
    {
   
        GameObject newBullet = myPool.GetPooledObject();

        if (newBullet != null)
        {
            hasShot.Invoke();
            BulletComponent newBulletBulletComponent = newBullet.GetComponent<BulletComponent>();

            // Weapon position should come from weapon bone position eventually)
            newBullet.transform.position = weaponPosition + transform.position + shootingDirection*weaponDistance;
            newBullet.transform.rotation = Quaternion.identity;
            newBullet.SetActive(true);
            newBulletBulletComponent.SpawnFromPool();
            newBulletBulletComponent.velocity = shootingDirection * bulletSpeed;
            

        }

    }

}
