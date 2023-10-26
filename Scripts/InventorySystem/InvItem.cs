using Godot;
using System;
[GlobalClass][Tool]
public partial class InvItem : Resource
{
	[Export] public int ID;
	[Export] public string Name;
	[Export] public Texture ItemIcon;
	[Export] public PackedScene InWorldItem;
	
}
