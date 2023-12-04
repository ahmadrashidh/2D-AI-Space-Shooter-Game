using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public int MaxHealth = 100;

    [SerializeField]
    private int health;

    public UnityEvent onDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent onHit, onHeal;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            OnHealthChange?.Invoke((float)Health / MaxHealth);
        }
    }

    private void Start()
    {

        health = MaxHealth;
    }

    internal void Hit(int damagePoints)
    {
        Health -= damagePoints;
        if(Health <= 0)
        {
            onDead?.Invoke();
        } else
        {
            onHit?.Invoke();
        }
    }

    public void Heal(int healthBoost)
    {
        Health += healthBoost;
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        onHeal?.Invoke();
    }

}
