
using UnityEngine;
using UnityEngine.SceneManagement;

public class launch_script : MonoBehaviour
{
    Vector3 beginPos;
    private bool isLaunched;
    private float idleTime;

    [SerializeField]private float launchMag = 200;
    private float currVelocityTrial;

    private void Awake()
    {
        beginPos = transform.position;
    }
    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, beginPos);
        GetComponent<LineRenderer>().SetPosition(1, transform.position);
        currVelocityTrial = GetComponent<Rigidbody2D>().velocity.magnitude;
        if (isLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            idleTime += Time.deltaTime;
        }
        if(transform.position.y > 10 ||
            transform.position.y < -10 ||
            transform.position.x > 10 ||
            transform.position.x < -10||
            idleTime>2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void onMouseDown()
    {
        GetComponent<LineRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnMouseUp()
    {
        GetComponent<LineRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 vectorToBegin = beginPos - transform.position;

        GetComponent<Rigidbody2D>().AddForce(vectorToBegin* launchMag);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        isLaunched = true;
    }
    private void OnMouseDrag()
    {
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(curPosition.x, curPosition.y);
    }
}
