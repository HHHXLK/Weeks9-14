using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public Button button;
    public float duration = 1f;

    public void OnButtonPress()
    {
        StartCoroutine(DoAction());
    }

    IEnumerator DoAction()
    {
        button.interactable = false;

        yield return new WaitForSeconds(duration);

        button.interactable = true;
    }
}