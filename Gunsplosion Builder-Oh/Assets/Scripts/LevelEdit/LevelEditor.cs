using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class LevelEditor : MonoBehaviour
{
    private int currentBlockID;
    private GameObject brushBlock;
    private Camera mainCamera;
    public LevelData levelData;
    public Transform levelGrid;
    public SpriteRenderer fullGridSprite;
    private BlockData currentBlock;
    private Quaternion currentRotation;
    private GameObject grabbedObject;

    public static LevelEditor instance;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        if (MenuManager.instance.isPlaying)
            return;

        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.x = Mathf.Round(newPosition.x);
        newPosition.y = Mathf.Round(newPosition.y);
        newPosition.z = 0;
        Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.y), 0);
        SerialiseVector3Int serialisedPosition = new SerialiseVector3Int(tilePosition.x, tilePosition.y, 0);
        SerialiseVector3Int serialisedRotation = new SerialiseVector3Int(0, 0, Mathf.RoundToInt(currentRotation.eulerAngles.z));

        if (!EventSystem.current.IsPointerOverGameObject()) {
            levelGrid.position = newPosition;

            if (currentBlock != null) {
                brushBlock.transform.position = newPosition;
                brushBlock.transform.rotation = currentRotation;

                if (Input.GetButton("PlaceBlock") && !levelData.tilemap.HasTile(tilePosition)) {
                    if (currentBlock.blockType == BlockData.BlockTypes.Tile) {
                        levelData.tilemap.SetTile(tilePosition, levelData.blocks[currentBlockID].tile);
                        levelData.tileData.Add(serialisedPosition, currentBlockID);
                        levelData.blockData[tilePosition] = levelData.blocks[currentBlockID];
                    }
                    else {
                        levelData.tilemap.SetTile(tilePosition, levelData.emptyTile);
                        levelData.tileData.Add(serialisedPosition, -1);
                        levelData.blockData[tilePosition] = Instantiate(currentBlock);
                        currentBlock = null;
                    }
                    levelData.blockData[tilePosition].blockInfo.position = serialisedPosition;
                    levelData.blockData[tilePosition].blockInfo.rotation = serialisedRotation;
                    SelectBlock(currentBlockID);
                }

                if (Input.GetKeyDown("q")) {
                    brushBlock.gameObject.transform.Rotate(0, 0, 90);
                    currentRotation = brushBlock.gameObject.transform.rotation;
                }

                else if (Input.GetKeyDown("e")) {
                    brushBlock.gameObject.transform.Rotate(0, 0, -90);
                    currentRotation = brushBlock.gameObject.transform.rotation;
                }
            }

            else if (Input.GetButton("PlaceBlock"))
            {
                if (!grabbedObject)
                {
                    RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 1), Vector2.down, 1f);
                    if (hit)
                    {
                        if (hit.transform.CompareTag("DragNode"))
                        {
                            grabbedObject = hit.transform.gameObject;
                        }
                    }
                }

                else if (grabbedObject)
                {
                    grabbedObject.transform.position = tilePosition;
                }
            }

            else
            {
                grabbedObject = null;
            }


            if (Input.GetButton("DeleteBlock") && levelData.tilemap.HasTile(tilePosition)) {
                levelData.tilemap.SetTile(tilePosition, null);
                levelData.tileData.Add(serialisedPosition, -2);
                if (levelData.blockData.ContainsKey(tilePosition) && levelData.blockData[tilePosition].blockType == BlockData.BlockTypes.Object) {
                    Destroy(levelData.blockData[tilePosition].gameObject);
                }
                levelData.blockData.Remove(tilePosition);
            }

            if (Input.GetKeyDown("g")) {
                fullGridSprite.enabled = !fullGridSprite.enabled;
            }
        }
    }

    public void SelectBlock(int id)
    {
        if (currentBlock != null)
        {
            Destroy(currentBlock.gameObject);
            currentBlock = null;
        }

        if (id < 0 || id >= levelData.blocks.Count) {
            return;
        }
        if (!levelData.blocks[id] || !levelData.blocks[id].prefab) {
            return;
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
