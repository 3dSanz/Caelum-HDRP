using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salto : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _anim;
    private Ataque _attack;
    [SerializeField] private float _alturaSalto = 1;
    private float _gravedad = -35f;
    private Vector3 _jugadorGravedad;
    [SerializeField] private Transform _posicionSensor;
    [SerializeField] private float _radioSensor = 0.2f;
    [SerializeField] private LayerMask _layerSuelo;
    public bool _isGrounded;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _attack = GetComponent<Ataque>();
    }

    void Update()
    {
        if(_attack._cantMove == false)
        {
            Jump();
        }
        _anim.SetBool("isJumping",!_isGrounded);
    }
    
    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_posicionSensor.position, _radioSensor, _layerSuelo);

        if(_isGrounded && _jugadorGravedad.y < 0)
        {
            _jugadorGravedad.y = -2;
        }

        if(_isGrounded && Input.GetButtonDown("Jump"))
        {
            _jugadorGravedad.y = Mathf.Sqrt(_alturaSalto * -2 * _gravedad);
            _anim.SetBool("isJumping",true);
        }
        _jugadorGravedad.y += _gravedad * Time.deltaTime;
        _controller.Move(_jugadorGravedad * Time.deltaTime);
    }

    //Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_posicionSensor.position, _radioSensor);
    }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salto : MonoBehaviour
{
    private Rigidbody _rigidbody; // Cambio de CharacterController a Rigidbody
    private Animator _anim;
    private Ataque _attack;
    [SerializeField] private float _alturaSalto = 1;
    private float _gravedad = -35f;
    private Vector3 _jugadorGravedad;
    [SerializeField] private Transform _posicionSensor;
    [SerializeField] private float _radioSensor = 0.2f;
    [SerializeField] private LayerMask _layerSuelo;
    public bool _isGrounded;

    void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _attack = GetComponent<Ataque>();
        _rigidbody = GetComponent<Rigidbody>(); // Cambio de CharacterController a Rigidbody
    }

    void Update()
    {
        if (_attack._cantMove == false)
        {
            Jump();
        }
        _anim.SetBool("isJumping", !_isGrounded);
    }

    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_posicionSensor.position, _radioSensor, _layerSuelo);

        if (_isGrounded && _jugadorGravedad.y < 0)
        {
            _jugadorGravedad.y = -2;
        }

        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            _jugadorGravedad.y = Mathf.Sqrt(_alturaSalto * -2 * _gravedad);
            _anim.SetBool("isJumping", true);
        }
        _jugadorGravedad.y += _gravedad * Time.deltaTime;

        // Aplicamos la gravedad al Rigidbody
        _rigidbody.velocity = _jugadorGravedad;
    }

    // Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_posicionSensor.position, _radioSensor);
    }
}
*/