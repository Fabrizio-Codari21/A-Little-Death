using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

// Maneja el cambio de skills al poseer y desposeer
// (ESTE LO TIENE QUE TENER EL PLAYER Y NADIE MAS)
public class PlayerSkillManager : MonoBehaviour
{
    [Header("SKILLS INFO")]
    public PlayerSkills sk;
    public ThaniaSkills defaultSkills;
    public JumpManager jumpManager;
    public ThaniaMovement thaniaMovement;
    [HideInInspector] public ThaniaHealth thaniaHealth;
    public GroundCheck groundCheck;
    public float possessingRange;



    [Header("UI")]
    public HabilityUI skillUI;
    public TimerBar timerUI;
    public GameObject possessionUI;
    public GameObject scythePossessionIcon;
    public GameObject hitVFX;

    [Header("POSSESSABLE CREATURE SPRITES")]
    public List<PlayerSprite> mySprites;
    public Dictionary<PlayerAppearance, PlayerSprite> sprites = new();
    [HideInInspector] public PlayerAppearance _currentSprite;
    float _possessingTime;

    IEnumerator _whilePossessing;
    //public ParticleSystem transitionParticle;
    public GameObject transitionParticle;
    [SerializeField] GameObject cadaver;
    CharacterSkillSet _victim;
    [SerializeField] float posesionSpeed;
    float posesionSpeedBase;

    [HideInInspector] public bool isBreaking;

    private void Start()
    {
        sk.baseSkills = GetComponent<CharacterSkillSet>();
        thaniaHealth = GetComponent<ThaniaHealth>();
        posesionSpeedBase = posesionSpeed;

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

        if (_isPossessing && this.Inputs(MyInputs.UnPossess))
        {
            if (_whilePossessing != null)
            {
                StopCoroutine(_whilePossessing);
                print("end possession");
            }
            EndPossession();
        }

        if (onTarget == false && _isPossessing)
        {
            if (Vector2.Distance(transform.position, _victim.transform.position) >= 0.2f)
            {
                transform.position += (_victim.transform.position - transform.position) * posesionSpeed * Time.deltaTime; 
                posesionSpeed *= 1.01f;
            }
            else
            {
                transform.position = _victim.transform.position;
                onTarget = true;
                thaniaMovement.rb.isKinematic = false;
                //transitionParticle.startColor = new Color(1, 0.5f, 0.25f);
                //transitionParticle.Play();

                thaniaMovement.isPossessing = true;
                if (_victim.GetComponent<EntityMovement>().facingRight != thaniaMovement.isFacingRight)
                { 
                    thaniaMovement.Flip(true);
                    Debug.Log("se da vuelta");
                }

                this.WaitAndThen(1f, () =>
                {
                    thaniaHealth.audioManager.possessionB.Play();
                    sprites[PlayerAppearance.Soul].gameObject.SetActive(false);
                    sprites[_currentSprite].gameObject.SetActive(true);
                    defaultSkills.movement.anim = sprites[_currentSprite].animator;
                    jumpManager.anim = sprites[_currentSprite].animator;
                    groundCheck.feet = _victim.creatureFeetArea;

                    _whilePossessing = WhilePossessing();
                    StartCoroutine(_whilePossessing);

                    possessionUI.gameObject.SetActive(true);
                    //if(scythePossessionIcon) scythePossessionIcon.gameObject.SetActive(true);
                    if (sprites[_currentSprite].timerSoul) sprites[_currentSprite].timerSoul.SetActive(true);

                    timerUI.maxTime = timerUI.timeLeft = _possessingTime;
                    timerUI.ActivateTimer(true);
                    _victim.GetComponent<PossessableHealth>().Die();
                    //thaniaMovement.canMove = true;
                    //jumpManager.canMove = true;
                    posesionSpeed = posesionSpeedBase;
                    thaniaMovement.isPossessing = false;
                });
            }
        }
    }

    public void CanMove()
    {
        thaniaMovement.canMove = true;
        if (_currentSprite != PlayerAppearance.Gorgon) jumpManager.canMove = true;
    }

    // Para revisar los inputs de las habilidades activas y ejecutarlas en base a ellos.
    public void CheckSkillInput(int skillId)
    {
        // AL apretar el boton del mouse correspondiente al tipo de habilidad...
        if (Input.GetMouseButtonDown(sk.skills[skillId].input))
        {
            print("usando habilidad");

            if (sk.mySkills == null) print("no tenemos sk.Myskills");
            if (sk.skills == null) print("no tenemos sk.Skills");
            if (sk.skills[skillId] == null) print("no tenemos sk.skills[skillId]");
            if (sk.skills[skillId].skillType == SkillType.Default) print("no tenemos skillType");
            if (sk.mySkills[sk.skills[skillId].skillType].Execute == null) print("no tenemos Execute");


            
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

        // Esto tambien habria que mejorarlo
        if (victim.secondarySkillType == SkillType.DeerSecondary) skillUI.FuerzaDash();
        if (victim.secondarySkillType == SkillType.HarpySecondary) skillUI.AlasDash();
        if (victim.secondarySkillType == SkillType.BustSecondary) skillUI.Busto();
        if (victim.primarySkillType == SkillType.GorgonPrimary) skillUI.Gorgona();

        _currentSprite = newAppearance;
        _possessingTime = possessTime;
        //timerUI.maxTime = possessTime;

        thaniaHealth.audioManager.possessionA.Play();
        sprites[PlayerAppearance.Thania].gameObject.SetActive(false);
        var cuerpo = Instantiate(cadaver, transform.position + new Vector3(1.66f, -1.2f, 0), Quaternion.identity);
        cuerpo.transform.localScale = transform.localScale;
        sprites[PlayerAppearance.Soul].gameObject.SetActive(true);
        if(victim.transform.position.x < transform.position.x && thaniaMovement.isFacingRight)
        {
            sprites[PlayerAppearance.Soul].gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if(victim.transform.position.x > transform.position.x && !thaniaMovement.isFacingRight)
        {
            sprites[PlayerAppearance.Soul].gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        thaniaMovement.canMove = false;
        jumpManager.canMove = false;
        thaniaMovement.rb.velocity = Vector2.zero;
        thaniaMovement.rb.isKinematic = true;


        _isPossessing = true;
        _victim = victim;
    }

    bool _isPossessing = false;
    private bool onTarget;

    public IEnumerator WhilePossessing()
    {
        yield return new WaitForSeconds(_possessingTime);

        EndPossession();
    }

    void EndPossession()
    {
        skillUI.Default();
        timerUI.timeLeft = 0;
        //timerUI.ActivateTimer(false);
        Instantiate(sprites[_currentSprite].soul, transform.position, Quaternion.Euler(270, 180, 0));
        //transitionParticle.startColor = Color.cyan;
        //transitionParticle.Play();
        //Instantiate(transitionParticle, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
        thaniaMovement.anim.animator.SetTrigger("DESPOSESS");

        timerUI.UI.gameObject.SetActive(false);
        //if (scythePossessionIcon) scythePossessionIcon.gameObject.SetActive(false);
        if (sprites[_currentSprite].timerSoul) sprites[_currentSprite].timerSoul.SetActive(false);
    }

    public void EndAnimationDesposession()
    {
        sprites[PlayerAppearance.Soul].gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        thaniaHealth.audioManager.possessionB.Play();
        sprites[_currentSprite].gameObject.SetActive(false);
        sprites[PlayerAppearance.Thania].gameObject.SetActive(true);
        thaniaMovement.anim.baseFormAnimator.SetTrigger("DESPOSESS");
        defaultSkills.movement.anim = sprites[PlayerAppearance.Thania].animator;
        jumpManager.anim = sprites[PlayerAppearance.Thania].animator;
        groundCheck.feet = sk.baseSkills.creatureFeetArea;

        // Despues habria que mejorar esto
        jumpManager.dJump = false;
        jumpManager.canMove = true;

        defaultSkills.DefineSkills(sk.baseSkills);
        BuildSkillSet(sk.baseSkills.primarySkill, sk.baseSkills.secondarySkill);

        _isPossessing = false;
        onTarget = false;
    }

    // Cambia la accion de la colision cuando la habilidad se activa y la hace desaparecer cuando se desactiva.
    public void SetColliderAction(CharacterSkillSet mySkills, bool isSkillActive, SkillSlot type = default)
    {
        if (isSkillActive)
        {
            sprites[_currentSprite].actionWhenColliding =
                (type == SkillSlot.primary)
                ? mySkills.primaryColliderAction
                : mySkills.secondaryColliderAction;

            print($"On collision this skill should: {sprites[_currentSprite].actionWhenColliding}");
        }
        else sprites[_currentSprite].actionWhenColliding = ColliderAction.None;
    }

    public ColliderAction GetColliderAction() => sprites[_currentSprite].actionWhenColliding;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, possessingRange);
    }
}
