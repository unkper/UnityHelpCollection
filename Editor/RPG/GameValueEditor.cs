using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(GameValuesSys))]
public class GameValueEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var show = target as GameValuesSys;
        EditorGUILayout.LabelField("生命上限");
        show.healthLimit = EditorGUILayout.IntField(show.healthLimit);
        EditorGUILayout.LabelField("魔法上限");
        show.magicLimit = EditorGUILayout.IntField(show.magicLimit);
        EditorGUILayout.LabelField("物理抗性");
        show.physicalResistance = EditorGUILayout.FloatField(show.physicalResistance);
        EditorGUILayout.LabelField("魔法抗性");
        show.magicResistance = EditorGUILayout.FloatField(show.magicResistance);
        EditorGUILayout.LabelField("攻击力");
        show.attackValue = EditorGUILayout.IntField(show.attackValue);
        EditorGUILayout.LabelField("攻击速度");
        show.attackSpeed = EditorGUILayout.IntField(show.attackSpeed);
        EditorGUILayout.LabelField("法术强度");
        show.spellPower = EditorGUILayout.FloatField(show.spellPower);
        EditorGUILayout.LabelField("暴击率");
        show.CritRate = EditorGUILayout.FloatField(show.CritRate);
        EditorGUILayout.LabelField("闪避率");
        show.dodgeRate = EditorGUILayout.FloatField(show.dodgeRate);
        EditorGUILayout.LabelField("生命恢复速度");
        show.hpRestoreSpeed = EditorGUILayout.IntField(show.hpRestoreSpeed);
        EditorGUILayout.LabelField("魔法恢复速度");
        show.mpRestoreSpeed = EditorGUILayout.IntField(show.mpRestoreSpeed);
        EditorGUILayout.LabelField("暴击倍数");
        show.CritcalMult = EditorGUILayout.IntField(show.CritcalMult);
        




    }
}
