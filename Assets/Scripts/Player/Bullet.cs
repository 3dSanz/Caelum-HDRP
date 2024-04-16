using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /*[SerializeField] float bulletSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }*/

    public float speed = 10f;
    public int damage = 1;
    private Transform player;
    private Rigidbody rb;
    private Vector3 initialDirection;
    private Vector3 target;
    private bool _reflected = false;

    void Awake()
    {
        ResetBullet();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.Find("Parcy Low").transform;
        //target = new Vector3(player.position.x, player.position.y, player.position.x);
        initialDirection = (player.position - transform.position).normalized;
    }

    void Update()
    {
        if(!_reflected)
        {
            rb.velocity = initialDirection * speed;
        }else
        {
            rb.velocity = -initialDirection * speed;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(damage);
            gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "MeleeAttack" && !_reflected)
        {
            _reflected = true;
        }

        if (other.gameObject.layer == 6 && _reflected)
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    public void ResetBullet()
    {
        _reflected = false;
    }
}

/*    public float speed = 5f;
    public int damage = 1;
    private Transform player;
    private Rigidbody rb;
    private Vector3 initialDirection; // Guarda la dirección inicial de la bala
    private bool _reflected = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Parcy Low").transform;
        //initialDirection = (player.position - transform.position).normalized; // Calcula la dirección inicial
        initialDirection = transform.forward;
    }

    void Update()
    {
        //Vector3 direccion = (_reflected) ? -initialDirection : initialDirection; // Cambia la dirección si está reflejada
        //rb.velocity = direccion * speed;
        if(_reflected == false)
        {
            rb.velocity = initialDirection * speed;
        }else
            {
                rb.velocity = initialDirection * speed;
            }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponentInParent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            gameObject.SetActive(false);
        }
        
        if (collision.gameObject.CompareTag("MeleeAttack") && !_reflected)
        {
            Vector3 reflectVector = Vector3.Reflect(initialDirection, collision.contacts[0].normal);
            initialDirection = reflectVector.normalized;
            _reflected = true;
        }
        
        if (collision.gameObject.layer == 6 && _reflected) // Asume que la capa 6 es la capa de enemigos
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            gameObject.SetActive(false);
        }
        
        if (collision.gameObject.layer == 3)
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetBullet()
    {
        // Restablece el estado de la bala (llamado cuando se crea una nueva instancia)
        _reflected = false;
    }*/
