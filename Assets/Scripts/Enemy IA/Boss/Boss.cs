using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Boss : MonoBehaviour
{
    NavMeshAgent _agent;
    Transform _playerPosition;
    //Codigo Enemigo Base
    Enemy _enemy;
    public int rutina;
    public float cronometro;
    public float time_rutina;
    public Animator _anim;
    public Quaternion angulo;
    public float grado;
    public GameObject target;
    public bool atacando;
    public RangoBoss rango;
    public float speed;
    public GameObject[] hit;
    public int hit_Select;

    //Jump Attack
    public float jump_distance;
    public bool direction_Skill;

    //Fase1
    public int fase = 1;
    public float hp_min;
    public float hp_max;
    public Image barra;
    public AudioSource _sfxboss;
    public bool muerto;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        target = GameObject.Find("Player");
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Comportamiento_Boss()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < 15)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            //point.transform.LookAt(target.transform.position);
            _sfxboss.enabled = true;

            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando)
            {
                switch (rutina)
                {
                    case 0:
                        //WALK
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        _anim.SetBool("walk", true);
                        _anim.SetBool("run", false);
                        if (transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        }

                        _anim.SetBool("attack", false);
                        cronometro += 1 * Time.deltaTime;
                        if (cronometro > time_rutina)
                        {
                            rutina = Random.Range(0, 1);
                            cronometro = 0;
                        }
                        break;

                    case 1:
                        //JUMPATTACK
                        if (fase == 2)
                        {
                            jump_distance += 1 * Time.deltaTime;
                            _anim.SetBool("walk", false);
                            _anim.SetBool("run", false);
                            _anim.SetBool("attack", true);
                            _anim.SetFloat("skills", 1);
                            hit_Select = 1;
                            rango.GetComponent<CapsuleCollider>().enabled = false;

                            if (direction_Skill)
                            {
                                if (jump_distance < 1f)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                                }

                                transform.Translate(Vector3.forward * 8 * Time.deltaTime);
                            }
                        }
                        else
                        {
                            rutina = 0;
                            cronometro = 0;
                        }
                        break;
                }
            }
        }

        
    }

    public void Final_Anim()
    {
        rutina = 0;
        _anim.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;
        jump_distance = 0;
        direction_Skill = false;
    }

    public void Direction_Attack_Start()
    {
        direction_Skill = true;
    }

    public void Direction_Attack_Final()
    {
        direction_Skill = false;
    }

    //Melee
    public void ColliderWeaponTrue()
    {
        hit[0].GetComponent<SphereCollider>().enabled = true;
        hit[1].GetComponent<SphereCollider>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        hit[0].GetComponent<SphereCollider>().enabled = false;
        hit[1].GetComponent<SphereCollider>().enabled = false;
    }

    public void Vivo()
    {
        if(_enemy._currentHealth < 10)
        {
            fase = 2;
            time_rutina = 1;
        }

        Comportamiento_Boss();
    }



    // Update is called once per frame
    void Update()
    {
        /*barra.fillAmount = hp_min / hp_max;
        if(hp_min > 0)
        {
            Vivo();
        }else
        {
            if(!muerto)
            {
                _anim.SetTrigger("dead");
                _sfxboss.enabled = false;
                muerto = true;
            }
        }*/
        
    }
}
