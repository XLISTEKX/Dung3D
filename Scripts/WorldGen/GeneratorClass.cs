using Godot;
using System;
using System.Collections;

public static class GeneratorClass
{
    public static bool[,] CreateBool2DArray(FastNoiseLite noise, int size, float generationThreshold)
    {
        bool[,] boolArray = new bool[size, size];

        for(int i = 0; i < size; i++) 
            {
                for(int j = 0; j < size; j++)
                {
                    if(noise.GetNoise2D(i,j) > generationThreshold)
                    {
                        boolArray[i,j] = true;
                    }
                }
            }
        return boolArray;
    }

    public static float[,] CreateFloat2DArray(FastNoiseLite noise, int size)
    {
        float[,] floatArr = new float[size, size];

        for(int i = 0; i < size; i++) 
            {
                for(int j = 0; j < size; j++)
                {
                        floatArr[i,j] = noise.GetNoise2D(i,j);
                }
            }
        return floatArr;
    }

    public static TileClass[,] CreateTileClass2DArray(bool[,] tileExists, float[,] vegetationArray, float[,] temperatureArray, int size) 
    {
        TileClass[,] tileClassArray = new TileClass[size, size];
        for(int i = 0; i < size; i++) 
        {
            for(int j = 0; j < size; j++)
            {
                if(tileExists[i,j])
                {
                    tileClassArray[i,j] = new TileClass(i,j,vegetationArray[i,j],temperatureArray[i,j]);
                }
            }
        }
        return tileClassArray;
    }

    public static Godot.Collections.Array<Vector2I> TCArrToVector2IArr(TileClass[,] tcArr)
    {
        Godot.Collections.Array<Vector2I> cellsArray = new Godot.Collections.Array<Vector2I>();
        foreach(TileClass item in tcArr)
        {
            cellsArray.Add(new Vector2I(item.GetPosX(),item.GetPosZ()));
        }
        return cellsArray;
    }
}