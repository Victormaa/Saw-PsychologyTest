using UnityEngine;
using PathCreation;
using System.Collections;

public class Follow : MonoBehaviour
{
    public PathCreator Path;

    [Range(0,15)]
    public float speed = 5;

    public float Distance = 0;

    EndOfPathInstruction end;

    public bool isLeft = false;


    private void Awake()
    {
        if (isLeft)
        {
            Path = GameObject.Find("PathCreator").GetComponent<PathCreator>();
        }
        else
        {
            Path = GameObject.Find("PathCreator (1)").GetComponent<PathCreator>();
        }
        
        //Path = FindObjectOfType<PathCreator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        end = EndOfPathInstruction.Stop;
        Destroy(this.gameObject, 42.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Distance += speed * Time.deltaTime;
        transform.position = Path.path.GetPointAtDistance(Distance, end);
    }

    
}
