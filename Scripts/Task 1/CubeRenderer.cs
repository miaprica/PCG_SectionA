using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRenderer : MonoBehaviour
{

    [SerializeField]
    private int buildingLengthSize = 10; //number of cubes

    [SerializeField]
    private int buildingHeightSize = 5; // number of cubes.

    [SerializeField]
    private int buildingDepthSize = 10; //umber of cubes

    [SerializeField]
    private Vector3 initialisePosition = Vector3.zero;

    //private int submeshIndex;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //function for initialising a building//
    public void initialiseCube(int newbuildingLengthSize, int newbuildingHeightSize, int newbuildingDepthSize, Vector3 newInitialisePosition)
    {
        //this.submeshIndex = submeshIndex;

        buildingLengthSize = newbuildingLengthSize;
        buildingHeightSize = newbuildingHeightSize;
        buildingDepthSize = newbuildingDepthSize;
        initialisePosition = newInitialisePosition;
        RenderCube();

    }


    //rendering cubes that make up a building
    private void RenderCube(){

        Vector3 nextPosition = initialisePosition;

        float cubeHeight = 0;
        float cubeDepth = 0;


        // BUILDING //
        for (int j = 0; j < buildingHeightSize; j++)
        {
            for (int k = 0; k < buildingDepthSize; k++)
            {
                for (int i = 0; i < buildingLengthSize; i++)
                {
                    GameObject cube = new GameObject();
                    cube.name = "Cube " + j + "-" + i + "-" + k;
                    cube.AddComponent<Cube>();
                    cube.transform.position = nextPosition;
                    cube.transform.parent = this.transform;

                    //cube.GetComponent<Cube>().submeshIndex = submeshIndex;
                    nextPosition.x = cube.GetComponent<Cube>().CubeSize().x + nextPosition.x;
                    cubeHeight = cube.GetComponent<Cube>().CubeSize().y;
                    cubeDepth = cube.GetComponent<Cube>().CubeSize().z;
                }

                //set the z position of the next cube to the z value that comes after the last rendered cube
                nextPosition.z = cubeDepth + nextPosition.z;
                nextPosition.x = 0;

            }

            //set the y position of the next cube to the y value after the last rendered cube
            nextPosition.y = cubeHeight + nextPosition.y;

            //increase only the y value
            nextPosition.x = 0; 
            nextPosition.z = 0;
        }

    }
}
