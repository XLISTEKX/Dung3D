using Godot;

namespace XGeneric.Inventory
{
	public enum ItemType
	{
		None = 0,
		Weapon = 1,
		Useable = 2,
		
	}
	
	[GlobalClass][Tool]
	public partial class InvItem : Resource
	{
		[Export] public int ID;
		[Export] public string Name;
		[Export] public Texture2D ItemIcon;
		[Export] public PackedScene InWorldItem;
		[Export] public RarityTypes rarity;
		[Export] public ItemType itemType;
		
	}
}

