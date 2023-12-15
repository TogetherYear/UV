using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TStartMenu : MonoBehaviour, IPointerClickHandler
{
    public Image enter;

    public Image exit;

    public void OnPointerClick(PointerEventData e)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(enter.rectTransform, e.position))
        {
            TSceneManager.Instance.LoadScene(TSceneKey.Fracture, () => { }, false);
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(exit.rectTransform, e.position))
        {
#if UNITY_EDITOR_WIN
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
