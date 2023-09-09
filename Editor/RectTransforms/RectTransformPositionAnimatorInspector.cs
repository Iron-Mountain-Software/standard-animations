using IronMountain.StandardAnimations.RectTransforms;
using UnityEditor;
using UnityEngine;

namespace IronMountain.StandardAnimations.Editor.RectTransforms
{
    [CustomEditor(typeof(RectTransformPositionAnimator))]
    public class RectTransformPositionAnimatorInspector : UnityEditor.Editor
    {
        protected static readonly GUIContent 
            MoveUpButtonContent = new ("↑", "Move up."),
            MoveDownButtonContent = new ("↓", "Move down."),
            AddNewButtonContent = new ("Add", "Add new."),
            DeleteButtonContent = new ("✕", "Delete.");

        private RectTransformPositionAnimator _rectTransformPositionAnimator;

        private void OnEnable()
        {
            _rectTransformPositionAnimator = (RectTransformPositionAnimator) target;
        }

        public override void OnInspectorGUI()
        {
            using (new EditorGUI.DisabledScope(true))
                EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), GetType(), false);
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("loop"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("closeLoop"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("smooth"));
            
            SerializedProperty list = serializedObject.FindProperty("steps");
            
            for (int i = 0; i < list.arraySize; i++)
            {
                SerializedProperty step = list.GetArrayElementAtIndex(i);
                
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Step " + i, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(75)))
                {
                    Vector2 anchorMin = step.FindPropertyRelative("anchorMin").vector2Value;
                    Vector2 anchorMax = step.FindPropertyRelative("anchorMax").vector2Value;
                    if (_rectTransformPositionAnimator) _rectTransformPositionAnimator.SetPosition(anchorMin, anchorMax);
                }

                EditorGUILayout.BeginVertical();
                DrawStepData(step);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(GUILayout.MaxWidth(30));
                DrawButtons(list, i);
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.Space(10);
            }
            
            if (GUILayout.Button(AddNewButtonContent))
            {
                list.InsertArrayElementAtIndex(list.arraySize);
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawStepData(SerializedProperty step)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Before", GUILayout.MaxWidth(50));
            EditorGUILayout.PropertyField(step.FindPropertyRelative("beforeTravelSeconds"), GUIContent.none);
            EditorGUILayout.LabelField("During", GUILayout.MaxWidth(50));
            EditorGUILayout.PropertyField(step.FindPropertyRelative("duringTravelSeconds"), GUIContent.none);
            EditorGUILayout.LabelField("After", GUILayout.MaxWidth(50));
            EditorGUILayout.PropertyField(step.FindPropertyRelative("afterTravelSeconds"), GUIContent.none);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Anchor Min", GUILayout.MaxWidth(100));
            EditorGUILayout.PropertyField(step.FindPropertyRelative("anchorMin"), GUIContent.none);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Anchor Max", GUILayout.MaxWidth(100));
            EditorGUILayout.PropertyField(step.FindPropertyRelative("anchorMax"), GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawButtons(SerializedProperty list, int i)
        {
            if (GUILayout.Button(MoveUpButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            {
                list.MoveArrayElement(i, i - 1);
            }            
            if (GUILayout.Button(DeleteButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            { 
                list.DeleteArrayElementAtIndex(i);
            }            
            if (GUILayout.Button(MoveDownButtonContent, GUILayout.ExpandHeight(true), GUILayout.MaxWidth(20)))
            { 
                list.MoveArrayElement(i, i + 1);
            }
        }
    }
}
