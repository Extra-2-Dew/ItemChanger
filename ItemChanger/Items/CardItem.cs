namespace ID2.ItemChanger;

/// <summary>
/// A Dream World card.
/// </summary>
class CardItem : ICItem
{
	public override string Icon => "Card";

	public CardItem(string displayName) : base(displayName)
	{
	}

	public override void Trigger()
	{
		ModCore.Utility.MainSaver.GetSaver("/local/cards").SaveInt(Flag, 1);
		base.Trigger();
	}
}