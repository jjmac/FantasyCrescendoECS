﻿using System;
using Unity.Mathematics;
using Unity.Entities;

namespace HouraiTeahouse.FantasyCrescendo {

[Serializable]
public struct ScalableValue {
  public float Base;
  public float Scaling;

  public float ScaledTo(float scale) {
    return Base + scale * Scaling;
  }
}

[Serializable]
public enum HurtboxType : byte {
  INACTIVE   = 1,
  INTANGIBLE = 2,
  INVINCIBLE = 3,
  GRAZING    = 4,
  SHIELD     = 5,
}

[Flags]
[Serializable]
public enum HitboxFlags : byte {
  MIRROR_DIRECTION     = 1 << 0,
  TRASCENDENT_PRIORITY = 1 << 1,
}

public struct HitboxState : IComponentData {
  public Entity Player;
  public int ID;
  public uint PlayerID;
  public bool Enabled;
  public float3? PreviousPosition;
}

[GenerateAuthoringComponent]
public struct Hitbox : IComponentData {
  public HitboxFlags Flags;
  public float Radius;
  public uint Priority;
  public ScalableValue Damage;
  public float KnockbackAngle;
  public ScalableValue KnockbackForce;
  public ScalableValue Hitstun;

  public bool Is(HitboxFlags flags) => (Flags & flags) != 0;
  public void Set(HitboxFlags flags) => Flags = Flags | flags;
  public void Unset(HitboxFlags flags) => Flags = Flags & ~flags;
}

[GenerateAuthoringComponent]
public struct Hurtbox : IComponentData {
  public bool Enabled => Type != HurtboxType.INACTIVE;

  public Entity Player;
  public int ID;
  public uint PlayerID;
  public HurtboxType Type;
}

}