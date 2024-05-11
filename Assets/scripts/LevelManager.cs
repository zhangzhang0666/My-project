using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Texture[] brushs;
    public Color[] brushColor;
    private List<RTHandle>  maskrts = new List<RTHandle>();
    private List<RTHandle> brushrts = new List<RTHandle>();
    public float brushsize;
    #endregion

    #region 2D

    public float radius;
    public int levelNumber2D;
    //public Transform pivot2D;
    public GameObject collider;
    public Transform world2D;
    [HideInInspector]
    public List<Transform> starts;
    [HideInInspector]
    public List<Animator> startAnims;
    //public Transform[] starts;
    private List<Vector2> lineList;
    public int star2D;
    #endregion

    public int brushNumber;
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
        
        set2Dworld(0);
        levelNumber2D = 1;
    }

    public void set3Dlevel(GameObject renderobject,int number,Vector2 size)
    {
        levelNumber3D = number;
        maskHandle = maskrts[levelNumber3D];
        brushHandle = brushrts[brushNumber];
        cmd.Clear();
        Blitter.BlitCameraTexture(cmd, brushHandle,maskHandle, StepMat, 0);
        renderobject.GetComponent<Renderer>().material.SetTexture("_Mask",maskHandle);
        renderobject.GetComponent<Renderer>().material.SetColor("_maskColor",brushColor[brushNumber]);
        StepMat.SetVector("_BrushSize",size/brushsize);
        StepMat.SetVector("_BrushPos",Vector2.zero);
        PlayerController3D.Instance.pivot3D = levels[levelNumber3D-1].pivot;
    }

    public void setBrush(int number)
    {
        brushHandle = brushrts[number];
        brushNumber = number;
    }
    
    void drawRT(ScriptableRenderContext context, Camera camera)
    {
        if(isDraw)
            context.ExecuteCommandBuffer(cmd);
    }

    public void drawMask(Vector2 newpoint)
    {
        
        Vector2 point = newpoint / levels[levelNumber3D-1].size;
        StepMat.SetVector("_BrushPos",point);
    }
    
    public void AddPoints(Vector2 newpoint)
    {
        if(levelNumber3D!=levelNumber2D)
            return;
        EdgeCollider2D edge = collider.GetComponent<EdgeCollider2D>();
        lineList.Add(newpoint);
        edge.points = lineList.ToArray();
        // CircleCollider2D newcircle = collider.AddComponent<CircleCollider2D>();
        // newcircle.offset = newpoint;
        // newcircle.radius = radius;
    }

    public void end2DLevel(int number)
    {
        star2D = 0;
        PlayerController2D.Instance.gameObject.SetActive(true);
        PlayerController2D.Instance.sprite.gameObject.SetActive(true);
        set2Dworld(number);
        PlayerController2D.Instance.setSprite();
        PlayerController2D.Instance.gameObject.SetActive(false);
        PlayerController2D.Instance.sprite.gameObject.SetActive(false);
        startAnims[number].SetTrigger("start");
    }

    public void set2Dworld(int number)
    {
        PlayerController2D.Instance.pivot2D = levels[number].pivot;
        EdgeCollider2D edge = world2D.GetComponent<EdgeCollider2D>();
        EdgeCollider2D line = collider.GetComponent<EdgeCollider2D>();
        lineList= new List<Vector2>();
        line.points =  new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0, 0),
        };
        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(0, 0);
        points[1] = new Vector2(0, levels[number].size.y);
        points[2] = new Vector2(levels[number].size.x, levels[number].size.y);
        points[3] = new Vector2(levels[number].size.x, 0);
        points[4] = new Vector2(0, 0);
        edge.points = points;
        PlayerController2D.Instance.transform.position = starts[number].position;
        if (levels[number].director)
        {
            levels[number].director.Play();
        }
    }
    public void start2Dlevel(int number)
    {
        levelNumber2D = number+1;
        PlayerController2D.Instance.gameObject.SetActive(true);
        PlayerController2D.Instance.sprite.gameObject.SetActive(true);
    }

    
}
