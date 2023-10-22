using System.Collections.Generic;
using System.Linq;
using Godot;

namespace XGeneric.Inventory
{
	public static class InventorySystem
	{
		// Reads Json file with given path -> output Item Class Array
		public static Item[] ReadAllFromJson(string path)
		{
			List<Item> output = new();
			var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
			
			var jsonString = file.GetAsText();
				
			Json json = new();	
			var parseResult = json.Parse(jsonString);
			if(parseResult != Error.Ok)
			{
				GD.Print("Error - Load Json");
			}	
			var tempData = new Godot.Collections.Dictionary<string, Variant>(json.Data.AsGodotDictionary());
			
			foreach(string key in tempData.Keys)
			{
				var data = tempData[key].AsGodotDictionary();
				
				Item item = new(data["name"].ToString(), data["id"].AsInt32(), data["icon"].ToString());
				output.Add(item);
			}
			
			return output.ToArray();
		}
		public static Item GetItemFromJson(int itemID = 0, string path = "res://Scripts/Items/Items.json")
		{
			var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
			
			var jsonString = file.GetAsText();
			
			Json json = new();	
			var parseResult = json.Parse(jsonString);
			if(parseResult != Error.Ok)
			{
				GD.Print("Error - Load Json");
			}	
			
			var tempData = new Godot.Collections.Dictionary<string, Variant>(json.Data.AsGodotDictionary());
			var key = itemID.ToString();
			if(tempData.ContainsKey(key))
			{
				var itemData = tempData[key].AsGodotDictionary();
				
				Item tempItem = new(itemData["name"].ToString(), itemID, itemData["icon"].ToString());
			
				return tempItem;
			}
			return null;
			
		}
	}
}