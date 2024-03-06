using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [Space]
    [SerializeField] private bool canAttack;

    [SerializeField] private bool createCloneOnDashStart;
    [SerializeField] private bool createCloneOnDashOver;
    [SerializeField] private bool canCreateCloneOnCounterAttack;
    [Header("Clone Dupliacte")]
    [SerializeField] private bool canDuplitateClone;
    [SerializeField] private float changeToDuplicate;
    [Header("Crystal instead of clone")]
    public bool crystalInseadOfClone;

    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        if(crystalInseadOfClone)
        {
            SkillManager.instance.crystal.CreateCrystal();
            
            return;
        }

        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition, cloneDuration, canAttack, _offset, FindClosestEnemy(newClone.transform), canDuplitateClone, changeToDuplicate);
    }

    public void CreateCloneOnDashStart()
    {
        if(createCloneOnDashStart) 
            CreateClone(player.transform,Vector3.zero);
    }
    public void CreateCloneOnDashOver()
    {
        if(createCloneOnDashOver) 
            CreateClone(player.transform,Vector3.zero);
    }

    public void CreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if (canCreateCloneOnCounterAttack)
            StartCoroutine(CreateCloneWithDelay(_enemyTransform,new Vector3(2* player.facingDir,0)));
    }

    private IEnumerator CreateCloneWithDelay(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);
        CreateClone(_transform,_offset);

    }
}
