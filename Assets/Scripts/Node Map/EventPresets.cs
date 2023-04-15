using System.Collections.Generic;

public class EventPresets
{
    public static List<EventData> presets = new List<EventData>();

    public static void InitPresets()
    {
        if(presets.Count != 0)
            return;
        
        // Passive Events
        EventData AbandonedStarFighter = new EventData();
        AbandonedStarFighter.eventType = EventData.EventType.passive;
        AbandonedStarFighter.description = "Abandoned star fighter.";
        AbandonedStarFighter.text = "+1 star fighter to fleet";
        AbandonedStarFighter.starfighter += 1;
        AbandonedStarFighter.starfighterStr = "FederationFighter";
        presets.Add(AbandonedStarFighter);
        
        // TODO: remove, for debug ONLY!!!!!!
        EventData MatchThree = new EventData();
        MatchThree.eventType = EventData.EventType.singleMinigame;
        MatchThree.description = "MatchThree";
        MatchThree.text = "matching";
        MatchThree.minigameType = EventData.Minigame.MatchingMinigame;
        presets.Add(MatchThree);

        // Passive Events
        EventData miningOutpost = new EventData();
        miningOutpost.eventType = EventData.EventType.passive;
        miningOutpost.description = "This planet seems to be sparkling...";
        miningOutpost.text = "Upon landing, you find large surface deposits of Orichalcum. Though you can't mine it you know people who would be interested in its location. +1 currency";
        miningOutpost.currency += 50;
        presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);
        // presets.Add(miningOutpost);

        // Passive Events
        EventData shop = new EventData();
        shop.eventType = EventData.EventType.shop;
        shop.description = "Shop";
        shop.text = "shop text";
        presets.Add(shop);
        
        //Nairan Fleet attack
        EventData NairanFleetAttack = new EventData();
        NairanFleetAttack.eventType = EventData.EventType.battle;
        NairanFleetAttack.description = "An uninhabited planet that is a common jumping point of FTL travel.";
        NairanFleetAttack.text = "As you travel past the planet, ships begin appearing out of FTL. Its a Nairan fleet. Battle stations!";
        NairanFleetAttack.EnemyCapitalShips.Add("NairanFrigate");
        NairanFleetAttack.EnemyCapitalShipsNums.Add(2);
        NairanFleetAttack.EnemyStarFighers.Add("NairanFighter");
        NairanFleetAttack.EnemyStarFigherNums.Add(3);
        NairanFleetAttack.currency += 100;
        presets.Add(NairanFleetAttack);
        
        //Nairan Fleet attack
        EventData NairanPlanetarySiege = new EventData();
        NairanPlanetarySiege.eventType = EventData.EventType.battle;
        NairanPlanetarySiege.description = "Scans indicate a large mass of ships around this planet. Holo news indicates a siege of the planet";
        NairanPlanetarySiege.text = "As you exit FLT, you see the siege in effect. A large amount of Nairan ships head towards you. Battle stations!";
        NairanPlanetarySiege.EnemyCapitalShips.Add("NairanBattlecruiser");
        NairanPlanetarySiege.EnemyCapitalShipsNums.Add(1);
        NairanPlanetarySiege.EnemyCapitalShips.Add("NairanFrigate");
        NairanPlanetarySiege.EnemyCapitalShipsNums.Add(2);
        NairanPlanetarySiege.EnemyStarFighers.Add("NairanFighter");
        NairanPlanetarySiege.EnemyStarFigherNums.Add(5);
        NairanPlanetarySiege.currency += 200;
        presets.Add(NairanPlanetarySiege);
        
        // TODO: edit me (not the comment the thing below this [not that comment the thing below that])
        //Single minigame
        // EventData singleMinigame = new EventData();
        // singleMinigame.eventType = EventData.EventType.singleMinigame;
        // singleMinigame.minigameType = EventData.Minigame.FlakCannon;
        // singleMinigame.description = "A single minigame awaits, DONT LEAVE THIS IN! REMOVE ME!";
        // presets.Add(singleMinigame);
        
        //Heal your stuff
        // EventData FriendlyDocks = new EventData();
        // FriendlyDocks.eventType = EventData.EventType.passive;
        // FriendlyDocks.description = "This system is friendly to the federation, perhaps you will find aid here.";
        // FriendlyDocks.text = "You gain nothing. You achieve nothing. You die. Friendly you are";
        // FriendlyDocks.hp += 2;
        // presets.Add(FriendlyDocks);
        
        // Turncoat
        EventData turncoat = new EventData();
        turncoat.eventType = EventData.EventType.turncoat;
        turncoat.description = "Your coat begins to move... as if it knows something...";
        turncoat.text = "IT DID! You find the enemy turncoat while his fighters are refueling!";
        turncoat.EnemyCapitalShips.Add("NairanFrigate");
        turncoat.EnemyCapitalShipsNums.Add(1);
        presets.Add(turncoat);
        
        // Boss
        EventData boss = new EventData();
        boss.eventType = EventData.EventType.boss;
        boss.description = "The turncoats last navigation waypoint was set here... It looks heavily fortified.";
        boss.text = "It was!";
        boss.EnemyCapitalShips.Add("NairanBattlecruiser");
        boss.EnemyCapitalShipsNums.Add(2);
        boss.EnemyCapitalShips.Add("NairanFrigate");
        boss.EnemyCapitalShipsNums.Add(4);
        boss.EnemyStarFighers.Add("NairanFighter");
        boss.EnemyStarFigherNums.Add(8);
        boss.currency += 300;
        presets.Add(boss);
    }

}
