using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Maneja el cambio de skills al poseer y desposeer
// (ESTE LO TIENE QUE TENER EL PLAYER Y NADIE MAS)
public class PlayerSkillManager : MonoBehaviour
{
    [Header("SKILLS INFO")]
    public PlayerSkills sk;
    public ThaniaSkills defaultSkills;
    public JumpManager jumpManager;
    public GroundCheck groundCheck;
    public float possessingRange;

    [Header("UI")]
    public HabilityUI skillUI;
    public TimerBar timerUI;
    public GameObject possessionUI;

    [Header("POSSESSABLE CREATURE SPRITES")]
    public List<PlayerSprite> mySprites;
    public Dictionary<PlayerAppearance, PlayerSprite> sprites = new();
    PlayerAppearance _currentSprite;
    float _possessingTime;

    private void Start()
    {
        sk.baseSkills = GetComponent<CharacterSkillSet>();

        foreach (var sprite in mySprites)
        {
            if (sprites.ContainsKey(sprite.whatSpriteIsThis)) sprites[sprite.whatSpriteIsThis] = sprite; 
            else sprites.Add(sprite.whatSpriteIsThis, sprite);
        }

        if (sk.baseSkills == null) print("no encuentro el script");
        if (sk.baseSkills.primarySkill.Equals(default)
           || sk.baseSkills.secondarySkill.Equals(default)) print("no encuentro los structs");

        var readyUpSkills = BuildSkillSet(sk.baseSkills.primarySkill, sk.baseSkills.secondarySkill);
        if (!readyUpSkills) Debug.Log("No hubo cambio en las habilidades"); else print(sk.baseSkills.secondarySkill.skillType);
    }



    private void Update()
    {
        CheckSkillInput(0);
        CheckSkillInput(1);

        if (_isPossessing && Input.GetKeyDown(KeyCode.E)) 
        {
            StopCoroutine(WhilePossessing());
            EndPossession();
        } 
    }


    // Para revisar los inputs de las habilidades activas y ejecutarlas en base a ellos.
    public void CheckSkillInput(int skillId)
    {
        // AL apretar el boton del mouse correspondiente al tipo de habilidad...
        if (Input.GetMouseButtonDown(sk.skills[skillId].input))
        {
            print("usando habilidad");

            // ...chequeamos si tenemos la habilidad necesaria y de ser asi la ejecutamos.
            if (sk.mySkills.ContainsKey(sk.skills[skillId].skillType))
                sk.mySkills[sk.skills[skillId].skillType].Execute(this);

            // Si no, se realiza la habilidad por defecto
            else { sk.mySkills[SkillType.Default].Execute(this); print("default"); }
        }
    }

    // Para reconstruir el set de habilidades a la hora de poseer
    public bool BuildSkillSet(SkillSet primary, SkillSet secondary)
    {
        // Borramos las habilidades previas si es que existen
        //if(sk.skills != default) sk.skills = default;

        var oldSkills = sk.mySkills;

        // Creamos nuevas habilidades con los struct recibidos
        sk.skills[0] = new SkillList(primary);
        sk.skills[1] = new SkillList(secondary);

        // Agregamos las skills asignadas

        if (!sk.mySkills.ContainsKey(sk.skills[0].skillType))
            sk.mySkills.Add(sk.skills[0].skillType, sk.skills[0]);
        else
            sk.mySkills[sk.skills[0].skillType] = sk.skills[0];

        if (!sk.mySkills.ContainsKey(sk.skills[1].skillType))
            sk.mySkills.Add(sk.skills[1].skillType, sk.skills[1]);
        else
            sk.mySkills[sk.skills[1].skillType] = sk.skills[1];

        //foreach (var skill in sk.skills) {
        //    if (!sk.mySkills.ContainsKey(skill.skillType))
        //        sk.mySkills.Add(skill.skillType, skill);
        //    else
        //        sk.mySkills[skill.skillType] = skill;
        //}

        return sk.mySkills != oldSkills;
        //return sk.mySkills != default;
    }

    // Se llama al momento de la posesion: recibe el skill set de la victima y su "identidad" (para decidir apariencia).
    public void Possess(CharacterSkillSet victim, PlayerAppearance newAppearance, float possessTime = 10f)
    {
        print($"Obtencion de habilidades: {BuildSkillSet(victim.primarySkill, victim.secondarySkill)}");

        skillUI.FuerzaDash();
        _currentSprite = newAppearance;
        _possessingTime = possessTime;

        sprites[PlayerAppearance.Thania].gameObject.SetActive(false);
        sprites[_currentSprite].gameObject.SetActive(true);
        defaultSkills.movement.anim = sprites[_currentSprite].animator;
        jumpManager.anim = sprites[_currentSprite].animator;
        groundCheck.feet = victim.creatureFeetArea;

        StartCoroutine(WhilePossessing());
        possessionUI.gameObject.SetActive(true);
        timerUI.maxTime = timerUI.timeLeft = _possessingTime;
        timerUI.timerActive = true;
    }

    bool _isPossessing = false;
    IEnumerator WhilePossessing()
    {
        _isPossessing = true;
        yield return new WaitForSeconds(_possessingTime);

        EndPossession();
    }

    void EndPossession()
    {
        skillUI.Default();
        timerUI.timeLeft = 0;
        timerUI.timerActive = false;
        timerUI.UI.gameObject.SetActive(false);

        sprites[_currentSprite].gameObject.SetActive(false);
        sprites[PlayerAppearance.Thania].gameObject.SetActive(true);
        defaultSkills.movement.anim = sprites[PlayerAppearance.Thania].animator;
        jumpManager.anim = sprites[PlayerAppearance.Thania].animator;
        groundCheck.feet = sk.baseSkills.creatureFeetArea;

        defaultSkills.DefineSkills(sk.baseSkills);
        BuildSkillSet(sk.baseSkills.primarySkill, sk.baseSkills.secondarySkill);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, possessingRange);
    }
}


