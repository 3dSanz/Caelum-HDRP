using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangoBoss : MonoBehaviour
{
    public Animator _anim;
    public Boss boss;
    public int melee;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            melee = Random.Range(0, 2);
            switch(melee)
            {
                case 0:
                    //Golpe1
                    _anim.SetFloat("skills",0);
                    boss.hit_Select = 0;
                    break;

                case 1:
                    //Golpe2
                    _anim.SetFloat("skills", 0.5f);
                    boss.hit_Select = 1;
                    break;
            }
            _anim.SetBool("walk", false);
            _anim.SetBool("run", false);
            _anim.SetBool("attack", true);
            boss.atacando = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
