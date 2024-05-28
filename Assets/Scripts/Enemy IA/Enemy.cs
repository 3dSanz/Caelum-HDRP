using System;
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

    //[SerializeField] private GameObject _onDeath;
    //private ParticleSystem _onDeathPS;

    Boss _boss;

    //Transform para en la funcion Awake hacer que la IA busque todo objeto con el Tag "Player"
    Transform _player;

    //Puntos de patrulla
    [SerializeField] Transform[] _patrolPoints;
    private int _currentPatrolIndex = 0;

    //Rangos de deteccion y de ataque de la IA
    [SerializeField] float _detectionRange = 2;
    [SerializeField] float _attackRange =1;

    [SerializeField] float _maxHealth = 3f;
    [SerializeField] public bool _isAttacking;

    public float _currentHealth;


    [SerializeField] private float spawnInterval = 3f;

    [SerializeField] private float damage = 1f;
    [SerializeField] private float _timeGolpe = 1f;
    private float _timeSiguienteGolpe;
    [SerializeField] private float attackRadius = 1.2f;
    [SerializeField] private LayerMask playerLayer;
    //[SerializeField] private bool facingRight = false;

    [SerializeField] float horizontal;
    [SerializeField] private float _zPosition = 83.6f;

    [SerializeField] bool _toPlayer;
    [SerializeField] bool _toPatrolPoint;
    [SerializeField] bool _normalEnemy;
    [SerializeField] bool _bossEnemy;

    SoundManager _bgm;
    EntradaBoss _entry;

    public bool _isFacingRight = true;
    private bool _isMoving = false;
    private float _tolerance = 4f;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _sMoney = GetComponent<SpawnMoney>();
        _pSystem = GetComponent<ParticleSystem>();
        _boss = GetComponent<Boss>();
        _bgm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _entry = GameObject.Find("Activation Boss").GetComponent<EntradaBoss>();
        //_onDeathPS = _onDeath.GetComponent<ParticleSystem>();
    }
    void Start()
    {
        _currentHealth = _maxHealth;
        //_agent.destination = _patrolPoints[UnityEngine.Random.Range(0,_patrolPoints.Length)].position;
        _currentState = State.Patroling;
    }

    void Update()
    {
        if(_normalEnemy == true && _bossEnemy == false)
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

            //float horizontal = _agent.velocity.x;

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

            /*_agent.Move(_agent.desiredVelocity * Time.deltaTime);

            // Obtiene la posición actual del agente
            Vector3 position = _agent.transform.position;

            // Bloquea el movimiento en el eje Z
            position.z = _zPosition;

            // Aplica la nueva posición al agente
            _agent.transform.position = position;*/
        }
        if (_normalEnemy == false && _bossEnemy == true)
        {
            _boss.barra.fillAmount = _currentHealth / _maxHealth;
            if (_currentHealth > 0)
            {
                _boss.Vivo();
            }
            else
            {
                if (!_boss.muerto)
                {
                    _boss._anim.SetTrigger("dead");
                    _bgm.StopBGM();
                    SFXEnemyManager.instance.PlaySound(SFXEnemyManager.instance.deathBoss);
                    _entry._bossUI.SetActive(false);
                    _boss.muerto = true;
                    _entry.BossMuerto();
                }
            }
        }
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        //StartCoroutine(ParticulaDanoRecibido());
        _pSystem.Clear();
        _pSystem.Play();

        if (_currentHealth <= 0 && _normalEnemy == true && _bossEnemy == false)
        {
            //_onDeathPS.Play();
            Die();
            _sMoney.InstanciarMonedas();
        }
    }

    /*IEnumerator ParticulaDanoRecibido()
    {
        _pSystem.Play();
        yield return new WaitForSeconds(0.3f);
        _pSystem.Clear();
    }*/

    IEnumerator WaitToWatch()
    {
        yield return new WaitForSeconds(2f);
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
    /*// Establecer el destino del agente
    _agent.SetDestination(_agent.destination);

    // Obtener la dirección a la que el agente debe mirar
    Vector3 lookDirection = _agent.destination - transform.position;
    lookDirection.y = 0;  // Mantener la rotación solo en el plano XZ

    // Crear una rotación que mire en la dirección deseada
    Quaternion rotation = Quaternion.LookRotation(lookDirection);

    // Aplicar la rotación al transform del agente
    _agent.transform.rotation = rotation;*/



    _isFacingRight = !_isFacingRight;
    Vector3 characterScale = transform.localScale;
    characterScale.x *= -1;
    transform.localScale = characterScale;
    
}

    void MovementAI()
    {
        if(_toPlayer == true && _normalEnemy == true)
        {
            _agent.destination = _player.position;
            Vector3 targetPosition = _player.position;
            Vector3 directionToTarget = targetPosition - transform.position;
            if (directionToTarget.x > 0 && !_isFacingRight)
            {
                Flip();
            }else if (directionToTarget.x < 0 && _isFacingRight)
            {
                Flip();
            }
        }

        if(_toPatrolPoint == true && _normalEnemy == true && !_isMoving)
        {
            _isMoving = true;
            //_agent.destination = _patrolPoints[UnityEngine.Random.Range(0,_patrolPoints.Length)].position;
            //Vector3 targetPosition = _patrolPoints[UnityEngine.Random.Range(0,_patrolPoints.Length)].position;
            _agent.destination = _patrolPoints[_currentPatrolIndex].position;
            Vector3 targetPosition = _patrolPoints[_currentPatrolIndex].position;
            Vector3 directionToTarget = targetPosition - transform.position;
            if (directionToTarget.x > 0 && !_isFacingRight)
            {
                Flip();
            }else if (directionToTarget.x < 0 && _isFacingRight)
            {
                Flip();
            }
        }

        if (Vector3.Distance(transform.position, _patrolPoints[_currentPatrolIndex].position) < _tolerance)
        {
            _isMoving = false;
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
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
