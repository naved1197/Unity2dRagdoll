using UnityEngine;

public class Grabber : MonoBehaviour
{
    private FixedJoint2D joint;
     private Camera cam;
   [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        cam=Camera.main;
    }
    private void FixedUpdate()
    {
        if (joint != null)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            joint.connectedAnchor = mousePos;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray to find the object
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
              Rigidbody2D  rb2 = hit.collider.GetComponent<Rigidbody2D>();
                if (rb2 != null)
                {
                    ConnectToMouse(hit.transform.gameObject,hit.point);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseFromMouse();
        }

      
    }

    void ConnectToMouse(GameObject obj,Vector2 point)
    {
        joint = obj.AddComponent<FixedJoint2D>();
        joint.connectedBody = rb;
        joint.dampingRatio = 1;
        joint.frequency = 1;
        joint.autoConfigureConnectedAnchor = false;
        Vector2 connectedPos=(Vector2)obj.transform.position-point;
        joint.anchor = connectedPos;
    }

    void ReleaseFromMouse()
    {
        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }
    }
}
