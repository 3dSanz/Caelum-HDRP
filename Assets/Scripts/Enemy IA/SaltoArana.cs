using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SaltoArana : MonoBehaviour
{
    Enemy _enemy;
    NavMeshAgent _agent;
    //private Animator _anim;
    //private Rigidbody _rigidbody;

    //Salto
    public bool _isGrounded;
    Collider[] _groundCollisions;
    [SerializeField] private float _radioSensor = 0.2f;
    [SerializeField] private LayerMask _layerSuelo;
    [SerializeField] private Transform _posicionSensor;
    [SerializeField] private float _alturaSalto = 1;
    //private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        //_anim = GetComponentInChildren<Animator>();
        //_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump();
        //_anim.SetBool("AranaJumping", !_isGrounded);

        /*if(_agent.destination.x>0 && !facingRight)
                {
                    Flip();
                } else if(_agent.destination.x<0 && facingRight)
                {
                    Flip();
                }*/
    }

    /*void Jump()
    {

        if(_isGrounded)
        {
            _isGrounded = false;
            //_anim.SetBool("AranaGrounded",_isGrounded);
            _rigidbody.AddForce(Vector3.up * _alturaSalto, ForceMode.Impulse);
        }

        _groundCollisions = Physics.OverlapSphere(_posicionSensor.position, _radioSensor, _layerSuelo);
        
        if(_groundCollisions.Length>0)
        {
            _isGrounded = true;
        }else 
        {
            _isGrounded = false;
        }

        //_anim.SetBool("AranaGrounded",_isGrounded);
    }*/

    /*void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }*/
}

