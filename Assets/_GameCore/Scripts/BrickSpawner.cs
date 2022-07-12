using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] brickSlotArray;
    [SerializeField] private Material[] brickMaterialArray;

    [SerializeField] private GameObject brickPrefab;
    
    private void Start()
    {
        SpawnStart();
    }

    void SpawnStart()
    {
        for (int i = 0; i < brickSlotArray.Length; i++)
        {
            var newBrick = Instantiate(brickPrefab,brickSlotArray[i].transform);
            SetBrickColor(newBrick);
        }
    }

    void SetBrickColor(GameObject brick)
    {
        var colorNumber = Random.Range(0, 3);

        switch (colorNumber)
        {
            case 0: 
                brick.GetComponent<MeshRenderer>().material = brickMaterialArray[0];
                brick.GetComponent<BrickController>().brickColor = CharacterGeneralController.ColorEnum.Red;  
                break;
            case 1: 
                brick.GetComponent<MeshRenderer>().material = brickMaterialArray[1];
                brick.GetComponent<BrickController>().brickColor = CharacterGeneralController.ColorEnum.Blue;
                break;
            case 2: 
                brick.GetComponent<MeshRenderer>().material = brickMaterialArray[2];
                brick.GetComponent<BrickController>().brickColor = CharacterGeneralController.ColorEnum.Yellow;
                break;
        }
    }
    
    
}
