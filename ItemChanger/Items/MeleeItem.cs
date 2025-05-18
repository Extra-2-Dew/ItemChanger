namespace ID2.ItemChanger;

/// <summary>
/// Stick, Fire Sword, Fire Mace, and EFCS
/// </summary>
class MeleeItem : ICItem
{
	public override string Flag => "melee";
	public int Level { get; set; }
	public bool Progressive { get; set; }

	public MeleeItem(string displayName) : base(displayName)
	{
	}

	public override void Trigger()
	{
		Entity player = ModCore.Utility.GetPlayer();
		int newLevel = Progressive ? player.GetStateVariable(Flag) + 1 : Level;
		player.SetStateVariable(Flag, newLevel);

		DisplayName = newLevel switch
		{
			1 => "Fire Sword",
			2 => "Fire Mace",
			3 => "EFCS",
			_ => "Stick",
		};

		Icon = newLevel switch
		{
			0 => "Custom/Stick",
			1 => "melee1",
			2 => "melee2",
			3 => "EFCS",
			_ => "",
		};

		base.Trigger();
	}
}