using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

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
    SpawnMoney _sMoney;
    private ParticleSystem _pSystem;

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


    [SerializeField] private float spawnInterval = 3f;

    [SerializeField] private float damage = 1f;
    [SerializeField] private float _timeGolpe = 2f;
    private float _timeSiguienteGolpe;
    [SerializeField] private float attackRadius = 1.2f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private bool facingRight = false;

    [SerializeField] float horizontal;
    [SerializeField] private float _zPosition = 83.6f;

    [SerializeField] bool _toPlayer;
    [SerializeField] bool _toPatrolPoint;
    [SerializeField] bool _normalEnemy;
    [SerializeField] bool _bossEnemy;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _sMoney = GetComponent<SpawnMoney>();
        _pSystem = GetComponent<ParticleSystem>();
    }
    void Start()
    {
        _currentHealth = _maxHealth;
        PuntoAleatorio();
        _currentState = State.Patroling;
    }

    void Update()
    {
        if(_normalEnemy == true)
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
            MovementAI();

            float horizontal = _agent.velocity.x;

            if(Time.time >= _timeSiguienteGolpe)
            {
                EnemyCollisionAttack(damage);
                _timeSiguienteGolpe = Time.time + _timeGolpe;
            }

            /*if(horizontal>0 && !facingRight)
            {
                Flip();
            } else if(horizontal<0 && facingRight)
            {
                Flip();
            }*/
            //Flip();

            _agent.Move(_agent.desiredVelocity * Time.deltaTime);

            // Obtiene la posición actual del agente
            Vector3 position = _agent.transform.position;

            // Bloquea el movimiento en el eje Z
            position.z = _zPosition;

            // Aplica la nueva posición al agente
            _agent.transform.position = position;
        }
        
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        StartCoroutine(ParticulaDanoRecibido());
        //_pSystem.Play();

        if (_currentHealth <= 0)
        {
            Die();
            _sMoney.InstanciarMonedas();
        }
    }

    IEnumerator ParticulaDanoRecibido()
    {
        _pSystem.Play();
        yield return new WaitForSeconds(0.3f);
        _pSystem.Clear();
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
            //PuntoAleatorio();
            _toPatrolPoint = true;
        }
    }

    void Chase()
    {
        _toPatrolPoint = false;
        _toPlayer = true;
        if (EnRango(_detectionRange) == false)
        {
            _toPlayer = false;
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
        /*for (int i = 1; i <= 1; i++)
        {
            SpawnBullets();
        }*/
        _isAttacking = false;
        _currentState = State.Chasing;
        /*Debug.Log("PUM!");
        _isAttacking = false;
        _currentState = State.Chasing;

        InvokeRepeating("SpawnBullets", 0f, spawnInterval);*/
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

    
    
    /*void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && Time.time >= _timeSiguienteGolpe)
        {
            other.GetComponentInParent<Health>().TakeDamage(damage);
            _timeSiguienteGolpe = Time.time + _timeGolpe;
        }
    }*/

    public void EnemyCollisionAttack(float dmg)
    {
       Collider[] players = Physics.OverlapSphere(transform.position, attackRadius, playerLayer);
        foreach (Collider player in players)
        {
            player.GetComponent<Health>().TakeDamage(dmg);
        }
    }

    void Flip()
{
    // Establecer el destino del agente
    _agent.SetDestination(_agent.destination);

    // Obtener la dirección a la que el agente debe mirar
    Vector3 lookDirection = _agent.destination - transform.position;
    lookDirection.y = 0;  // Mantener la rotación solo en el plano XZ

    // Crear una rotación que mire en la dirección deseada
    Quaternion rotation = Quaternion.LookRotation(lookDirection);

    // Aplicar la rotación al transform del agente
    _agent.transform.rotation = rotation;
}

    void MovementAI()
    {
        if(_toPlayer == true && _normalEnemy == true)
        {
            _agent.destination = _player.position;
        }

        if(_toPatrolPoint == true && _normalEnemy == true)
        {
            PuntoAleatorio();
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

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
