using UnityEngine;

namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;

    /// <summary>
    ///   Handles all bullet entity interactions
    ///   Things this system handles:
    ///   - Bullet Life Cycle
    ///   - Bullet Movement
    ///   - Bullet Collision (via Raycast)
    /// </summary>
    [Preserve]
    public unsafe class BulletSystem : SystemMainThreadFilter<BulletSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public BulletFields* BulletFields;
        }
        public override void Update(Frame frame, ref Filter filter)
        {
            var bullet = filter.Entity;
            var bulletTransform = filter.Transform;
            var bulletFields = filter.BulletFields;

            if (CheckRaycastCollision(frame, bullet, *bulletFields))
            {
                return;
            }

            bulletTransform->Position += bulletFields->Direction * frame.DeltaTime;
            bulletFields->Time += frame.DeltaTime;

            FPVector3 sourcePosition = frame.Unsafe.GetPointer<Transform3D>(bulletFields->Source)->Position;
            BulletData bulletData = frame.FindAsset<BulletData>(bulletFields->BulletData.Id);

            FP distanceSquared = FPVector3.DistanceSquared(bulletTransform->Position, sourcePosition);
            bool bulletIsTooFar = FPMath.Sqrt(distanceSquared) > bulletData.Range;
            bool bulletIsOld = bulletData.Duration > FP._0 && bulletFields->Time >= bulletData.Duration;

            if (bulletIsTooFar || bulletIsOld)
            {
                // Applies polymorphic behavior on the bullet action
                bulletData.BulletAction(frame, bullet, EntityRef.None);
            }
        }

        private bool CheckRaycastCollision(Frame frame, EntityRef bullet, BulletFields bulletFields)
        {
            if (bulletFields.Direction.Magnitude <= 0)
            {
                return false;
            }

            Transform3D* bulletTransform = frame.Unsafe.GetPointer<Transform3D>(bullet);
            FPVector3 futurePosition = bulletTransform->Position + bulletFields.Direction * frame.DeltaTime;
            BulletData data = frame.FindAsset<BulletData>(bulletFields.BulletData.Id);

            if (FPVector3.DistanceSquared(bulletTransform->Position, futurePosition) <= FP._0_01)
            {
                return false;
            }

            //using (var hits = f.Scene.Linecastc)
            Physics3D.HitCollection3D hits = frame.Physics3D.LinecastAll(bulletTransform->Position, futurePosition);
            for (int i = 0; i < hits.Count; i++)
            {
                var entity = hits[i].Entity;
                if (entity != EntityRef.None && frame.Has<Status>(entity) && entity != bulletFields.Source)
                {
                    if (frame.Get<Status>(entity).IsDead)
                    {
                        continue;
                    }

                    bulletTransform->Position = hits[i].Point;
                    // Applies polymorphic behavior on the bullet action
                    data.BulletAction(frame, bullet, entity);
                    return true;
                }

                if (entity == EntityRef.None)
                {
                    bulletTransform->Position = hits[i].Point;
                    // Applies polymorphic behavior on the bullet action
                    data.BulletAction(frame, bullet, EntityRef.None);
                    return true;
                }
            }
            return false;
        }
    }
}