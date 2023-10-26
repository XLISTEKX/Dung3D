using Godot;
using System;

namespace XGeneric.Inventory
{
	[GlobalClass][Tool]
	public partial class InvItem : Resource
	{
		[Export] public int ID;
		[Export] public string Name;
		[Export] public Texture2D ItemIcon;
		[Export] public PackedScene InWorldItem;
		[Export] public RarityTypes rarity;
		
	}
}

