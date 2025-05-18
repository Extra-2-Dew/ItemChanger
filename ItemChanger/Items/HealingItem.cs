namespace ID2.ItemChanger;

class HealingItem : ICItem
{
	public float Amount { get; set; }
	public bool IncreaseMax { get; set; }

	public HealingItem(string displayName) : base(displayName)
	{
	}

	public override void Trigger()
	{
		Entity player = ModCore.Utility.GetPlayer();
		Killable killable = player.GetEntityComponent<Killable>();

		// If crayons
		if (IncreaseMax)
		{
			killable.MaxHp += 1;
			killable.CurrentHp = killable.MaxHp;
			player.SetStateVariable(Flag, player.GetStateVariable(Flag) + 1);
		}
		// If heart
		else
		{
			killable.CurrentHp += Amount;
		}

		base.Trigger();
	}
}