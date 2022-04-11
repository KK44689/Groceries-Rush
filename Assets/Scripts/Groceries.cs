using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groceries : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    private GameObject selectedObject;

    private SpawnManager spawnManagerScript;

    // public bool gameover;
    // Start is called before the first frame update
    void Start()
    {
        // gameover = false;
        spawnManagerScript =
            GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // groceries moves
        if (!spawnManagerScript.gameover)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        // gameover when off screen
        if (transform.position.x >= 8)
        {
            Debug.Log("GameOver Off screen");
            spawnManagerScript.gameover = true;
        }
    }

    // when mouse drag
    private void OnMouseDrag()
    {
        if (!spawnManagerScript.gameover)
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    // make player can't drag a non-dragable object
                    if (
                        !hit.collider.CompareTag("Food") &&
                        !hit.collider.CompareTag("Drink") &&
                        !hit.collider.CompareTag("Fruit")
                    )
                    {
                        return;
                    }
                    selectedObject = hit.collider.gameObject;
                    // Cursor.visible = false;
                }
            }

            // make groceries object equal mouse position
            if (selectedObject != null)
            {
                Vector3 position =
                    new Vector3(Input.mousePosition.x,
                        Camera
                            .main
                            .WorldToScreenPoint(selectedObject
                                .transform
                                .position)
                            .y,
                        Camera
                            .main
                            .WorldToScreenPoint(selectedObject
                                .transform
                                .position)
                            .z);

                Vector3 worldPosition =
                    Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position =
                    new Vector3(worldPosition.x, .25f, worldPosition.z);
            }
        }
    }

    // when mouse release
    private void OnMouseUp()
    {
        if (selectedObject != null)
        {
            Vector3 position =
                new Vector3(Input.mousePosition.x,
                    Camera
                        .main
                        .WorldToScreenPoint(selectedObject.transform.position)
                        .y,
                    Camera
                        .main
                        .WorldToScreenPoint(selectedObject.transform.position)
                        .z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position =
                new Vector3(worldPosition.x, 1f, worldPosition.z);
            selectedObject = null;
            // Cursor.visible = true;
        }
    }

    // set raycast
    private RaycastHit CastRay()
    {
        Vector3 screenMousePositionFar =
            new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.farClipPlane);
        Vector3 screenMousePositionNear =
            new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.nearClipPlane);
        Vector3 worldMousePosFar =
            Camera.main.ScreenToWorldPoint(screenMousePositionFar);
        Vector3 worldMousePosNear =
            Camera.main.ScreenToWorldPoint(screenMousePositionNear);
        RaycastHit hit;
        Physics
            .Raycast(worldMousePosNear,
            worldMousePosFar - worldMousePosNear,
            out hit);
        return hit;
    }
}
