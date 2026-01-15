using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeAttackZone : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private GameObject _player;

    private bool _canAttack = false;
    private float _timerBetweenAttacks;

    private void Start()
    {
        _timerBetweenAttacks = _timeBetweenAttacks;
    }

    private void Update()
    {
        if (_canAttack)
        {
            _timerBetweenAttacks -= Time.deltaTime;
            if (_timerBetweenAttacks <= 0)
            {
                Debug.Log("playerDamaged");
                _player.GetComponent<Player>().TakeDamage(_damage);
                _timerBetweenAttacks = _timeBetweenAttacks;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;
            _canAttack = true;
            _timerBetweenAttacks = _timeBetweenAttacks;
            Debug.Log("playerDamaged");
            other.gameObject.GetComponent<Player>().TakeDamage(_damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject == _player)
        {
            _canAttack = false;
        }
    }


}
