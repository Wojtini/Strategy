using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    CanvasScaler canvasscaler;

    private void Start()
    {
        canvasscaler = this.GetComponent<CanvasScaler>();
    }
    // Update is called once per frame
    void Update()
    {
        canvasscaler.referenceResolution = new Vector2(Screen.width,Screen.height);
    }

    public void OnResolutionChange()
    {
        canvasscaler.referenceResolution = new Vector2(Screen.width, Screen.height);
    }
}
