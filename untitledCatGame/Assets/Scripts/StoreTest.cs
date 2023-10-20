using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTest : MonoBehaviour
{
    //Funny

    //String cardNum = "1234123412341234";
    String cardNum = "79927398713";
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
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Internet connection not available, please try again later");
        }
        else
        {
            Debug.Log("Welcome to the Untitled Cat Game Store!");
            if (checkLuhn(cardNum) == true)
            {
                Debug.Log("Thank you for your purchase! We hope you continue to enjoy our game!");
            }
            else
            {
                Debug.Log("Attempting fraud is a felony in the United States. Please enter a valid credit card number or authorities will be contacted.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
