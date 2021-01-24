using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Singleton")]
    public static CameraManager sharedInstance = null;

    [Header("GameManager")]
    GameManager gameManager;
    PieceManager pieceManager;

    [Header("Camera")]
    Camera camera;
    Vector3 mousePosition;
    float maxDistance = 15.0f;

    void Awake()
    {
        Singleton();
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pieceManager = GameObject.Find("GameManager").GetComponent<PieceManager>();

        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray();
        }
    }

    void Singleton()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
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
                if (gameManager.isStop == false)
                {
                    pieceManager.SpawnPiece(hit.collider.gameObject);
                }
            }
        }
    }
}
