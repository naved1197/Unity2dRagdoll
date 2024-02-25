using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dynamicObj;
    [SerializeField] private GameObject staticObj;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(dynamicObj, GetMousePos(cam, Input.mousePosition), Quaternion.identity, this.transform);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(staticObj, GetMousePos(cam, Input.mousePosition), Quaternion.identity, this.transform);
        }
    }

  public static Vector2 GetMousePos(Camera camera,Vector2 mousePos)
    {
        return camera.ScreenToWorldPoint(mousePos);
    }
}
