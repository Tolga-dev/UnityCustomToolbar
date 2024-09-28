# Unity Custom Editor - Player Inspector

This Unity project demonstrates how to create a custom editor for a `Player` object, allowing for enhanced control and visualization of various parameters and options in the Unity Editor.

## Table of Contents
- [Features](#features)
- [Setup](#setup)
- [Usage](#usage)
- [Customization](#customization)
- [License](#license)

## Features

- **Custom Inspector**: Adds a detailed custom inspector interface for the `Player` class.
- **Enum Support**: Provides support for custom enums (`MyEnum`) with flags and popup options.
- **Animation Curve**: Allows visualization and control of animation curves within the inspector.
- **Knob Control**: Includes a custom knob element for precise control over specific float values.
- **Foldout Section**: Allows folding and unfolding specific sections for a cleaner UI.
- **Pop-Up Menu**: Includes a popup selection for primitive GameObjects (Cube, Sphere, Plane) with creation buttons.
- **Sliders, Toggles, and Fields**: Provides various input controls including sliders, toggles, text fields, delayed input fields, and more.
- **Toolbar**: Offers a multi-button toolbar for quick selection and control.

## Setup

1. Clone or download this repository.
2. Open the project in Unity (Ensure you're using a version compatible with Unity Visual Scripting and Unity Editor tools).
3. The custom editor script can be found under the path: `Assets/Scripts/Editor/PlayerEditor.cs`.
4. Attach the `Player` script to a GameObject in the scene to see the custom editor in action.

## Usage

1. Select a GameObject with the `Player` component in the scene.
2. You will see the custom inspector with various interactive elements:
    - Adjust the player's health via the integer field.
    - Use the dropdown menu to spawn primitive GameObjects.
    - Adjust custom values using the knob or sliders.
    - Experiment with different `MyEnum` flag and popup settings.

### Custom Controls:

- **Knob Control**: Allows you to set float values in a circular visual interface.
- **Animation Curves**: Modify animation curves directly from the inspector.
- **Toggle & Slider**: Switch between different options and control values using sliders and toggles.

## Customization

You can easily extend the editor functionality by adding new controls and custom logic to the `PlayerEditor.cs` script.

To add new features:
1. Open the `PlayerEditor.cs` script.
2. Add or modify existing methods like `Knob()`, `PopUp()`, or `IntField()` to include new controls.
3. Use Unity's built-in `EditorGUILayout` and `GUILayout` methods for additional input options.
 