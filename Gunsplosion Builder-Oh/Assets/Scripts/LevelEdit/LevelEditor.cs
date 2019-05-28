using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelEditor : MonoBehaviour
{
    private int currentBlockID;
    private GameObject brushBlock;
    private Camera mainCamera;
    public Tilemap tileMap;
    public Tile emptyTile;
    public LevelData levelData;
    public List<BlockData> blocks;
    private BlockData currentBlock;
    private Quaternion currentRotation;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        if (currentBlock != null)
        {
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            newPosition.x = Mathf.Round(newPosition.x);
            newPosition.y = Mathf.Round(newPosition.y);
            newPosition.z = 0;
            brushBlock.transform.position = newPosition;
            brushBlock.transform.rotation = currentRotation;
            Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y), 0);
            SerialiseVector3 serialisedPosition = new SerialiseVector3(tilePosition.x, tilePosition.y, 0);
            SerialiseVector3 serialisedRotation = new SerialiseVector3(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);

            if (Input.GetButtonDown("Fire1") && !tileMap.HasTile(tilePosition))
            {
                if (currentBlock.blockType == BlockData.BlockTypes.Tile)
                {
                    tileMap.SetTile(tilePosition, blocks[currentBlockID].tile);
                    levelData.blockData[serialisedPosition] = blocks[currentBlockID];
                }
                else
                {
                    tileMap.SetTile(tilePosition, emptyTile);
                    levelData.blockData[serialisedPosition] = Instantiate(currentBlock);
                    currentBlock = null;
                }
                levelData.blockData[serialisedPosition].blockInfo.position = serialisedPosition;
                levelData.blockData[serialisedPosition].blockInfo.rotation = serialisedRotation;
                SelectBlock(currentBlockID);
            }

            if (Input.GetButtonDown("Fire2") && tileMap.HasTile(tilePosition))
            {
                tileMap.SetTile(tilePosition, null);
                if (levelData.blockData[serialisedPosition].blockType == BlockData.BlockTypes.Object)
                {
                    Destroy(levelData.blockData[serialisedPosition].gameObject);
                    SelectBlock(currentBlockID);
                }
                levelData.blockData.Remove(serialisedPosition);
            }

            if (Input.GetKeyDown("r"))
            {
                brushBlock.gameObject.transform.Rotate(0, 0, -90);
                currentRotation = brushBlock.gameObject.transform.rotation;
            }
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
        brushBlock = Instantiate(blocks[id].prefab, newPosition, currentRotation);
        currentBlockID = id;
        currentBlock = blocks[id];
        currentBlock.gameObject = brushBlock;
    }
}
