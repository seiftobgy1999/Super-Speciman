using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

    public Button AttackButton;
    public Button Attack2Button;
    public Button Attack3Button;
    public Button HealButton;
    public Button AttackEnergyButton;
    public Button ShieldButton;
    public Button EnergizeButton;

    Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

    Animator animator;

    public BattleState state;

    private float nextActionTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += 1;

            playerUnit.IncrementEnergy(1);
            playerHUD.SetEP(playerUnit.currentEP);

            enemyUnit.IncrementEnergy(1);
            enemyHUD.SetEP(enemyUnit.currentEP);
        }
    }

    IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();
        //animator = GetComponent<Animator>();
        //animator.SetTrigger("attacking");
        playerUnit.unitName = Player.name;
        playerUnit.HC = Player.maxHealth;
        playerUnit.EC = Player.maxEnergy;
        playerUnit.unitLevel = Player.level;
        playerUnit.currentHP = Player.maxHealth;
        playerUnit.currentEP = Player.maxEnergy;
        playerUnit.PlayerAbilities = Player.Abilities;
        playerUnit.PlayerPowerups = Player.Powerups;

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();
        enemyUnit.unitName = "monster" + Player.level;
        enemyUnit.HC = Player.maxHealth;
        enemyUnit.EC = Player.maxEnergy;
        enemyUnit.unitLevel = Player.level;
        enemyUnit.currentHP = Player.maxHealth;
        enemyUnit.currentEP = Player.maxEnergy;


		dialogueText.text = "Battle Opponent: " + enemyUnit.unitName + ".";


		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);
        dialogueText.text = "Battle Begins in .. ";
        yield return new WaitForSeconds(1f);
        dialogueText.text = "3";
        yield return new WaitForSeconds(1f);
        dialogueText.text = "2";
        yield return new WaitForSeconds(1f);
        dialogueText.text = "1";
        yield return new WaitForSeconds(1f);

        InitializeButtons();

        state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

    private void InitializeButtons()
    {
        if (playerUnit.PlayerAbilities.Contains(ProjectStrings.Attack))
        {
            AttackButton = GameObject.Find("AttackButton").GetComponent<Button>();
            AttackButton.interactable = true;
        }

        if (playerUnit.PlayerAbilities.Contains(ProjectStrings.Attack2))
        {
            Attack2Button = GameObject.Find("Attack2Button").GetComponent<Button>();
            Attack2Button.interactable = true;
        }

        if (playerUnit.PlayerAbilities.Contains(ProjectStrings.Attack3))
        {
            Attack3Button = GameObject.Find("Attack3Button").GetComponent<Button>();
            Attack3Button.interactable = true;
        }

        if (playerUnit.PlayerAbilities.Contains(ProjectStrings.Heal))
        {
            HealButton = GameObject.Find("HealButton").GetComponent<Button>();
            HealButton.interactable = true;
        }

        if (playerUnit.PlayerAbilities.Contains(ProjectStrings.AttackEnergy))
        {
            AttackEnergyButton = GameObject.Find("AttackEnergyButton").GetComponent<Button>();
            AttackEnergyButton.interactable = true;
        }

        if (playerUnit.PlayerAbilities.Contains(ProjectStrings.Shield))
        {
            ShieldButton = GameObject.Find("ShieldButton").GetComponent<Button>();
            ShieldButton.interactable = true;
        }

        if (playerUnit.PlayerPowerups.Contains(ProjectStrings.Energize))
        {
            EnergizeButton = GameObject.Find("EnergizeButton").GetComponent<Button>();
            EnergizeButton.interactable = true;
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Select an ability or powerup.";
    }

    IEnumerator EnemyTurn()
    {
        enemyUnit.damage = 5* enemyUnit.unitLevel;
        int EnergyCost = enemyUnit.damage;

        if (enemyUnit.currentEP < EnergyCost)
        {
            // waits for enough energy
            yield return new WaitForSeconds((float)(EnergyCost - enemyUnit.currentEP));
        }
        bool sheildOn = playerUnit.isShieldOn;

        enemyUnit.DecrementEnergy(EnergyCost);
        enemyHUD.SetEP(playerUnit.currentEP);
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        if (sheildOn)
        {
            dialogueText.text = "Enemy attacks for " + enemyUnit.damage + " damage.";
            yield return new WaitForSeconds(1f);
            dialogueText.text = "You were sheilded from " + enemyUnit.damage + " damage.";
        }
        else
        {
            dialogueText.text = "You have recieved " + enemyUnit.damage + " damage.";
        }

        

        yield return new WaitForSeconds(1f);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }


    IEnumerator PlayerAttack()
	{
        int Damage = 5;
        int EnergyCost = 10;

        if (playerUnit.currentEP >= EnergyCost)
        {
            playerUnit.DecrementEnergy(EnergyCost);
            playerHUD.SetEP(playerUnit.currentEP);

            bool isDead = enemyUnit.TakeDamage(Damage);


            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "The enemy has recieved " + Damage + " damage.";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            yield return new WaitForSeconds(2f);
            PlayerTurn();
        }
	}

    IEnumerator PlayerAttack2()
    {
        int Damage = 10;
        int EnergyCost = 20;

        if (playerUnit.currentEP >= EnergyCost)
        {
            playerUnit.DecrementEnergy(EnergyCost);
            playerHUD.SetEP(playerUnit.currentEP);

            bool isDead = enemyUnit.TakeDamage(Damage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "The enemy has recieved " + Damage + " damage.";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            yield return new WaitForSeconds(2f);
            PlayerTurn();
        }
    }

    IEnumerator PlayerAttack3()
    {
        int Damage = 15;
        int EnergyCost = 30;

        if (playerUnit.currentEP >= EnergyCost)
        {
            playerUnit.DecrementEnergy(EnergyCost);
            playerHUD.SetEP(playerUnit.currentEP);

            bool isDead = enemyUnit.TakeDamage(Damage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "The enemy has recieved " + Damage + " damage.";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            yield return new WaitForSeconds(2f);
            PlayerTurn();
        }
    }

    IEnumerator PlayerAttackEnergy()
    {
        int Damage = 10; // energy
        int EnergyCost = 5;

        if (playerUnit.currentEP >= EnergyCost)
        {
            playerUnit.DecrementEnergy(EnergyCost);
            playerHUD.SetEP(playerUnit.currentEP);

            enemyUnit.DecrementEnergy(Damage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "The enemy has lost " + Damage + " energy points.";

            yield return new WaitForSeconds(2f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            yield return new WaitForSeconds(2f);
            PlayerTurn();
        }
    }

    IEnumerator PlayerHeal()
    {
        int EnergyCost = 5;

        if (playerUnit.currentEP >= EnergyCost)
        {
            playerUnit.DecrementEnergy(EnergyCost);
            playerHUD.SetEP(playerUnit.currentEP);

            playerUnit.FillHealth();
            playerHUD.SetHP(playerUnit.currentHP);
            dialogueText.text = "Full Health!";


            yield return new WaitForSeconds(2f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            yield return new WaitForSeconds(2f);
            PlayerTurn();
        }


    }

    IEnumerator PlayerShield()
    {
        int EnergyCost = 10;

        if (playerUnit.currentEP >= EnergyCost)
        {
            playerUnit.DecrementEnergy(EnergyCost);
            playerHUD.SetEP(playerUnit.currentEP);

            playerUnit.TurnShieldOn();
            playerHUD.SetHP(playerUnit.currentHP);
            dialogueText.text = "You will be shielded from the next damage delt!";


            yield return new WaitForSeconds(2f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            dialogueText.text = "Not enough energy!";
            yield return new WaitForSeconds(2f);
            PlayerTurn();
        }


    }

    IEnumerator PlayerEnergize()
    {
        playerUnit.FillEnergy();

        playerHUD.SetEP(playerUnit.currentEP);
        dialogueText.text = "Full Energy!";

        yield return new WaitForSeconds(2f);

        EnergizeButton.interactable = false;

        Player.Powerups.Remove(ProjectStrings.Energize);
        Player.Save();

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

        
    }


    public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

    public void OnAttack2Button()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack2());
    }

    public void OnAttack3Button()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack3());
    }

    public void OnEnergyAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttackEnergy());
    }

    public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}

    public void OnShieldButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerShield());
    }

    public void OnEnergizeButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerEnergize());
    }


    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            StartCoroutine(WinDialog());
        }
        else if (state == BattleState.LOST)
        {
            StartCoroutine(DefeatDialog());
        }


        // change scene
        //SceneManager.LoadScene("Home Scene");
        StartCoroutine(ChangeToScene("Home Scene"));
    }

    private int RewardXP()
    {
        int rewardXP = 45;

        if (nextActionTime < 100)
        {
            rewardXP += (int)nextActionTime;
        }

        Player.XP += rewardXP;
        // save player
        Player.Save();
        return rewardXP;
    }

    private int RewardCoins()
    {
        int rewardCoins = Player.level * 50;
        Player.gold += rewardCoins;

        // save player
        Player.Save();
        return rewardCoins;
    }

    IEnumerator WinDialog()
    {
        int rewardCoins = RewardCoins();
        int rewardXP = RewardXP();

        dialogueText.text = "You won the battle! You are rewarded: " + rewardCoins + " Gold and " + rewardXP + " XP.";

        yield return new WaitForSeconds(5f);
    }

    IEnumerator DefeatDialog()
    {
        Player.XP += 10;
        // save player
        Player.Save();

        dialogueText.text = "You were defeated. You only recieve 10 XP.";

        yield return new WaitForSeconds(5f);
    }

    public IEnumerator ChangeToScene(string sceneToChangeTo)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
