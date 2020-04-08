using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HomeSceneScript : MonoBehaviour
{
    // UI Items
    public Button goStoreButton;
    public Button goTrainButton;
    public Button goBattleButton;
    public Button goLeaderboardButton;
    public Button goLogoutButton;

    // public Text HP;
    // public Text Energy;
    // public Text XP;
    // public Text gold;

    // Start is called before the first frame update
    void Start()
    {
        print("Home Scene Entered");
        // Get Buttons and Add listeners
        goStoreButton.onClick.AddListener(() => goStore());
        goTrainButton.onClick.AddListener(() => goTraining());
        goBattleButton.onClick.AddListener(() => goBattle());
        goLeaderboardButton.onClick.AddListener(() => goLeaderboard());
        goLogoutButton.onClick.AddListener(() => goLogout());
        //Player.Save();

    }

    void goLogout()
    {
        SceneManager.LoadScene("Login Menu");
        print("Entered Score scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void updateHomeScreenUI() {
        HP.text = Player.maxHealth.ToString();
        Energy.text = Player.maxEnergy.ToString();
        gold.text = Player.gold.ToString();
        XP.text = Player.XP.ToString();
    }

    private void OnLevelWasLoaded() {
        updateHomeScreenUI();
    }*/

    void goStore()
	{
        SceneManager.LoadScene("Store Scene");
        print("Entered Score scene");
    }

    void goTraining()
    {
        SceneManager.LoadScene("Training Area");
        print("Entered Score scene");
    }

    void goBattle()
    {
        SceneManager.LoadScene("Battle Scene");
        print("Entered Score scene");
    }

    void goLeaderboard() {
        SceneManager.LoadScene("Leaderboard Scene");
        print("Entered Score scene");
    }
}
