using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetController : MonoBehaviour
{
    public Button betPlusBtn;
    public Button betMinusBtn;
    public TMP_Text betAmountText;

    public static int betAmount = 10;

    void Start()
    {
        betPlusBtn.onClick.AddListener(() => Handlebet(true));
        betMinusBtn.onClick.AddListener(() => Handlebet(false));
    }

    private void Handlebet(bool increase)
    {
        if (increase)
        {
            if (betAmount >= PlayerData.instance.playerBalance)
            {
                return;
            }

            betAmount += 5;


        }
        else
        {
            if (betAmount > 10)
            {
                betAmount -= 5;
                PlayerData.instance.Updatebalance(5);

            }
        }
        betAmountText.text = betAmount.ToString();

    }


}
