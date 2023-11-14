using Godot;
using System;
using System.Collections;

public partial class TileMapCreator : TileMap
{
    [Export] int size = 32;
    [Export] float generationThresholdBase = 0;


    Godot.Collections.Array<Vector2I> cellsArray = new Godot.Collections.Array<Vector2I>();

    FastNoiseLite baseNoise;
    FastNoiseLite vegetationNoise;
    FastNoiseLite tempreatureNoise;

    public override void _Ready()
	{
        
        bool[,] boolCellsArr = GeneratorClass.CreateBool2DArray(baseNoise, size, generationThresholdBase);
        float[,] vegetationArr = GeneratorClass.CreateFloat2DArray(vegetationNoise, size);
        float[,] temperatureArr = GeneratorClass.CreateFloat2DArray(tempreatureNoise, size);

        TileClass[,] tileClassArray = GeneratorClass.CreateTileClass2DArray(boolCellsArr, vegetationArr, temperatureArr, size);
        cellsArray = GeneratorClass.TCArrToVector2IArr(tileClassArray);
        generateWorld(cellsArray);
	}
    //Creates a Tilemap from a custom TileClass array


    private void generateWorld(Godot.Collections.Array<Vector2I> cellsArray)
    {
        this.SetCellsTerrainConnect(0, cellsArray, 0, 0, true);
    }
}