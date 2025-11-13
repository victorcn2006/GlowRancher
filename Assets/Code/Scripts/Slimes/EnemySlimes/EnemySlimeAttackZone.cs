using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeAttackZone : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private GameObject player;

    private bool canAttack = false;
    private float timerBetweenAttacks;

    private void Start()
    {
        timerBetweenAttacks = timeBetweenAttacks;
    }

    private void Update()
    {
        if (canAttack)
        {
            timerBetweenAttacks -= Time.deltaTime;
            if (timerBetweenAttacks <= 0)
            {
                Debug.Log("playerDamaged");
                player.GetComponent<Player>().TakeDamage(damage);
                timerBetweenAttacks = timeBetweenAttacks;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            canAttack = true;
            timerBetweenAttacks = timeBetweenAttacks;
            Debug.Log("playerDamaged");
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject == player)
        {
            canAttack = false;
        }
    }


}
