namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>, ISignalOnPlayerAdded
    {
        private FP _attackCooldown = 0;
        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
        {
            var runtimePlayer = f.GetPlayerData(player);
            var entity = f.Create(runtimePlayer.PlayerAvatar);
            var playerLink = new PlayerLink()
            {
                Player = player,
            };
            f.Add(entity, playerLink);
            if(f.Unsafe.TryGetPointer<Transform3D>(entity, out var transform))
            {
                transform->Position = new FPVector3(0, 1, 0);
            }
        }

        public override void Update(Frame f, ref Filter filter)
        {
            var input = f.GetPlayerInput(filter.PlayerLink->Player);   
            var direction = input->Direction;
            direction = direction.Magnitude > 1 ? direction.Normalized : direction;
            if(input->Jump.WasPressed)
            {
                filter.CharacterController->Jump(f);
            }
            if(direction.Magnitude > 0)
            {
                FPQuaternion targetRotation = FPQuaternion.LookRotation(direction.XOY);
                FPQuaternion currentRotation = filter.Transform->Rotation;
                FP rotationSpeed = FP._5;
                filter.Transform->Rotation = FPQuaternion.Slerp(currentRotation, targetRotation, rotationSpeed * f.DeltaTime);
            }
            filter.CharacterController->Move(f, filter.Entity, direction.XOY);
        }


        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public CharacterController3D* CharacterController;
            public PlayerLink* PlayerLink;
            public PlayerStats* PlayerStats;
        }
    }
}
