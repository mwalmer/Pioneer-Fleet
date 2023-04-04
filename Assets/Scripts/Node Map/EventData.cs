using UnityEngine;

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
        turncoat
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

    // flack cannon
    public int FC_timeGiven;//60-120
    public int FC_numEnemiesRequired;
    public int FC_enemyHP;
    public string lastScene;

    public EventData()
    {
        // set default values
        hp = 0;
        currency = 0;
        starfighter = 0;
        starfighterStr = "";
        nodeType = NodeType.planet;
        lastScene = "NodeMap";

        FC_timeGiven = 60;
    }

    public static EventData GetData()
    {
        if (data == null)
            data = new EventData();

        return data;
    }

    public void SetData()
    {
        data = this;
    }

    public void SetPassiveData()
    {
        PlayerData playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();

        if (starfighter > 0)
        {
            if (starfighterStr == "")
                Debug.LogError("Need a starfighter string! I have no idea which fighter to add! (though it's probably PlayerFighter, i can't read minds)");
            playerData.addPlayerStarFighter(starfighterStr);
        }

        if (hp != 0)
            playerData.updateHP(hp);
        if (currency != 0)
            playerData.updateCurrency(currency);
    }

    public void setFleetBattleData()
    {
        PlayerData playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        playerData.setEnemyFleet(EnemyCapitalType1, EnemyCapitalNum1, EnemyStarFighterType1, EnemyStarFighterNum1);
    }
}
