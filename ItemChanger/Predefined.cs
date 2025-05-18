using System.Collections.Generic;
using System.Linq;

namespace ID2.ItemChanger;

/// <summary>
/// Predefined lists of items and locations.
/// These are the items and locations that exist in the vanilla game.
/// </summary>
public class Predefined
{
	/// <summary>
	/// Locations that exist in the vanilla game.
	/// </summary>
	private static readonly List<Location> predefinedLocations =
	[
		// Pillow Fort
		new ChestLocation("Pillow Fort - Treasure Chest", Area.PillowFort, "Dungeon_PuzzleChest-39--28"),
		new PickupLocation("Pillow Fort - Shellbun Nest Key", Area.PillowFort, "KeyChest-7--44"),
		new ChestLocation("Pillow Fort - Crayon Chest", Area.PillowFort, "Dungeon_EnemyChest-3--29"),
		new PickupLocation("Pillow Fort - Safety Jenny Gate Key", Area.PillowFort, "KeyChest-17--17"),
		new ChestLocation("Pillow Fort - Boss Reward Chest", Area.PillowFort, "Dungeon_Chest-37--4"),
	];

	/// <summary>
	/// Items that exist in the vanilla game.
	/// </summary>
	private static readonly List<ICItem> predefinedItems =
	[
		// Player items
		new MeleeItem("Melee") { Progressive = true },
		new MeleeItem("Stick") { Level = 0 },
		new MeleeItem("Fire Sword") { Level = 1 },
		new MeleeItem("Fire Mace") { Level  = 2 },
		new MeleeItem("EFCS") { Level = 3 },
		new PlayerItem("Forcewand") { Icon = "Forcewand1" },
		new PlayerItem("Dynamite") { Icon = "Dynamite1" },
		new PlayerItem("Ice Ring") { Icon = "Icering1" },
		new PlayerItem("Chain") { Icon = "chain" },
		new PlayerItem("Headband") { Icon = "BlueHeadBand" },
		new PlayerItem("Tome") { Icon = "Tome" },
		new PlayerItem("Amulet") { Icon = "Amulet" },
		new PlayerItem("Tracker") { Icon = "Tracker" },
		new PlayerItem("Raft Piece") { Flag = "raft", Icon = "RaftPiece" },
		new PlayerItem("Secret Shard") { Flag = "shards", Icon = "SecretShard" },
		new PlayerItem("Forbidden Key") { Flag = "evilKeys", Icon = "ForbiddenKey" },
		new PlayerItem("Lockpick") { Flag = "keys", Icon = "MasterKey" },

		// Keys
		new KeyItem("Pillow Fort Key", Area.PillowFort),
		new KeyItem("Sand Castle Key", Area.SandCastle),
		new KeyItem("Art Exhibit Key", Area.ArtExhibit),
		new KeyItem("Trash Cave Key", Area.TrashCave),
		new KeyItem("Flooded Basement Key", Area.FloodedBasement),
		new KeyItem("Potassium Mine Key", Area.PotassiumMine),
		new KeyItem("Boiling Grave Key", Area.BoilingGrave),
		new KeyItem("Grand Library Key", Area.GrandLibrary),
		new KeyItem("Sunken Labyrinth Key", Area.SunkenLabyrinth),
		new KeyItem("Machine Fortress Key", Area.MachineFortress),
		new KeyItem("Dark Hypostyle Key", Area.DarkHypostyle),
		new KeyItem("Tomb of Simulacrum Key", Area.TombOfSimulacrum),
		new KeyItem("Syncope Key", Area.DreamDynamite),
		new KeyItem("Bottomless Tower Key", Area.DreamIce),
		new KeyItem("Antigram Key", Area.DreamFireChain),
		new KeyItem("Quietus Key", Area.DreamAll),

		// Outfits
		new OutfitItem("Jenny Dew Outfit", 3) { Icon = "SuitJenny" },
		new OutfitItem("Swimsuit Outfit", 4) { Icon = "SuitSwim" },
		new OutfitItem("Tippsie Outfit", 1) { Icon = "SuitTippsie" },
		new OutfitItem("Little Dude Outfit", 6) { Icon = "SuitCard" },
		new OutfitItem("Tiger Jenny Outfit", 5) { Icon = "SuitArmor" },
		new OutfitItem("Ittle Dew 1 Outfit", 2) { Icon = "SuitIttleOriginal" },
		new OutfitItem("Delinquent Outfit", 7) { Icon = "SuitDelinquent" },
		new OutfitItem("Jenny Berry Outfit", 10) { Icon = "Custom/SuitJennyBerry" },
		new OutfitItem("Apathetic Frog Outfit", 8) { Icon = "Custom/SuitApaFrog" },
		new OutfitItem("That Guy Outfit", 9) { Icon = "Custom/SuitThatGuy" },

		// Cards
		new CardItem("Card 1 - Fishbun") { Flag = "Fishbun" },
		new CardItem("Card 2 - Stupid Bee") { Flag = "StupidBee" },
		new CardItem("Card 3 - Safety Jenny") { Flag = "JennySafety" },
		new CardItem("Card 4 - Shellbun") { Flag = "Shellbun" },
		new CardItem("Card 5 - Spikebun") { Flag = "Spikebun" },
		new CardItem("Card 6 - Feral Gate") { Flag = "FeralGate" },
		new CardItem("Card 7 - Candy Snake") { Flag = "CandySnake" },
		new CardItem("Card 8 - Hermit Legs") { Flag = "HermitLegs" },
		new CardItem("Card 9 - Ogler") { Flag = "Ogler" },
		new CardItem("Card 10 - Hyperdusa") { Flag = "Hyperdusa" },
		new CardItem("Card 11 - Evil Easel") { Flag = "EvilEasel" },
		new CardItem("Card 12 - Warnip") { Flag = "Warnip" },
		new CardItem("Card 13 - Octacle") { Flag = "Octacle" },
		new CardItem("Card 14 - Rotnip") { Flag = "Rotnip" },
		new CardItem("Card 15 - Bee Swarm") { Flag = "BeeSwarm" },
		new CardItem("Card 16 - Volcano") { Flag = "Volcano" },
		new CardItem("Card 17 - Jenny Shark") { Flag = "JennyShark" },
		new CardItem("Card 18 - Swimmy Roger") { Flag = "SwimmyRoger" },
		new CardItem("Card 19 - Bunboy") { Flag = "Bunboy" },
		new CardItem("Card 20 - Spectre") { Flag = "Spectre" },
		new CardItem("Card 21 - Return of Brutus") { Flag = "Brutus" },
		new CardItem("Card 22 - Jelly") { Flag = "Jelly" },
		new CardItem("Card 23 - Skullnip") { Flag = "Skullnip" },
		new CardItem("Card 24 - Slayer Jenny") { Flag = "JennySlayer" },
		new CardItem("Card 25 - Titan") { Flag = "Titan" },
		new CardItem("Card 26 - Chilly Roger") { Flag = "ChillyRoger" },
		new CardItem("Card 27 - Jenny Flower") { Flag = "JennyFlower" },
		new CardItem("Card 28 - Hexrot") { Flag = "Hexrot" },
		new CardItem("Card 29 - Jenny Mole") { Flag = "JennyMole" },
		new CardItem("Card 30 - Jenny Bun (Unemployed)") { Flag = "JennyBun" },
		new CardItem("Card 31 - Jenny Cat") { Flag = "JennyCat" },
		new CardItem("Card 32 - Jenny Mermaid") { Flag = "JennyMermaid" },
		new CardItem("Card 33 - Jenny Berry (Vacation)") { Flag = "JennyBerry" },
		new CardItem("Card 34 - Mapman") { Flag = "Mapman" },
		new CardItem("Card 35 - Cyberjenny") { Flag = "Cyberjenny" },
		new CardItem("Card 36 - Le Biadlo") { Flag = "LeBiadlo" },
		new CardItem("Card 37 - Lenny") { Flag = "Lenny" },
		new CardItem("Card 38 - Passel Carver") { Flag = "Passel" },
		new CardItem("Card 39 - Tippsie") { Flag = "Tippsie" },
		new CardItem("Card 40 - Ittle Dew") { Flag = "IttleDew" },
		new CardItem("Card 41 - Napping Fly") { Flag = "NappingFly" },

		// Healing items
		new HealingItem("Box of Crayons") { IncreaseMax = true, Flag = "hpcs", Icon = "Fullpaper" },
		new HealingItem("Red Heart") { Amount = 4, Icon = "Custom/ChestHeartRed" },
		new HealingItem("Blue Heart") { Amount = 8, Icon = "Custom/ChestHeartBlue" },
		new HealingItem("Green Heart") { Amount = 12, Icon = "Custom/ChestHeartGreen" },
		new HealingItem("Yellow Heart") { Amount = 20, Icon = "ChestHeart" },
		new HealingItem("Ice Cream") { Amount = 100 },
	];

	/// <summary>
	/// A Dictariony lookup table to improve performance when looping through locations.
	/// </summary>
	private static readonly Dictionary<string, Location> locationMap = predefinedLocations.ToDictionary(location => location.Name);
	/// <summary>
	/// A Dictariony lookup table to improve performance when looping through items.
	/// </summary>
	private static readonly Dictionary<string, ICItem> itemMap = predefinedItems.ToDictionary(item => item.DisplayName);

	/// <summary>
	/// Adds custom locations to the predefined list of locations.
	/// </summary>
	/// <param name="customLocations">A list of custom locations to add.</param>
	public static void AddCustomLocations(List<Location> customLocations)
	{
		foreach (Location location in customLocations)
		{
			predefinedLocations.Add(location);
			locationMap.Add(location.Name, location);
		}
	}

	/// <summary>
	/// Adds custom items to the predefined list of items.
	/// </summary>
	/// <param name="customItems">A list of custom items to add.</param>
	public static void AddCustomItems(List<ICItem> customItems)
	{
		foreach (ICItem item in customItems)
		{
			predefinedItems.Add(item);
			itemMap.Add(item.DisplayName, item);
		}
	}

	/// <summary>
	/// Returns true if a location with name <paramref name="name"/> exists, false otherwise.
	/// </summary>
	/// <param name="name">The name of the location.</param>
	/// <param name="location">The found location, null if not found.</param>
	public static bool TryGetLocation(string name, out Location location)
	{
		return locationMap.TryGetValue(name, out location);
	}

	/// <summary>
	/// Returns the location with name <paramref name="name"/>, null otherwise.
	/// </summary>
	/// <param name="name">The name of the location.</param>
	public static Location GetLocation(string name)
	{
		return locationMap.First(location => location.Key == name).Value;
	}

	/// <summary>
	/// Returns true if an item with name <paramref name="name"/> exists, false otherwise.
	/// </summary>
	/// <param name="name">The name of the item.</param>
	/// <param name="item">The found item, null if not found.</param>
	/// <returns></returns>
	public static bool TryGetItem(string name, out ICItem item)
	{
		return itemMap.TryGetValue(name, out item);
	}

	/// <summary>
	/// Returns the item with name <paramref name="name"/>, null otherwise.
	/// </summary>
	/// <param name="name">The name of the item.</param>
	public static ICItem GetItem(string name)
	{
		return itemMap.First(item => item.Key == name).Value;
	}
}