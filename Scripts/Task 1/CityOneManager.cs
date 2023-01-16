using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityOneManager : MonoBehaviour
{
    [SerializeField]
    private int baseLengthSize = 40; //number of cubes

    [SerializeField]
    private int baseDepthSize = 45; // number of cubes

    [Header("Car Controller")]
    [SerializeField]
    private GameObject car;

    private float carXPosition;
    private float carYPosition;
    private float carZPosition;

    [SerializeField]
    private bool addCar = true;

    Vector3 carRandomPosition;

    // Start is called before the first frame update
    void Start()
    {
        CreateBase();
        SpawnCar();
    }

    private void CreateBase()
    {
        Vector3 nextPosition = Vector3.zero;

        float cubeDepth = 0;

        //adding a box collider for car controller
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = new Vector3(40, 0, 44);
        boxCollider.size = new Vector3(81, 1, 90);


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
            carXPosition = Random.Range(0, 62);
            carYPosition = Random.Range(0, 5);
            carZPosition = Random.Range(0, 75);

            carRandomPosition = new Vector3(carXPosition, carYPosition, carZPosition);

            GameObject carController = Instantiate(car, this.transform.position, this.transform.rotation);
            carController.name = "Car";
            carController.transform.position = carRandomPosition; //setting the fog height to a random height on the terrain
            carController.transform.localScale = new Vector3(2, 2, 2);
        }

    }


}
