namespace ID2.ItemChanger;

class OutfitItem : ICItem
{
	public OutfitItem(string displayName, int outfitId) : base(displayName)
	{
		Flag = "outfit" + outfitId;
	}

	public override void Trigger()
	{
		// Save to Changing Tent
		ModCore.Utility.MainSaver.GetSaver("/local/world").SaveInt(Flag, 1);
		base.Trigger();
	}
}