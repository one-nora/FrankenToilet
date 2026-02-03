namespace FrankenToilet.Bryan;

using UnityEngine;

/// <summary> Dupes projectiles after 0.5 seconds </summary>
public class ProjectileFucker : MonoBehaviour
{
    /// <summary> Time when created so we can know when to dupe. </summary>
    public float time;

    /// <summary> Whether we've duped or not so we dont keep duping over and over. </summary>
    public bool Duped = false;

    /// <summary> Set time. </summary>
    public void Awake() => time = Time.realtimeSinceStartup;

    /// <summary> Check if we should dupe. </summary>
    public void Update()
    {
        if (Time.realtimeSinceStartup - time > 0.5 && !Duped)
        {
            var newRot = transform.localEulerAngles - new Vector3(0f, 5f, 0f);
            var NewObj = Object.Instantiate(BundleLoader.Projectile, transform.position, Quaternion.Euler(newRot.x, newRot.y, newRot.z));
            transform.localEulerAngles += new Vector3(0f, 5f, 0f);

            Setup(NewObj);
            Duped = true;
        }
    }

    /// <summary> Sets up a projectile with all the fancy data stuff </summary>
    public void Setup(GameObject ProjectileObj)
    {
        var Old = GetComponent<Projectile>();

        foreach (var New in ProjectileObj.GetComponentsInChildren<Projectile>())
        {
            New.speed = Old.speed;
            New.turnSpeed = Old.turnSpeed;
            New.speedRandomizer = Old.speedRandomizer;
            New.damage = Old.damage;
            New.enemyDamageMultiplier = Old.enemyDamageMultiplier;
            New.friendly = Old.friendly;
            New.playerBullet = Old.playerBullet;
            New.bulletType = Old.bulletType;
            New.weaponType = Old.weaponType;
            New.decorative = Old.decorative;
            New.origScale = Old.origScale;
            New.active = Old.active;
            New.safeEnemyType = Old.safeEnemyType;
            New.explosive = Old.explosive;
            New.bigExplosion = Old.bigExplosion;
            New.homingType = Old.homingType;
            New.turningSpeedMultiplier = Old.turningSpeedMultiplier;
            New.target = Old.target;
            New.maxSpeed = Old.maxSpeed;
            New.targetRotation = Old.targetRotation;
        }
    }
}
