using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Enemy script component here
    public enum RoomType
    {
        PowerUp,
        Enemies,
    }

    public enum EnemyType
    {
        Zombie,
        Ghost,
        GhostBoss,
    }

    public enum PowerUpType
    {
        SpeedBoost,
        JumpBoost,
    }

    public GameObject speedBoostPrefab;
    public GameObject jumpBoostPrefab;
    public GameObject enemyPrefab;
    private GameObject[] rooms;
    private GameObject[] corridors;

    public void SpawnInteractableGameObject()
    {
        //Spawn interactable in room
        foreach (GameObject room in rooms)
        {
            RoomType roomType = RandomEnumValue<RoomType>();
            switch (roomType)
            {
                case RoomType.Enemies:
                    SpawnEnemiesInRoom(room);
                    break;
                case RoomType.PowerUp:
                    SpawnPowerUpInRoom(room);
                    break;
            }
        }
       
        foreach (GameObject corridor in corridors)
        {
            SpawnPatrolInCorridor();
        }
    }

    private void SpawnEnemiesInRoom(GameObject room)
    {
        // CalculateCornerPoint(room);
        // Vector3 spawnPoint = GenerateSpawnPoint();
        // Instantiate(enemyPrefab, new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z), Quaternion.identity, room.transform);
    }

    private void SpawnPowerUpInRoom(GameObject room)
    {
        CalculateCornerPoint(room);
        Vector3 spawnPoint = GenerateSpawnPoint();
        GameObject createdObj;
        PowerUpType powerUpType = RandomEnumValue<PowerUpType>();
        switch (powerUpType)
        {
            case PowerUpType.JumpBoost:
                createdObj = Instantiate(jumpBoostPrefab, spawnPoint, Quaternion.identity, room.transform);
                break;
            case PowerUpType.SpeedBoost:
                createdObj = Instantiate(speedBoostPrefab, spawnPoint, Quaternion.identity, room.transform);
                break;
        }
    }

    private void SpawnPatrolInCorridor()
    {

    }

    public void Start()
    {
        GetRoomAndCorridor();
        SpawnInteractableGameObject();
    }

    private T RandomEnumValue<T> ()
    {
        var v = Enum.GetValues(typeof (T));
        System.Random random = new System.Random();
        return (T) v.GetValue(random.Next(v.Length));
    }

    private void GetRoomAndCorridor()
    {
        rooms = GameObject.FindGameObjectsWithTag("Room");
        corridors = GameObject.FindGameObjectsWithTag("Corridor");
    }

    List<Vector3> Corners = new List<Vector3>();
    private Vector3 GenerateSpawnPoint()
    {
        float u = UnityEngine.Random.Range(0.0f, 1.0f); 
        float v = UnityEngine.Random.Range(0.0f, 1.0f);

        if (v + u > 1) //sum of coordinates should be smaller than 1 for the point be inside the triangle
        {
            v = 1 - v;
            u = 1 - u;
        }
        int randomCornerIdx = UnityEngine.Random.Range(0, 2) == 0 ? 0 : 2;
        List<Vector3> EdgeVectors = new List<Vector3>();
        EdgeVectors.Clear();
        EdgeVectors.Add(Corners[3] - Corners[randomCornerIdx]);
        EdgeVectors.Add(Corners[1] - Corners[randomCornerIdx]);
        Vector3 randomPoint = Corners[randomCornerIdx] + u * EdgeVectors[0] + v * EdgeVectors[1];
        return new Vector3(randomPoint.x, randomPoint.y + 4, randomPoint.z);
    }

    private void CalculateCornerPoint(GameObject plane)
    {
        GameObject tempPlane = Instantiate(plane);
        Vector3 scaleChange = new Vector3(-2f, 0, -2f);
        tempPlane.transform.localScale += scaleChange;

        List<Vector3> VerticeList = new List<Vector3>(tempPlane.GetComponent<MeshFilter>().sharedMesh.vertices);
        Corners.Clear();
        Corners.Add(tempPlane.transform.TransformPoint(VerticeList[0])); //corner points are added to show  on the editor
        Corners.Add(tempPlane.transform.TransformPoint(VerticeList[10]));
        Corners.Add(tempPlane.transform.TransformPoint(VerticeList[110]));
        Corners.Add(tempPlane.transform.TransformPoint(VerticeList[120]));

        Destroy(tempPlane);
    }
}