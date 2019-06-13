using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Articy.Unity;
using UnityEngine.Events;
public class ChangeGlobalVariable : MonoBehaviour
{
    [Tooltip("remember to also add the base class. For example GameState.awake")]
    [SerializeField] private string variableToChange;
    [SerializeField] private UnityEvent OnVariableChanged;
    [SerializeField] private delegate void IntChanged(int newInt);
 
    
    private void Start() {
        if(ArticyDatabase.DefaultGlobalVariables.Variables.ContainsKey(variableToChange)) 
            Debug.LogWarning("Global variables does not contain - " + variableToChange + " -");
    }
    public void SetBool(bool newState){
        if(variableToChange == ""){
            Debug.LogError("Set variableToChange on " + gameObject.name);
            return;
        }
        ArticyDatabase.DefaultGlobalVariables.SetVariableByString(variableToChange, newState);
        OnVariableChanged.Invoke();
    }
    public void AddToInt(int add){
        if(variableToChange == ""){
            Debug.LogError("Set variableToChange on " + gameObject.name);
            return;
        }
        ArticyDatabase.DefaultGlobalVariables.SetVariableByString(variableToChange, ArticyDatabase.DefaultGlobalVariables.GetVariableByString<int>(variableToChange) + add);
        OnVariableChanged.Invoke();
    }
    public void SetInt(bool newInt){
        if(variableToChange == ""){
            Debug.LogError("Set variableToChange on " + gameObject.name);
            return;
        }
        ArticyDatabase.DefaultGlobalVariables.SetVariableByString(variableToChange, newInt);
        OnVariableChanged.Invoke();
    }
}
