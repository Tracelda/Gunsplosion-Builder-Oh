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
            Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y), 0);

            if (Input.GetButtonDown("Fire1") && !tileMap.HasTile(tilePosition))
            {
                if (currentBlock.blockType == BlockData.BlockTypes.Tile)
                {
                    tileMap.SetTile(tilePosition, blocks[currentBlockID].tile);
                    levelData.blockData[tilePosition] = blocks[currentBlockID];
                }
                else
                {
                    tileMap.SetTile(tilePosition, emptyTile);
                    levelData.blockData[tilePosition] = Instantiate(currentBlock);
                    currentBlock = null;
                    print(levelData.blockData[tilePosition].gameObject.transform.position);
                }
                SelectBlock(currentBlockID);
            }

            if (Input.GetButtonDown("Fire2") && tileMap.HasTile(tilePosition))
            {
                tileMap.SetTile(tilePosition, null);
                if (levelData.blockData[tilePosition].blockType == BlockData.BlockTypes.Object)
                {
                    Destroy(levelData.blockData[tilePosition].gameObject);
                    SelectBlock(currentBlockID);
                }
                levelData.blockData.Remove(tilePosition);
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
        brushBlock = Instantiate(blocks[id].prefab, newPosition, Quaternion.identity);
        currentBlockID = id;
        currentBlock = blocks[id];
        currentBlock.gameObject = brushBlock;
    }
}
