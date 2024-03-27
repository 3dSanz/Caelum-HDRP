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
    private Vector3 target;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.Find("Parcy Low").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.x);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInParent<Health>().TakeDamage(damage);
        }

        if (other.gameObject.tag == "MeleeAttack")
        {
            target = -target;
        }

        if (other.gameObject.layer == 6)
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
