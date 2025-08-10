using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public int playerBalance = 1000;
    [SerializeField] TMP_Text playerBalanceText;

    void Awake()
    {
        instance = this;
        Updatebalance(0);
    }
    public void Updatebalance(int balance)
    {
        playerBalance += balance;
        playerBalanceText.text = playerBalance.ToString();
    }
}
