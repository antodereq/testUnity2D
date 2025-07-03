using UnityEngine;
using UnityEngine.EventSystems;

public class InputFocusFix : MonoBehaviour
{
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
