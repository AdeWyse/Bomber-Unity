using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject wall;
    public GameObject floor;
    public GameObject[] characters;
    public int gridXSize;
    public int gridZSize;
    public Vector3[,] gridPos;
    public GameObject[,] secondLayerObjects;
    public bool[,] isInSecondLayer;

    private GameController gameController;
    public int numberChar;
    public bool isControllable = true;

    public int[,] charactersInitialPos;

    public GameObject[] selected;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gridXSize = gameController.gridXSize;
        gridZSize = gameController.gridZSize;
        gridPos = new Vector3[gridXSize,gridZSize];
        secondLayerObjects = new GameObject[gridXSize , gridZSize];
        isInSecondLayer = new bool[gridXSize, gridZSize];
        //Initializes the array containing the grid coodinates of the inital positions for the characters
        charactersInitialPos = new int[4, 2];

        charactersInitialPos[0, 0] = 0;
        charactersInitialPos[0, 1] = 0;
        charactersInitialPos[1, 0] = 0;
        charactersInitialPos[1, 1] = gridZSize-1;
        charactersInitialPos[2, 0] = gridXSize-1;
        charactersInitialPos[2, 1] = 0;
        charactersInitialPos[3, 0] = gridXSize-1;
        charactersInitialPos[3, 1] = gridZSize-1;

    }

    public void generateMap(){
        numberChar = gameController.numberChar;
        if(numberChar == 0)
        {
            numberChar = 2;
        }
        isControllable = gameController.isControllable;
        generateFloorGrid();
        GameObject[] characters = characterSelection(numberChar, isControllable);
        generateSecondLayer(characters);
        CheckCharacterPos(characters);
    }

    //Generates the second layer of objects. It includes walls, obstacles and  characters. It needs an array whith the characters that are gonna be created
   private void generateSecondLayer(GameObject[] characters)
    {
        Vector3[,] gridPositionSpawned = gridPos;
        //Spawns the characters
        for (int c = 0; c < characters.Length; c++)
        {
            //Gets the initial position using the floor gridPos and the initial coordenates
            var position = new Vector3(gridPos[charactersInitialPos[c, 0], charactersInitialPos[c, 1]].x, 1.5f, gridPos[charactersInitialPos[c, 0], charactersInitialPos[c, 1]].z);
            //Sets the rotation, the positons on the other side of the grid are facing the reverse direction
            var rotation =  Quaternion.LookRotation(Vector3.forward);
            if(position.z == gridZSize - 1)
            {
                rotation = Quaternion.LookRotation(Vector3.back);
            }

            //Changes gridPos so that it can check later if something was spawned
            gridPositionSpawned[charactersInitialPos[c, 0], charactersInitialPos[c, 1]] = new Vector3(0,5,0);
            var  spawnedCharacter = Instantiate(characters[c], position, rotation);
            
        }

        GameObject[] toSpawn = new GameObject[2];
        toSpawn[0] = wall;
        toSpawn[1] = obstacle;
        for (int i = 0; i < gridXSize; i++)
        {
            for (int j = 0; j < gridZSize; j++)
            { int random = Probability.ItemProbability();
                var position = new Vector3(this.gridPos[i, j].x, 1.002f, this.gridPos[i, j].z);
                if(random != 2 && gridPositionSpawned[i,j].y != 5)
                {
                     var spawnedObject = Instantiate(toSpawn[Random.Range(0,2)], position, Quaternion.identity);
                    isInSecondLayer[i,j] = true;
                    secondLayerObjects[i,j] = spawnedObject;
                }
            }

        }
    }

    private void CheckCharacterPos(GameObject[] characters)
    {
        for(int i = 0; i < characters.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (isInSecondLayer[charactersInitialPos[i, 0] + 1, charactersInitialPos[i, 1]])
                    {
                        GameObject.Destroy(secondLayerObjects[charactersInitialPos[i, 0] + 1, charactersInitialPos[i, 1]]);
                    }
                    if (isInSecondLayer[charactersInitialPos[i, 0] + 2, charactersInitialPos[i, 1]])
                    {
                        GameObject.Destroy(secondLayerObjects[charactersInitialPos[i, 0] + 2, charactersInitialPos[i, 1]]);
                    }
                    break;
                case 1:
                    if (isInSecondLayer[charactersInitialPos[i, 0], charactersInitialPos[i, 1] - 1])
                    {
                        GameObject.Destroy(secondLayerObjects[charactersInitialPos[i, 0], charactersInitialPos[i, 1] - 1]);
                    }
                    if(isInSecondLayer[charactersInitialPos[i, 0], charactersInitialPos[i, 1] - 2])
                    {
                        GameObject.Destroy(secondLayerObjects[charactersInitialPos[i, 0], charactersInitialPos[i, 1] - 2]);
                    }
                    break;
                case 2:
                    if (isInSecondLayer[charactersInitialPos[i, 0], charactersInitialPos[i, 1] + 1])
                    {
                        GameObject.Destroy(secondLayerObjects[charactersInitialPos[i, 0] , charactersInitialPos[i, 1] + 1]);
                    }
                    if (isInSecondLayer[charactersInitialPos[i, 0] , charactersInitialPos[i, 1]  +2])
                    {
                        GameObject.Destroy(secondLayerObjects[charactersInitialPos[i, 0], charactersInitialPos[i, 1] + 2]);
                    }
                    break;
                case 3:
                    if (isInSecondLayer[charactersInitialPos[i, 0] - 1, charactersInitialPos[i, 1]])
                    {
                        GameObject.Destroy(secondLayerObjects[charactersInitialPos[i, 0] - 1, charactersInitialPos[i, 1] ]);

                    }
                    if (isInSecondLayer[charactersInitialPos[i, 0] - 2 , charactersInitialPos[i, 1]])
                    {
                        GameObject.Destroy(secondLayerObjects[charactersInitialPos[i, 0] - 2, charactersInitialPos[i, 1]]);

                    }
                    break;
                 default :
                    break;
            }
            
        }
    }

    //Decides how many and types of characters that will be generated
    private GameObject[] characterSelection(int numberOfCharacters, bool isControlable)
    {
        selected = new GameObject[numberOfCharacters];
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
            for(int j = 0; j <gridZSize; j++){
                var position =  new Vector3(i, 0, j);
                var spawnedTile = Instantiate(floor, position , Quaternion.identity);
                gridPos[i, j] = position;

            }

        }
    }
}
