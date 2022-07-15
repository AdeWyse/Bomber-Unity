using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject wall;
    public GameObject floor;
    public GameObject[] characters;
    public int gridXSize = 10;
    public int gridYSize = 10;
    public Vector3[,] gridPos;

    private GameController gameController;
    public int numberChar;
    public bool isControllable = true;


    // Start is called before the first frame update
    void Start()
    {
        gridPos = new Vector3[gridXSize,gridYSize];
        gameController = GameObject.Find("GameController").GetComponent<GameController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateMap(){
        numberChar = gameController.numberChar;
        if(numberChar == 0)
        {
            numberChar = 2;
        }
        isControllable = gameController.isControllable;
        generateFloorGrid();
        generateSecondLayer();

    }

    //Generates the second layer of objects. It includes walls, obstacles and  characters.
   private void generateSecondLayer()
    {
        GameObject[] toSpawn = new GameObject[2];
        toSpawn[0] = wall;
        toSpawn[1] = obstacle;
        for (int i = 0; i < gridXSize; i++)
        {
            for (int j = 0; j < gridYSize; j++)
            { int random = Random.Range(0, 3);
                var position = new Vector3(this.gridPos[i, j].x, 1.002f, this.gridPos[i, j].z);
                if(random != 2)
                {
                     var spawnedObject = Instantiate(toSpawn[Random.Range(0,2)], position, Quaternion.identity);
                }
            }

        }
    }

    //Decides how many and types of characters that will be generated
    private GameObject[] characterSelection(int numberOfCharacters, bool isControlable)
    {
        GameObject[] selected = new GameObject[numberOfCharacters];
        for(int i = numberOfCharacters - 1; i > 0; i--)
        {
            selected[i] = characters[0];
        }
        if (isControlable)
        {
            selected[0] = characters[1];
        }
        else
        {
            selected[0] = characters[0];
        }
        return selected;
    }

    //Generates the initial floor and grid location
    private void generateFloorGrid(){
        for(int i = 0; i <gridXSize; i++){
            for(int j = 0; j <gridYSize; j++){
                var position =  new Vector3(i, 0, j);
                var spawnedTile = Instantiate(floor, position , Quaternion.identity);
                gridPos[i, j] = position;

            }

        }
    }
}
