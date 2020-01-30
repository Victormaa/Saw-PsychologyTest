using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMath : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    Vector4 a = new Vector4(7, -5, 0, 0);
    [SerializeField]
    Vector4 b = new Vector4(2, -7, 0,0);
    [SerializeField]
    Vector4 c = new Vector4(1, 6, 8,0);
    Vector4 d = new Vector4(0, 0, 0, 0);

    Matrix4x4 M = new Matrix4x4();

    Matrix4x4 Mtemp = new Matrix4x4();

    Vector3 tem = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        M = new Matrix4x4(a, b, c, d);
        Matrix4x4 M2 = new Matrix4x4(c, b, a, d);
        Matrix4x4 Mtemp = Matrix4x4.Transpose(M) * Matrix4x4.Transpose(M2);
    }

    // Update is called once per frame
    void Update()
    {
        tem = Vector3.Cross(a, b);
        text.text = Mtemp.ToString();
    }
}
