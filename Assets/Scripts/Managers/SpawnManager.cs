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

    public GameObject enemyPrefab;
    public GameObject itemPrefab;
    public GameObject plane;

    public void SpawnEnemiesInRoom(GameObject plane)
    {
        CalculateCornerPoint(plane);
        Vector3 spawnPoint = FindRandomPoint();
        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, plane.transform);
    }

    public void SpawnPowerUpInRoom(GameObject plane)
    {

    }

    public void SpawnPatrolInCorridor(GameObject plane)
    {

    }

    public void Start()
    {
        SpawnEnemiesInRoom(plane);
    }

    List<Vector3> Corners = new List<Vector3>();
    private Vector3 FindRandomPoint()
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
        return randomPoint;
    }

    private void CalculateCornerPoint(GameObject plane)
    {
        List<Vector3> VerticeList = new List<Vector3>(plane.GetComponent<MeshFilter>().sharedMesh.vertices);
        Corners.Clear();
        Corners.Add(plane.transform.TransformPoint(VerticeList[0])); //corner points are added to show  on the editor
        Corners.Add(plane.transform.TransformPoint(VerticeList[10]));
        Corners.Add(plane.transform.TransformPoint(VerticeList[110]));
        Corners.Add(plane.transform.TransformPoint(VerticeList[120]));
    }
}