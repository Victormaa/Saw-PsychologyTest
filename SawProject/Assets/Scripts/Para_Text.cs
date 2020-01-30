using UnityEngine;
using UnityEngine.UI;

public class Para_Text : MonoBehaviour
{
    [SerializeField]
    CharactorParametre parametre;

    Text text;
    // Start is called before the first frame update
    void Start()
    {
        parametre.HealthPoint = 100;
        text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = parametre.HealthPoint.ToString();
    }
}
