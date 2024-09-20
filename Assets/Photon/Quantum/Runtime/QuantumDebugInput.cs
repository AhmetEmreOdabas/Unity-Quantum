namespace Quantum {
  using Photon.Deterministic;
  using UnityEngine;

  /// <summary>
  /// A Unity script that creates empty input for any Quantum game.
  /// </summary>
  public class QuantumDebugInput : MonoBehaviour {
    public PlayerInput playerInput;
    private void OnEnable() {
      QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    }

    /// <summary>
    /// Set an empty input when polled by the simulation.
    /// </summary>
    /// <param name="callback"></param>
    public void PollInput(CallbackPollInput callback) {
      Quantum.Input i = new Quantum.Input();
      i.Direction = new FPVector2(playerInput.Joystick.Horizontal.ToFP(), playerInput.Joystick.Vertical.ToFP());
      i.Jump = playerInput.JumpWasPressed;
      callback.SetInput(i, DeterministicInputFlags.Repeatable);
    }
  }
}
