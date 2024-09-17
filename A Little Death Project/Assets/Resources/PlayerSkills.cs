using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkills", menuName = "ScriptableObjects/PlayerSkills")]
public class PlayerSkills : ScriptableObject
{
    public SkillList[] skills = new SkillList[2];
    public Dictionary<SkillType, SkillList> mySkills = new Dictionary<SkillType, SkillList>();
    public CharacterSkillSet baseSkills;
}
