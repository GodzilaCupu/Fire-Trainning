using System.Collections;
using Asperio;
using TMPro;
using UnityEngine;

public class DecisionPanelResult : Singleton<DecisionPanelResult>
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI textResult;
    [SerializeField] private float timer;

    private void Start() {
        panel.SetActive(false);
    }

    public void DecisionPanel(bool deci, DecisionPanelSO decisionPanelSO){
        StartCoroutine(ISetTimerPanel());

        if(deci){
            textResult.text = decisionPanelSO.textCorrctDecision;
            return;
        }

        textResult.text = decisionPanelSO.textWrongDecision;
    }

    private IEnumerator ISetTimerPanel(){
        panel.SetActive(true);
        yield return new WaitForSeconds(timer);
        panel.SetActive(false);
    }
}
