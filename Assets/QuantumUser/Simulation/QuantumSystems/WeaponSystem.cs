namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;

    /// <summary>
    /// Handles all things weapon related
    ///   Things this system handles:
    ///   - Weapon ammo recharge 
    ///   - Firing bullets
    /// </summary> 
    [Preserve]
    public unsafe class WeaponSystem : SystemMainThreadFilter<WeaponSystem.Filter>, ISignalOnRobotRespawn
    {
        public struct Filter
        {
            public EntityRef Entity;
            public PlayerLink* PlayerLink;
            public Status* Status;
            public Weapon* CurrentWeapon;
        }

        public void OnRobotRespawn(Frame f, EntityRef player)
        {   
            Weapon* weapon = f.Unsafe.GetPointer<Weapon>(player);
            var weaponData = f.FindAsset<WeaponData>(weapon->WeaponData.Id);
            weapon->IsRecharging = false;
            weapon->CurrentAmmo = weaponData.MaxAmmo;
            weapon->FireRateTimer = FP._0;
            weapon->DelayToStartRechargeTimer = FP._0;
            weapon->RechargeRate = FP._0;
        }
        public override void Update(Frame frame, ref Filter filter)
        {
            var robot = filter.Entity;
            var playerLink = filter.PlayerLink;
            var status = filter.Status;
            var currentWeapon = filter.CurrentWeapon;

            if (status->IsDead)
            {
                return;
            }
            currentWeapon->FireRateTimer -= frame.DeltaTime;
            currentWeapon->DelayToStartRechargeTimer -= frame.DeltaTime;
            currentWeapon->RechargeRate -= frame.DeltaTime;

            WeaponData weaponData = frame.FindAsset<WeaponData>(currentWeapon->WeaponData.Id);
            if (currentWeapon->DelayToStartRechargeTimer < 0 && currentWeapon->RechargeRate <= 0 &&
                currentWeapon->CurrentAmmo < weaponData.MaxAmmo)
            {
                IncreaseAmmo(frame, currentWeapon, weaponData);
            }

            if (currentWeapon->FireRateTimer <= FP._0 && !currentWeapon->IsRecharging && currentWeapon->CurrentAmmo > 0)
            {
                Input* i = frame.GetPlayerInput(playerLink->Player);

                if (i->Fire.IsDown)
                {
                    SpawnBullet(frame, robot, currentWeapon);
                    currentWeapon->FireRateTimer = FP._1 / weaponData.FireRate;
                    currentWeapon->ChargeTime = FP._0;
                }
            }
        }

        private static void IncreaseAmmo(Frame frame, Weapon* weapon, WeaponData data)
        {
            weapon->RechargeRate = data.RechargeTimer / (FP)data.MaxAmmo;
            weapon->CurrentAmmo++;

            if (weapon->CurrentAmmo == data.MaxAmmo)
            {
                weapon->IsRecharging = false;
            }
        }

        private static void SpawnBullet(Frame frame, EntityRef robot, Weapon* weapon)
        {
            weapon->CurrentAmmo -= 1;
            if (weapon->CurrentAmmo == 0)
            {
                weapon->IsRecharging = true;
                weapon->DelayToStartRechargeTimer = -1;
            }

            WeaponData weaponData = frame.FindAsset<WeaponData>(weapon->WeaponData.Id);
            weapon->DelayToStartRechargeTimer = weaponData.TimeToRecharge;

            frame.Events.OnWeaponShoot(robot);

            BulletData bulletData = frame.FindAsset<BulletData>(weaponData.BulletData.Id);

            EntityPrototype prototypeAsset = frame.FindAsset<EntityPrototype>(new AssetGuid(bulletData.BulletPrototype.Id.Value));
            EntityRef bullet = frame.Create(prototypeAsset);

            BulletFields* bulletFields = frame.Unsafe.GetPointer<BulletFields>(bullet);
            Transform3D* bulletTransform = frame.Unsafe.GetPointer<Transform3D>(bullet);

            bulletFields->BulletData = bulletData;
            Transform3D* robotTransform = frame.Unsafe.GetPointer<Transform3D>(robot);

            var fireSpotWorldOffset = WeaponHelper.GetFireSpotWorldOffset(
              frame.FindAsset<WeaponData>(weapon->WeaponData.Id), robotTransform->Forward);

            bulletTransform->Position = robotTransform->Position + fireSpotWorldOffset + robotTransform->Right / 4;

            bulletFields->Direction = robotTransform->Forward * weaponData.ShootForce;
            bulletFields->Source = robot;
            bulletFields->Time = FP._0;
        }
    }
}