using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum Target
{
    start,
    end
}
    
public class set2D : MonoBehaviour
{  
    public Transform pivot;
    public Transform setTarget;
    public Target target;
    private SpriteRenderer sr;
    public int number;
    public LevelManager levelManager;
    private Animator _animator;
    public void Set()
    {
        _animator = GetComponent<Animator>();
        Vector3 normal = transform.up;

        // 获取世界空间中的上方向
        Vector3 worldUp = Vector3.up;

        // 计算法线与世界上方向的点积
        float dotProduct = Vector3.Dot(normal, worldUp);

        // 设置一个阈值，表示法线是否朝上
        float threshold = 0.9f;

        // 判断法线是否朝上
        bool isNormalUp = dotProduct > threshold;

        
        if (setTarget == null)
        {
            return;
        }
        setTarget.position=pivot.InverseTransformPoint(transform.position );
        setTarget.position = new Vector3(setTarget.position.x,setTarget.position.y,0f);
        if (target == Target.start)
        {
            if (levelManager.starts.Count < number)
            {
                levelManager.starts.Add(setTarget);
            }
            else
            {
                levelManager.starts[number - 1] = setTarget;
            }
            if (levelManager.startAnims.Count < number)
            {
                levelManager.startAnims.Add(_animator);
            }
            else
            {
                levelManager.startAnims[number - 1] =_animator;
            }
        }
        else
        {
            sr = GetComponent<SpriteRenderer>();
            Vector3 spriteSize = sr.bounds.size;
            BoxCollider2D box= setTarget.GetComponent<BoxCollider2D>();
            if (box == null)
            {
                box=setTarget.AddComponent<BoxCollider2D>();
            }

            if (isNormalUp)
            {
                box.size =  new Vector2(spriteSize.x,spriteSize.y);
            }
            else
            {
                box.size =  new Vector2(spriteSize.x,spriteSize.z); 
            }
            
            box.isTrigger = true;
            doorTrigger endDoor = setTarget.GetComponent<doorTrigger>();
            if (endDoor == null)
            {
                endDoor=setTarget.AddComponent<doorTrigger>();
            }
            endDoor.number = number;
            endDoor.animatorEnd = _animator;
        }

        Debug.Log("success");
    }
    public void levelEnd()
    { 
        LevelManager.Instance.end2DLevel(number);
        CameraManager.Instance.changeCamera(number);
    }
    public void levelStart()
    { 
        
        LevelManager.Instance.start2Dlevel(number-1);
    }

}

