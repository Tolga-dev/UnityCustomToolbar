using UnityEngine;

namespace EditorProgramming.E4.GUIStyle
{
    public class SimpleGuiExample : MonoBehaviour
    {
        private string textFieldInput = "Enter text here...";
        private float sliderValue = 0.5f;

        public void OnGUI()
        {
            
            // Create a label
            GUI.Label(new Rect(10, 10, 200, 20), "Welcome to OnGUI Example!");

            // Create a text field
            textFieldInput = GUI.TextField(new Rect(10, 40, 200, 20), textFieldInput, 25);

            // Create a button
            if (GUI.Button(new Rect(10, 70, 200, 30), "Click Me!"))
            {
                Debug.Log("Button clicked!");
            }

            // Create a slider
            sliderValue = GUI.HorizontalSlider(new Rect(10, 110, 200, 30), sliderValue, 0.0f, 1.0f);
            GUI.Label(new Rect(220, 110, 100, 20), "Value: " + sliderValue.ToString("F2"));

            // Create a toggle
            bool toggleValue = GUI.Toggle(new Rect(10, 150, 200, 30), false, "Toggle me!");
            if (toggleValue)
            {
                Debug.Log("Toggle is ON!");
            }
            
            
            
        }
    }
}