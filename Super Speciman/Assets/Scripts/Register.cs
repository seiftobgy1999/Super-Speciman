using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class Register : MonoBehaviour
{
    public GameObject username;
    public GameObject email;
    public GameObject password;
    public GameObject confpassword;
    private string Username;
    private string Email;
    private string Password;
    private string Confpassword;
    private string form;
    private bool EmailValid = false;
    private string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                       + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                       + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                       + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";



    void EmailValidation()
    {
        if (Regex.IsMatch(Email, MatchEmailPattern))
        {
            EmailValid = true;
        }
        else
        {
            EmailValid = false;
        }
    }


    public void RegisterButton()
    {
        bool UN = false;
        bool EM = false;
        bool PW = false;
        bool CPW = false;

        if (Username != "")
        {
            if (!System.IO.File.Exists(@"/Users/seifeltobgy/Desktop/Github/Super-Speciman/Super Speciman/Assets/Data/" + Username + ".txt"))
            {
                UN = true;
            }
            else
            {
                Debug.LogWarning("Username Taken!");
            }
        }
        else
        {
            Debug.LogWarning("Username Field Empty!");
        }

        if (Email != "")
        {
            EmailValidation();
            if (EmailValid)
            {
                EM = true;
            }
            else
            {
                Debug.LogWarning("Email is Incorrect!");
            }
        }
        else
        {
            Debug.LogWarning("Email Field Empty!");
        }

        if (Password != "")
        {
            if (Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                Debug.LogWarning("Password Must Be Atleast 6 Characters Long!");
            }
        }
        else
        {
            Debug.LogWarning("Password Field Empty!");
        }

        if (Confpassword != "")
        {
            if (Confpassword == Password)
            {
                CPW = true;
            }
            else
            {
                Debug.LogWarning("Passwords don't Match!");
            }
        }
        else
        {
            Debug.LogWarning("Confirm Password Field Empty!");
        }

        if (UN == true && PW == true && EM == true && CPW == true){
            bool Clear = true;
            int i = 1;
            foreach(char c in Password)
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
            form = (Username + Environment.NewLine + Email + Environment.NewLine + Password);
            System.IO.File.WriteAllText("/Users/seifeltobgy/Desktop/Github/Super-Speciman/Super Speciman/Assets/Data/" + Username + ".txt", form);
            username.GetComponent<InputField>().text = "";
            email.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confpassword.GetComponent<InputField>().text = "";
            print("Registration Successful!");

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
                email.GetComponent<InputField>().Select();
            }
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confpassword.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Password != "" && Email != "" && Username != "" && Confpassword != "")
                {
                    RegisterButton();
                }
            }

        Username = username.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        Confpassword = confpassword.GetComponent<InputField>().text;


    }
}
