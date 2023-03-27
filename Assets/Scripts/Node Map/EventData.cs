using UnityEngine;

public class EventData
{
    public enum EnemyType
    {
    
    }

    public enum EventType
    {
        battle,
        passive,
        singleMinigame
    }

    public enum Minigame
    {
        StarFighter,
        FlakCannon,
        MatchingMinigame
    }

    public enum Resource
    {
        hp,
        shield,
        starfighter,
    }
    
    // use this to get data!
    // Example:
    //     EventData data = EventData.data;
    //     int numEnemies = data.SF_numEnemies;
    public static EventData data;
    
    public EventType eventType;
    public string description;
    public string text;
    public Resource resource;
    public Minigame minigameType;
    public string enemyName;
    public EnemyType enemyType;
    
    // player stats (for passive events)
    public int hp;
    public int currency;
    public int starfighter;
    public string starfighterStr;
    
    // battle bridge vars
    public string EnemyCapitalType1;
    public int EnemyCapitalNum1;
    public string EnemyStarFighterType1;
    public int EnemyStarFighterNum1;
    
    // star fighter vars
    public int SF_numEnemies;//1-10
    public int SF_difficulty;//0-3

    public EventData()
    {
        // set default values
        hp = 0;
        currency = 0;
        starfighter = 0;
        starfighterStr = "";
    }
    
    public void SetData()
    {
        // sets every variable in the static class
        data = this;

        setFleetBattleData();
        SetPassiveData();
    }

    public void SetPassiveData()
    {
        PlayerData playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        
        if (starfighter > 0)
        {
            if(starfighterStr == "")
                Debug.LogError("Need a starfighter string! I have no idea which fighter to add! (though it's probably PlayerFighter i can't read minds)");
            playerData.addPlayerStarFighter(starfighterStr);
        }

        if(hp != 0)
            playerData.updateHP(hp);
        if(currency != 0)
            playerData.updateCurrency(currency);
    }

    public void setFleetBattleData()
    {    
        GameObject.Find("PlayerData").GetComponent<PlayerData>().setEnemyFleet(EnemyCapitalType1,EnemyCapitalNum1,EnemyStarFighterType1,EnemyStarFighterNum1);
    }
}
