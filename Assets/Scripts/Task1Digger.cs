using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1Digger : MonoBehaviour {

    public const float TILE_SIZE = 2f;

    // Static variable to keep track of the number of diggers in the scene.
    public static int totalDiggers = 0;

    // How many floor tiles that have been spawned (note: needs to be reset manually, usually by the a manager script).
    public static int totalFloors = 0;

    public GameObject floorTilePrefab;
    public GameObject treasureTilePrefab;

    public float turnLeftProbability = 0.1f;
    public float turnRightProbability = 0.1f;
    public float turnAroundProbability = 0.05f;

    public int roomWidth = 2;
    public int roomHeight = 2;

    public float roomProbability = 0.5f;

    public int maxNumberOfDiggers = 10;
    // Thse are marked as "base" probabilities because the actual probability of spawning a new digger should be HIGHER if there are fewer diggers
    // Similarly, the probability of deleting a digger should be higher if there are more diggers
    public float spawnNewDiggerBaseProbability = 0.01f;
    public float deleteDiggerBaseProbability = 0.1f;

    void Awake() {
        totalDiggers++;
    }

    void OnDestroy() {
        totalDiggers--;
    }




    public void maybeStampFloor(Vector2 floorPosition) {
        bool isFloorTileThere = Physics2D.OverlapPoint(floorPosition) != null;
        if (!isFloorTileThere) {
            Instantiate(floorTilePrefab, floorPosition, Quaternion.identity);
            totalFloors++;
        }
    }

    public void maybeStampTreasure(Vector2 treasurePosition) {
        bool isTreasureThere = Physics2D.OverlapPoint(treasurePosition, LayerMask.GetMask("Treasure"));
        if (!isTreasureThere) {
            Instantiate(treasureTilePrefab, treasurePosition, Quaternion.identity);
        }
    }

    void Update() {
        // Stamp and move. 
        if (Random.value <= roomProbability) {
            for (int xOffset = 0; xOffset < roomWidth; xOffset++) {
                for (int yOffset = 0; yOffset < roomHeight; yOffset++) {
                    Vector2 floorPosition = (Vector2)transform.position + Vector2.right * xOffset * TILE_SIZE + Vector2.up * yOffset * TILE_SIZE;
                    maybeStampFloor(floorPosition);
                }
            }
        }
        else {
            maybeStampFloor(transform.position);
        }

        transform.position += (Vector3)transform.right * TILE_SIZE;

        float randomNumber = Random.value;
        if (randomNumber <= turnLeftProbability) {
            transform.Rotate(0, 0, 90);
        }
        else if (randomNumber <= turnLeftProbability + turnRightProbability) {
            transform.Rotate(0, 0, -90);
        }
        else if (randomNumber <= turnLeftProbability + turnRightProbability + turnAroundProbability) {
            transform.Rotate(0, 0, 180);
        }
        spawnOrDeleteDiggers();
    }


    public void spawnOrDeleteDiggers() {
        // Task 1-b Code HERE: 
        // Use the probabilities associated with spawning and deleting diggers to decide whether we spawn a new digger or delete a digger here.
        // REMEMBER: more diggers means we're more likely to delete, less likely to spawn.
        // fewer diggers means we're less likely to delete, more likely to spawn.
        // There should never be fewer than 1 diggers (the generator deletes the last digger for us)
        // AND there should never be more diggers than "maxNumberOfDiggers" 
    }
}
