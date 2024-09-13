using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public PlayerSkills sk;

    private void Start()
    {
        sk.baseSkills = GetComponent<CharacterSkillSet>();

        if (sk.baseSkills == null) print("no encuentro el script");
        if (sk.baseSkills.primarySkill.Equals(default) 
           || sk.baseSkills.secondarySkill.Equals(default)) print("no encuentro los structs");

        var readyUpSkills = BuildSkillSet(sk.baseSkills.primarySkill, sk.baseSkills.secondarySkill);
        if (!readyUpSkills) Debug.Log("No se pudieron crear las habilidades");
    }

    public bool BuildSkillSet(SkillSet primary, SkillSet secondary)
    {
        // Borramos las habilidades previas si es que existen
        //if(sk.skills != default) sk.skills = default;

        // Creamos nuevas habilidades con los struct recibidos
        sk.skills[0] = new SkillList(primary);
        sk.skills[1] = new SkillList(secondary);

        // Agregamos las skills asignadas
        foreach (var skill in sk.skills) sk.mySkills.Add(skill.skillType, skill);

        return sk.mySkills != default;
    }

    private void Update()
    {
        CheckSkillInput(0);
        CheckSkillInput(1);
    }

    public void CheckSkillInput(int skillId)
    {
        // AL apretar el boton del mouse correspondiente al tipo de habilidad...
        if (Input.GetMouseButtonDown(sk.skills[skillId].input))
        {
            print("usando habilidad");

            // ...chequeamos si tenemos la habilidad necesaria y de ser asi la ejecutamos.
            if (sk.mySkills.ContainsKey(sk.skills[skillId].skillType)) 
                sk.mySkills[sk.skills[skillId].skillType].Execute();

            // Si no, se realiza la habilidad por defecto
            else sk.mySkills[SkillType.Default].Execute();
        }
    }
}
