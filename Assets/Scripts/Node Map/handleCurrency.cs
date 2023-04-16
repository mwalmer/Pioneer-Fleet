using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleCurrency : MonoBehaviour
{
    public static int last = 0;
    void Start()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        if (playerData.currency != last)
        {
            if (last == 0)
            {
                last = playerData.currency;
                return;
                
            }
            FindObjectOfType<UI_ResourceTab>().AddAmount(playerData.currency - last);
            last = playerData.currency;
        }
    }

}
