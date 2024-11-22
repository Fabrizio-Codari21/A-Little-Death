using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enums de acceso general
public static class ENUMS
{

}

public enum SkillSlot { primary, secondary, }

public enum SkillType
{
    Default,

    ThaniaPrimary,
    ThaniaSecondary,

    DeerPrimary,
    DeerSecondary,

    HarpyPrimary,
    HarpySecondary,

    BustPrimary,
    BustSecondary,

    GorgonPrimary,
    GorgonSecondary,
}

public enum PlayerAppearance
{
    Thania,
    Deer,
    Harpy,
    Soul,
    Bust,
    Gorgon,
}

public enum ColliderAction
{
    None,
    Damage,
    Break,
    Crouch,
}

public enum MyInputs 
{ 
    None,
    Up,
    Down,
    Left,
    Right,
    MoveSkill,
    PrimarySkill,
    SecondarySkill,
    Possess,
    UnPossess,
    Pause,
    Secret1,
    Secret2,
    Secret3,
    Secret4,
    Skip,
}
