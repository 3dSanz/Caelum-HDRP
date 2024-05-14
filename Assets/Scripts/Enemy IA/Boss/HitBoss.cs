using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoss : MonoBehaviour
{
    public float damage;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRadius;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            //EnemyCollisionAttack();
            Debug.Log("Jugador Golpeado!");
        }
    }

    /*public void EnemyCollisionAttack()
    {
       Collider[] players = Physics.OverlapSphere(transform.position, attackRadius, playerLayer);
        foreach (Collider player in players)
        {
            player.GetComponent<Health>().TakeDamage(damage);
        }
    }*/

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
