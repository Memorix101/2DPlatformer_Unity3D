using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private string _filePath;

    public GameObject Tile0, Tile1, Tile2, Coin, Player, TestActor, Enemy, Exit;

    // Start is called before the first frame update    
    void Start()
    {
        if (File.Exists((Application.dataPath + "/Content/Levels/Test.map")))
        {
            Debug.Log("File Found at: " + Application.dataPath);
            _filePath = Application.dataPath + "/Content/Levels/Test.map";
            LoadLevel("Test.map");
        }
        else
        {
            Debug.LogError("map file not found!");
        }
    }

    //level loader
    public void LoadLevel(string name)
    {
        /*
         if (mapItems == null)
            mapItems = new List<ItemTile>();

        if (mapCoins == null)
            mapCoins = new List<Coins>();

        if (mapEnemies == null)
            mapEnemies = new List<Enemy>();
            */

        //bool levelDone = false;
        //int coins = 0;
        //filePath = Game1.content.RootDirectory.ToString() + "\\Levels\\" + name + ".map";

        int x = 0;
        int y = 0;

        GameObject go;

        foreach (string line in File.ReadAllLines(_filePath))
        {
            x = 0;

            foreach (char token in line)
            {
                switch (token)
                {

                    // Blank space
                    case '.':
                        //
                        break;

                    case '#':
                        //mapItems.Add(new ItemTile(new Vector2(x * 32, y * 32), 1f, ItemTileType.Block));
                        go = Instantiate(Tile0, new Vector3(x * 0.32f, y * -0.32f, 0f), Quaternion.identity);
                        go.transform.parent = transform;
                        break;

                    case '+':
                        //mapItems.Add(new ItemTile(new Vector2(x * 32, y * 32), 1f, ItemTileType.BlockC));
                        go = Instantiate(Tile1, new Vector3(x * 0.32f, y * -0.32f, 0f), Quaternion.identity);
                        go.transform.parent = transform;
                        break;

                    case '-':
                        //mapItems.Add(new ItemTile(new Vector2(x * 32, y * 32), 1f, ItemTileType.DarkBrick));
                        go = Instantiate(Tile2, new Vector3(x * 0.32f, y * -0.32f, 0f), Quaternion.identity);
                        go.transform.parent = transform;
                        break;

                    case 'c':
                        //mapCoins.Add(new Coins(new Vector2(x * 32, y * 32)));
                        go = Instantiate(Coin, new Vector3(x * 0.32f, y * -0.32f, 0f), Quaternion.identity);
                        go.transform.parent = transform;
                        break;

                    case 'p':
                        //player = new Player(new Vector2(x * 32, y * 32));
                        go = Instantiate(Player, new Vector3(x * 0.32f, y * -0.32f, 0f), Quaternion.identity);
                        //go.transform.parent = transform;
                        break;

                    case 'q':
                        //testActor = new TestActor(new Vector2(x * 32, y * 32));
                        go = Instantiate(TestActor, new Vector3(x * 0.32f, y * 0.32f, 0), Quaternion.identity);
                        go.transform.parent = transform;
                        break;

                    case 'x':
                        //exit = new Exit(new Vector2(x * 32, y * 32));
                        go = Instantiate(Exit, new Vector3(x * 0.32f, y * -0.32f, 0f), Quaternion.identity);
                        go.transform.parent = transform;
                        break;

                    case 'e':
                        //mapEnemies.Add(new Enemy(new Vector2(x * 32, y * 32)));
                        go = Instantiate(Enemy, new Vector3(x * 0.32f, y * -0.32f, 0f), Quaternion.identity);
                        go.transform.parent = transform;
                        break;

                        // Unknown tile type character
                        //    default:
                        //      throw new Exception(String.Format("Wrong Char"));
                }

                x++;
            }

            y++;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        //GUI.Box(new Rect(0, 0, 100, 90), "Loader Menu");
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), testTexture2D, ScaleMode.StretchToFill, false);
        //GUI.DrawTexture(new Rect(10, 10, 320, 200), pix.PixMaps[0].Texture);
    }
}
