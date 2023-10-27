using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StoreTest : MonoBehaviour
{
    public GameObject WiFi;
    public GameObject Store;
    public GameObject Warning;
    public GameObject CCField;

    TMP_Text warnText;
    TMP_InputField cardField;
    //Funny

    //String cardNum = "1234123412341234";
    //String cardNum = "79927398713";
    // Start is called before the first frame update

    static bool checkLuhn(String cardNo)
    {
        int nDigits = cardNo.Length;

        int nSum = 0;
        bool isSecond = false;
        for (int i = nDigits - 1; i >= 0; i--)
        {

            int d = cardNo[i] - '0';

            if (isSecond == true)
                d = d * 2;

            nSum += d / 10;
            nSum += d % 10;

            isSecond = !isSecond;
        }
        return (nSum % 10 == 0);
    }

    void Start()
    {
        warnText = Warning.GetComponent<TMP_Text>();
        cardField = CCField.GetComponent<TMP_InputField>();
        Warning.SetActive(false);
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Debug.Log("Internet connection not available, please try again later");
            Store.SetActive(false);
        }
        else
        {
            WiFi.SetActive(false);
            //Debug.Log("Welcome to the Untitled Cat Game Store!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicky()
    {
        String cardNum = cardField.text;
        Debug.Log(cardNum);
        //add check for if input number is in valid format and warn user
        bool notNum = false;
        foreach (char c in cardNum)
        {
            if (!char.IsDigit(c))
                notNum = true;
        }
        if (cardNum.Length < 8 || cardNum.Length > 19 || notNum)
        {
            warnText.text = "The value you entered is not a valid credit card number format";
            notNum = false;
        }
        else
        {
            if (checkLuhn(cardNum) == true)
            {
                warnText.text = "Thank you for your purchase! We hope you continue to enjoy our game!";
                //Debug.Log("Thank you for your purchase! We hope you continue to enjoy our game!");
            }
            else
            {
                warnText.text = "Attempting fraud is a felony in the United States. Please enter a valid credit card number or authorities will be contacted.";
                //Debug.Log("Attempting fraud is a felony in the United States. Please enter a valid credit card number or authorities will be contacted.");
            }
        }
        Warning.SetActive(true);
    }
}
