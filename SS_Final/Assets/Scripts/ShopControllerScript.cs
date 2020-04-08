using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControllerScript : MonoBehaviour
{
    // Controller Variables
    public string[] itemNames;
    public Dictionary <string, int> itemCosts;
    public Dictionary <string, int> itemType; // 0 = Skill, 1 = Consumable

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
        moneyAmtText.text = Player.gold.ToString();
        homeButton.onClick.AddListener(() => goHome());

        // Load shop items 
        // First 3 always skill, last 2 always consumable. 
        // Prices are a function of level. 
        itemNames = new string[5] { ProjectStrings.Attack2, ProjectStrings.Attack3, ProjectStrings.AttackEnergy, ProjectStrings.Shield, ProjectStrings.Energize };

        // Generate Costs and Types for items
        itemCosts = new Dictionary<string, int>();
        itemType = new Dictionary<string, int>();
        for (int i = 0; i < 4; i++)
        {
            itemCosts.Add(itemNames[i], Player.level * 100 * (i+1));
            itemType.Add(itemNames[i], 0);
        }
        // Powerup
        itemCosts.Add(itemNames[4], Player.level * 5);
        itemType.Add(itemNames[4], 1);
        // else{}

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
            shopItemCosts[i].text = itemCosts[itemNames[i]].ToString(); 
            
            // if player owns skill / consumables - TODO
            if (Player.Abilities.Contains(itemNames[i]) || Player.Powerups.Contains(itemNames[i]))
			{
                buyButtons[i].gameObject.SetActive(false); // Disable
            }
		}
        print("Player has");
        foreach (string i in Player.Abilities)
        {
            print(i);
        }
        foreach (string i in Player.Powerups)
        {
            print(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        moneyAmtText.text = Player.gold.ToString();

        // if level increased
        if (Player.level > (itemCosts[itemNames[0]] / (100)))
		{
            //itemNames = new string[5] { "L" + Player.level.ToString() + " Skill 1", "L" + Player.level.ToString() + " Skill 2", "L" + Player.level.ToString() + " Skill 3", "L" + Player.level.ToString() + " Consumable 1", "L" + Player.level.ToString() + " Consumable 2" };
            itemCosts = new Dictionary<string, int>();
            itemType = new Dictionary<string, int>();
            for (int i = 0; i < 4; i++)
            {
                itemCosts.Add(itemNames[i], Player.level * 100* (i + 1));
                itemType.Add(itemNames[i], 0);
            }
            // Powerup
            itemCosts.Add(itemNames[4], Player.level * 5);
            itemType.Add(itemNames[4], 1);

            for (int i = 0; i < 5; i++)
            {
                shopItemNames[i].text = itemNames[i];
                shopItemCosts[i].text = itemCosts[itemNames[i]].ToString();
            }
        }
        
    }

    public void buyItem(Button b, int index)
	{
        string name = itemNames[index];
        print("Player has");
        foreach (string i in Player.Abilities)
        {
            print(i);
        }
        foreach (string i in Player.Powerups)
        {
            print(i);
        }
        // Check if enough money to buy item 
        // No -> Nothing happens
        // Yes -> Add to player inventory 
        if (itemCosts[name] <= Player.gold){
            // Check if item is a skill or consumable 
            if (itemType[name] == 0 && !Player.Abilities.Contains(name)) // Skill
			{
                Player.Abilities.Add(name);
                Player.gold -= itemCosts[name];
                Player.Save();
                b.gameObject.SetActive(false); // Disable
            }
			else if (!Player.Powerups.Contains(name)) // Powerup

            {
                Player.Powerups.Add(name);
                Player.gold -= itemCosts[name];
                Player.Save();
                b.gameObject.SetActive(false); // Disable
            }
            
        }
    }

	// Integrations
	public void goHome()    // TODO
	{
        SceneManager.LoadScene("Home Scene");
    }


}
