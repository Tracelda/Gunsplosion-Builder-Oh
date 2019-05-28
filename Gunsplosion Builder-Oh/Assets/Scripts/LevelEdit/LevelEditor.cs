using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelEditor : MonoBehaviour
{
    private int currentBlockID;
    private GameObject brushBlock;
    private Camera mainCamera;
    public LevelData levelData;
    private BlockData currentBlock;
    private Quaternion currentRotation;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.x = Mathf.Round(newPosition.x);
        newPosition.y = Mathf.Round(newPosition.y);
        newPosition.z = 0;
        Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y), 0);
        SerialiseVector3Int serialisedPosition = new SerialiseVector3Int(tilePosition.x, tilePosition.y, 0);
        SerialiseVector3Int serialisedRotation = new SerialiseVector3Int(0, 0, Mathf.RoundToInt(currentRotation.eulerAngles.z));

        if (currentBlock != null)
        {
            brushBlock.transform.position = newPosition;
            brushBlock.transform.rotation = currentRotation;

            if (Input.GetButton("Fire1") && !levelData.tilemap.HasTile(tilePosition))
            {
                if (currentBlock.blockType == BlockData.BlockTypes.Tile)
                {
                    levelData.tilemap.SetTile(tilePosition, levelData.blocks[currentBlockID].tile);
                    levelData.tileData.Add(serialisedPosition, currentBlockID);
                    levelData.blockData[tilePosition] = levelData.blocks[currentBlockID];
                }
                else
                {
                    levelData.tilemap.SetTile(tilePosition, levelData.emptyTile);
                    levelData.tileData.Add(serialisedPosition, -1);
                    levelData.blockData[tilePosition] = Instantiate(currentBlock);
                    currentBlock = null;
                }
                levelData.blockData[tilePosition].blockInfo.position = serialisedPosition;
                levelData.blockData[tilePosition].blockInfo.rotation = serialisedRotation;
                SelectBlock(currentBlockID);
            }

            if (Input.GetKeyDown("r"))
            {
                brushBlock.gameObject.transform.Rotate(0, 0, -90);
                currentRotation = brushBlock.gameObject.transform.rotation;
            }
        }

        if (Input.GetButton("Fire2") && levelData.tilemap.HasTile(tilePosition)) {
            levelData.tilemap.SetTile(tilePosition, null);
            levelData.tileData.Remove(serialisedPosition);
            if (levelData.blockData.ContainsKey(tilePosition) && levelData.blockData[tilePosition].blockType == BlockData.BlockTypes.Object) {
                Destroy(levelData.blockData[tilePosition].gameObject);
            }
            levelData.blockData.Remove(tilePosition);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectBlock(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectBlock(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectBlock(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectBlock(3);
        }

        if (Input.GetKeyDown("s"))
        {
            SaveAndLoad.instance.SaveLevel(levelData, SaveAndLoad.instance.fileName);
            print(levelData.blockData.Count);
        }

    }

    private void SelectBlock(int id)
    {
        if (currentBlock != null)
        {
            Destroy(currentBlock.gameObject);
            currentBlock = null;
        }

        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.x = Mathf.Round(newPosition.x);
        newPosition.y = Mathf.Round(newPosition.y);
        newPosition.z = 0;
        brushBlock = Instantiate(levelData.blocks[id].prefab, newPosition, currentRotation);
        currentBlockID = id;
        currentBlock = levelData.blocks[id];
        currentBlock.gameObject = brushBlock;
    }
}
