/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashSpeed = 10f;
    private Vector3 dashStartPos;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool _lookDash;
    Animator _anim;


    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            _lookDash = true;
        }else if(Input.GetButtonUp("Horizontal"))
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
            _anim.SetBool("isDash",true);     
        }else
        {
            _anim.SetBool("isDash",false);
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
        _controller.Move(dashMove);

        if (Vector3.Distance(transform.position, dashStartPos) >= dashDistance)
        {
            isDashing = false;
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody _rigidbody; // Cambiamos a Rigidbody
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashSpeed = 10f;
    private Vector3 dashStartPos;
    private bool isDashing;
    private bool _lookDash;
    private Animator _anim;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); // Obtenemos el componente Rigidbody
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
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
        _rigidbody.MovePosition(_rigidbody.position + dashMove); // Usamos MovePosition para mover el Rigidbody

        if (Vector3.Distance(transform.position, dashStartPos) >= dashDistance)
        {
            isDashing = false;
        }
    }
}
