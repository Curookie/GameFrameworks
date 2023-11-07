using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.UI;
using UnityEditor.AnimatedValues;

namespace GameFrameworks 
{
    [CustomEditor(typeof(UI_Button), true)]
    [CanEditMultipleObjects]
    public class UI_ButtonEditor : ButtonEditor
    {
        SerializedProperty m_animType;
        // SerializedProperty m_falloffDist;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_animType = serializedObject.FindProperty("animType");

            // m_Type = serializedObject.FindProperty("m_Type");
            // m_FillMethod = serializedObject.FindProperty("m_FillMethod");
            // m_FillOrigin = serializedObject.FindProperty("m_FillOrigin");
            // m_FillClockwise = serializedObject.FindProperty("m_FillClockwise");
            // m_FillAmount = serializedObject.FindProperty("m_FillAmount");
            // m_Sprite = serializedObject.FindProperty("m_Sprite");

            // var typeEnum = (Image.Type)m_Type.enumValueIndex;

            // showFilled = new AnimBool(!m_Type.hasMultipleDifferentValues && typeEnum == Image.Type.Filled);
            // showFilled.valueChanged.AddListener(Repaint);

            // attrList = ModifierUtility.GetAttributeList();

            // m_borderWidth = serializedObject.FindProperty("borderWidth");
            // m_falloffDist = serializedObject.FindProperty("falloffDistance");

            // if ((target as ProceduralImage).GetComponent<ProceduralImageModifier>() != null)
            // {
            //     selectedId = attrList.IndexOf(((ModifierID[])(target as ProceduralImage).GetComponent<ProceduralImageModifier>().GetType().GetCustomAttributes(typeof(ModifierID), false))[0]);
            // }
            // selectedId = Mathf.Max(selectedId, 0);
            // EditorApplication.update -= UpdateProceduralImage;
            // EditorApplication.update += UpdateProceduralImage;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            // ModifierGUI();
            EditorGUILayout.PropertyField(m_animType);
            // EditorGUILayout.PropertyField(m_falloffDist);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
