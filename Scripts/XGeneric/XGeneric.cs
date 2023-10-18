using System.Collections.Generic;

namespace XGeneric.Utilities
{
	public class XGeneric<T>
	{
		public static T[] GetTilesByDictionary(Dictionary<T, T> dictionary, T last)
		{
			T lastTile = last;
			List<T> values = new List<T>() { lastTile };

			while (dictionary.ContainsKey(lastTile) )
			{
				if (dictionary[lastTile] == null)
					break;

				lastTile = dictionary[lastTile];

				values.Add(lastTile);
			}
			values.Remove(lastTile);

			values.Reverse();

			return values.ToArray();
		}
	}
}