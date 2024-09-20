namespace Quantum
{
    using Photon.Deterministic;
#if QUANTUM_UNITY
    using UnityEngine;
#endif

    [System.Serializable]
    public class WeaponData : AssetObject
    {
        public FP FireRate;
        public FP ShootForce;
        public int MaxAmmo;
        public FP RechargeTimer;
        public FP TimeToRecharge;
        public FPVector3 PositionOffset;
        public AssetRef<BulletData> BulletData;
    }
}