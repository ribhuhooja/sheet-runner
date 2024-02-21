using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a class to hold global configuration data
public class GlobalConfig {
    // note: since these are consts, they have to be written in the file
    // instead of through the editor
    // this annoyance is probably worth the safety that const-ness provides
    public const float distanceBetweenLines = 1;
    public const float screenWidth = 18;
    public const float barLength = 9;

    public const float playerX = -3.5f;
}
