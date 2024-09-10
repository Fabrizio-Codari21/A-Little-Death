using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public SkillList[] skills = new SkillList[2];
    //public List<SkillList> skills = new();
    public Dictionary<ENUMS.SkillType, SkillList> mySkills = new Dictionary<ENUMS.SkillType, SkillList>();
    public CharacterSkillSet baseSkills;

    private void Start()
    {
        if (baseSkills == null) print("no encuentro el script");
        if (baseSkills.primarySkill.Equals(default) 
           || baseSkills.secondarySkill.Equals(default)) print("no encuentro los structs");

        var readyUpSkills = BuildSkillSet(baseSkills.primarySkill, baseSkills.secondarySkill);
        if (!readyUpSkills) Debug.Log("No se pudieron crear las habilidades");
    }

    public bool BuildSkillSet(SkillSet primary, SkillSet secondary)
    {
        // Borramos las habilidades previas si es que existen
        if(skills != default) skills = default;

        // Creamos nuevas habilidades con los struct recibidos
        skills[0] = new SkillList(primary);
        skills[1] = new SkillList(secondary);

        // Agregamos las skills asignadas
        foreach (var skill in skills) mySkills.Add(skill.skillType, skill);

        return mySkills != default;
    }

    private void Update()
    {
        CheckSkillInput(0);
        CheckSkillInput(1);
    }

    public void CheckSkillInput(int skillId)
    {
        // AL apretar el boton del mouse correspondiente al tipo de habilidad...
        if (Input.GetMouseButtonDown(skills[skillId].input))
        {
            print("usando habilidad");

            // ...chequeamos si tenemos la habilidad necesaria y de ser asi la ejecutamos.
            if (mySkills.ContainsKey(skills[skillId].skillType)) 
                mySkills[skills[skillId].skillType].Execute();

            // Si no, se realiza la habilidad por defecto
            else mySkills[ENUMS.SkillType.Default].Execute();
        }
    }
}
