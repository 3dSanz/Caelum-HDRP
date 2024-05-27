using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    /*private Rigidbody _rigidbody;
    private Animator _anim;
    private Salto _jump;
    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashDuration = 0.5f;
    private float dashTimer;
    [SerializeField] private float dashCooldown;
    [SerializeField] private bool canDash;
    [SerializeField] private bool dashed;
    public LayerMask obstacleLayer;
    private Vector3 dashStartPosition;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _jump = GetComponent<Salto>();
        canDash = true;
    }

    void Update()
    {
        StartDash();
    }

    void StartDash()
    {
        if(Input.GetKeyDown(KeyCode.Z) && canDash && !dashed)
        {
            dashStartPosition = transform.position;
            StartCoroutine(DoDash());
            dashed = true;
            Debug.Log("StartDash OK");
        }

        if(_jump._isGrounded)
        {
            dashed = false;
        }
    }

    IEnumerator DoDash()
    {
        Debug.Log("DoDash OK");
        canDash = false;
        _anim.SetTrigger("Dashing");
        _rigidbody.useGravity = false;
         if (dashTimer < dashDuration)
        {
            float dashProgress = dashTimer / dashDuration;
            Vector3 dashTargetPosition = dashStartPosition + transform.forward * dashSpeed;
            RaycastHit hit;
            if (Physics.Raycast(dashStartPosition, transform.forward, out hit, dashSpeed, obstacleLayer))
            {
                    Vector3 resta = new Vector3(1,0,0);
                    dashTargetPosition = hit.point - resta;
            }
            _rigidbody.MovePosition(Vector3.Lerp(dashStartPosition, dashTargetPosition, 5));
            dashTimer += Time.deltaTime;
            yield return new WaitForSeconds(dashDuration);
            _rigidbody.useGravity = true;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
            Debug.Log("DoDash End");
        }
    }*/
    private ParticleSystem _pSystem;
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public LayerMask obstacleLayer;
    private Rigidbody rb;
    private Animator _anim;
    private Health _hp;
    private Salto _jump;
    private Vector3 dashStartPosition;
    private Vector3 finalDashDirection;
    private float dashTimer;
    private bool _directionPressed;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool airDashPerformed = false;
    private float _horizontal;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _hp = GetComponentInChildren<Health>();
        _jump = GetComponent<Salto>();
        _pSystem = GameObject.Find("ParticulaDash").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(_hp._isAlive == true)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Fire2") && !isDashing && _horizontal != 0 && airDashPerformed == false)
            {
                airDashPerformed = true;
                dashStartPosition = transform.position;

                dashTimer = 0f;

                isDashing = true;
                _anim.SetTrigger("isDash");
                SFXManager.instance.StopSound();
                SFXManager.instance.PlaySound(SFXManager.instance.dashSound);
                _pSystem.Play();
            }

            if(_jump._isGrounded == true)
            {
                airDashPerformed = false;
            }

            if (isDashing)
            {
                rb.useGravity = false;
                float dashProgress = dashTimer / dashDuration;

                //Vector3 dashDirection = transform.forward;
                int _dashDirection = 0;
                if (_horizontal < 0)
                {
                    //finalDashDirection = -dashDirection;
                    _dashDirection = -1;
                    _directionPressed = true;
                }
                else if (_horizontal > 0)
                {
                    //finalDashDirection = dashDirection;
                    _dashDirection = 1;
                    _directionPressed = true;
                }else{
                    _directionPressed = false;
                }

                if(_directionPressed == true)
                {
                    /*Vector3 dashTargetPosition = dashStartPosition + finalDashDirection * dashDistance;

                    RaycastHit hit;
                    if (Physics.Raycast(dashStartPosition, finalDashDirection, out hit, dashDistance, obstacleLayer))
                    {
                        Vector3 resta = new Vector3(1,0,0);
                        dashTargetPosition = hit.point - resta;
                    }*/

                    rb.AddForce(/*Vector3.Lerp(dashStartPosition, dashTargetPosition, dashProgress)*/new Vector3(_dashDirection,0,0) * dashDistance, ForceMode.Impulse);

                    dashTimer += Time.deltaTime;

                    if (dashTimer >= dashDuration)
                    {
                        isDashing = false;
                        rb.useGravity = true;
                    }
                }  
            }
        }
        
    }
}

/*
   private Rigidbody _rigidbody;
    private Animator _anim;
    private Salto _jump;
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public LayerMask obstacleLayer;
    private Vector3 dashStartPosition;
    private float dashTimer;
    [SerializeField] private bool canDash;
    [SerializeField] private bool dashed;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _jump = GetComponent<Salto>();
        canDash = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && canDash && !dashed)
        {
            dashStartPosition = transform.position;
            dashTimer = 0f;
            dashed = true;
            IsDashing();
        }

        if(_jump._isGrounded)
        {
            dashed = false;
        }
    }

    void IsDashing()
    {
        if (dashTimer < dashDuration)
        {
            float dashProgress = dashTimer / dashDuration;

            Vector3 dashTargetPosition = dashStartPosition + transform.forward * dashDistance;

            RaycastHit hit;
            if (Physics.Raycast(dashStartPosition, transform.forward, out hit, dashDistance, obstacleLayer))
            {
                Vector3 resta = new Vector3(1,0,0);
                dashTargetPosition = hit.point - resta;
            }

            _rigidbody.MovePosition(Vector3.Lerp(dashStartPosition, dashTargetPosition, dashProgress));
            dashTimer += Time.deltaTime;
        }
    }
*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody _rigidbody; // Cambiamos a Rigidbody
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashSpeed = 10f;
    private  Movimiento _mov;
    private Vector3 dashStartPos;
    private bool isDashing;
    private bool _lookDash;
    private Animator _anim;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); // Obtenemos el componente Rigidbody
        _anim = GetComponentInChildren<Animator>();
        _mov = GetComponent<Movimiento>();
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            _lookDash = true;
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            _lookDash = false;
        }

        if (Input.GetKeyDown(KeyCode.Z) && _lookDash == true)
        {
            PerformDash();
        }

        if (isDashing)
        {
            DashMovement();
            _anim.SetBool("isDash", true);
        }
        else
        {
            _anim.SetBool("isDash", false);
        }
    }

    void PerformDash()
    {
        if (!isDashing)
        {
            dashStartPos = transform.position;
            isDashing = true;
        }
    }

    void DashMovement()
    {
        Vector3 finalPosition = dashStartPos + transform.forward * dashDistance;
        Vector3 dashMove = transform.forward * dashSpeed * Time.deltaTime;
        dashMove.y = 0;
        if(!_mov.facingRight)
            {
                _rigidbody.MovePosition(_rigidbody.position + dashMove); // Usamos MovePosition para mover el Rigidbody
            } else if(_mov.facingRight)
            {
                 _rigidbody.MovePosition(_rigidbody.position - dashMove); // Usamos MovePosition para mover el Rigidbody
            }
       

        if (Vector3.Distance(transform.position, dashStartPos) >= dashDistance)
        {
            isDashing = false;
        }
    }
}*/
