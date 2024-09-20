
namespace Quantum {
  using UnityEngine;
  public class PlayerInput : MonoBehaviour {
    public bool JumpWasPressed { get; private set; }
    public bool FireWasPressed { get; private set; }
    public FixedJoystick Joystick;
    public void OnJumpButtonDown() {
      JumpWasPressed = true;
    }
    public void OnJumpButtonUp() {
      JumpWasPressed = false;
    }
    public void OnFireButtonDown() {
      FireWasPressed = true;
    }
    public void OnFireButtonUp() {
      FireWasPressed = false;
    }
  }
}
