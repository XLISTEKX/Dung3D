using Godot;
using System;
using System.Collections;

public partial class TileMapCreator : TileMap
{
    [Export] int size = 32;
    [Export] float generationThresholdBase = 0;


    FastNoiseLite baseNoise = new();
    FastNoiseLite vegetationNoise = new();
    FastNoiseLite rocksNoise = new();

    public override void _Ready()
	{
        ApplyNoiseSettings();

        bool[,] boolCellsArr = GeneratorClass.CreateBool2DArray(baseNoise, size, generationThresholdBase);
        float[,] vegetationArr = GeneratorClass.CreateFloat2DArray(vegetationNoise, size);
        float[,] rocksArr = GeneratorClass.CreateFloat2DArray(rocksNoise, size);

        //TileClass[,] tileClassArray = GeneratorClass.CreateTileClass2DArray(boolCellsArr, vegetationArr, rocksArr, size);
        //cellsArray = GeneratorClass.TCArrToVector2IArr(tileClassArray);
        // -- -- --
        //This is unnecessary, it was meant to be used as a weapper for every single tile
        //So every single tile was supposed to be it's own object with some attributes, but that is not needed
        //instead 2d arrays will be used to keep track of attributes

        //GenerateWorldBasic(boolCellsArr, size);
        GenerateWorldComplex(boolCellsArr, vegetationArr, rocksArr, size);
	}


    private void GenerateWorldBasic(/*Godot.Collections.Array<Vector2I> cellsArray*/ bool[,] boolArr, int size)
    {
        /*
        //For TileClass solution, doesn't work

        foreach(Vector2I item in cellsArray)
        {
        SetCell(0, item);
        }
        */

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                if (boolArr[i,j])
                {
                    SetCell(0, new Vector2I(i,j), 0, new Vector2I(0,0));
                }
                
            }
        }
    }
    private void GenerateWorldComplex(bool[,] boolArr, float[,] vegeArr, float[,] rockArr, int size)
    {
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                if (boolArr[i,j])
                {
                    SetCell(0, new Vector2I(i,j), 0, new Vector2I(0,0));
                    if(rockArr[i,j] < -0.6)
                    {
                        SetCell(1, new Vector2I(i,j), 0, new Vector2I(2,0));
                    }
                    else if(rockArr[i,j] < -0.4)
                    {
                        SetCell(2, new Vector2I(i,j), 0, new Vector2I(2,1));
                    }
                    else if(rockArr[i,j] < -0.1)
                    {
                        SetCell(3, new Vector2I(i,j), 0, new Vector2I(2,2));
                    }

                    if(vegeArr[i,j] > 0.6)
                    {
                        SetCell(4, new Vector2I(i,j), 0, new Vector2I(1,0));
                    }
                    else if(vegeArr[i,j] > 0.4)
                    {
                        SetCell(5, new Vector2I(i,j), 0, new Vector2I(1,1));
                    }
                    else if(vegeArr[i,j] > 0.1)
                    {
                        SetCell(6, new Vector2I(i,j), 0, new Vector2I(1,2));
                    }
                }
            }
        }
        //#TODO unuglify
    }

    private void ApplyNoiseSettings()
    {
        RandomNumberGenerator seed = new();

        baseNoise.Seed = seed.RandiRange(0,64);
		baseNoise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
		baseNoise.FractalOctaves = 1;
		baseNoise.FractalGain = 0;
		baseNoise.Frequency = 0.12f;

        vegetationNoise.Seed = seed.RandiRange(0,64);
		vegetationNoise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
		vegetationNoise.FractalOctaves = 1;
		vegetationNoise.FractalGain = 0;
		vegetationNoise.Frequency = 0.04f;

        rocksNoise.Seed = seed.RandiRange(0,64);
		rocksNoise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
		rocksNoise.FractalOctaves = 1;
		rocksNoise.FractalGain = 0;
		rocksNoise.Frequency = 0.06f;
    }
}