using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject wall;
    public GameObject floor;
    public GameObject[] characters;
    public int gridXSize = 10;
    public int gridZSize = 10;
    public Vector3[,] gridPos;

    private GameController gameController;
    public int numberChar;
    public bool isControllable = true;

    public int[,] charactersInitialPos;

    public GameObject[] selected;
    // Start is called before the first frame update
    void Start()
    {
        gridPos = new Vector3[gridXSize,gridZSize];
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
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
        generateSecondLayer(characterSelection(numberChar, isControllable));
        ;
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
            { int random = Random.Range(0, 3);
                var position = new Vector3(this.gridPos[i, j].x, 1.002f, this.gridPos[i, j].z);
                if(random != 2 && gridPositionSpawned[i,j].y != 5)
                {
                     var spawnedObject = Instantiate(toSpawn[Random.Range(0,2)], position, Quaternion.identity);
                }
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
