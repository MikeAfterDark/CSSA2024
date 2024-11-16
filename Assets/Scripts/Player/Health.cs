using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 100;
    private int hp;
    // Start is called before the first frame update

    private void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
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
                Damaged?.Invoke(hp);
            }
            else
            {
                Healed?.Invoke(hp);
            }

            if (hp <= 0)
            {
                Died?.Invoke();
            }
        }
    }

    public UnityEvent<int> Healed;
    public UnityEvent<int> Damaged;
    public UnityEvent Died;


    public void Damage(int amount)
    {
        hp-=amount;
    }

    public void Heal(int amount)
    {
        hp+=amount;
    }

    public void Kill()
    {
        hp = 0;
    }
}
