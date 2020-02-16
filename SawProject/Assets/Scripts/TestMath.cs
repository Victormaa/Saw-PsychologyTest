using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMath : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    Vector3 a = new Vector4(7, -5, 0);
    [SerializeField]
    Vector3 b = new Vector4(2, -7, 0);
    [SerializeField]
    Vector3 c = new Vector4(1, 6, 8);
    Vector4 d = new Vector4(0, 0, 0, 0);

    Matrix4x4 M = new Matrix4x4();

    Matrix4x4 Mtemp = new Matrix4x4();

    Vector3 tem = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        tem = Vector3.Max(c, a);

        text.text = tem.ToString();

    }

    // Update is called once per frame
    void Update()
    {
    }
}
