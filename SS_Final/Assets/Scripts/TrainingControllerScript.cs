using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Timers;

public class TrainingControllerScript : MonoBehaviour
{
    // Controller Variables
    private static System.Timers.Timer aTimer;
    private static bool started = false;

    public string[] itemNames;
    public int[] itemValues;
    public int[] itemCosts;
    public int[] itemType; // 0 = Health 1 = Energy

    // Game Items
    public Text moneyAmtText;
    public Button homeButton;

    public Text itemName0;
    public Text itemName1;
    public Text itemName2;
    public Text itemName3;
    public Text itemName4;
    public Text[] shopItemNames;

    public Text itemCost0;
    public Text itemCost1;
    public Text itemCost2;
    public Text itemCost3;
    public Text itemCost4;
    public Text[] shopItemCosts;


    // UI Items
    public Button buyButton0;
    public Button buyButton1;
    public Button buyButton2;
    public Button buyButton3;
    public Button buyButton4;
    public Button[] buyButtons;

    // Start is called before the first frame update
    void Start()
    {
        if (!started)
		{
            SetTimer();
            started = true;
        }

        moneyAmtText.text = Player.gold.ToString();
        homeButton.onClick.AddListener(() => goHome());

        // Load training items 
        // Prices are a function of level. 
        itemNames = new string[5] { "+5 Max Health", "+5 Max Energy", "+10 Max Health", "+10 Max Energy", "+15 Max Health" };
        itemType = new int[5] { 0, 1, 0, 1, 0 };
        itemValues = new int[5] { 5, 5, 10, 10, 15 };
        itemCosts = new int[5] { Player.level * 20, Player.level * 20, Player.level * 20 * 2, Player.level * 20 * 2, Player.level * 20 * 3 };

        // Get Buttons and Add listeners
        buyButton0.onClick.AddListener(() => buyItem(buyButton0, 0));
        buyButton1.onClick.AddListener(() => buyItem(buyButton1, 1));
        buyButton2.onClick.AddListener(() => buyItem(buyButton2, 2));
        buyButton3.onClick.AddListener(() => buyItem(buyButton3, 3));
        buyButton4.onClick.AddListener(() => buyItem(buyButton4, 4));
        

        // Get Shop Item Containers, check if player already owns skill
        shopItemNames = new Text[5] { itemName0, itemName1, itemName2, itemName3, itemName4 };
        shopItemCosts = new Text[5] { itemCost0, itemCost1, itemCost2, itemCost3, itemCost4 };
        buyButtons = new Button[5] { buyButton0, buyButton1, buyButton2, buyButton3, buyButton4};

        for (int i = 0; i < 5; i++)
		{
            shopItemNames[i].text = itemNames[i];
            shopItemCosts[i].text = itemCosts[i].ToString(); 
		}


        if (Player.coolTime > 0)
		{
            for (int i = 0; i < 5; i++)
            {
                buyButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        moneyAmtText.text = Player.gold.ToString();
        if (Player.coolTime == 0)
        {
            for (int i = 0; i < 5; i++)
			{
                buyButtons[i].gameObject.SetActive(true);
            }
            
        }
    }

    private static void SetTimer()
	{
        aTimer = new System.Timers.Timer(1000);
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
	}
    private static void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        if (Player.coolTime > 0)
        {
            Player.coolTime -= 1;
            print(Player.coolTime);
            Player.Save();
        }
	}

    public void buyItem(Button b, int i)
	{
        // Check if enough money to buy item 
        // No -> Nothing happens
        // Yes -> Add to player inventory 
        if (itemCosts[i] <= Player.gold)
        {
            Player.gold -= itemCosts[i];
            if (itemType[i] == 0)
			{
                Player.maxHealth += itemValues[i];
			}
			else
			{
                Player.maxEnergy += itemValues[i];
            }
            Player.coolTime = 15;
            Player.Save();

            // Check if item is a skill or consumable 
            for (i = 0; i < 5; i++)
			{
                buyButtons[i].gameObject.SetActive(false);
			}
        }
    }

	// Todo Integrations
	public void goHome()    // TODO
	{
        SceneManager.LoadScene("Home Scene");
    }

}
