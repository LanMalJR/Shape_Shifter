using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] roadPrefab; // Assign 4 planes in the Inspector
    public float roadLength = 20f; // Adjust based on plane size
    private float nextSpawnX = 0f; // Tracks the next plane to spawn on X-axis
    private float deleteDistance = 40f; // The distance from the player to delete old roads

    public Transform player; // Assign the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initially position the planes correctly
        for (int i = 0; i < roadPrefab.Length; i++)
        {
            // Reposition the initial planes along the X-axis
            roadPrefab[i].transform.position = new Vector3(i * roadLength, 0, 0); // Position planes along X-axis
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is near the next spawn position on the X-axis
        if (player.position.x > nextSpawnX - 40f)
        {
            SpawnRoad();
        }

        // Delete/reposition roads that are far behind the player
        RepositionOldRoads();
    }

    void SpawnRoad()
    {
        // Select a random road prefab from the array
        GameObject selectedRoad = roadPrefab[Random.Range(0, roadPrefab.Length)];

        // Instantiate the selected road prefab at the next spawn position
        Instantiate(selectedRoad, new Vector3(0, 0, nextSpawnX), Quaternion.identity);

        // Update the spawn position for the next road
        nextSpawnX += roadLength;
    }

    void RepositionOldRoads()
    {
        // Loop through all road prefabs (planes) in the scene
        foreach (GameObject road in roadPrefab)
        {
            // If the road is far behind the player, move it to the next position ahead of the player
            if (road.transform.position.x < player.position.x - deleteDistance)
            {
                road.transform.position = new Vector3(nextSpawnX, 0, 0);
                nextSpawnX += roadLength; // Update the spawn position for the next road
            }
        }
    }
}
