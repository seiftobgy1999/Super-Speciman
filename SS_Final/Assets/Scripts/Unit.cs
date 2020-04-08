using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    // wouldn't usually be defined here
    public List<string> PlayerAbilities = new List<string>() { ProjectStrings.Attack, ProjectStrings.Attack2,
                                                               ProjectStrings.Attack3, ProjectStrings.Heal,
                                                               ProjectStrings.AttackEnergy, ProjectStrings.Shield };

    public List<string> PlayerPowerups = new List<string>() { ProjectStrings.Energize };

    public string unitName;
	public int unitLevel;

	public int damage; // for enemies 

	public int HC;
	public int currentHP;

    public int EC;
    public int currentEP;

    public bool isShieldOn = false;

    void Start()
    {
        // query for player ablities .. then save in playerAbilities list
        // grab all specimen data
    }


    public bool TakeDamage(int dmg)
	{
        if (isShieldOn)
        {
            isShieldOn = false;
        }
        else
        {
            currentHP -= dmg;
        }

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

    public void FillHealth()
    {
        currentHP = HC;
    }

    public void FillEnergy()
    {
        currentEP = EC;
    }

    public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > HC)
			currentHP = HC;
	}

    public void IncrementEnergy(int amount)
    {
        currentEP += amount;
        if (currentEP > EC)
            currentEP = EC;
    }

    public void DecrementEnergy(int amount)
    {
        currentEP -= amount;
        if (currentEP < 0)
            currentEP = 0;
    }

    public void TurnShieldOn()
    {
        isShieldOn = true;
    }

    

}
