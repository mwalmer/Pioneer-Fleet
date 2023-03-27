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
        AbandonedStarFighter.resource = EventData.Resource.starfighter;
        AbandonedStarFighter.starfighterStr = "PlayerFighter";
        presets.Add(AbandonedStarFighter);

        //Nairan Fleet attack
        EventData NairanFleetAttack = new EventData();
        NairanFleetAttack.eventType = EventData.EventType.battle;
        NairanFleetAttack.description = "An uninhabited planet that is a common jumping point of FTL travel.";
        NairanFleetAttack.text = "As you travel past the planet, ships begin appearing out of FTL. Its a Nairan fleet. Battle stations!";
        NairanFleetAttack.EnemyCapitalType1 = "NairanBattlecruiser";
        NairanFleetAttack.EnemyCapitalNum1 = 2;
        NairanFleetAttack.EnemyStarFighterType1 = "NairanFighter";
        NairanFleetAttack.EnemyStarFighterNum1 = 3;
        presets.Add(NairanFleetAttack);

        //Nairan Fleet attack
        EventData NairanPlanetarySiege = new EventData();
        NairanPlanetarySiege.eventType = EventData.EventType.battle;
        NairanPlanetarySiege.description = "Scans indicate a large mass of ships around this planet. Holo news indicates a siege of the planet";
        NairanPlanetarySiege.text = "As you exit FLT, you see the siege in effect. A large amount of Nairan ships head towards you. Battle stations!";
        NairanPlanetarySiege.EnemyCapitalType1 = "NairanBattlecruiser";
        NairanPlanetarySiege.EnemyCapitalNum1 = 3;
        NairanPlanetarySiege.EnemyStarFighterType1 = "NairanFighter";
        NairanPlanetarySiege.EnemyStarFighterNum1 = 5;
        presets.Add(NairanPlanetarySiege);

        //Single minigame
        EventData singleMinigame = new EventData();
        singleMinigame.eventType = EventData.EventType.singleMinigame;
        singleMinigame.minigameType = EventData.Minigame.FlakCannon;
        singleMinigame.description = "A single minigame awaits, DONT LEAVE THIS IN! REMOVE ME!";
        presets.Add(singleMinigame);

    }

}
