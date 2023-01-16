using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityTwoManager : MonoBehaviour
{
    [SerializeField]
    private int baseLengthSize = 35; //number of cubes

    [SerializeField]
    private int baseDepthSize = 65; // number of cubes

    [Header("Car Controller")]
    [SerializeField]
    private GameObject car;

    private float carXPosition;
    private float carYPosition;
    private float carZPosition;

    [SerializeField]
    private bool addCar = true;

    Vector3 carRandomPosition;

    [Header("Button")]
    public GameObject canvas; ///////////////////////////////////////////
    public GameObject button; ///////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        CreateBase();
        SpawnCar();

        GameObject newButton = Instantiate(button) as GameObject;
        GameObject newCanvas = Instantiate(canvas) as GameObject;
        newButton.transform.SetParent(newCanvas.transform, false);

        newButton.GetComponent<Button>().onClick.AddListener(TaskOnClick);

    }

    private void CreateBase()
    {
        Vector3 nextPosition = Vector3.zero;

        float cubeDepth = 0;

        //adding a box collider for car controller
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = new Vector3(34, 0, 64);
        boxCollider.size = new Vector3(70, 1, 130);


        for (int j = 0; j < baseDepthSize; j++)
        {
            for (int i = 0; i < baseLengthSize; i++)
            {
                GameObject cube = new GameObject();
                cube.name = "Cube " + j + "-" + i;
                cube.AddComponent<Cube>();
                cube.transform.position = nextPosition;
                cube.transform.parent = this.transform;

                nextPosition.x = cube.GetComponent<Cube>().CubeSize().x + nextPosition.x;
                cubeDepth = cube.GetComponent<Cube>().CubeSize().z;

            }

            nextPosition.z = cubeDepth + nextPosition.z;
            nextPosition.x = 0;
        }

        this.transform.position = new Vector3(0, 0, 0);
    }


    private void SpawnCar()
    {
        if (addCar)
        {
            ////get the position of the car
            //CarPosition();

            //getting a random range of car positions to be spawned at
            //keeping the y coordinates 0 to 5 so that the car doesn't spawn on top of the buildings
            carXPosition = Random.Range(0, 57);
            carYPosition = Random.Range(0, 5);
            carZPosition = Random.Range(0, 107);

            carRandomPosition = new Vector3(carXPosition, carYPosition, carZPosition);

            GameObject carController = Instantiate(car, this.transform.position, this.transform.rotation);
            carController.name = "Car";
            carController.transform.position = carRandomPosition; //setting the fog height to a random height on the terrain
            carController.transform.localScale = new Vector3(3, 3, 3);
        }

    }

    //quit game - called on button click
    void TaskOnClick()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
