using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIController))]
public class AIControllerEditor : Editor {
    public SerializedProperty
    _aiBehaviourProp,
    _searchRadiusProp,
    _fieldofViewProp,
    _guardPositionProp,
    _patrolPathHolderProp,
    controllable_Prop;
    void OnEnable()
    {
        _aiBehaviourProp = serializedObject.FindProperty("_aiBehaviour");
        _searchRadiusProp = serializedObject.FindProperty("_searchRadius");
        _fieldofViewProp = serializedObject.FindProperty("_fieldofView");
        _guardPositionProp = serializedObject.FindProperty("_guardPosition");
        _patrolPathHolderProp = serializedObject.FindProperty("_patrolPathHolder");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.IntSlider(_searchRadiusProp, 0, 100, new GUIContent("Search radius"));
        EditorGUILayout.IntSlider(_fieldofViewProp, 0, 360, new GUIContent("FOV Angle"));
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_aiBehaviourProp, new GUIContent("AI Type"));

        AIController.AIBehaviour type = (AIController.AIBehaviour) _aiBehaviourProp.enumValueIndex;

        switch (type)
        {
            case AIController.AIBehaviour.Guard:
                EditorGUILayout.ObjectField( _guardPositionProp, typeof(Transform), new GUIContent("Guarding position"));
                break;
            case AIController.AIBehaviour.Patrol:
                EditorGUILayout.ObjectField(_patrolPathHolderProp, typeof(Transform), new GUIContent("Patrol path holder"));
                break;
            case AIController.AIBehaviour.Kamikaze:
                break;
            case AIController.AIBehaviour.Mage:
                break;
            default:
                break;
        }
        serializedObject.ApplyModifiedProperties();

    }

    private void OnSceneGUI()
    {
        AIController controller = (AIController)target;
        var controllerPos = controller.transform.position;
        var FOVstart = controller.transform.DirectionFromAngle(-controller.FieldOfView / 2);
        var FOVEnd = controller.transform.DirectionFromAngle(controller.FieldOfView / 2);

        Handles.color = Color.red;
        Handles.DrawLine(controllerPos, controllerPos + FOVstart * controller.SearchRadius);
        Handles.DrawLine(controllerPos, controllerPos + FOVEnd * controller.SearchRadius);

        Handles.DrawWireArc(controllerPos, Vector3.up, FOVstart, controller.FieldOfView, controller.SearchRadius);
    }

}
