using System.Collections;
using UnityEngine;

public class FXHandler : MonoBehaviour
{
    [SerializeField] private GameObject spark;
    [SerializeField] private GameObject fire;

    public void Reset() {
        spark.SetActive(false);
        fire.SetActive(false);
    }

    private void Start() {
        Reset();
    }

    public void StartFire(){
        StartCoroutine(IStartFire());
    }

    private IEnumerator IStartFire(){
        spark.SetActive(true);
        yield return new WaitForSeconds(2f);
        spark.SetActive(false);
        fire.SetActive(true);
        GetComponent<Decision>().SetUserDecision(false);
    }
}
