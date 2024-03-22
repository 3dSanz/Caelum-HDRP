using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _anim;
    private Movimiento _mov;
    private Ataque _attack;
    private Salto _jump;
    private Parry _parry;
    private Dash _dash;


    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _mov = GetComponent<Movimiento>();
        _attack = GetComponent<Ataque>();
        _jump = GetComponent<Salto>();
        _parry = GetComponent<Parry>();
        _dash = GetComponent<Dash>();
    }
}
