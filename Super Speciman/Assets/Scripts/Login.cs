using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public static string tempUser;
    public GameObject username;
    public GameObject password;
    private String Username;
    private string Password;
    private string[] Lines;



    public void LoginButton()
    {
        bool UN = false;
        bool PW = false;
        if (Username != "")
        {
            if (!System.IO.File.Exists(@"/Users/seifeltobgy/Desktop/Github/Super-Speciman/Super Speciman/Assets/Data/" + Username + ".txt")){
                Debug.LogWarning("Username Invalid!");
            }
            else
            {
                UN = true;
                Lines = System.IO.File.ReadAllLines(@"/Users/seifeltobgy/Desktop/Github/Super-Speciman/Super Speciman/Assets/Data/" + Username + ".txt");
            }
        }
        else
        {
            Debug.LogWarning("Username Field Empty!");
        }
        if (Password != "")
        {
            if (System.IO.File.Exists(@"/Users/seifeltobgy/Desktop/Github/Super-Speciman/Super Speciman/Assets/Data/" + Username + ".txt"))
            {
                bool Clear = true;
                int i = 1;
                foreach (char c in Password)
                {
                    if (Clear)
                    {
                        Password = "";
                        Clear = false;
                    }
                    i++;
                    char hash = (char)(c * i);
                    Password += hash.ToString();
                }
                if (Password == Lines[2])
                {
                    PW = true;
                }
                else
                {
                    Debug.LogWarning("Password Invalid!");
                }

            }
            else
            {
                Debug.LogWarning("No Username Associated With Password!");
            }

        }
        else
        {
            Debug.LogWarning("Password Field Empty!");
        }


        if (UN == true && PW == true)
        {
            tempUser = username.GetComponent<InputField>().text;
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            print("Login Successful!");
            SceneManager.LoadScene("Start Menu");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Username != "")
            {
                LoginButton();
            }
        }



        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }
}
