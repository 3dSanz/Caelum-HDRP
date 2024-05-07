using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    Animator _anim;
    //private CharacterController _controller;
    private Rigidbody _rigidbody;
    private Salto _jump;
    private  Movimiento _mov;
    private Health _hp;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRadius = 1.5f;
    //[SerializeField] private float downwardAttackForce = 15;
    [SerializeField] private Transform _attackForward, _attackUp, _attackDown;
    [SerializeField] private bool _lookUp;
    [SerializeField] private bool _lookDown;

    //Retroceso Ataque
    //private float mass = 3.0f; // Define the character mass
    private Vector3 impact = Vector3.zero;
    private Transform characterTransform; // Reference to the character's
    [SerializeField] private float slideForce = 8.0f;
    [SerializeField] private float slideForceAir = 15.0f;
    //[SerializeField] private float movementSpeed = 5.0f;
    public bool _cantMove = false;
    [SerializeField] private float _attackCooldown = 0.3f;

    //Ataque
    //Dano ataque normal
    [SerializeField] private float _damage = 1f;

    private float _horizontal;
    [SerializeField] private float _timeMelee;
    private float _timeSiguienteMelee;

    //GameObjects
    [SerializeField] private GameObject _ataqueArriba;
    [SerializeField] private GameObject _ataqueCentro;
    [SerializeField] private GameObject _ataqueAbajo;
    


    void Awake()
    {
        //_controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _jump = GetComponent<Salto>();
        _mov = GetComponent<Movimiento>();
        _hp = GetComponentInChildren<Health>();
        _rigidbody = GetComponent<Rigidbody>();
        characterTransform = transform;
    }

    void Update()
    {
        if(_hp._isAlive == true)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            //Ataque
            if (Input.GetButtonDown("Fire1") && _lookDown == false && _lookUp == false /*&& Time.time >= _timeSiguienteMelee*/)
            {
                SFXManager.instance.StopSound();
                SFXManager.instance.PlaySound(SFXManager.instance.airHitSound);
                PerformAttack(_damage);
                //_anim.SetBool("isAttacking",true);
                _anim.SetTrigger("isAttack");
                Debug.Log("Ataque Normal");
                _ataqueCentro.SetActive(true);
                //_timeSiguienteMelee = Time.time + _timeMelee;
            }

            //Ataque hacia arriba
            if (Input.GetKeyDown(KeyCode.W))
            {
                _lookUp = true;
            }else if(Input.GetKeyUp(KeyCode.W))
            {
                _lookUp = false;
            }

            if (_lookUp == true && Input.GetButtonDown("Fire1") /*&& Time.time >= _timeSiguienteMelee*/)
            {
                SFXManager.instance.StopSound();
                SFXManager.instance.PlaySound(SFXManager.instance.airHitSound);
                PerformUpAttack(_damage);
                _anim.SetTrigger("upAttack");
                _ataqueArriba.SetActive(true);
                Debug.Log("Ataque hacia arriba"); 
                //_timeSiguienteMelee = Time.time + _timeMelee;
            }

            //Ataque hacia abajo aire
            if (Input.GetKeyDown(KeyCode.S))
            {
                _lookDown = true;
            }else if(Input.GetKeyUp(KeyCode.S))
            {
            _lookDown = false;
            }

            if (_jump._isGrounded == false && _lookDown == true && Input.GetButtonDown("Fire1") /*&& Time.time >= _timeSiguienteMelee*/)
            {
                SFXManager.instance.StopSound();
                SFXManager.instance.PlaySound(SFXManager.instance.airHitSound);
                DownAttack(_damage);
                _anim.SetTrigger("isDownAttack");
                Debug.Log("Ataque Hacia Abajo en salto");
                _ataqueAbajo.SetActive(true);
                //_timeSiguienteMelee = Time.time + _timeMelee;
            }

            //Controla la desactivacion del ataque a melee
            if(Time.time >= _timeSiguienteMelee)
            {
                _ataqueArriba.SetActive(false);
                _ataqueCentro.SetActive(false);
                _ataqueAbajo.SetActive(false);
                _timeSiguienteMelee = Time.time + _timeMelee;
            }

            //Retroceso
            /*if (impact.magnitude > 0.2f)
            {
            _   controller.Move(impact * Time.deltaTime * movementSpeed);
                impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
            }*/
        }
    }

    public void PerformAttack(float dmg)
    {

       Collider[] enemies = Physics.OverlapSphere(_attackForward.position, attackRadius, enemyLayer);
       

        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(dmg);
        }

        if(enemies.Length > 0)
        {
            Vector3 _direccion = new Vector3 (1,0,0);
            if(!_mov.facingRight)
            {
                //StartCoroutine(AttackCooldown());
                //_rigidbody.AddForce(_direccion * slideForce, ForceMode.Impulse);
                _rigidbody.AddForce(new Vector3(0,Mathf.Sqrt(slideForce * -2 * Physics.gravity.x),0), ForceMode.Impulse);
                Debug.Log("Desplazado lateral");

            } else if(_mov.facingRight)
            {
                //StartCoroutine(AttackCooldown());
                //_rigidbody.AddForce(-_direccion * slideForce, ForceMode.Impulse);
                _rigidbody.AddForce(new Vector3(0,Mathf.Sqrt(-slideForce * -2 * Physics.gravity.x),0), ForceMode.Impulse);
                Debug.Log("Desplazado lateral");
            }
        }
            

        /*if (enemies.Length > 0)
        {
            foreach (Collider enemy in enemies)
            {
            enemy.GetComponent<Enemy>().TakeDamage(_inputDamage);
            StartCoroutine(AttackCooldown());
            //Vector3 slideDirection = -transform.right;
            //AddImpact(slideDirection, slideForce);
            Vector3 enemyDirection = enemy.transform.position - transform.position;
            enemyDirection.Normalize();
            _rigidbody.AddForce(-enemyDirection * slideForce, ForceMode.Impulse);
            Debug.Log("Desplazado");
            }
        }*/

        /*Collider[] projectiles = Physics.OverlapSphere(transform.position, attackRadius, bulletLayer);
        foreach (Collider projectile in projectiles)
        {
            projectile = GetComponent<Projectile>();
            ReturnShoot();
        }*/
    }

    void PerformUpAttack(float dmg)
    {
       Collider[] enemies = Physics.OverlapSphere(_attackUp.position, attackRadius, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(dmg);
        }

        /*Collider[] projectiles = Physics.OverlapSphere(transform.position, attackRadius, bulletLayer);
        foreach (Collider projectile in projectiles)
        {
            projectile = GetComponent<Projectile>();
            ReturnShoot();
        }*/
    }

    void DownAttack(float dmg)
    {
        //animator.SetTrigger("DownwardAttack"); // Activa la animacion de el ataque hacia abajo
        Collider[] enemies = Physics.OverlapSphere(_attackDown.position, attackRadius, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(dmg);
        }
        if (enemies.Length > 0)
        {
            //Vector3 slideDirection = -transform.up;
            //AddImpact(slideDirection, slideForceAir);
            //_rigidbody.AddForce(Vector3.up * slideForceAir, ForceMode.Impulse);
            _rigidbody.AddForce(new Vector3(0,Mathf.Sqrt(slideForceAir * -2 * Physics.gravity.y),0), ForceMode.Impulse);
            Debug.Log("Desplazado arriba");
        }

    }

    IEnumerator AttackCooldown()
    {
        _cantMove = true;
        yield return new WaitForSeconds(_attackCooldown);
        _cantMove = false;
    }

    /*void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0)
        {
            dir.y = -dir.y;
        }

        Vector3 adjustedDir = characterTransform.TransformDirection(dir);

        impact += adjustedDir.normalized * force / mass;
    }*/

    //Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackForward.position, attackRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackDown.position, attackRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackUp.position, attackRadius);
    }
}
