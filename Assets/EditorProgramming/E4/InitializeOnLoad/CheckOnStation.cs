using System;
using UnityEditor;
using UnityEngine;

namespace EditorProgramming.E4.InitializeOnLoad
{
    [InitializeOnLoad]
    public static class CheckOnStation
    {
        
        static CheckOnStation()
        {
            AddEvent();
        }

        private static void AddEvent()
        {
            EditorApplication.playModeStateChanged += playMode => (playMode switch
            {
                PlayModeStateChange.EnteredPlayMode => () => Debug.Log("play enter"),
                PlayModeStateChange.ExitingPlayMode => () => Debug.Log("play exit"),
                _ => (Action)(() => { }) 
            })();
        }
        
     
    }
}