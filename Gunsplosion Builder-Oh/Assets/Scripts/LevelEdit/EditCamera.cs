using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditCamera : MonoBehaviour
{
    Vector3 previousMousePos;
    public Vector2 cameraRange;
    public Camera camera;
    public float moveSpeed, zoomSpeed, playMoveSpeed;
    public Transform target;
    private Vector3 editModePosition;
    public bool playMode;
    private float editModeZoom;

    private void Update() {
        if (playMode) {
            Vector3 targetPosition = target.position;
            targetPosition.z = -10;
            transform.position = Vector3.Lerp(transform.position, targetPosition, playMoveSpeed * Time.deltaTime);
        }
        else {
            if (!EventSystem.current.IsPointerOverGameObject()) {
                if (Input.GetMouseButton(2)) {
                    transform.Translate((previousMousePos - Input.mousePosition) * Time.deltaTime * camera.orthographicSize * moveSpeed);
                }

                camera.orthographicSize += Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed;
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, cameraRange.x, cameraRange.y);
            }

            previousMousePos = Input.mousePosition;
        }
    }

    public void ChangeToPlay(Transform newTarget) {
        playMode = true;
        target = newTarget;
        editModePosition = transform.position;
        editModeZoom = camera.orthographicSize;
        camera.orthographicSize = 5f;
    }

    public void ChangeToEdit() {
        playMode = false;
        transform.position = editModePosition;
        camera.orthographicSize = editModeZoom;
    }
}
