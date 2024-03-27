using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Animator _anim;
    public float _currentHealth = 4f;

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float amount)
    {
        _anim.SetTrigger("Hit");
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            _anim.SetTrigger("IsDeath");
            Invoke(nameof(Die), 1.5f);
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
