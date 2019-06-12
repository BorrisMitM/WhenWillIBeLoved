using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Articy.Unity;
using UnityEngine.Events;
public class ChangeGlobalVariable : MonoBehaviour
{
    [Tooltip("remember to also add the base class. For example GameState.awake")]
    [SerializeField] private string variableToChange;
    [SerializeField] private UnityEvent variableChanged;
    [SerializeField] private delegate void IntChanged(int newInt);
 
    public void SetBool(bool newState){
        ArticyDatabase.DefaultGlobalVariables.SetVariableByString(variableToChange, newState);
        variableChanged.Invoke();
    }
    public void AddToInt(int add){
        ArticyDatabase.DefaultGlobalVariables.SetVariableByString(variableToChange, ArticyDatabase.DefaultGlobalVariables.GetVariableByString<int>(variableToChange) + add);
        variableChanged.Invoke();
    }
    public void SetInt(bool newInt){
        ArticyDatabase.DefaultGlobalVariables.SetVariableByString(variableToChange, newInt);
        variableChanged.Invoke();
    }
}
