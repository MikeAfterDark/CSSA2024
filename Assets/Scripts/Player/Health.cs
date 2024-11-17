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

    public void Start()
    {
        hp = 100;
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
                GameManager.Instance.NewPlayerEvent(GameManager.PlayerEvent.Damaged);
            }
            else
            {
                GameManager.Instance.NewPlayerEvent(GameManager.PlayerEvent.Healed);
            }

            if (hp <= 0)
            {
                GameManager.Instance.NewPlayerEvent(GameManager.PlayerEvent.Died);
            }
        }
    }

    public void Damage(int amount)
    {
        Hp-=amount;
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
