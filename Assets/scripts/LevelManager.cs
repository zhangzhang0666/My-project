using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class LevelManager : MonoBehaviour
{
    public static LevelManager  Instance;
    private int levelSum;
    public LeverTrigger[] levels;
    
    #region 3D
    public int levelNumber3D;
    //public Transform pivot3D;
    public bool isDraw;
    public Material StepMat;
    private CommandBuffer cmd;
    private RTHandle maskHandle;
    private RTHandle brushHandle;
    private Vector2 levelSize;
    public Texture[] brushs;
    private List<RTHandle>  maskrts = new List<RTHandle>();
    private List<RTHandle> brushrts = new List<RTHandle>();
    public float brushsize;
    #endregion

    #region 2D
    public int levelNumber2D;
    //public Transform pivot2D;
    public GameObject collider;
    public Transform world2D;
    public Transform[] starts;
    #endregion
    
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        levelSum = levels.Length;
        RenderTextureDescriptor descriptor= new RenderTextureDescriptor(1024,1024);
        for (int i = 0; i <= levelSum; i++)
        {
            RTHandle rtHandle = RTHandles.Alloc(descriptor);
            maskrts.Add(rtHandle);
        }
        
        maskHandle = maskrts[0];
        
        foreach (Texture br in brushs)
        {
            RTHandle brHandle = RTHandles.Alloc(br);
            brushrts.Add(brHandle);
        }
        brushHandle = brushrts[0];
        
        cmd = CommandBufferPool.Get();
        Blitter.BlitCameraTexture(cmd, brushHandle,maskHandle, StepMat, 0);
        RenderPipelineManager.beginCameraRendering += drawRT;
        
        change2Dlevel(0);
    }

    public void set3Dlevel(GameObject renderobject,int number,Vector2 size)
    {
        levelNumber3D = number;
        levelSize = size;
        maskHandle = maskrts[number];
        cmd.Clear();
        Blitter.BlitCameraTexture(cmd, brushHandle,maskHandle, StepMat, 0);
        renderobject.GetComponent<Renderer>().material.SetTexture("_Mask",maskHandle);
        StepMat.SetVector("_BrushSize",size/brushsize);
        StepMat.SetVector("_BrushPos",Vector2.zero);
        PlayerController3D.Instance.pivot3D = levels[number-1].pivot;
    }
    
    void drawRT(ScriptableRenderContext context, Camera camera)
    {
        if(isDraw)
            context.ExecuteCommandBuffer(cmd);
    }

    public void drawMask(Vector2 newpoint)
    {
        
        Vector2 point = newpoint / levelSize;
        StepMat.SetVector("_BrushPos",point);
    }
    
    public void AddPoints(Vector2 newpoint)
    {
        CircleCollider2D newcircle = collider.AddComponent<CircleCollider2D>();
        newcircle.offset = newpoint;
        newcircle.radius = 0.5f;
    }

    public void change2Dlevel(int number)
    {
        levelNumber2D = number+1;
        PlayerController2D.Instance.pivot2D = levels[number].pivot;
        
        EdgeCollider2D edge = world2D.GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(0, 0);
        points[1] = new Vector2(0, levels[number].size.z);
        points[2] = new Vector2(levels[number].size.x, levels[number].size.z);
        points[3] = new Vector2(levels[number].size.x, 0);
        points[4] = new Vector2(0, 0);
        edge.points = points;
        PlayerController2D.Instance.transform.position = starts[number].position;
    }
}
