using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomMapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public const int mapSizeX = 40;
    public const int mapSizeY = 40;
    public List<string> tileNames;
    public List<double> tileProb;
    Dictionary<string, Tile> tileTypes;
    int[,] mapID;
    System.Random randomGen;
    // Start is called before the first frame update
    void Start()
    {
        tileTypes = new Dictionary<string, Tile>();
        randomGen = new System.Random();
        initTileTypes();
        generateRandomMap();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void generateRandomMap()
    {
        
        mapID = new int[mapSizeX, mapSizeY];
        for(int i = 0; i < mapSizeY; ++i)
        {
            for(int j = 0; j < mapSizeX; ++j)
            {
                mapID[i, j] = 0;
                for(int k = 0; k < tileNames.Count; k++)
                {
                    double temp = randomGen.NextDouble();
                    if(temp <= tileProb[k])
                    {
                        mapID[i, j] = k + 1;
                        break;
                    }
                }
                if (i == 0 || j == 0) mapID[i, j] = 1;
                if(mapID[i,j] > 0)
                {
                    tilemap.SetTile(new Vector3Int(i - 21, j, 0), tileTypes[tileNames[mapID[i,j] - 1]]);
                }
            }
        }


    }

    void initTileTypes()
    {
        AddTile("rock_1_big", "Sprites/Interactives/Immobile/rock/rock/1/rock_1_big", 0.03);
        AddTile("rock_1_mid", "Sprites/Interactives/Immobile/rock/rock/1/rock_1_mid", 0.03);
        AddTile("rock_1_small", "Sprites/Interactives/Immobile/rock/rock/1/rock_1_small", 0.03);
    }

    void AddTile(string typeName, string spriteLocation, double prob)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        Sprite tempSprite = Resources.Load<Sprite>(spriteLocation);
        if (tempSprite == null) print("Failed to find sprite!");
        tile.sprite = tempSprite;
        tileTypes.Add(typeName, tile);
        tileNames.Add(typeName);
        tileProb.Add(prob);
    }
}
