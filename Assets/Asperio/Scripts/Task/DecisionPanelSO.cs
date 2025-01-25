using UnityEngine;

[CreateAssetMenu(fileName = "DecisionPanel", menuName = "Scriptable Objects/DecisionPanel")]
public class DecisionPanelSO : ScriptableObject
{
    [TextArea] public string textCorrctDecision;
    [TextArea] public string textWrongDecision;
}
