using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterGeneralController : MonoBehaviour
{
    public static GameObject brickSlotObj;
    public static float _brickSlotObjYPos;
    public static List<GameObject> characterCarryBricks  = new List<GameObject>();
    
    public enum ColorEnum {  Red, Yellow, Blue };

    [SerializeField] public ColorEnum characterEnum;
    
    private float _multiplier;
    private int _index;

    private void Start()
    {
        _multiplier = 1;
        brickSlotObj = GameObject.Find("slot1");
    }

    private void OnEnable()
    {
        EventManager.PutBrickAtBridge += MoveToBridge;
        EventManager.OnBrickEnter += SetEmpySlotPos;
    }

    private void OnDisable()
    {
        EventManager.PutBrickAtBridge -= MoveToBridge;
        EventManager.OnBrickEnter -= SetEmpySlotPos;
    }

    void SetEmpySlotPos()
    {
        _brickSlotObjYPos = brickSlotObj.transform.localPosition.y + (0.6f * _multiplier);
        _multiplier++;
    }
    
    void MoveToBridge(GameObject targetSlot, int index)
    {
        
        var lastItemIndex = characterCarryBricks.Count-1;
        Debug.Log("ilk index count -1 li : " + lastItemIndex);
        
        var obj = characterCarryBricks[lastItemIndex];
        var transform1 = targetSlot.transform;
        obj.transform.parent = targetSlot.transform;
        
        obj.transform.DOLocalMove(new Vector3(
           0,
           0,
           0
        ),1);
        
        obj.transform.rotation = new Quaternion(0, 0, 0,0);
        
        var lastItem = characterCarryBricks[lastItemIndex];
        
        characterCarryBricks.RemoveAt(characterCarryBricks.Count-1);
       
        StartCoroutine(BridgeBrickProcess(lastItem));
    }

    IEnumerator BridgeBrickProcess(GameObject lastItem)
    {
        yield return new WaitForSeconds(2);
        lastItem.transform.GetComponent<BrickController>().enabled = false;
        lastItem.transform.GetComponent<BoxCollider>().enabled = true;
        lastItem.transform.GetComponent<BoxCollider>().isTrigger = false;
       
        
    }
}
