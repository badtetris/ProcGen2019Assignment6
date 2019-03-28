using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Task1Generator : MonoBehaviour {

    public GameObject diggerPrefab;
    public GameObject playerPrefab;

    public int maxNumFloors = 100;

    public bool generating = false;

    public Text statusText;

    public void beginGeneration() {
        Task1Digger.totalFloors = 0;
        Instantiate(diggerPrefab, Vector3.zero, Quaternion.identity);
        generating = true;
    }

    public void endGeneration() {
        generating = false;
        foreach (GameObject diggerObj in GameObject.FindGameObjectsWithTag("Digger")) {
            Destroy(diggerObj);
        }

        // Convert the floor tiles into something that makes sense (i.e. not walls)
        convertFloorTiles();

        // Finally, spawn the player at 0, 0, 0
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    public void convertFloorTiles() {
        // Task 1-a code HERE:
        // By default, the digger spawns "wall" tiles where the floor should go. 
        // Your task is to convert these walls into something else so the player can move over every location where there's
        // a floor tile and CANNOT move over any location where there isn't a floor tile.

    }

    void Start() {
        beginGeneration();
    }

    void Update() {
        if (generating) {
            float percentDone = Task1Digger.totalFloors / (float)maxNumFloors;
            statusText.text = "Generating ("
                + Mathf.FloorToInt(percentDone*100)
                +"%) Diggers: " + Task1Digger.totalDiggers 
                +"\nPress R to Reload";

            // We're done generating after we exceed the max number of floors
            if (Task1Digger.totalFloors >= maxNumFloors) {
                endGeneration();
            }
        }
        else {
            statusText.text = "Done, Press R to Reload";
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


   
}
