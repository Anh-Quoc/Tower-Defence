using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject detectionZone;
    public GameObject gun;
    private EnemyDetection enemyDetection;
    private float rotationOffset = -90f; // Offset angle if the gun's default orientation isn't right
    public GameObject BulletPrefab;
    public float BulletSpeed = 10f;
    public float FireRate = 2f;

    private void Fire(GameObject target)
    {
        GameObject bullet = BulletObjectPoolManager.instance.GetBullet();
        if (bullet != null)
        {
            // animator.SetBool("isShooting", true);
            bullet.GetComponent<BulletController>().Initialize(gun, target);
            bullet.SetActive(true);
        }
    }


    void Start()
    {
        // Use GetComponent if detectionZone is the same GameObject
        if (detectionZone == null)
        {
            Debug.LogError("DetectionZone is not set in GunController!");
            return; // Exit early to prevent null reference exceptions
        }

        enemyDetection = detectionZone.GetComponent<EnemyDetection>();
        if (enemyDetection == null)
        {
            Debug.LogError("EnemyDetection component not found in DetectionZone!");
        }
    }
    private float nextFireTime = 0f;

    void Update()
    {
        // Only proceed if all components are valid
        if (enemyDetection != null && gun != null && enemyDetection.CurrentTarget != null)
        {
            AimAt(enemyDetection.CurrentTarget);

            float buffedFireRate = FireRate / UpgradeStats.Instance.attackSpeedMultiplier;

			if (Time.time >= nextFireTime)
            {
                Fire(enemyDetection.CurrentTarget);
                nextFireTime = Time.time + buffedFireRate;
            }
        }
    }

    private void AimAt(GameObject target)
    {
        if (target == null) return;

        // Calculate direction vector from gun to target
        Vector2 direction = target.transform.position - gun.transform.position;

        // Calculate angle in degrees and add offset
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;

        // Apply rotation to the gun
        gun.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Debug the direction and angle
        Debug.DrawRay(gun.transform.position, direction.normalized * 5, Color.red);
    }
}
