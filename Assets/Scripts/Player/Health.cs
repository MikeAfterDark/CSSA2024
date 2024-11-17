using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 100;
    private int hp;
    private float damageInterval = 0.5f;
    private float damageTimeout;

    public void Start()
    {
        damageTimeout = damageInterval;
        hp = 100;
    }  

    public void Update()
    {
        if (damageTimeout > 0)
            damageTimeout -= Time.deltaTime;
    }  

    public int Hp
    {
        get => hp;
        private set
        {
            var isDamage = value < hp;
            hp = Mathf.Clamp(value, 0, maxHp);
            if (isDamage)
            {
                if (hp <= 0)
                {
                    GameManager.Instance.NewPlayerEvent(GameManager.PlayerEvent.Died);
                    hp = maxHp;
                }
                GameManager.Instance.NewPlayerEvent(GameManager.PlayerEvent.Damaged);
            }
            else
            {
                GameManager.Instance.NewPlayerEvent(GameManager.PlayerEvent.Healed);
            }
        }
    }

    public void Damage(int amount)
    {
        if (damageTimeout <= 0)
        {
            damageTimeout = damageInterval;
            Hp-=amount;
        } 
    }

    public void Heal(int amount)
    {
        Hp+=amount;
    }

    public void Kill()
    {
        Hp = 0;
    }
}
