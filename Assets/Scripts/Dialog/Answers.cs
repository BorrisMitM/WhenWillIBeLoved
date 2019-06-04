using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "answer")]
public class Answers : ScriptableObject
{
    public int dialogId;
    public GameObject[] answerButtons;
}
