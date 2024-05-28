using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.VFX;
using UnityEngine;

public class RangoBoss : MonoBehaviour
{
    public Animator _anim;
    public Boss boss;
    public int melee;
    private float _tiempoEntreGolpes;
    [SerializeField] private bool _golpeando = true;

    float timeAtaque1 = 1.3f;
    float timeSiguienteAtaque1;

    float timeAtaque2 = 2.5f;
    float timeSiguienteAtaque2;

    float timeAtaque3 = 2f;
    float timeSiguienteAtaque3;


    void Awake()
    {
        _anim = GameObject.Find("BOSS 1").GetComponent<Animator>();
        boss = GameObject.Find("BOSS 1").GetComponent<Boss>();
    }

    void OnTriggerEnter(Collider other)
    {
        //StartCoroutine(TimingEntreGolpes());
        if (other.CompareTag("Player") && _golpeando == true)
        {
            melee = Random.Range(0, 3);
            switch (melee)
            {
                case 0:
                    //Golpe1
                    if (Time.time >= timeSiguienteAtaque1)
                    {
                        _anim.SetFloat("skills",0);
                        SFXEnemyManager.instance.PlaySound(SFXEnemyManager.instance.attack1Boss);
                        boss.hit_Select = 0;
                        timeSiguienteAtaque1 = Time.time + timeAtaque1;
                    }
                    
                    break;

                case 1:
                    //Golpe2
                    if (Time.time >= timeSiguienteAtaque2)
                    {
                        _anim.SetFloat("skills", 0.5f);
                        SFXEnemyManager.instance.PlaySound(SFXEnemyManager.instance.attack2Boss);
                        boss.hit_Select = 0;
                        timeSiguienteAtaque2 = Time.time + timeAtaque2;
                    }
                    break;

                case 2:
                    //Golpe3
                    if(boss.fase == 2)
                    {
                        if (Time.time >= timeSiguienteAtaque3)
                        {
                            _anim.SetFloat("skills", 1f);
                            SFXEnemyManager.instance.PlaySound(SFXEnemyManager.instance.attack3Boss);
                            boss.hit_Select = 0;
                            timeSiguienteAtaque3 = Time.time + timeAtaque3;
                        }
                    }
                    break;
                    
            }
            _anim.SetBool("walk", false);
            _anim.SetBool("run", false);
            _anim.SetBool("attack", true);
            boss.atacando = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    IEnumerator TimingEntreGolpes()
    {
        _golpeando = false;
        _tiempoEntreGolpes = Random.Range(1.5f, 3f);
        yield return new WaitForSeconds(_tiempoEntreGolpes);
        _golpeando = true;
    }


}
