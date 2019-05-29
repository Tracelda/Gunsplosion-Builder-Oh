using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroll : MonoBehaviour {
    public bool scrolling, parallax;
    public float backgroundSize;
    public float parallaxSpeed;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;
    private float lastCameraY;

    void Start() {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    void Update() {
        if (parallax) {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * parallaxSpeed);
            lastCameraX = cameraTransform.position.x;
        }

        if (scrolling) {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
                ScrollLeft();
            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
                ScrollRight();
        }
        transform.position = new Vector3(transform.position.x, cameraTransform.position.y, transform.position.z);
        foreach (Transform child in layers)
        {
            child.position = new Vector3(child.position.x, cameraTransform.position.y, 10);
        }
    }

    private void ScrollLeft() {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        layers[rightIndex].position = new Vector3(layers[rightIndex].position.x, cameraTransform.position.y, layers[rightIndex].position.z);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0) {
            rightIndex = layers.Length - 1;
        }
    }

    private void ScrollRight() {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        layers[leftIndex].position = new Vector3(layers[leftIndex].position.x, cameraTransform.position.y, 10);
        leftIndex = rightIndex;
        leftIndex++;
        if (leftIndex == layers.Length ) {
            leftIndex = 0;
        }
    }
}
