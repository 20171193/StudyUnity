using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraZoomType
{
    // Index of cvCameras
    LongZoom = 0,
    ShortZoom = 1,
    NormalZoom = 2,

    CurZoom = 10    // «ˆ¿Á ¡‹¿Œ
}

public class TestCameraManager : MonoBehaviour
{
    [Header("INDEX [ Long:0 | Short:1 | Normal:2 ]")]
    public CinemachineVirtualCamera[] cvCameras;
    public int curZoomType; // «ˆ¿Á ¡‹ ≈∏¿‘

    public void ZoomIn(CameraZoomType zoomType)
    {
        ZoomOut();
        curZoomType = (int)zoomType;
        switch (zoomType)
        {
            case CameraZoomType.LongZoom:
                cvCameras[(int)CameraZoomType.LongZoom].Priority = (int)CameraZoomType.CurZoom;
                break;
            case CameraZoomType.ShortZoom:
                cvCameras[(int)CameraZoomType.ShortZoom].Priority = (int)CameraZoomType.CurZoom;
                break;
            default:
                curZoomType = (int)CameraZoomType.NormalZoom;
                break;
        }
    }
    public void ZoomOut()
    {
        Debug.Log("zoom init");
        curZoomType = (int)CameraZoomType.NormalZoom;
        cvCameras[(int)CameraZoomType.LongZoom].Priority = (int)CameraZoomType.LongZoom;
        cvCameras[(int)CameraZoomType.ShortZoom].Priority = (int)CameraZoomType.ShortZoom;
        cvCameras[(int)CameraZoomType.NormalZoom].Priority = (int)CameraZoomType.NormalZoom;
    }


    #region ΩÃ±€≈œ ∏ﬁº≠µÂ
    private static TestCameraManager instance = null;

    private void Awake()
    {
        cvCameras[(int)CameraZoomType.LongZoom].Priority = (int)CameraZoomType.LongZoom;
        cvCameras[(int)CameraZoomType.ShortZoom].Priority = (int)CameraZoomType.ShortZoom;
        cvCameras[(int)CameraZoomType.NormalZoom].Priority = (int)CameraZoomType.NormalZoom;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public static TestCameraManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            else
                return instance;
        }
    }
    #endregion
    

}
