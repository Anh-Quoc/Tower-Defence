using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private GameObject targetEnemy;
    private BulletObjectPoolManager bulletObjectPoolManager;

    public int damage = 1;

    void Start()
    {
        // bulletObjectPoolManager = BulletObjectPoolManager.instance;
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            // Move towards the enemy
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);

            // Rotate towards the enemy
            Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            // If enemy is lost, just move forward
            transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        }
    }

    // Separate method to assign the enemy target
    public void Initialize(GameObject gunBase, GameObject targetEnemy)
    {
        // Set enemy target
        this.targetEnemy = targetEnemy;

        if (gunBase != null)
        {
            // Set bullet position and rotation to match the gun
            transform.position = gunBase.transform.position;
            transform.rotation = gunBase.transform.rotation;
        }
        else
        {
            Debug.LogWarning("GunBase not found!");
        }
    }

    public void SetPool(BulletObjectPoolManager pool){
        bulletObjectPoolManager = pool;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            bulletObjectPoolManager.ReturnBullet(gameObject);
            Debug.Log("Bullet hit wall or enemy!");
        }
    }

}
