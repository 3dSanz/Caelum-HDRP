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
    EntradaBoss _entry;
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
    public GameObject[] posicionSalto;
    int randomJump;
    [SerializeField] private float _timeSiguienteSalto;
    [SerializeField] private float _timeSalto = 10f;

    //Fase1
    public int fase = 1;
    public float hp_min;
    public float hp_max;
    public Image barra;
    public AudioSource _sfxboss;
    public bool muerto;

    WalkSound walkBossSound;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        target = GameObject.Find("Parcy");
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        walkBossSound = GameObject.Find("WalkBossSound").GetComponent<WalkSound>();
    }

    public void Comportamiento_Boss()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < 15)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            //point.transform.LookAt(target.transform.position);
            //_sfxboss.enabled = true;

            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando)
            {
                switch (rutina)
                {
                    case 0:
                        //WALK
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        _anim.SetBool("walk", true);
                        if(_anim.GetBool("walk") && !walkBossSound.IsPlaying("caminar"))
                        {
                                walkBossSound.PlaySound("caminar");
                        }else
                            {
                                walkBossSound.StopSound("caminar");
                            }
                            
                        _anim.SetBool("run", false);
                        if (transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        }

                        _anim.SetBool("attack", false);
                        cronometro += 1 * Time.deltaTime;
                        if (cronometro > time_rutina)
                        {
                            rutina = Random.Range(0, 2);
                            cronometro = 0;
                        }
                        break;

                    case 1:
                        //JUMPATTACK
                        if (fase == 2)
                        {
                            //jump_distance += 1 * Time.deltaTime;
                            /*_anim.SetBool("walk", false);
                            _anim.SetBool("run", false);
                            _anim.SetBool("attack", false);
                            //_anim.SetBool("attack", true);
                            //_anim.SetFloat("skills", 1);
                            _anim.SetTrigger("jump");*/
                            //hit_Select = 1;
                            /*rango.GetComponent<CapsuleCollider>().enabled = false;
                            Direction_Attack_Start();

                            if (direction_Skill)
                            {
                                if (jump_distance < 1f)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                                }
                                //_anim.SetTrigger("jump");
                                transform.Translate(Vector3.forward * 8 * Time.deltaTime);
                            }*/
                            if (Time.time >= _timeSiguienteSalto)
                            {
                             _anim.SetBool("walk", false);
                             _anim.SetBool("run", false);
                             _anim.SetBool("attack", false);
                             _anim.SetTrigger("jump");
                             _timeSiguienteSalto = Time.time + _timeSalto;
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

    public void JumpPosition()
    {
        randomJump = Random.Range(0, 2);
        transform.position = posicionSalto[randomJump].transform.position;
    }

    //Melee
    public void ColliderWeaponTrue()
    {
        hit[0].GetComponent<SphereCollider>().enabled = true;
        hit[1].GetComponent<SphereCollider>().enabled = true;

        //hit[0].GetComponent<HitBoss>().enabled = true;
        //hit[1].GetComponent<HitBoss>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        hit[0].GetComponent<SphereCollider>().enabled = false;
        hit[1].GetComponent<SphereCollider>().enabled = false;

        //hit[0].GetComponent<HitBoss>().enabled = false;
        //hit[1].GetComponent<HitBoss>().enabled = false;
    }

    public void Vivo()
    {
        if(_enemy._currentHealth < 15)
        {
            fase = 2;
            time_rutina = 1;
        }

        Comportamiento_Boss();
    }



    // Update is called once per frame
    void Update()
    {
        

    }
}
