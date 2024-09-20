using UnityEngine;

namespace Quantum
{
    public class CharacterAnimationView : QuantumEntityViewComponent<CustomViewContext>
    {
        public CharacterAnimatorHandler CharacterAnimatorHandler;
        public override void OnUpdateView()
        {
            var characterController = PredictedFrame.Get<CharacterController3D>(EntityRef);
            CharacterAnimatorHandler.SetParameter("Movement", characterController.Velocity.Magnitude.AsFloat / characterController.MaxSpeed.AsFloat);
        }
    }

}
