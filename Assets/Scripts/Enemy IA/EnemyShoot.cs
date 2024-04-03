using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform _gunPosition;
    [SerializeField] private int _bulletType = 0;
    Enemy _enemy;
    NavMeshAgent _agent;
    //Bullet _bullet;

    [SerializeField] private float _timeDisparo = 3f;
    private float _timeSiguienteDisparo;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        //_bullet = GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        if( _enemy._isAttacking == true && Time.time >= _timeSiguienteDisparo)
        {
            for (int i = 1; i <= 1; i++)
            {
                SpawnBullets();
            }
            _timeSiguienteDisparo = Time.time + _timeDisparo;
        }
    }

    private void SpawnBullets()
    {
        GameObject bullet = PoolManager.Instance.GetPooledObjects(_bulletType, _gunPosition.position, _gunPosition.rotation);


        if(bullet != null)
        {
            bullet.GetComponent<Bullet>().ResetBullet();
            bullet.SetActive(true);
        }else
        {
            Debug.LogError("Pool demasiado pequeno");
        }
    }
}
