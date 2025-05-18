namespace ID2.ItemChanger;

public abstract class ICItem(string displayName)
{
	/// <summary>
	/// The name of the item as it appears in item get notifications and recent items display.
	/// </summary>
	public virtual string DisplayName { get; protected set; } = displayName;
	/// <summary>
	/// The name of the icon. If using a custom icon, add 'Custom/' before the name of the icon.
	/// </summary>
	public virtual string Icon { get; set; } = "";
	/// <summary>
	/// The save file flag for the item. No spaces or special characters are allowed.
	/// </summary>
	public virtual string Flag { get; set; } = "";

	/// <summary>
	/// Fires when the item is obtained
	/// </summary>
	public virtual void Trigger()
	{
	}

	/// <summary>
	/// Returns a valid save file flag based on the given name.
	/// </summary>
	/// <param name="flagName">The name of the flag.</param>
	public static string GetFlagFromName(string flagName)
	{
		return flagName.Replace(" ", "").ToLower();
	}
}