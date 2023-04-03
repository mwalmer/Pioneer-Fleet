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
        AbandonedStarFighter.starfighterStr = "PlayerFighter";
        presets.Add(AbandonedStarFighter);

        // // Passive Events
        // EventData miningOutpost = new EventData();
        // miningOutpost.eventType = EventData.EventType.passive;
        // miningOutpost.description = "This planet seems to be sparkling...";
        // miningOutpost.text = "Upon landing, you find large surface deposits of Orichalcum. Though you can't mine it you know people who would be interested in its location. +1 currency";
        // miningOutpost.currency += 1;
        // presets.Add(miningOutpost);
        //
        // //Nairan Fleet attack
        // EventData NairanFleetAttack = new EventData();
        // NairanFleetAttack.eventType = EventData.EventType.battle;
        // NairanFleetAttack.description = "An uninhabited planet that is a common jumping point of FTL travel.";
        // NairanFleetAttack.text = "As you travel past the planet, ships begin appearing out of FTL. Its a Nairan fleet. Battle stations!";
        // NairanFleetAttack.EnemyCapitalType1 = "NairanBattlecruiser";
        // NairanFleetAttack.EnemyCapitalNum1 = 2;
        // NairanFleetAttack.EnemyStarFighterType1 = "NairanFighter";
        // NairanFleetAttack.EnemyStarFighterNum1 = 3;
        // presets.Add(NairanFleetAttack);
        //
        // //Nairan Fleet attack
        // EventData NairanPlanetarySiege = new EventData();
        // NairanPlanetarySiege.eventType = EventData.EventType.battle;
        // NairanPlanetarySiege.description = "Scans indicate a large mass of ships around this planet. Holo news indicates a siege of the planet";
        // NairanPlanetarySiege.text = "As you exit FLT, you see the siege in effect. A large amount of Nairan ships head towards you. Battle stations!";
        // NairanPlanetarySiege.EnemyCapitalType1 = "NairanBattlecruiser";
        // NairanPlanetarySiege.EnemyCapitalNum1 = 3;
        // NairanPlanetarySiege.EnemyStarFighterType1 = "NairanFighter";
        // NairanPlanetarySiege.EnemyStarFighterNum1 = 5;
        // presets.Add(NairanPlanetarySiege);
        //
        // // TODO: edit me (not the comment the thing below this [not that comment the thing below that])
        // //Single minigame
        // EventData singleMinigame = new EventData();
        // singleMinigame.eventType = EventData.EventType.singleMinigame;
        // singleMinigame.minigameType = EventData.Minigame.FlakCannon;
        // singleMinigame.description = "A single minigame awaits, DONT LEAVE THIS IN! REMOVE ME!";
        // presets.Add(singleMinigame);
        //
        // //Heal your stuff
        // EventData FriendlyDocks = new EventData();
        // FriendlyDocks.eventType = EventData.EventType.passive;
        // FriendlyDocks.description = "This system is friendly to the federation, perhaps you will find aid here.";
        // FriendlyDocks.text = "You gain nothing. You achieve nothing. You die. Friendly you are";
        // FriendlyDocks.hp += 2;
        // presets.Add(FriendlyDocks);
        //
        // Turncoat
        EventData turncoat = new EventData();
        turncoat.eventType = EventData.EventType.turncoat;
        turncoat.description = "Your coat begins to move... as if it knows something...";
        turncoat.text = "IT DID! You find the enemy turncoat while his fighters are refueling!";
        turncoat.EnemyCapitalType1 = "NairanBattlecruiser";
        turncoat.EnemyCapitalNum1 = 1;
        turncoat.EnemyStarFighterType1 = "NairanFighter";
        turncoat.EnemyStarFighterNum1 = 0;
        presets.Add(turncoat);
        
        // Boss
        EventData boss = new EventData();
        boss.eventType = EventData.EventType.boss;
        boss.description = "The turncoats last navigation waypoint was set here... It looks heavily fortified.";
        boss.text = "It was!";
        boss.EnemyCapitalType1 = "NairanBattlecruiser";
        boss.EnemyCapitalNum1 = 4;
        boss.EnemyStarFighterType1 = "NairanFighter";
        boss.EnemyStarFighterNum1 = 8;
        presets.Add(boss);
    }

}
