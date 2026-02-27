// =============================================================================
// InspireFace .NET Interop - Native Struct Definitions
// Mapped from inspireface.h / intypedef.h
// =============================================================================

using System.Runtime.InteropServices;

namespace InspireFace;

/// <summary>
/// 人脸矩形区域 (映射 HFaceRect)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFaceRect
{
    /// <summary>左上角 X 坐标</summary>
    public int X;
    /// <summary>左上角 Y 坐标</summary>
    public int Y;
    /// <summary>宽度</summary>
    public int Width;
    /// <summary>高度</summary>
    public int Height;

    public override string ToString() => $"Rect({X}, {Y}, {Width}x{Height})";
}

/// <summary>
/// 2D 浮点坐标 (映射 HPoint2f)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HPoint2f
{
    public float X;
    public float Y;

    public HPoint2f(float x, float y) { X = x; Y = y; }
    public override string ToString() => $"({X:F2}, {Y:F2})";
}

/// <summary>
/// 2D 整型坐标 (映射 HPoint2i)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HPoint2i
{
    public int X;
    public int Y;

    public HPoint2i(int x, int y) { X = x; Y = y; }
    public override string ToString() => $"({X}, {Y})";
}

/// <summary>
/// 颜色 (映射 HColor)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HColor
{
    public float R;
    public float G;
    public float B;

    public HColor(float r, float g, float b) { R = r; G = g; B = b; }
}

/// <summary>
/// 图像数据 (映射 HFImageData)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFImageData
{
    /// <summary>图像数据指针</summary>
    public IntPtr Data;
    /// <summary>图像宽度</summary>
    public int Width;
    /// <summary>图像高度</summary>
    public int Height;
    /// <summary>图像格式</summary>
    public HFImageFormat Format;
    /// <summary>旋转角度</summary>
    public HFRotation Rotation;
}

/// <summary>
/// 位图数据 (映射 HFImageBitmapData)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFImageBitmapData
{
    /// <summary>数据指针</summary>
    public IntPtr Data;
    /// <summary>宽度</summary>
    public int Width;
    /// <summary>高度</summary>
    public int Height;
    /// <summary>通道数（1 或 3）</summary>
    public int Channels;
}

/// <summary>
/// Session 自定义参数 (映射 HFSessionCustomParameter)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFSessionCustomParameter
{
    /// <summary>启用人脸识别</summary>
    public int EnableRecognition;
    /// <summary>启用 RGB 活体检测</summary>
    public int EnableLiveness;
    /// <summary>启用 IR 活体检测</summary>
    public int EnableIRLiveness;
    /// <summary>启用口罩检测</summary>
    public int EnableMaskDetect;
    /// <summary>启用人脸质量检测</summary>
    public int EnableFaceQuality;
    /// <summary>启用人脸属性预测</summary>
    public int EnableFaceAttribute;
    /// <summary>启用交互活体检测</summary>
    public int EnableInteractionLiveness;
    /// <summary>启用检测模式下的关键点</summary>
    public int EnableDetectModeLandmark;
    /// <summary>启用人脸姿态估计</summary>
    public int EnableFacePose;
    /// <summary>启用人脸情绪识别</summary>
    public int EnableFaceEmotion;

    /// <summary>
    /// 创建只启用人脸识别的参数
    /// </summary>
    public static HFSessionCustomParameter CreateForRecognition()
    {
        return new HFSessionCustomParameter { EnableRecognition = 1 };
    }

    /// <summary>
    /// 创建只启用人脸识别的参数
    /// </summary>
    public static HFSessionCustomParameter CreateForRecognitionWithFaceQuality()
    {
        return new HFSessionCustomParameter { EnableRecognition = 1, EnableFaceQuality = 1 };
    }

    /// <summary>
    /// 创建启用全部功能的参数
    /// </summary>
    public static HFSessionCustomParameter CreateAll()
    {
        return new HFSessionCustomParameter
        {
            EnableRecognition = 1,
            EnableLiveness = 1,
            EnableIRLiveness = 1,
            EnableMaskDetect = 1,
            EnableFaceQuality = 1,
            EnableFaceAttribute = 1,
            EnableInteractionLiveness = 1,
            EnableDetectModeLandmark = 1,
            EnableFacePose = 1,
            EnableFaceEmotion = 1,
        };
    }
}

/// <summary>
/// 人脸基础令牌 (映射 HFFaceBasicToken)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFaceBasicToken
{
    /// <summary>令牌大小</summary>
    public int Size;
    /// <summary>令牌数据指针</summary>
    public IntPtr Data;
}

/// <summary>
/// 人脸欧拉角 (映射 HFFaceEulerAngle)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFaceEulerAngle
{
    /// <summary>翻滚角数组指针</summary>
    public IntPtr Roll;
    /// <summary>偏航角数组指针</summary>
    public IntPtr Yaw;
    /// <summary>俯仰角数组指针</summary>
    public IntPtr Pitch;
}

/// <summary>
/// 多人脸检测数据 (映射 HFMultipleFaceData)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFMultipleFaceData
{
    /// <summary>检测到的人脸数量</summary>
    public int DetectedNum;
    /// <summary>人脸矩形数组指针</summary>
    public IntPtr Rects;
    /// <summary>跟踪 ID 数组指针</summary>
    public IntPtr TrackIds;
    /// <summary>跟踪计数数组指针</summary>
    public IntPtr TrackCounts;
    /// <summary>检测置信度数组指针</summary>
    public IntPtr DetConfidence;
    /// <summary>人脸欧拉角</summary>
    public HFFaceEulerAngle Angles;
    /// <summary>人脸令牌数组指针</summary>
    public IntPtr Tokens;

    /// <summary>
    /// 获取第 i 个人脸的矩形区域
    /// </summary>
    public HFaceRect GetRect(int index)
    {
        if (index < 0 || index >= DetectedNum) throw new ArgumentOutOfRangeException(nameof(index));
        return Marshal.PtrToStructure<HFaceRect>(Rects + index * Marshal.SizeOf<HFaceRect>());
    }

    /// <summary>
    /// 获取第 i 个人脸的跟踪 ID
    /// </summary>
    public int GetTrackId(int index)
    {
        if (index < 0 || index >= DetectedNum) throw new ArgumentOutOfRangeException(nameof(index));
        return Marshal.ReadInt32(TrackIds, index * sizeof(int));
    }

    /// <summary>
    /// 获取第 i 个人脸的检测置信度
    /// </summary>
    public float GetConfidence(int index)
    {
        if (index < 0 || index >= DetectedNum) throw new ArgumentOutOfRangeException(nameof(index));
        unsafe
        {
            return ((float*)DetConfidence)[index];
        }
    }

    /// <summary>
    /// 获取第 i 个人脸的令牌
    /// </summary>
    public HFFaceBasicToken GetToken(int index)
    {
        if (index < 0 || index >= DetectedNum) throw new ArgumentOutOfRangeException(nameof(index));
        return Marshal.PtrToStructure<HFFaceBasicToken>(Tokens + index * Marshal.SizeOf<HFFaceBasicToken>());
    }

    /// <summary>
    /// 获取所有人脸矩形
    /// </summary>
    public HFaceRect[] GetAllRects()
    {
        var rects = new HFaceRect[DetectedNum];
        for (int i = 0; i < DetectedNum; i++)
            rects[i] = GetRect(i);
        return rects;
    }
}

/// <summary>
/// 人脸特征 (映射 HFFaceFeature)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFaceFeature
{
    /// <summary>特征数据大小</summary>
    public int Size;
    /// <summary>特征数据指针 (float*)</summary>
    public IntPtr Data;

    /// <summary>
    /// 将特征数据复制为 float 数组
    /// </summary>
    public float[] ToArray()
    {
        if (Data == IntPtr.Zero || Size <= 0) return Array.Empty<float>();
        var arr = new float[Size];
        Marshal.Copy(Data, arr, 0, Size);
        return arr;
    }
}

/// <summary>
/// 人脸特征身份 (映射 HFFaceFeatureIdentity)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFaceFeatureIdentity
{
    /// <summary>人脸 ID</summary>
    public long Id;
    /// <summary>指向 HFFaceFeature 的指针</summary>
    public IntPtr Feature;
}

/// <summary>
/// TopK 搜索结果 (映射 HFSearchTopKResults)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFSearchTopKResults
{
    /// <summary>搜索到的人脸数量</summary>
    public int Size;
    /// <summary>置信度数组指针</summary>
    public IntPtr Confidence;
    /// <summary>人脸 ID 数组指针</summary>
    public IntPtr Ids;
}

/// <summary>
/// FeatureHub 配置 (映射 HFFeatureHubConfiguration)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFeatureHubConfiguration
{
    /// <summary>主键模式</summary>
    public HFPKMode PrimaryKeyMode;
    /// <summary>启用持久化</summary>
    public int EnablePersistence;
    /// <summary>持久化数据库路径</summary>
    public IntPtr PersistenceDbPath;
    /// <summary>搜索阈值</summary>
    public float SearchThreshold;
    /// <summary>搜索模式</summary>
    public HFSearchMode SearchMode;
}

/// <summary>
/// 相似度转换配置 (映射 HFSimilarityConverterConfig)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFSimilarityConverterConfig
{
    public float Threshold;
    public float MiddleScore;
    public float Steepness;
    public float OutputMin;
    public float OutputMax;
}

/// <summary>
/// RGB 活体检测置信度 (映射 HFRGBLivenessConfidence)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFRGBLivenessConfidence
{
    public int Num;
    public IntPtr Confidence;
}

/// <summary>
/// 口罩检测置信度 (映射 HFFaceMaskConfidence)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFaceMaskConfidence
{
    public int Num;
    public IntPtr Confidence;
}

/// <summary>
/// 人脸质量置信度 (映射 HFFaceQualityConfidence)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFaceQualityConfidence
{
    public int Num;
    public IntPtr Confidence;

    public float Get(int index)
    {
        if (index < 0 || index >= Num) throw new ArgumentOutOfRangeException(nameof(index));
        unsafe
        {
            return ((float*)Confidence)[index];
        }
    }
}

/// <summary>
/// 人脸交互状态 (映射 HFFaceInteractionState)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFaceInteractionState
{
    public int Num;
    public IntPtr LeftEyeStatusConfidence;
    public IntPtr RightEyeStatusConfidence;
}

/// <summary>
/// 人脸交互动作 (映射 HFFaceInteractionsActions)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFaceInteractionsActions
{
    public int Num;
    public IntPtr Normal;
    public IntPtr Shake;
    public IntPtr JawOpen;
    public IntPtr HeadRaise;
    public IntPtr Blink;
}

/// <summary>
/// 人脸属性结果 (映射 HFFaceAttributeResult)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFaceAttributeResult
{
    public int Num;
    public IntPtr Race;
    public IntPtr Gender;
    public IntPtr AgeBracket;
}

/// <summary>
/// 人脸情绪结果 (映射 HFFaceEmotionResult)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFaceEmotionResult
{
    public int Num;
    public IntPtr Emotion;
}

/// <summary>
/// InspireFace 版本信息 (映射 HFInspireFaceVersion)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFInspireFaceVersion
{
    public int Major;
    public int Minor;
    public int Patch;

    public override string ToString() => $"{Major}.{Minor}.{Patch}";
}

/// <summary>
/// InspireFace 扩展信息 (映射 HFInspireFaceExtendedInformation)
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public unsafe struct HFInspireFaceExtendedInformation
{
    public fixed byte Information[256];

    public string GetInformation()
    {
        fixed (byte* p = Information)
        {
            return Marshal.PtrToStringAnsi((IntPtr)p) ?? string.Empty;
        }
    }
}

/// <summary>
/// 人脸检测像素级别列表 (映射 HFFaceDetectPixelList)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct HFFaceDetectPixelList
{
    public fixed int PixelLevel[20];
    public int Size;
}

/// <summary>
/// FeatureHub 已有 ID 列表 (映射 HFFeatureHubExistingIds)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct HFFeatureHubExistingIds
{
    public int Size;
    public IntPtr Ids;
}
