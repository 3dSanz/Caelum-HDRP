using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

/*public class EnemyMovement : MonoBehaviour
{
    Enemy _enemy;
    NavMeshAgent _agent;
    Transform _player;
    public bool _perseguir;
    public bool _patrullar;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(_perseguir == true)
        {
            Perseguir();
        }
        else
        {
            Patrullar();
        }

        if (_patrullar == true)
        {
            Patrullar();
        }
    }

    void Perseguir()
    {
        _agent.destination = _player.position;
    }

    void Patrullar()
    {
        _agent.destination = _enemy._patrolPoints[Random.Range(0, _enemy._patrolPoints.Length)].position;
    }
}*/
