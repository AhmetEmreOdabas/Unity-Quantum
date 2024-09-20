namespace Quantum
{
    public class CharacterCameraView : QuantumEntityViewComponent<CustomViewContext>
    {
        public override void OnActivate(Frame frame)
        {
            var link = frame.Get<PlayerLink>(EntityRef);
            var isLocal = Game.PlayerIsLocal(link.Player);
            if(isLocal)
            {
                ViewContext.thirdPersonCamera.Follow = transform;
                ViewContext.thirdPersonCamera.LookAt = transform;
            }
        }
    }
}
