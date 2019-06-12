using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Articy.Unity;
public class GetGlobalVariable : MonoBehaviour
{
    public static bool Bool(string variableName){
        return ArticyDatabase.DefaultGlobalVariables.GetVariableByString<bool>(variableName);
    }
    public static int Int(string variableName){
        return ArticyDatabase.DefaultGlobalVariables.GetVariableByString<int>(variableName);
    }
}
