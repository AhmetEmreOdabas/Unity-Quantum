namespace Quantum
{
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class CharacterSystem : SystemMainThreadFilter<CharacterSystem.Filter>, ISignalOnComponentAdded<PlayerStats>
    {
        public void OnAdded(Frame f, EntityRef entity, PlayerStats* component)
        {
            var spec = f.FindAsset(component->statsAsset);
            if(f.Unsafe.TryGetPointer<CharacterController3D>(entity, out var characterController))
            {
                characterController->MaxSpeed =  spec.MoveSpeed;
            }
        }

        public override void Update(Frame f, ref Filter filter)
        {

        }

        public struct Filter
        {
            public EntityRef Entity;
            public PlayerStats* PlayerStats;
        }
    }
}
