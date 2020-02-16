using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QTE_Images_Asset", menuName = "QTE System/QTE_Images_Asset")]

public class QTE_Images : ScriptableObject
{
    public List<Sprite> InputSprite = new List<Sprite>();
    public List<Sprite> KeyboardSprite = new List<Sprite>();
}
