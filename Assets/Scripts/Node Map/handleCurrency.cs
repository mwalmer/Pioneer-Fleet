using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleCurrency : MonoBehaviour
{
    void Start()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        FindObjectOfType<UI_ResourceTab>().SetValue(playerData.currency);
    }

}
