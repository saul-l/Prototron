/* Component for shooting projectiles
 * Launches GameObject bullet towards shootingDirection when boolean fire is true
 * */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using static CustomExtension;
using static UnityEngine.GraphicsBuffer;

public class ShootingComponent : MonoBehaviour, IShooting
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



    public bool fire;
    private bool prevFired;
    private float timingCorrection;
    public UnityEvent hasShot;
    private Vector3 weaponPosition = new Vector3(0, 0.55f, 0);
    private float weaponDistance = 0.2f;
    [SerializeField] private int bulletsLeft;
    private GameObject shootingEffect;
    private int bulletSequencePosition;
    private bool reloading;
    void Start()
    {
        InitializeWeapon();
    }
    void FixedUpdate()
    {
        if (nextShotTime <= Time.time)
        {
            if (reloading)
            {
                reloading = false;
                bulletsLeft = weaponType.clipSize;
            }
            
            if (fire)
            {
                //Correct timing if continuously shooting
                if (prevFired)
                {
                    timingCorrection = Time.time - nextShotTime;
                }

                prevFired = true;

                if (weaponType.clipSize > 0)
                {
                    bulletsLeft--;

                    if (bulletsLeft <= 0)
                    {
                        reloading = true;
                        nextShotTime = Time.time + weaponType.reloadTime - timingCorrection;
                    }
                    else
                    {
                        nextShotTime = Time.time + (weaponType.attackRate * 0.016666667f) - timingCorrection;
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
        Vector3 spawnPosition = weaponPosition + transform.position + shootingDirection * weaponDistance;

        shootingEffect.SetActive(false);
        shootingEffect.SetActive(true);
        SASSimpleAudioSystem.PlayAudioEvent(weaponType.shootingAudioEvent, gameObject);
        for (int ii = 0; ii < spawnAmount; ii++)
        {
            GameObject newBullet = myPool.GetPooledObject();

            if (newBullet != null)
            {
                hasShot.Invoke();

                float localInaccuracy = Random.Range(-weaponType.inaccuracy, weaponType.inaccuracy);

                BulletComponent newBulletBulletComponent = newBullet.GetComponent<BulletComponent>();

                // Weapon position should come from weapon bone position eventually)
                newBullet.transform.position = spawnPosition;
                newBullet.transform.rotation = Quaternion.identity;
                newBullet.SetActive(true);
                newBulletBulletComponent.SpawnFromPool();
                newBulletBulletComponent.velocity = weaponType.bulletSpeed * (Quaternion.Euler(0, localInaccuracy+bulletSequencePosition*weaponType.bulletSequenceAngleDifference+weaponType.bulletAngle, 0) * shootingDirection);
                newBulletBulletComponent.damage = weaponType.damage;

                bulletSequencePosition++;
                if (bulletSequencePosition >= weaponType.bulletSequenceLength)
                {
                    bulletSequencePosition = 0;
                }
            }
        }
    }

    void InitializeWeapon()
    {
        bulletsLeft = weaponType.clipSize;
        myPool = GameObjectDependencyManager.instance.GetGameObject("PoolHandler").GetComponent<PoolHandler>().GetPool(bullet.gameObject.name, PoolType.ForcedRecycleObjectPool);
        myPool.PopulatePool(bullet, weaponType.pooledBullets);
        shootingEffect = GameObject.Instantiate(weaponType.shootingEffect, transform.position + weaponType.shootingEffect.transform.localPosition + transform.forward, weaponType.shootingEffect.transform.rotation * transform.rotation, transform);
        shootingEffect.SetActive(false);
    }

    Vector3 IShooting.shootingDirection
    {
        get => shootingDirection;
        set => shootingDirection = value;
    }

    bool IShooting.fire
    {
        get => fire;
        set => fire = value;
    }
    WeaponRangedScriptableObject IShooting.weaponType
    {
        get => weaponType;
        set
        {
            weaponType = value;
            InitializeWeapon();
        }
    }
    int IShooting.bulletsLeft
    {
        get => bulletsLeft;
    }
}
