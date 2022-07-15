using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject wall;
    public GameObject floor;
    public int gridXSize = 10;
    public int gridYSize = 10;
    public Vector3[,] grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Vector3[gridXSize,gridYSize];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateMap(){

    }

    public void generateFloorGrid(){
        for(int i = 0; i <gridXSize; i++){
            for(int j = 0; j <gridYSize; j++){
                var spawnedTile = Instantiate(floor, new Vector3(x, y), Quaternion.identity);
            }

        }
    }
}
