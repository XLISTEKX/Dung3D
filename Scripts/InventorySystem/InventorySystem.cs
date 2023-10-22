
using System.Collections.Generic;
using System.Text.Json;
using Godot;

namespace XGeneric.Inventory
{
	public static class InventorySystem
	{
		
		public static Item[] GetAllItems()
		{
			Item item = new("S", 1, "S");
			
			ResourceSaver.Save(item,"res://Prefabs/Items/s.tres");
			
			var itemsPaths = DirAccess.GetFilesAt("res://Prefabs/Items");
			List<Item> output = new();

			Item t = ResourceLoader.Load("res://Prefabs/Items/s.tres", "",ResourceLoader.CacheMode.Ignore) as Item;

			// var k = ResourceLoader.Load("res://Prefabs/Items/s.tres");
			// GD.Print(k);
			
			// foreach(string path in itemsPaths)
			// {
			// 	output.Add(ResourceLoader.Load<Item>("res://Prefabs/Items/" + path));
			// }
			
			return null;
		}
		
		public static void ReadFromJson(string path)
		{
			
			
			// if(File.Exists(path))
			// {
				
			// 	var data = Json.ParseString(File.ReadAllText(path));
			// 	return data;
			// }
			// else
			// {
			// 	return new Variant();
			// }
			using var file = FileAccess.Open("res://Scripts/Items/Items.json", FileAccess.ModeFlags.Read);
   			 string content = file.GetAsText();


			Item deptObj = JsonSerializer.Deserialize<Item>(content);
				
		}
	}
}