using Godot;
using System;

public static class MapArray
{
    public static Godot.Collections.Array<Vector2I> CreateMapArray(FastNoiseLite noise, int size, float generationThreshold) 
    {
        Godot.Collections.Array<Vector2I> vectorArr = new Godot.Collections.Array<Vector2I>();
        for(int i = 0; i < size; i++) 
        {
            for(int j = 0; j < size; j++)
            {
                if(noise.GetNoise2D(i,j) > generationThreshold)
                {
                    vectorArr.Add(new(i,j));
                }
            }
        }
        return vectorArr;
    }
    //Ziuuuuu loops through two arrays and reads the level based on noise and it's threshold
    //creates a Vector2I array with coordinates of existing fields



    /*
    public static Godot.Collections.Array<Vector2I> MapVector2I(Godot.Collections.Array<Vector2I> inputArray, Func<Vector2I, Vector2I> function) 
    {
        Godot.Collections.Array<Vector2I> result = new Godot.Collections.Array<Vector2I>();
        foreach(Vector2I item in inputArray)
        {
            Vector2I mappedItem = function(item);
            result.Add(mappedItem);
        }
        return result;
    }
    */

}