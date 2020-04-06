using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string name;
    public int maxHealth;
    public int currentHealth;
    public int maxEnergy;
    public int currentEnergy;
    public string[] Abilities;
    public string[] Powerups;
    public int level;
    public int XP;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        name = Login.tempUser;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        print(name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
        
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
