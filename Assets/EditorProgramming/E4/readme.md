
# Unity Editor Scripting with InitializeOnLoad

## Overview

This repository showcases various Unity editor scripting techniques, primarily focusing on utilizing the `[InitializeOnLoad]` attribute. This attribute allows the registration of events and setup tasks that run when the Unity editor loads, enabling automated functionalities without requiring user interaction.

## Key Features

### 1. Custom Toolbar Buttons

- Added custom buttons to the Unity toolbar for quick access to scene management.
- Each button allows users to log scene names and start scenes with or without entering play mode.

### 2. Scene Management

- Implemented a dynamic scene list that automatically populates from the scenes included in the build settings.
- Users can remove scenes from the list by clicking the "X" button next to each scene.

### 3. Drag-and-Drop Functionality

- Users can drag and drop scenes into a designated area within the toolbar for easy management.
- Added visual feedback to indicate where users can drop scenes.

### 4. Play Mode State Change Events

- Utilized `[InitializeOnLoad]` to set up event listeners that respond to Unity’s play mode state changes.
- Log messages are displayed when entering or exiting play mode.

### 5. Code Implementation

#### Example of Using `[InitializeOnLoad]`

```csharp
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
            EditorApplication.playModeStateChanged += PlayModeChanged;
        }

        private static void PlayModeChanged(PlayModeStateChange playMode)
        {
            switch (playMode)
            {
                case PlayModeStateChange.EnteredPlayMode:
                    Debug.Log("Play mode entered.");
                    break;

                case PlayModeStateChange.ExitingPlayMode:
                    Debug.Log("Play mode exiting.");
                    break;

                default:
                    break;
            }
        }
    }
}
```
## Get Help


* https://github.com/marijnz/unity-toolbar-extender
