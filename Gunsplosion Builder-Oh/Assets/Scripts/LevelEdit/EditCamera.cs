using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditCamera : MonoBehaviour
{
    Vector3 previousMousePos;
    public Vector2 cameraRange;
    public Camera camera;
    public float moveSpeed, zoomSpeed;

    private void Update() {
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
