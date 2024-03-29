/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private CharacterController _controller;
    private Transform _camera;
    private Animator _anim;
    private Ataque _attack;
    [SerializeField] private float _vel = 9;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float _horizontal;
    private float turnSmoothVelocity;
    
    void Awake()
    {
        _camera = Camera.main.transform;
        _anim = GetComponentInChildren<Animator>();
        _controller = GetComponent<CharacterController>();
        _attack = GetComponent<Ataque>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        if(_attack._cantMove == false)
        {
           Movement();
        }
    }

    void Movement()
    {
        Vector3 _direccion = new Vector3 (_horizontal, 0, 0);

        _anim.SetFloat("VelX",0);
        _anim.SetFloat("VelZ", _direccion.magnitude);

        if (_direccion != Vector3.zero)
        {
            float _targetAngle = Mathf.Atan2(_direccion.x, _direccion.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float _smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, _smoothAngle, 0);
            Vector3 _moveDirection = Quaternion.Euler(0, _targetAngle, 0) * Vector3.forward;
            _moveDirection.z = 0;
            _controller.Move(_moveDirection.normalized * _vel * Time.deltaTime);
        }


        /*Vector3 movimiento = -transform.right * _horizontal * _vel * Time.deltaTime;
        _controller.Move(movimiento);
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Rigidbody _rigidbody; // Cambio de CharacterController a Rigidbody
    private Transform _camera;
    private Animator _anim;
    private Ataque _attack;
    [SerializeField] private float _vel = 9;
    //[SerializeField] private float turnSmoothTime = 0.1f;
    private float _horizontal;
    private float turnSmoothVelocity;

    public bool facingRight;

    void Awake()
    {
        _camera = Camera.main.transform;
        _anim = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>(); // Cambio de CharacterController a Rigidbody
        _attack = GetComponent<Ataque>();
        facingRight = true;
    }

    void FixedUpdate()
    {
        
        if (_attack._cantMove == false)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _anim.SetFloat("Speed",Mathf.Abs(_horizontal));
            _rigidbody.velocity = new Vector3(_horizontal * _vel, _rigidbody.velocity.y, 0);

            if(_horizontal>0 && !facingRight)
            {
                Flip();
            } else if(_horizontal<0 && facingRight)
            {
                Flip();
            }
            //Movement();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

    /*void Movement()
    {
        /*Vector3 _direccion = new Vector3(_horizontal, 0, 0);

        _anim.SetFloat("VelX", 0);
        _anim.SetFloat("VelZ", _direccion.magnitude);

        //if (_direccion != Vector3.zero)
        //{
            float _targetAngle = Mathf.Atan2(_direccion.x, _direccion.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float _smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, _smoothAngle, 0);
            //Vector3 _moveDirection = Quaternion.Euler(0, _targetAngle, 0) * Vector3.forward;
            //_moveDirection.z = 0;

            // Aplicamos la velocidad al Rigidbody
            //_rigidbody.velocity = _moveDirection.normalized * _vel;
            _rigidbody.velocity = new Vector3(_horizontal * _vel, _rigidbody.velocity.y, _rigidbody.velocity.z);
        //}


    }*/

        /*void Movimiento()
    {
        Vector3 _direccion = new Vector3 (_horizontal, 0, 0);

        _anim.SetFloat("VelX",0);
        _anim.SetFloat("VelZ", _direccion.magnitude);

        /*if(_direccion != Vector3.zero)
        {
            float _targetAngle = Mathf.Atan2(_direccion.x, _direccion.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float _smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0,_smoothAngle,0);
            Vector3 _moveDirection = Quaternion.Euler(0, _targetAngle, 0) * Vector3.forward;
            _controller.Move(_moveDirection.normalized * _vel * Time.deltaTime);
        }*/

       /* if (_direccion != Vector3.zero)
        {
        // Calcula el angulo objetivo basado en la direccion de entrada
        float _targetAngle = Mathf.Atan2(_direccion.x, _direccion.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;

        // Suaviza el angulo de rotacion
        float _smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        // Aplica la rotacion solo en el eje Y
        transform.rotation = Quaternion.Euler(0, _smoothAngle, 0);

        // Calcula la direccion de movimiento en el plano XZ (sin componente Z)
        Vector3 _moveDirection = Quaternion.Euler(0, _targetAngle, 0) * Vector3.forward;
        _moveDirection.z = 0; // Bloquea el movimiento en el eje Z

        // Normaliza y aplica la velocidad de movimiento
        _controller.Move(_moveDirection.normalized * _vel * Time.deltaTime);
        }


        /*Vector3 movimiento = -transform.right * _horizontal * _vel * Time.deltaTime;

        _controller.Move(movimiento);*/
    //}

}