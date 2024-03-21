using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    Ataque _attack;
    Animator _anim;

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
    }

    void Update()
    {
        //Parry
        if (Input.GetButtonDown("Fire2") && canParry)
        {
            DoParry();
        }else{
            _anim.SetBool("isParry", false);
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
        _anim.SetBool("isParry", true);
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
            _anim.SetTrigger("Parry_Attack");
            Debug.Log("Parry acertado!");
        }else
        {
            _anim.SetBool("isParry", false);
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
