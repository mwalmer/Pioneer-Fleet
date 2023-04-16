using System.Collections.Generic;
using UnityEngine;

public static class NodeData
{
    public static int mapNum = 0;
    public static GameObject bossNode;
    public static GameObject nodeMap;
    public static GameObject currentNode;
    public static string galaxyName;
    public static GameObject selectedNode;
    public static GameObject travelIndicator;
    public static List<GameObject> eventNodeList = new List<GameObject>();

    public static List<string> GalaxyNames = new List<string>()
    {
        "Andromna"
    };
    
    public static List<string> PlanetNames = new List<string>() 
    {
        "Absolutno", "Acamar", "Achernar", "Achird", "Acrab", "Acrux", "Acubens", "Adhafera", "Adhara", "Adhil", "Ain",
        "Ainalrami", "Aladfar", "Alamak", "Alasia", "Alathfar", "Albaldah", "Albali", "Albireo", "Alchiba", "Alcor",
        "Alcyone", "Aldebaran", "Alderamin", "Aldhanab", "Aldhibah", "Aldulfin", "Alfirk", "Algedi", "Algenib",
        "Algieba", "Algol", "Algorab", "Alhena", "Alioth", "Aljanah", "Alkaid", "Al Kalb al Rai",
        "Alkalurops", "Alkaphrah", "Alkarab", "Alkes", "Almaaz", "Almach", "Al Minliar al Asad", "Alnair", "Alnasl",
        "Alnilam", "Alnitak", "Alniyat", "Alphard", "Alphecca", "Alpheratz", "Alpherg", "Alrakis", "Alrescha", "Alruba",
        "Alsafi", "Alsciaukat", "Alsephina", "Alshain", "Alshat", "Altair", "Altais", "Alterf", "Aludra",
        "Alula Australis", "Alula Borealis", "Alya", "Alzirr", "Amadioha", "Amansinaya", "Anadolu", "Ancha",
        "Angetenar", "Aniara", "Ankaa", "Anser", "Antares", "Arcalís", "Arcturus", "Arkab Posterior", "Arkab Prior",
        "Arneb", "Ascella", "Asellus Australis", "Asellus Borealis", "Ashlesha", "Asellus Primus", "Asellus Secundus",
        "Asellus Tertius", "Aspidiske", "Asterope, Sterope", "Atakoraka", "Athebyne", "Atik", "Atlas", "Atria",
        "Avior", "Axólotl", "Ayeyarwady", "Azelfafage", "Azha", "Azmidi", "Baekdu", "Barnard's Star", "Baten Kaitos",
        "Beemim", "Beid", "Belel", "Belenos", "Bellatrix", "Berehynia", "Betelgeuse", "Bharani", "Bibhā", "Biham",
        "Bosona", "Botein", "Brachium", "Bubup", "Buna", "Bunda", "Canopus", "Capella", "Caph", "Castor", "Castula",
        "Cebalrai", "Ceibo", "Celaeno", "Cervantes", "Chalawan", "Chamukuy", "Chaophraya", "Chara", "Chason", "Chechia",
        "Chertan", "Citadelle", "Citalá", "Cocibolca", "Copernicus", "Cor Caroli", "Cujam", "Cursa", "Dabih", "Dalim",
        "Deneb", "Deneb Algedi", "Denebola", "Diadem", "Dingolay", "Diphda", "Diwo", "Diya", "Dofida", "Dombay",
        "Dschubba", "Dubhe", "Dziban", "Ebla", "Edasich", "Electra", "Elgafar", "Elkurud", "Elnath", "Eltanin", "Emiw",
        "Enif", "Errai", "Fafnir", "Fang", "Fawaris", "Felis", "Felixvarela", "Flegetonte", "Fomalhaut", "Formosa",
        "Franz", "Fulu", "Funi", "Fumalsamakah", "Furud", "Fuyue", "Gacrux", "Gakyid", "Garnet Star", "Geminga",
        "Giausar", "Gienah", "Ginan", "Gloas", "Gomeisa", "Graffias", "Grumium", "Gudja", "Gumala", "Guniibuu",
        "Hadar", "Haedus", "Hamal", "Hassaleh", "Hatysa", "Helvetios", "Heze", "Hoggar", "Homam", "Horna", "Hunahp",
        "Hunor", "Iklil", "Illyrian", "Imai", "Intercrus", "Inquill", "Intan", "Irena", "Itonda", "Izar", "Jabbah",
        "Jishui", "Kaffaljidhma", "Kakkab", "Kalausi", "Kamuy", "Kang", "Karaka", "Kaus Australis",
        "Kaus Borealis", "Kaus Media", "Kaveh", "Kekouan", "Keid", "Khambalia", "Kitalpha", "Kochab",
        "Koeia", "Koit", "Kornephoros", "Kraz", "Kuma", "Kurhah", "La Superba", "Larawag", "Lerna", "Lesath",
        "Libertas", "Lich", "Liesma", "Lilii Borea", "Lionrock", "Lucilinburhuc", "Lusitânia", "Maasym", "Macondo",
        "Mago", "Mahasim", "Mahsati", "Maia", "Malmok", "Marfak", "Marfik", "Markab", "Markeb",
        "Márohu", "Marsic", "Matar", "Mazaalai", "Mebsuta", "Megrez", "Meissa", "Mekbuda", "Meleph", "Menkalinan",
        "Menkar", "Menkent", "Menkib", "Merak", "Merga", "Meridiana", "Merope", "Mesarthim", "Miaplacidus", "Mimosa",
        "Minchir", "Minelauva", "Mintaka", "Mira", "Mirach", "Miram", "Mirfak", "Mirzam", "Misam", "Mizar",
        "Moldoveanu", "Mönch", "Montuno", "Morava", "Moriah", "Mothallah", "Mouhoun", "Mpingo", "Muliphein", "Muphrid",
        "Muscida", "Musica", "Muspelheim", "Nahn", "Naledi", "Naos", "Nash", "Nashira", "Nasti",
        "Natasha", "Navi", "Nekkar", "Nembus", "Nenque", "Nervia", "Nihal", "Nikawiy", "Nosaxa", "Nunki", "Nusakan",
        "Nushagak", "Nyamien", "Ogma", "Okab", "Paikauhale", "Parumleo", "Peacock", "Petra", "Phact", "Phecda",
        "Pherkad", "Phoenicia", "Piautos", "Pincoya", "Pipoltr", "Pipirima", "Pleione", "Poerava", "Polaris",
        "Polaris Australis", "Polis", "Pollux", "Porrima", "Praecipua", "Prima Hyadum", "Procyon", "Propus",
        "Proxima Centauri", "Ran", "Rana", "Rapeto", "Rasalas", "Rasalgethi", "Rasalhague", "Rastaban", "Regor",
        "Regulus", "Revati", "Rigel", "Rigil Kentaurus", "Rosalíadecastro", "Rotanev", "Ruchbah", "Rukbat", "Sabik",
        "Saclateni", "Sadachbia", "Sadalbari", "Sadalmelik", "Sadalsuud", "Sadr", "Sagarmatha", "Saiph", "Salm",
        "Sāmaya", "Sansuna", "Sargas", "Sarin", "Sarir", "Sceptrum", "Scheat", "Schedar",
        "Secunda Hyadum", "Segin", "Seginus", "Sham", "Shama", "Sharjah", "Shaula", "Sheliak", "Sheratan", "Sika",
        "Sirius", "Situla", "Skat", "Solaris", "Spica", "Sterrennacht", "Stribor", "Sualocin", "Subra", "Suhail",
        "Sulafat", "Syrma", "Tabit", "Taika", "Taiyangshou", "Taiyi", "Talitha", "Tangra", "Tania Australis",
        "Tania Borealis", "Tapecue", "Tarazed", "Tarf", "Taygeta", "Tegmine", "Tejat", "Terebellum", "Tevel", "Thabit",
        "Theemin", "Thuban", "Tiaki", "Tianguan", "Tianyi", "Timir", "Tislit", "Titawin", "Tojil", "Toliman",
        "Tonatiuh", "Torcular", "Tuiren", "Tupã", "Tupi", "Tureis", "Ukdah", "Uklun", "Unukalhai", "Unurgunite", "Uruk",
        "Vega", "Veritate", "Vindemiatrix", "Wasat", "Wazn", "Wezen", "Wurren", "Xamidimura", "Xihe", "Xuange",
        "Yed Posterior", "Yed Prior", "Yildun", "Zaniah", "Zaurak", "Zavijava", "Zhang", "Zibal", "Zosma",
        "Zubenelgenubi", "Zubenelhakrabi", "Zubeneschamali"
    };
    
    
}
