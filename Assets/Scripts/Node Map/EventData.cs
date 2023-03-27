public class EventData
{
    public enum EnemyType
    {
    
    }

    public enum EventType
    {
        battle,
        passive
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
    public string enemyName;
    public EnemyType enemyType;
    
    // player stats (for passive events)
    public float hp;
    public float shield;
    public int starfighter;
    
    // battle bridge vars
    public int BB_capitalType;
    public int BB_capitalNum;
    public int BB_fleetNum;
    public int BB_fleetType;
    
    // star fighter vars
    public int SF_numEnemies;//1-10
    public int SF_difficulty;//0-3

    public EventData()
    {
        // set default values
        hp = 0;
        shield = 0;
        starfighter = 0;
    }
    
    public void SetData()
    {
        // sets every variable in the static class
        data = this;
    }

    public void SetPassiveData()
    {
        // PlayerData.shield += shield;
        // PlayerData.hp += hp;
        // PlayerData.starfighters += starfighter;
    }
}
