using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] GetHitEvent getHitEvent;
    [SerializeField] Pet pet;
    [SerializeField] GameObject spriteAnimtorPoolKey;


    [Space(10f)]
    [Header("WEAPON HIT ANMIMATION")]
    [SerializeField] SpriteAnimationSO weaponHitAnimationSO;

    [Space(10f)]
    [Header("WEAPON HIT PARTICAL")]
    [SerializeField] GameObject weaponHitParticalPoolKey;

    [Space(10f)]
    [Header("HEART ANMIMATION")]
    [SerializeField] float spreadRadious;
    [SerializeField] SpriteAnimationSO heartAnimationSO;
    [SerializeField] float moveUpSpeed;

    [Space(10f)]
    [Header("CRITICAL HIT PARTICAL")]
    [SerializeField] GameObject criticalHitParticalPoolKey;

    [Space(10f)]
    [Header("DAMAGE POP UP")]
    [SerializeField] GameObject popUpTextPoolKey;
    [SerializeField] float popUpLifeTime;
    [SerializeField] float textSize;
    [SerializeField] float criticalTextSize;
    [SerializeField] Color textColor;
    [SerializeField] Color criticalTextColor;

    private void OnEnable()
    {
        getHitEvent.OnGetHit += GetHitEvent_OnGetHit;
    }
    private void OnDisable()
    {
        getHitEvent.OnGetHit -= GetHitEvent_OnGetHit;
    }

    private void GetHitEvent_OnGetHit(GetHitEvent getHitEvent, GetHitEventArgs getHitEventArgs)
    {
        //PlayWeaponHitAnimation(getHitEventArgs.worldPosition);
        PlayHeartAnimation(getHitEventArgs.worldPosition);
        //WeaponHitPartical(getHitEventArgs.worldPosition);
        //DamagePopUp(getHitEventArgs.worldPosition, getHitEventArgs.damage, getHitEventArgs.isCritical);

        if (getHitEventArgs.isCritical)
        {
            //PlayCriticalAnimation(getHitEventArgs.worldPosition);
            //CriticalHitPartical(getHitEventArgs.worldPosition);
        }
    }

    private void PlayWeaponHitAnimation(Vector3 worldPosition)
    {
        SpriteAnimatorPoolObject spriteAnimatorPoolObject = (SpriteAnimatorPoolObject)pet.poolManager.GetComponentFromPool(spriteAnimtorPoolKey);
        spriteAnimatorPoolObject.SetPositionAngle(worldPosition, 0);
        spriteAnimatorPoolObject.PlayAnimation(weaponHitAnimationSO, spriteAnimtorPoolKey, pet.poolManager);
    }

    private void WeaponHitPartical(Vector3 worldPosition)
    {
        ParticalPoolObject particalPoolObject = (ParticalPoolObject)pet.poolManager.GetComponentFromPool(weaponHitParticalPoolKey);

        particalPoolObject.Emit(pet.poolManager, weaponHitParticalPoolKey, worldPosition);
    }

    private void CriticalHitPartical(Vector3 worldPosition)
    {
        ParticalPoolObject particalPoolObject = (ParticalPoolObject)pet.poolManager.GetComponentFromPool(criticalHitParticalPoolKey);

        particalPoolObject.Emit(pet.poolManager, criticalHitParticalPoolKey, worldPosition);
    }

    private void PlayHeartAnimation(Vector3 worldPosition)
    {
        Vector3 randomSpreadPositon = worldPosition + (Vector3)Random.insideUnitCircle * spreadRadious;

        SpriteAnimatorPoolObject spriteAnimatorPoolObject = (SpriteAnimatorPoolObject)pet.poolManager.GetComponentFromPool(spriteAnimtorPoolKey);
        spriteAnimatorPoolObject.SetPositionAngle(randomSpreadPositon, 0);
        spriteAnimatorPoolObject.SetMovement(true, moveUpSpeed);
        spriteAnimatorPoolObject.PlayAnimation(heartAnimationSO, spriteAnimtorPoolKey, pet.poolManager);
    }

    private void DamagePopUp(Vector3 position, int damage, bool isCritical)
    {
        Debug.Log("pop up");

        Vector3 randomSpreadPositon = position + (Vector3)Random.insideUnitCircle * spreadRadious;

        PopUpTextPoolObject popUpTextPoolObject = (PopUpTextPoolObject)pet.poolManager.GetComponentFromPool(popUpTextPoolKey);

        if (isCritical)
            popUpTextPoolObject.PopText(randomSpreadPositon, popUpLifeTime, damage.ToString(), criticalTextSize, criticalTextColor, popUpTextPoolKey, pet.poolManager);
        else
            popUpTextPoolObject.PopText(randomSpreadPositon, popUpLifeTime, damage.ToString(), textSize, textColor, popUpTextPoolKey, pet.poolManager);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {

    }
#endif
}
