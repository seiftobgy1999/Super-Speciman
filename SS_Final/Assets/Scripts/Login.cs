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
    private string Username;
    private string Password;
    private string[] Lines;



    public void LoginButton()
    {
        bool UN = false;
        bool PW = false;
        string path = "./Assets/Data/" + Username + ".txt";
        print(path);

        if (Username != "")
        {
            if (!System.IO.File.Exists(path)){  //\@" / Users / seifeltobgy / Desktop / Super Speciman / Assets
                Debug.LogWarning("Username Invalid!");
            }
            else
            {
                UN = true;
                Lines = System.IO.File.ReadAllLines(path); ///Users/seifeltobgy/Desktop/Super Speciman/Assets
            }
        }
        else
        {
            Debug.LogWarning("Username Field Empty!");
        }
        if (Password != "")
        {
            if (System.IO.File.Exists(path)) ///Users/seifeltobgy/Desktop/Super Speciman
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
            tempUser = Username;
            //Player.
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            print("Login Successful!");
            SceneManager.LoadScene("Home Scene");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(Application.dataPath);
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
