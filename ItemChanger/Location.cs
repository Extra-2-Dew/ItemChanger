namespace ID2.ItemChanger;

public abstract class Location(string name, Area area, string flag)
{
	/// <summary>
	/// The name of the location as it appears when displayed to the user.
	/// </summary>
	public virtual string Name { get; protected set; } = name;
	/// <summary>
	/// The scene the location is in.
	/// </summary>
	public virtual Area Area { get; protected set; } = area;
	/// <summary>
	/// The save file flag that is used to uniquely identify the location from others.
	/// </summary>
	public virtual string Flag { get; protected set; } = flag;
	/// <summary>
	/// Delegate that fires when the location has been checked.
	/// </summary>
	public LocationFunc OnChecked { get; set; }
	/// <summary>
	/// Has this location been checked?
	/// </summary>
	public bool HasChecked { get; private set; }

	/// <summary>
	/// Fires when the location has been checked.
	/// </summary>
	/// <param name="itemAtLocation">The <see cref="ICItem"/> placed at this location.</param>
	public virtual void Checked(ICItem itemAtLocation)
	{
		OnChecked?.Invoke(this, itemAtLocation);
		HasChecked = true;
	}

	public delegate void LocationFunc(Location location, ICItem itemAtLocation);
}