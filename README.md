# Item Changer

This mod provides a framework for replacing the items at various locations throughout the game.

## Intallation
1. 

## Usage (for mod authors)
### Replacing items
`ID2.ItemChanger.Replacer.Replace(string itemName, string locationName, ICItem.ItemFunc onItemTriggeredCallback)`
<br>
<details>
	<summary>Example</summary>

```csharp
using ID2.ItemChanger;

void PlaceItemExample()
{
	// Places a Raft Piece on Pillow Fort - Shellbun Nest Key
	Replacer.PlaceItem("Pillow Fort - Shellbun Nest Key", "Raft Piece", OnItemTriggered);
}

private void OnItemTriggered(ICItem item)
{
	// Runs when any placed item is triggered (by a location being checked)
}
```
</details>

### Getting predefined locations and items
- `ID2.ItemChanger.Predefined.TryGetLocation(string name, out Location location)`
- `ID2.ItemChanger.Predefined.TryGetItem(string name, out ICItem item)`

### Sending "item get" notifications
- `ID2.ItemChanger.NotificationHandler.ShowNotification(string message, string icon, float displayTime = defaultDisplayTime)`
  - You give it the name of the vanilla icon you want to use
- `ID2.ItemChanger.NotificationHandler.ShowNotification(string message, Texture2D icon, float displayTime = defaultDisplayTime)`
  - You give it the texture of the custom icon you want to use

## Attributions
- Thanks to [dpinela](https://github.com/dpinela)'s work on
    [Item Changer for Death's Door](https://github.com/dpinela/DeathsDoor.ItemChanger),
	which I took inspiration from for the basis of applying it here.