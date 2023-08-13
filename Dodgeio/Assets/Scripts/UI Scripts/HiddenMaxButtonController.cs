using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenMaxButtonController : MonoBehaviour
{
    public void ClickHiddenButton()
    {
        AdManager.instance.ShowDebugger();
    }
}
