using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEndScript : MonoBehaviour
{
   public void EndSceneMethod()
    {
        PixelCrushers.DialogueSystem.Sequencer.Message("SceneEnd");
    }
}
