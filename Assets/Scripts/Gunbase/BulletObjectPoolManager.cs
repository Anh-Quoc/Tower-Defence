using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPoolManager : MonoBehaviour
{
    // public static BulletObjectPoolManager instance;

    public GameObject bulletPrefab;
    public int bulletAmount = 20;
    public bool shouldExpand = true;

    private Queue<GameObject> bulletPool;

    void Awake()
    {
        // instance = this;
    }

    void Start()
    {
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<BulletController>().SetPool(this);     
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count == 0)
        {
            if (shouldExpand)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                return bullet;
            }
            else
            {
                return null;
            }
        }
        else
        {
            GameObject bullet = bulletPool.Dequeue();
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

}