// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial
// declarations in another file.
// </auto-generated>
#pragma warning disable 0109
#pragma warning disable 1591


namespace Quantum {
  using Photon.Deterministic;
  using Quantum;
  using Quantum.Core;
  using Quantum.Collections;
  using Quantum.Inspector;
  using Quantum.Physics2D;
  using Quantum.Physics3D;
  using Byte = System.Byte;
  using SByte = System.SByte;
  using Int16 = System.Int16;
  using UInt16 = System.UInt16;
  using Int32 = System.Int32;
  using UInt32 = System.UInt32;
  using Int64 = System.Int64;
  using UInt64 = System.UInt64;
  using Boolean = System.Boolean;
  using String = System.String;
  using Object = System.Object;
  using FlagsAttribute = System.FlagsAttribute;
  using SerializableAttribute = System.SerializableAttribute;
  using MethodImplAttribute = System.Runtime.CompilerServices.MethodImplAttribute;
  using MethodImplOptions = System.Runtime.CompilerServices.MethodImplOptions;
  using FieldOffsetAttribute = System.Runtime.InteropServices.FieldOffsetAttribute;
  using StructLayoutAttribute = System.Runtime.InteropServices.StructLayoutAttribute;
  using LayoutKind = System.Runtime.InteropServices.LayoutKind;
  #if QUANTUM_UNITY //;
  using TooltipAttribute = UnityEngine.TooltipAttribute;
  using HeaderAttribute = UnityEngine.HeaderAttribute;
  using SpaceAttribute = UnityEngine.SpaceAttribute;
  using RangeAttribute = UnityEngine.RangeAttribute;
  using HideInInspectorAttribute = UnityEngine.HideInInspector;
  using PreserveAttribute = UnityEngine.Scripting.PreserveAttribute;
  using FormerlySerializedAsAttribute = UnityEngine.Serialization.FormerlySerializedAsAttribute;
  using MovedFromAttribute = UnityEngine.Scripting.APIUpdating.MovedFromAttribute;
  using CreateAssetMenu = UnityEngine.CreateAssetMenuAttribute;
  using RuntimeInitializeOnLoadMethodAttribute = UnityEngine.RuntimeInitializeOnLoadMethodAttribute;
  #endif //;
  
  public unsafe partial class Frame {
    public unsafe partial struct FrameEvents {
      static partial void GetEventTypeCountCodeGen(ref Int32 eventCount) {
        eventCount = 6;
      }
      static partial void GetParentEventIDCodeGen(Int32 eventID, ref Int32 parentEventID) {
        switch (eventID) {
          case EventOnRobotTakeDamage.ID: parentEventID = EventRobotEvent.ID; return;
          case EventOnRobotDeath.ID: parentEventID = EventRobotEvent.ID; return;
          default: break;
        }
      }
      static partial void GetEventTypeCodeGen(Int32 eventID, ref System.Type result) {
        switch (eventID) {
          case EventOnBulletDestroyed.ID: result = typeof(EventOnBulletDestroyed); return;
          case EventRobotEvent.ID: result = typeof(EventRobotEvent); return;
          case EventOnRobotTakeDamage.ID: result = typeof(EventOnRobotTakeDamage); return;
          case EventOnRobotDeath.ID: result = typeof(EventOnRobotDeath); return;
          case EventOnWeaponShoot.ID: result = typeof(EventOnWeaponShoot); return;
          default: break;
        }
      }
      public EventOnBulletDestroyed OnBulletDestroyed(Int32 BulletRefHashCode, EntityRef Robot, FPVector3 BulletPosition, FPVector3 BulletDirection, AssetRef<BulletData> BulletData) {
        var ev = _f.Context.AcquireEvent<EventOnBulletDestroyed>(EventOnBulletDestroyed.ID);
        ev.BulletRefHashCode = BulletRefHashCode;
        ev.Robot = Robot;
        ev.BulletPosition = BulletPosition;
        ev.BulletDirection = BulletDirection;
        ev.BulletData = BulletData;
        _f.AddEvent(ev);
        return ev;
      }
      public EventOnRobotTakeDamage OnRobotTakeDamage(EntityRef Robot, FP Damage, EntityRef Source) {
        var ev = _f.Context.AcquireEvent<EventOnRobotTakeDamage>(EventOnRobotTakeDamage.ID);
        ev.Robot = Robot;
        ev.Damage = Damage;
        ev.Source = Source;
        _f.AddEvent(ev);
        return ev;
      }
      public EventOnRobotDeath OnRobotDeath(EntityRef Robot, EntityRef Killer) {
        if (_f.IsPredicted) return null;
        var ev = _f.Context.AcquireEvent<EventOnRobotDeath>(EventOnRobotDeath.ID);
        ev.Robot = Robot;
        ev.Killer = Killer;
        _f.AddEvent(ev);
        return ev;
      }
      public EventOnWeaponShoot OnWeaponShoot(EntityRef Robot) {
        var ev = _f.Context.AcquireEvent<EventOnWeaponShoot>(EventOnWeaponShoot.ID);
        ev.Robot = Robot;
        _f.AddEvent(ev);
        return ev;
      }
    }
  }
  public unsafe partial class EventOnBulletDestroyed : EventBase {
    public new const Int32 ID = 1;
    public Int32 BulletRefHashCode;
    public EntityRef Robot;
    public FPVector3 BulletPosition;
    public FPVector3 BulletDirection;
    public AssetRef<BulletData> BulletData;
    protected EventOnBulletDestroyed(Int32 id, EventFlags flags) : 
        base(id, flags) {
    }
    public EventOnBulletDestroyed() : 
        base(1, EventFlags.Server|EventFlags.Client) {
    }
    public new QuantumGame Game {
      get {
        return (QuantumGame)base.Game;
      }
      set {
        base.Game = value;
      }
    }
    public override Int32 GetHashCode() {
      unchecked {
        var hash = 41;
        hash = hash * 31 + BulletRefHashCode.GetHashCode();
        hash = hash * 31 + Robot.GetHashCode();
        hash = hash * 31 + BulletPosition.GetHashCode();
        hash = hash * 31 + BulletDirection.GetHashCode();
        hash = hash * 31 + BulletData.GetHashCode();
        return hash;
      }
    }
  }
  public abstract unsafe partial class EventRobotEvent : EventBase {
    public new const Int32 ID = 2;
    public EntityRef Robot;
    protected EventRobotEvent(Int32 id, EventFlags flags) : 
        base(id, flags) {
    }
    public new QuantumGame Game {
      get {
        return (QuantumGame)base.Game;
      }
      set {
        base.Game = value;
      }
    }
    public override Int32 GetHashCode() {
      unchecked {
        var hash = 43;
        hash = hash * 31 + Robot.GetHashCode();
        return hash;
      }
    }
  }
  public unsafe partial class EventOnRobotTakeDamage : EventRobotEvent {
    public new const Int32 ID = 3;
    public FP Damage;
    public EntityRef Source;
    protected EventOnRobotTakeDamage(Int32 id, EventFlags flags) : 
        base(id, flags) {
    }
    public EventOnRobotTakeDamage() : 
        base(3, EventFlags.Server|EventFlags.Client) {
    }
    public override Int32 GetHashCode() {
      unchecked {
        var hash = 47;
        hash = hash * 31 + Robot.GetHashCode();
        hash = hash * 31 + Damage.GetHashCode();
        hash = hash * 31 + Source.GetHashCode();
        return hash;
      }
    }
  }
  public unsafe partial class EventOnRobotDeath : EventRobotEvent {
    public new const Int32 ID = 4;
    public EntityRef Killer;
    protected EventOnRobotDeath(Int32 id, EventFlags flags) : 
        base(id, flags) {
    }
    public EventOnRobotDeath() : 
        base(4, EventFlags.Server|EventFlags.Client|EventFlags.Synced) {
    }
    public override Int32 GetHashCode() {
      unchecked {
        var hash = 53;
        hash = hash * 31 + Robot.GetHashCode();
        hash = hash * 31 + Killer.GetHashCode();
        return hash;
      }
    }
  }
  public unsafe partial class EventOnWeaponShoot : EventBase {
    public new const Int32 ID = 5;
    public EntityRef Robot;
    protected EventOnWeaponShoot(Int32 id, EventFlags flags) : 
        base(id, flags) {
    }
    public EventOnWeaponShoot() : 
        base(5, EventFlags.Server|EventFlags.Client) {
    }
    public new QuantumGame Game {
      get {
        return (QuantumGame)base.Game;
      }
      set {
        base.Game = value;
      }
    }
    public override Int32 GetHashCode() {
      unchecked {
        var hash = 59;
        hash = hash * 31 + Robot.GetHashCode();
        return hash;
      }
    }
  }
}
#pragma warning restore 0109
#pragma warning restore 1591
