using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IDamagable
{
    void TakeDamage(int damage);
}

public class EnemyAI : MonoBehaviour, IDamagable
{
    private State currentState;
    public PlayerController player;
    private DungeonRoom dungeonRoom;
    public Rigidbody rb;
    public bool detected;
    public Vector3 followDirection;
    public int enemyHp;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        dungeonRoom = GetComponentInParent<DungeonRoom>();
        dungeonRoom.enemies.Add(gameObject);

        // Initialize the starting state
        StartCoroutine(Patrol());
    }

    void Update()
    {
        followDirection = player.transform.position - transform.position;

        if (player.currentRoom != dungeonRoom)
        {
            ChangeState(new IdleState(this));
        }
        else
        {
            currentState?.Handle();
        }
    }

    public void ChangeState(State newState)
    {
        currentState = newState;
    }

    IEnumerator Patrol()
    {
        while (!detected)
        {
            ChangeState(new PatrolState(this));
            yield return new WaitForSeconds(1.2f);
            if (!detected)
            {
                ChangeState(new IdleState(this));
            }
            yield return new WaitForSeconds(1.2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detected = true;
            StopCoroutine("Patrol");
            ChangeState(new ChaseState(this));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detected = false;
            StartCoroutine(Patrol());
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHp = enemyHp - damage;
        if (enemyHp <= 0)
        {
            Destroy(gameObject);
            dungeonRoom.enemies.Remove(gameObject);
        }
    }
}

