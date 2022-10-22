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
   
    public UnityEvent hasShot;
    private Vector3 weaponPosition = new Vector3(0,0.15f,0);
    void Start()
    {
       myPool = PoolHandler.instance.GetPool(this.gameObject.name, PoolTypes.PoolType.ForcedRecycleObjectPool);
       myPool.PopulatePool(bullet, 20);
    }
    void FixedUpdate()
    {
        /*  Start shooting based on shootingDirection and force it to 4 angles
            This is stupid. No need to be check this every frame. Should be 
            a coroutine started by input or ai component */

                if (shootingDirection!=Vector3.zero && lastShotTime <= Time.time)
                {
                    angle = fourer * Mathf.Atan2(shootingDirection.x, shootingDirection.z);
                    angle = Mathf.Round(angle);
                    angle *= antiFourer;
                    newShootingDirection.z = Mathf.Cos(angle);
                    newShootingDirection.x = Mathf.Sin(angle);

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
            newBullet.transform.position = weaponPosition + transform.position + newShootingDirection;
            newBullet.transform.rotation = Quaternion.identity;
            newBullet.SetActive(true);
            newBulletBulletComponent.SpawnFromPool();
            newBulletBulletComponent.velocity = newShootingDirection * bulletSpeed;
            

        }

    }

}
