using System.Collections.Generic;

public class EventPresets
{
    public static List<EventData> presets = new List<EventData>();

    public static void InitPresets()
    {
        if(presets.Count != 0)
            return;
        
        EventData e = new EventData();
        e.enemyName = "enemy name";
        e.eventType = EventData.EventType.battle;
        e.description = "event description";

        e.capitalType = 2;
        e.capitalNum  = 2;
        e.fleetType   = 2;
        e.fleetNum    = 2;
        
        presets.Add(e);
        
        // Passive Events
        e = new EventData();
        e.eventType = EventData.EventType.passive;
        e.description = "Abandoned star fighter.";
        e.text = "+1 star fighter to fleet";
        e.starfighter += 1;
        e.resource = EventData.Resource.starfighter;
        presets.Add(e);
    }

}
