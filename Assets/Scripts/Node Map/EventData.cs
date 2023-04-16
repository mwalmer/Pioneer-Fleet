using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class EventData
{
    public enum EnemyType
    {
        shadowFleet
    }

    public enum NodeType
    {
        planet,
        asteroid,
        star
    }

    public enum EventType
    {
        battle,
        passive,
        singleMinigame,
        boss,
        turncoat,
        MatchingMinigame,
        shop,
        completed
    }

    public enum Minigame
    {
        StarFighter,
        Bomber,
        FlakCannon,
        MatchingMinigame
    }

    // use GetData() to get data!
    // Example:
    //     EventData data = EventData.GetData();
    //     int numEnemies = data.SF_numEnemies;
    public static EventData data;

    public EventType eventType;
    public string description;
    public string text;
    public Minigame minigameType;
    public string enemyName;
    public EnemyType enemyType;
    public NodeType nodeType;
    public string buffText;
    public string debuffText;

    // player stats (for passive events)
    public int hp;
    public int currency;
    public int starfighter;
    public string starfighterStr;

    // battle bridge vars
    // public string EnemyCapitalType1;
    // public int EnemyCapitalNum1;
    // public string EnemyStarFighterType1;
    // public int EnemyStarFighterNum1;


    public List<string> EnemyStarFighers;
    public List<int> EnemyStarFigherNums;
    public List<string> EnemyCapitalShips;
    public List<int> EnemyCapitalShipsNums;


    // star fighter vars
    public int SF_numEnemies;//1-10
    public int starFighterDif;//0-5


    // flack cannon
    public int FC_timeGiven;//60-120
    public int FC_numEnemiesRequired;
    public int FC_enemyHP;
    public string lastScene; 
    public string gameMode; //Elimination, Survival
    public int difficulty;  //0-7
    public bool isBossFight;
    public int CannonFireSpeed; // 1-8
    public int CannonMagazineNumber; // 10-60
    public int ShieldSustain; //50-400
    public int CannonHP; // 3-20
    public int EnergyGain; // 1-100
    
    // match three
    public int MT_timeGiven;
    public static bool FC_firstTimePlay = true;
    

    public EventData()
    {
        // set default values   
        hp = 0;
        currency = 0;
        starfighter = 0;
        starfighterStr = "";
        nodeType = NodeType.planet;

<<<<<<< Updated upstream
        starFighterDif = Random.Range(0, 5);
=======
        starFighterDif = 6;
>>>>>>> Stashed changes
        
        lastScene = "BattleBridge";
        gameMode = "Elimination";
        difficulty = 0;
        isBossFight = false;
        CannonFireSpeed = 4;
        CannonMagazineNumber = 20;
        ShieldSustain = 100;
        CannonHP = 7;
        EnergyGain = 10;

        FC_timeGiven = 60;
        MT_timeGiven = 30;

        EnemyStarFighers = new List<string>();
        EnemyStarFigherNums = new List<int>();
        EnemyCapitalShips = new List<string>();
        EnemyCapitalShipsNums = new List<int>();
    }

    public static EventData GetData()
    {
        if (data == null)
        {
            data = new EventData();
            data.lastScene = "NodeMap";
        }

        return data;
    }

    public void SetData()
    {
        if (GameObject.Find("PlayerData") != null)
        {
            PlayerData playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
            playerData.updateCurrency(currency);
        }
        
        data = this;
    }

    public void SetPassiveData()
    {
        PlayerData playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();

        if (starfighter > 0)
        {
            if (starfighterStr == "")
                Debug.LogError("Need a starfighter string! I have no idea which fighter to add! (though it's probably FederationFighter, i can't read minds)");
            playerData.addPlayerStarFighter(starfighterStr);
        }

        if (hp != 0)
            playerData.updateHP(hp);
    }

    public void setFleetBattleData()
    {
        GameObject.Find("PlayerData").GetComponent<PlayerData>().LoadBridgeFirstTime = true;
        PlayerData playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        playerData.setEnemyFleet();
    }
}
