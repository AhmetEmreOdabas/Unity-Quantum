
namespace Quantum {
  using UnityEngine;
  public class PlayerInput : MonoBehaviour {
    public bool JumpWasPressed { get; private set; }
    public FixedJoystick Joystick;
    public void OnJumpButtonDown() {
      JumpWasPressed = true;
    }
    public void OnJumpButtonUp() {
      JumpWasPressed = false;
    }
  }
}
