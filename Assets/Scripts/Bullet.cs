using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
            enemy.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
