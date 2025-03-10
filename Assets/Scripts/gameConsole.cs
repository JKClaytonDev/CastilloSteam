﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gameConsole : MonoBehaviour
{
    public Canvas C;
    public string[] consoleList;
    public Text[] orderText;
    public InputField inf;
    public Text t;
    string mode;
    public Text modeText;
    string setIntName;
    string setStringName;
    string setFloatName;
    public AudioClip cheatSound;
    
    // Start is called before the first frame update
    void Start()
    {
        mode = "Default";
    }
    public void addText(string input)
    {
        bool Bot = true;
        if (input == "")
        {
            input = t.text;
            Bot = false;
        }
        t.text = "";
        inf.text = "";
        if (input != "")
        {
            for (int i = 1; i < consoleList.Length; i++)
                consoleList[i - 1] = consoleList[i];
            consoleList[consoleList.Length - 1] = input;
        }
        if (Bot)
            return;
        if (input == "Restart")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (input == "Reset" || input == "ResetAll")
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Menu");
        }
        if (input == "HUD" || input == "NoHUD")
        {
            if (C)
            {
                C.enabled = false;
            }
        }
        if (input == "Buy Me A Switch")
            addText("No");
        if (input == "Quit")
            Application.Quit();
        if (input == "Reset")
        {
            PlayerPrefs.DeleteAll();
            addText("Done!");
        }
        if (input == "god" || input == "GodMode")
            addText("nope, sorry");
        if (input == "AllGuns" || input == "GiveAll" || input == "gimme weapons" || input == "Impulse 101" || input == "GiveGuns" || input == "GiveAllGuns" || input == "IDKFA")
        {
            PlayerPrefs.SetFloat("SpeedBoots", 0);
            PlayerPrefs.SetFloat("Healing", 0);
            GetComponent<AudioSource>().PlayOneShot(cheatSound);
            if (FindObjectOfType<PlayerAnimations>())
            {
                PlayerAnimations a = FindObjectOfType<PlayerAnimations>();
                for (int i = 0; i < a.equippedWeapons.Length; i++)
                {
                    PlayerPrefs.SetInt("EquippedWeapon" + i, 1);
                    a.equippedWeapons[i] = PlayerPrefs.GetInt("EquippedWeapon" + i) == 1;
                    
                }
                PlayerPrefs.SetInt("UnlockedIndex1", 1);
                PlayerPrefs.SetInt("UnlockedIndex2", 1);
                PlayerPrefs.SetInt("UnlockedIndex3", 1);
                PlayerPrefs.SetInt("UnlockedIndex4", 1);
                if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
                if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            }
            addText("Activated");
        }
        if (input == "TrailerCanvas" || input == "ToggleCanvas" || input == "UI")
        {
            FindObjectOfType<PlayerMovement>().ToggleTrailerCanvas();
            return;
        }
        if (input == "LoadScene")
        {
            mode = "LoadScene";
            addText("Preparing scene load: Type Scene Name");
            return;
        }
        else if (mode == "LoadScene")
        {
            inf.gameObject.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(input);
            return;
        }
        if (input == "SetString")
        {
            mode = "SetString";
            addText("Setting String: Type String name");
            return;
        }
        else if (mode == "SetString")
        {
            mode = "SetStringValue";
            setStringName = input;
            addText("String name is " + input + " type value");
            return;
        }
        else if (mode == "SetStringValue")
        {
            mode = "SetStringValue";
            PlayerPrefs.SetString(setStringName, input);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            addText("Set String " + setStringName + " to " + input);
            mode = "Default";
            return;
        }
        if (input == "noclip" || input == "NoClip" || input == "noClip" || input == "Noclip")
        {
            FindObjectOfType<PlayerMovement>().noClip = !FindObjectOfType<PlayerMovement>().noClip;
        }
        if (input == "SetInt")
        {
            mode = "SetInt";
            addText("Setting Integer: Type Integer name");
            return;
        }
        else if (mode == "SetInt")
        {
            mode = "SetIntValue";
            setIntName = input;
            addText("Integer name is " + input + " type value");
            return;
        }
        else if (mode == "SetIntValue")
        {
            int i = int.Parse(input);
            PlayerPrefs.SetInt(setIntName, i);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            addText("Set Integer " + setStringName + " to " + input);
            mode = "Default";
            return;
        }
        if (input == "GiveGun" || input == "Give" || input == "Equip" || input == "Weapon")
        {
            mode = "giveGun";
            addText("Equipping Gun: Type gun number");
            return;
        }
        if (mode == "giveGun")
        {
            PlayerPrefs.SetInt("EquippedWeapon" + input, 1);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            FindObjectOfType<PlayerAnimations>().equippedWeapons[int.Parse(input)] = PlayerPrefs.GetInt("EquippedWeapon" + input) == 1;
        }
        if (input == "Reset")
        {
            PlayerPrefs.DeleteAll();
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            return;
        }
        if (input == "SetFloat")
        {
            mode = "SetFloat";
            addText("Setting Float: Type Float name");
            return;
        }
        else if (mode == "SetFloat")
        {
            mode = "SetFloatValue";
            setIntName = input;
            addText("Float name is " + input + " type value");
            return;
        }
        else if (mode == "SetFloatValue")
        {
            float i = float.Parse(input);
            PlayerPrefs.SetFloat(setIntName, i);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            addText("Set Float " + setStringName + " to " + input);
            mode = "Default";
            return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            addText("");
        }
        inf.gameObject.SetActive(true);
        modeText.text = "Mode: " + mode;
        for (int i = 0; i<consoleList.Length; i++)
            orderText[i].text = consoleList[i];
    }
}
