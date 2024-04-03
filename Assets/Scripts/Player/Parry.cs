using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    Ataque _attack;
    Animator _anim;
    private Health _hp;

    [SerializeField] private Transform _parcy;
    
    //Parry
    [SerializeField] private float parryCooldown = 1f;
    [SerializeField] private float meleeRange = 2f;
    [SerializeField] private LayerMask meleeLayer;
    [SerializeField] private bool canParry = true;
    [SerializeField] private float _damageParry = 3f;

    //Parry Disparo
    //public LayerMask bulletLayer;

    void Awake()
    {
        _attack = GetComponent<Ataque>();
        _anim = GetComponentInChildren<Animator>();
        _hp = GetComponentInChildren<Health>();
    }

    void Update()
    {
        if(_hp._isAlive == true)
        {
            //Parry
            if (Input.GetButtonDown("Fire2") && canParry)
            {
                DoParry();
            }
        }
    }

    IEnumerator ParryCooldown()
    {
        canParry = false;
        yield return new WaitForSeconds(parryCooldown);
        canParry = true;
    }

    void DoParry()
    {
        _anim.SetTrigger("isParry");
        Debug.Log("Parrendo");
        StartCoroutine(ParryCooldown());

        Collider[] hitColliders = Physics.OverlapSphere(_parcy.position, meleeRange, meleeLayer);
        if (hitColliders.Length > 0)
        {
            PerformMeleeParry(hitColliders[0]);
        }
    }

    void PerformMeleeParry(Collider target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null) //enemy._isAttacking == true
        {  
            _attack.PerformAttack(_damageParry);
            _anim.SetTrigger("Perform_Parry");
            Debug.Log("Parry acertado!");
        }else
        {
            Debug.Log("Parry fallado!");
        }
    }

    //Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_parcy.position, meleeRange);
    }
}
