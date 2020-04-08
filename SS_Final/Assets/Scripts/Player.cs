using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text XP_text;
    public Text gold_text;
    public Text HPValue;
    public Text EnergyValue;
    public HealthBar xpBar;
    public Text levelText;

    public static string name;
    //public static string name = Login.tempUser;
    public static int maxHealth;
    public static int maxEnergy;
    public static int gold;
    public static List<string> Abilities = new List<string>();
    public static List<string> Powerups = new List<string>();
    public static int level;
    public static int XP;
    public static int coolTime;
    // public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        print("Player Loaded");
        name = Login.tempUser;
        string[] tempPowerups;
        string[] tempAbilities;
        string[] Lines;
        string path = "./Assets/Data/" + name + ".txt";
        Lines = System.IO.File.ReadAllLines(path);
        tempAbilities = Lines[6].Split(',');
        tempPowerups = Lines[7].Split(',');
        Abilities.Clear();
        Powerups.Clear();
        maxHealth = Int32.Parse(Lines[3]);
        maxEnergy = Int32.Parse(Lines[4]);
        gold = Int32.Parse(Lines[5]);
        foreach (string s in tempAbilities)
        {
            Abilities.Add(s);
        }
        foreach (string s in tempPowerups)
        {
            Powerups.Add(s);
        }
        level = Int32.Parse(Lines[8]);
        XP = Int32.Parse(Lines[9]);
        coolTime = Int32.Parse(Lines[10]);


        

        //healthBar.SetMaxHealth(maxHealth);
        
        levelUp();
        xpBar.SetHealth(XP);
        gold_text.text = gold.ToString();
        XP_text.text = XP.ToString() + "/100";
        HPValue.text = maxHealth.ToString();
        EnergyValue.text = maxEnergy.ToString();
        levelText.text = level.ToString();

    }
    public void levelUp() {
        while (XP >= 100) {
            level += 1;
            XP = XP - 100;
            gold += 100;
        }
        Save();
    }
    

    // Update is called once per frame
    void Update()
    {
        //Save();

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }*/

    }

    public static void Save()
    {
        
        string Powerupform;
        string path = "./Assets/Data/" + name + ".txt";
        string[] Lines = System.IO.File.ReadAllLines(path);
        string form;
        string Abilityform = Abilities[0];
        for (int i = 1; i < Abilities.Count; i++)
        {
            Abilityform += "," + Abilities[i];
        }


        Powerupform = "";
        for (int j = 0; j < Powerups.Count; j++)
        {
            if (Powerupform == "," || Powerupform == "")
            {
                Powerupform = "" + Powerups[j];
            }
			else
			{
                Powerupform += "," + Powerups[j];
            }
            
        }




        form = (Lines[0] + Environment.NewLine + Lines[1] + Environment.NewLine + Lines[2] + Environment.NewLine + maxHealth + Environment.NewLine + maxEnergy + Environment.NewLine + gold + Environment.NewLine + Abilityform + Environment.NewLine + Powerupform + Environment.NewLine + level + Environment.NewLine + XP);
        form += Environment.NewLine + coolTime;
        System.IO.File.WriteAllText(path, form);


        //Leaderboard
        string leaderboardPath = "./Assets/Data/Leaderboard.txt";
        string[] lines = System.IO.File.ReadAllLines(leaderboardPath);
        for (int i = 0; i < lines.Length; i++) {
            if (name == lines[i].Split(',')[0]) {
                lines[i] = name + "," + (level * 100 + XP).ToString();
            }

        }
        string leaderboardForm = "";
        for (int i = 0; i < lines.Length - 1; i++) {
            leaderboardForm += lines[i] + Environment.NewLine;
        }
        leaderboardForm += lines[lines.Length -1] + Environment.NewLine;

        System.IO.File.WriteAllText(leaderboardPath, leaderboardForm);

    }
    /*void TakeDamage(int damage)
    {
        maxHealth -= damage;
        healthBar.SetHealth(maxHealth);
    }*/
}
