using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CustomExtension;
using static UnityEngine.GraphicsBuffer;

public class ShootingComponent : MonoBehaviour
{
    public GameObject bullet;
    public Vector3 shootingDirection;
    public Vector3 newShootingDirection;
    public Vector3 prevShootingDirection = Vector3.zero;
    [SerializeField] public float totalShootingDirectionValue;
    Vector3 zeroVector = new Vector3(1.0f, 0.0f, 0.0f);
    float rateOfFire = 0.2f;
    float lastShotTime = 0.0f;
    float bulletSpeed = 14.0f;
    public float angle;
    const float fourer = 2.0f / Mathf.PI;
    const float antiFourer = 1.0f / fourer;


    void FixedUpdate()
    {
        
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
        
        GameObject newBullet = ForcedRecycleObjectPool.SharedInstance.GetPooledObject();
 
        if (newBullet != null)
        {
            newBullet.transform.position = transform.position + newShootingDirection;
            newBullet.transform.rotation = Quaternion.identity;
            newBullet.SetActive(true);
            Rigidbody newBulletRb = newBullet.GetComponent<Rigidbody>();
            newBulletRb.velocity = newShootingDirection * bulletSpeed;
        }

    }

}
