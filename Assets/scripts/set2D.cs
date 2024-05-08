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
#if UNITY_EDITOR
    public Transform pivot;
    public Transform setTarget;
    public Target target;
    private SpriteRenderer sr;
    public int number;
    public LevelManager levelManager;
    
    public void Set()
    {
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
            endDoor.animatorEnd = GetComponent<Animator>();
        }

        Debug.Log("success");
    }
    public void nextLevel()
    { 
        Debug.Log(1);
        LevelManager.Instance.change2Dlevel(LevelManager.Instance.levelNumber2D);
        
    }
#endif
}

