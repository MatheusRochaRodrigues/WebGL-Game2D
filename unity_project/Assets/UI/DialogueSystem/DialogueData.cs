using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
public struct Dialogue {

    //public string name;
    [TextArea(8, 10)]
    public string text;
    [Space()]
    [Header ("Url Video")]
    public string videoURL;
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObject/TalkScript", order = 1)]
public class DialogueData : ScriptableObject {

    public List<Dialogue> talkScript;

}
