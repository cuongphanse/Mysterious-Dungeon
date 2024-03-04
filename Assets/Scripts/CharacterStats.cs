using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat strengh;
    public Stat damage;
    public Stat maxHealth;


    [SerializeField] private int currentHealth;
    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = damage.GetValue() + strengh.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        Debug.Log(_damage);
        if(currentHealth < 0 )
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        
    }
}
