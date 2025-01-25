using UnityEngine;
using UnityEngine.Events;

public class Decision : MonoBehaviour
{
    [SerializeField] private UnityEvent decisionTrue;
    [SerializeField] private UnityEvent decisionFalse;
    [SerializeField] private UnityEvent decisionReset;
    public DecisionPanelSO decisionPanelSO;
    public bool userDecision = false;
    public bool isChoose = false;

    public void SetUserDecision(bool deci){
        if(isChoose == false){
            userDecision = deci;
            CheckDecision();
        }

        else{
            Debug.Log("User alredy choose decision");
        }
    }

    public void CheckDecision(){
        DecisionPanelResult.Instance.DecisionPanel(userDecision, decisionPanelSO);
        isChoose = true;
        
        if(userDecision){
            decisionTrue?.Invoke();
        }

        else{
            decisionFalse?.Invoke();
        }
    }

    public void ResetDecision(){
        userDecision = false;
        isChoose = false;
    }
}
