using System.Collections;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    private GameObject player;
    private DungeonRoom dungeonRoom;

    private Vector3 lookDirection;
    [SerializeField] private GameObject pivotPoint;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bullet;

    void Awake()
    {
        dungeonRoom = GetComponentInParent<DungeonRoom>();
        dungeonRoom.enemies.Add(gameObject);
        player = GameObject.Find("Player");
        dungeonRoom.roomEntered.AddListener(Shoot);
        dungeonRoom.roomExited.AddListener(StopShooting);
    }

    // Update is called once per frame
    void Update()
    {
        dungeonRoom = GetComponentInParent<DungeonRoom>();
        lookDirection = transform.position - player.transform.position;
        lookDirection.y = 0;
        pivotPoint.transform.forward = lookDirection;
    }

    public void Shoot()
    {
        StartCoroutine(ShootLoop());
    }

    public void StopShooting()
    {
        StopAllCoroutines();
    }

    IEnumerator ShootLoop()
    {
        while (true)
        {
            Rigidbody bulletRb = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Rigidbody>();
            StartCoroutine(DestroyTimer(bulletRb.gameObject));
            bulletRb.AddForce(firePoint.transform.forward * -10, ForceMode.Impulse);
            yield return new WaitForSeconds(2.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            dungeonRoom.enemies.Remove(gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyTimer(GameObject bulletObj)
    {
        yield return new WaitForSeconds(5);
        Destroy(bulletObj.gameObject);
    }
}
