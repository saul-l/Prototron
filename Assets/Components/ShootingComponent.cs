/* Component for shooting projectiles
 * Launches GameObject bullet towards shootingDirection when boolean fire is true
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CustomExtension;
using static UnityEngine.GraphicsBuffer;

public class ShootingComponent : MonoBehaviour
{
    [SerializeField] private ScriptableObject weaponType;
    public Pool myPool;
    public GameObject bullet;
    public Vector3 shootingDirection;
    public Vector3 newShootingDirection;
    public Vector3 prevShootingDirection = Vector3.zero;
    public float totalShootingDirectionValue;
    public int pooledBullets = 2;
    private float rateOfFire = (1 / 60f) * 20.0f;
    private float lastShotTime = 0.0f;
    private float bulletSpeed = 1.0f;
    public float angle;

    public bool fire;
    private bool prevFired;
    private float timingCorrection;
    public UnityEvent hasShot;
    private Vector3 weaponPosition = new Vector3(0,0.55f,0);
    private float weaponDistance = 0.2f;
    void Start()
    {
       myPool = PoolHandler.instance.GetPool(bullet.gameObject.name, PoolType.ForcedRecycleObjectPool);
       myPool.PopulatePool(bullet, pooledBullets, PopulateStyle.Add);
    }
    void FixedUpdate()
    {
        if (fire)
        {
            if (lastShotTime <= Time.time)
            {
                //Correct timing if continuously shooting
                if (prevFired) { 
                    timingCorrection = Time.time - lastShotTime;
                }
                prevFired = true;                
                lastShotTime = Time.time + rateOfFire - timingCorrection;
                SpawnBullet();
            }
        }
        else if (prevFired)
        {
            prevFired = false;
            timingCorrection = 0.0f;
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
