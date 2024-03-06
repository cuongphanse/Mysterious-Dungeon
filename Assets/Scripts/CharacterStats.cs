using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strengh; // 1 point increate damage
    public Stat agility; //1 point increate evasion
    public Stat intelligence; //1 point increate magic
    public Stat vitality; //1 point increate health;s

    [Header("Major stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;


    public Stat damage;


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
