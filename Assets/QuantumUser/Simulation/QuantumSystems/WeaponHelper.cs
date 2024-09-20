namespace Quantum
{
    using Photon.Deterministic;

    public static unsafe class WeaponHelper
    {
        public static FPVector3 GetFireSpotWorldOffset(WeaponData weaponData, FPVector3 forward)
        {
            FPVector3 positionOffset = weaponData.PositionOffset;    
            return positionOffset + (forward * FP._1_33);
        }
    }
}