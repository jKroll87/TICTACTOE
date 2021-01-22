using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera camera;
    Vector3 mousePosition;
    float maxDistance = 15f;
    
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray();
        }
    }

    void Ray()
    {
        mousePosition = Input.mousePosition;
        mousePosition = camera.ScreenToWorldPoint(mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, maxDistance);
        Debug.DrawRay(mousePosition, transform.forward * 10, Color.red, 0.3f);

        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Tile"))
            {
                //hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
                hit.collider.gameObject.GetComponent<TileController>().SpawnPiece();
            }
        }
    }
}
