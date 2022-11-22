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
    [SerializeField] private WeaponRangedScriptableObject weaponType;
  
    public Pool myPool;
    public GameObject bullet;
    public Vector3 shootingDirection;
    public Vector3 newShootingDirection;
    public Vector3 prevShootingDirection = Vector3.zero;
    public float totalShootingDirectionValue;

    // Handled by weaponType ScriptableObject

    
    private float nextShotTime = 0.0f;
    
    public float angle;

    public bool fire;
    private bool prevFired;
    private float timingCorrection;
    public UnityEvent hasShot;
    private Vector3 weaponPosition = new Vector3(0,0.55f,0);
    private float weaponDistance = 0.2f;
    [SerializeField] private float bulletsLeft;

    void Start()
    {
       bulletsLeft = weaponType.clipSize;
       myPool = GameObjectDependencyManager.instance.GetGameObject("PoolHandler").GetComponent<PoolHandler>().GetPool(bullet.gameObject.name, PoolType.ForcedRecycleObjectPool);
       myPool.PopulatePool(bullet, weaponType.pooledBullets);
    }
    void FixedUpdate()
    {
        if (fire)
        {
            if (nextShotTime <= Time.time)
            {
                //Correct timing if continuously shooting
                if (prevFired) { 
                    timingCorrection = Time.time - nextShotTime;
                }

                prevFired = true;                
                
                if(weaponType.clipSize > 0)
                {
                    bulletsLeft--;

                    if(bulletsLeft <= 0)
                    {
                        bulletsLeft = weaponType.clipSize;
                        nextShotTime = Time.time + weaponType.reloadTime - timingCorrection;
                    }
                    else
                    {
                        nextShotTime = Time.time + (weaponType.attackRate* 0.016666667f) - timingCorrection;
                    }
                }
                else
                {
                    nextShotTime = Time.time + (weaponType.attackRate * 0.016666667f) - timingCorrection;
                }

                SpawnBullet(weaponType.bulletsPerShot);

                

            }
        }
        else if (prevFired)
        {
            prevFired = false;
            timingCorrection = 0.0f;
        }
    }

    void SpawnBullet(int spawnAmount)
    {
        
        Vector3 spawnPosition = weaponPosition + transform.position + shootingDirection*weaponDistance;
        
        for(int ii = 0; ii < spawnAmount; ii++)
        {
            Debug.Log("whatthefuuu");
        
            GameObject newBullet = myPool.GetPooledObject();

            if (newBullet != null)
            {
                hasShot.Invoke();
                BulletComponent newBulletBulletComponent = newBullet.GetComponent<BulletComponent>();

                // Weapon position should come from weapon bone position eventually)
                newBullet.transform.position = spawnPosition;
                newBullet.transform.rotation = Quaternion.identity;
                newBullet.SetActive(true);
                newBulletBulletComponent.SpawnFromPool();
                newBulletBulletComponent.velocity = (shootingDirection * weaponType.bulletSpeed) + new Vector3 (shootingDirection.z * Random.Range(-weaponType.inaccuracy,weaponType.inaccuracy), 0.0f, shootingDirection.x * Random.Range(-weaponType.inaccuracy,weaponType.inaccuracy));
                newBulletBulletComponent.damage = weaponType.damage;
            }
        }
    }

}
