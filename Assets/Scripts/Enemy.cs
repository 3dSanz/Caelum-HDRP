using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum State
    {
        Patroling,
        Chasing,
        Attacking
    }
    State _currentState;
    NavMeshAgent _agent;

    //Transform para en la funcion Awake hacer que la IA busque todo objeto con el Tag "Player"
    Transform _player;

    //Puntos de patrulla
    [SerializeField] Transform[] _patrolPoints;

    //Rangos de deteccion y de ataque de la IA
    [SerializeField] float _detectionRange = 2;
    [SerializeField] float _attackRange =1;

    [SerializeField] float _maxHealth = 3f;
    [SerializeField] public bool _isAttacking;

    [SerializeField] private float _currentHealth;

     void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        _currentHealth = _maxHealth;
        PuntoAleatorio();
        _currentState = State.Patroling;
    }

    void Update()
    {
        switch(_currentState)
        {
            case State.Patroling:
            Patrol();
            break;
            case State.Chasing:
            Chase();
            break;
            case State.Attacking:
            Attack();
            break;
        }
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }
        if (_currentHealth <= 0)
        {
            Invoke(nameof(Die), 0.5f);
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void Patrol()
    {
        if(EnRango(_detectionRange) == true)
        {
            _currentState = State.Chasing;
        }

        if(_agent.remainingDistance < 0.5f)
        {
            PuntoAleatorio();
        }
    }

    void Chase()
    {
        _agent.destination = _player.position;
        if(EnRango(_detectionRange) == false)
        {
            _currentState = State.Patroling;
        }

        if(EnRango(_attackRange) == true)
        {
            _isAttacking = true;
            _currentState = State.Attacking;

        }
    }

    void Attack()
    {
        Debug.Log("PUM!");
        _isAttacking = false;
        _currentState = State.Chasing;
    }

    void PuntoAleatorio()
    {
        _agent.destination = _patrolPoints[Random.Range(0,_patrolPoints.Length)].position;
    }

    bool EnRango(float _rango)
    {
        if(Vector3.Distance(transform.position, _player.position) < _rango)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach(Transform _point in _patrolPoints)
        {
            Gizmos.DrawWireSphere(_point.position, 0.5f);
        }
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}
