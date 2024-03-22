using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    Animator _anim;
    private CharacterController _controller;
    private Salto _jump;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private float downwardAttackForce = 15;
    [SerializeField] private Transform _attackForward, _attackUp, _attackDown;
    [SerializeField] private bool _lookUp;
    [SerializeField] private bool _lookDown;

    //Retroceso Ataque
    private float mass = 3.0f; // Define the character mass
    private Vector3 impact = Vector3.zero;
    private Transform characterTransform; // Reference to the character's
    [SerializeField] private float slideForce = 8.0f;
    [SerializeField] private float slideForceAir = 15.0f;
    [SerializeField] private float movementSpeed = 5.0f;
    public bool _cantMove = false;
    [SerializeField] private float _attackCooldown = 0.3f;

    //Ataque
    //Dano ataque normal
    [SerializeField] private float _damage = 1f;


    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _jump = GetComponent<Salto>();
        characterTransform = transform;
    }

    void Update()
    {
        //Ataque
        if (Input.GetButtonDown("Fire1") && _lookDown == false && _lookUp == false)
        {
            PerformAttack(_damage);
            _anim.SetBool("isAttacking",true);
            Debug.Log("Ataque Normal");
        }else{
            _anim.SetBool("isAttacking",false);
        }

        //Ataque hacia arriba
        if (Input.GetKeyDown(KeyCode.W))
        {
            _lookUp = true;
        }else if(Input.GetKeyUp(KeyCode.W))
        {
            _lookUp = false;
        }

        if (_lookUp == true && Input.GetButtonDown("Fire1"))
        {
            PerformUpAttack(_damage);
            //_anim.SetBool("upAttack", true);
            Debug.Log("Ataque hacia arriba");
        }else
        {
            //_anim.SetBool("upAttack", false);
        }

        //Ataque hacia abajo aire
        if (Input.GetKeyDown(KeyCode.S))
        {
            _lookDown = true;
        }else if(Input.GetKeyUp(KeyCode.S))
        {
           _lookDown = false;
        }

        if (_jump._isGrounded == false && _lookDown == true && Input.GetButtonDown("Fire1"))
        {
            DownAttack(_damage);
            //_anim.SetBool("downAttack", true);
            Debug.Log("Ataque Hacia Abajo en salto");
        }else
        {
            //_anim.SetBool("downAttack", false);
        }

        //Retroceso
        if (impact.magnitude > 0.2f)
        {
            _controller.Move(impact * Time.deltaTime * movementSpeed);
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }
    }

    public void PerformAttack(float _inputDamage)
    {

       Collider[] enemies = Physics.OverlapSphere(_attackForward.position, attackRadius, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_inputDamage);
        }

        if (enemies.Length > 0)
        {
            StartCoroutine(AttackCooldown());
            Vector3 slideDirection = -transform.right;
            AddImpact(slideDirection, slideForce);
            Debug.Log("Desplazado");
        }

        /*Collider[] projectiles = Physics.OverlapSphere(transform.position, attackRadius, bulletLayer);
        foreach (Collider projectile in projectiles)
        {
            projectile = GetComponent<Projectile>();
            ReturnShoot();
        }*/
    }

    void PerformUpAttack(float _inputDamage)
    {

       Collider[] enemies = Physics.OverlapSphere(_attackUp.position, attackRadius, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_inputDamage);
        }

        /*Collider[] projectiles = Physics.OverlapSphere(transform.position, attackRadius, bulletLayer);
        foreach (Collider projectile in projectiles)
        {
            projectile = GetComponent<Projectile>();
            ReturnShoot();
        }*/
    }

    void DownAttack(float _inputDamage)
    {
        //animator.SetTrigger("DownwardAttack"); // Activa la animacion de el ataque hacia abajo
        Collider[] enemies = Physics.OverlapSphere(_attackDown.position, attackRadius, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_inputDamage);
        }
        if (enemies.Length > 0)
        {
            StartCoroutine(AttackCooldown());
            Vector3 slideDirection = -transform.up;
            AddImpact(slideDirection, slideForceAir);
            Debug.Log("Desplazado");
        }

    }

    IEnumerator AttackCooldown()
    {
        _cantMove = true;
        yield return new WaitForSeconds(_attackCooldown);
        _cantMove = false;
    }

    void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0)
        {
            dir.y = -dir.y;
        }

        Vector3 adjustedDir = characterTransform.TransformDirection(dir);

        impact += adjustedDir.normalized * force / mass;
    }

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
