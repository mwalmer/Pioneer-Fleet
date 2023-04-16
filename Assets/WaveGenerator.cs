using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
   public GameObject[] objects; // an array of GameObjects to be generated
    private int starFighterDif; // the int value read from PlayerData.cs

    void Start()
    {
        PlayerData playerData = new PlayerData();

        starFighterDif = EventData.GetData().starFighterDif;

   
        GameObject wave = objects[starFighterDif % objects.Length];
        wave.SetActive(true);     // activate the wave based on the int value
    }
}