// =============================================================================
// InspireFace .NET - High-level Managed Wrappers
// Provides IDisposable wrappers for native handles with safe resource management
// 资源文件下载地址：https://github.com/HyperInspire/InspireFace/releases
// =============================================================================

using System.Runtime.InteropServices;

namespace InspireFace;

/// <summary>
/// InspireFace SDK 全局管理（初始化/终止）
/// </summary>
public static class InspireFaceLibrary
{
    /// <summary>
    /// Mobile and server models
    /// </summary>
    public const string DefaultResourcePath_Megatron = "res/Megatron"; // 默认资源路径
    /// <summary>
    /// Lightweight edge-side models
    /// </summary>
    public const string DefaultResourcePath_Pikachu = "res/Pikachu"; // 默认资源路径

    /// <summary>
    /// 启动 InspireFace SDK
    /// </summary>
    /// <param name="resourcePath">资源包路径（.ispk 文件或资源目录）</param>
    public static void Launch(string resourcePath)
    {
        NativeMethods.HFLaunchInspireFace(resourcePath).CheckResult();
    }

    /// <summary>
    /// 使用默认资源路径启动 InspireFace SDK 
    /// </summary>
    public static void Launch() => Launch(DefaultResourcePath_Megatron);

    /// <summary>
    /// 启动轻量级模型版本的 InspireFace SDK，适用于边缘设备和移动端
    /// </summary>
    public static void LaunchLight() => Launch(DefaultResourcePath_Pikachu);

    /// <summary>
    /// 重新加载 InspireFace SDK
    /// </summary>
    public static void Reload(string resourcePath)
    {
        NativeMethods.HFReloadInspireFace(resourcePath).CheckResult();
    }

    /// <summary>
    /// 终止 InspireFace SDK
    /// </summary>
    public static void Terminate()
    {
        NativeMethods.HFTerminateInspireFace().CheckResult();
    }

    /// <summary>
    /// 查询 SDK 是否已启动
    /// </summary>
    public static bool IsLaunched
    {
        get
        {
            NativeMethods.HFQueryInspireFaceLaunchStatus(out int status).CheckResult();
            return status != 0;
        }
    }

    /// <summary>
    /// 查询 SDK 版本
    /// </summary>
    public static HFInspireFaceVersion GetVersion()
    {
        NativeMethods.HFQueryInspireFaceVersion(out var version).CheckResult();
        return version;
    }

    /// <summary>
    /// 查询扩展信息
    /// </summary>
    public static string GetExtendedInformation()
    {
        NativeMethods.HFQueryInspireFaceExtendedInformation(out var info).CheckResult();
        return info.GetInformation();
    }

    /// <summary>
    /// 设置日志级别
    /// </summary>
    public static void SetLogLevel(HFLogLevel level)
    {
        NativeMethods.HFSetLogLevel(level).CheckResult();
    }

    /// <summary>
    /// 禁用日志
    /// </summary>
    public static void DisableLog()
    {
        NativeMethods.HFLogDisable().CheckResult();
    }

    /// <summary>
    /// 获取特征向量长度
    /// </summary>
    public static int GetFeatureLength()
    {
        NativeMethods.HFGetFeatureLength(out int len).CheckResult();
        return len;
    }

    /// <summary>
    /// 获取推荐的余弦阈值
    /// </summary>
    public static float GetRecommendedCosineThreshold()
    {
        NativeMethods.HFGetRecommendedCosineThreshold(out float threshold).CheckResult();
        return threshold;
    }
}

// =============================================================================
// ImageStream - 图像流封装
// =============================================================================

/// <summary>
/// InspireFace 图像流，封装原生 HFImageStream 句柄。
/// 用于向算法模块提供图像数据输入。
/// </summary>
public sealed class ImageStream : IDisposable
{
    internal IntPtr Handle { get; private set; }
    private bool _disposed;

    private ImageStream(IntPtr handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// 从原始图像数据创建图像流
    /// </summary>
    /// <param name="data">像素数据指针</param>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    /// <param name="format">像素格式</param>
    /// <param name="rotation">旋转角度</param>
    public static ImageStream Create(IntPtr data, int width, int height, HFImageFormat format, HFRotation rotation = HFRotation.Rotation0)
    {
        var imageData = new HFImageData
        {
            Data = data,
            Width = width,
            Height = height,
            Format = format,
            Rotation = rotation,
        };
        NativeMethods.HFCreateImageStream(ref imageData, out IntPtr handle).CheckResult();
        return new ImageStream(handle);
    }

    /// <summary>
    /// 从 byte[] 图像数据创建图像流
    /// </summary>
    public static ImageStream Create(byte[] pixelData, int width, int height, HFImageFormat format, HFRotation rotation = HFRotation.Rotation0)
    {
        var pinned = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
        try
        {
            var imageData = new HFImageData
            {
                Data = pinned.AddrOfPinnedObject(),
                Width = width,
                Height = height,
                Format = format,
                Rotation = rotation,
            };
            NativeMethods.HFCreateImageStream(ref imageData, out IntPtr handle).CheckResult();
            return new ImageStream(handle);
        }
        finally
        {
            pinned.Free();
        }
    }

    /// <summary>
    /// 从 ImageBitmap 创建图像流
    /// </summary>
    public static ImageStream CreateFromBitmap(ImageBitmap bitmap, HFRotation rotation = HFRotation.Rotation0)
    {
        NativeMethods.HFCreateImageStreamFromImageBitmap(bitmap.Handle, rotation, out IntPtr handle).CheckResult();
        return new ImageStream(handle);
    }

    public void Dispose()
    {
        if (!_disposed && Handle != IntPtr.Zero)
        {
            NativeMethods.HFReleaseImageStream(Handle);
            Handle = IntPtr.Zero;
            _disposed = true;
        }
    }
}

// =============================================================================
// ImageBitmap - 位图封装
// =============================================================================

/// <summary>
/// InspireFace 位图，封装原生 HFImageBitmap 句柄。
/// </summary>
public sealed class ImageBitmap : IDisposable
{
    internal IntPtr Handle { get; private set; }
    private bool _disposed;

    internal ImageBitmap(IntPtr handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// 从文件路径加载图像
    /// </summary>
    /// <param name="filePath">图像文件路径</param>
    /// <param name="channels">通道数（1 或 3）</param>
    public static ImageBitmap FromFile(string filePath, int channels = 3)
    {
        NativeMethods.HFCreateImageBitmapFromFilePath(filePath, channels, out IntPtr handle).CheckResult();
        return new ImageBitmap(handle);
    }

    /// <summary>
    /// 从像素数据创建位图
    /// </summary>
    public static ImageBitmap FromData(IntPtr data, int width, int height, int channels)
    {
        var bitmapData = new HFImageBitmapData
        {
            Data = data,
            Width = width,
            Height = height,
            Channels = channels,
        };
        NativeMethods.HFCreateImageBitmap(ref bitmapData, out IntPtr handle).CheckResult();
        return new ImageBitmap(handle);
    }

    /// <summary>
    /// 获取位图数据
    /// </summary>
    public HFImageBitmapData GetData()
    {
        NativeMethods.HFImageBitmapGetData(Handle, out var data).CheckResult();
        return data;
    }

    /// <summary>
    /// 保存到文件
    /// </summary>
    public void SaveToFile(string filePath)
    {
        NativeMethods.HFImageBitmapWriteToFile(Handle, filePath).CheckResult();
    }

    /// <summary>
    /// 在图像上绘制矩形
    /// </summary>
    public void DrawRect(HFaceRect rect, HColor color, int thickness = 2)
    {
        NativeMethods.HFImageBitmapDrawRect(Handle, rect, color, thickness).CheckResult();
    }

    /// <summary>
    /// 复制位图
    /// </summary>
    public ImageBitmap Copy()
    {
        NativeMethods.HFImageBitmapCopy(Handle, out IntPtr copyHandle).CheckResult();
        return new ImageBitmap(copyHandle);
    }

    public void Dispose()
    {
        if (!_disposed && Handle != IntPtr.Zero)
        {
            NativeMethods.HFReleaseImageBitmap(Handle);
            Handle = IntPtr.Zero;
            _disposed = true;
        }
    }
}

// =============================================================================
// FaceSession - 人脸分析会话
// =============================================================================

/// <summary>
/// InspireFace 人脸分析会话。负责人脸检测、跟踪、识别等全部算法功能。
/// 支持并发场景下创建多个独立会话。
/// </summary>
public sealed class FaceSession : IDisposable
{
    internal IntPtr Handle { get; private set; }
    private bool _disposed;

    private FaceSession(IntPtr handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// 创建人脸分析会话
    /// </summary>
    /// <param name="parameter">功能配置参数</param>
    /// <param name="detectMode">检测模式</param>
    /// <param name="maxDetectFaceNum">最大检测人脸数</param>
    /// <param name="detectPixelLevel">检测器输入分辨率级别（160 的倍数，默认 -1 即 320）</param>
    /// <param name="trackByDetectModeFPS">TRACK_BY_DETECTION 模式下的 FPS（默认 -1 即 30fps）</param>
    public static FaceSession Create(
        HFSessionCustomParameter parameter,
        HFDetectMode detectMode = HFDetectMode.AlwaysDetect,
        int maxDetectFaceNum = 10,
        int detectPixelLevel = -1,
        int trackByDetectModeFPS = -1)
    {
        NativeMethods.HFCreateInspireFaceSession(
            parameter, detectMode, maxDetectFaceNum,
            detectPixelLevel, trackByDetectModeFPS, out IntPtr handle).CheckResult();
        return new FaceSession(handle);
    }

    /// <summary>
    /// 创建仅用于人脸检测的会话
    /// </summary>
    public static FaceSession CreateDetectionOnly(int maxFaces = 10)
    {
        return Create(new HFSessionCustomParameter(), HFDetectMode.AlwaysDetect, maxFaces);
    }

    /// <summary>
    /// 创建用于人脸检测和识别的会话
    /// </summary>
    public static FaceSession CreateForRecognition(int maxFaces = 10)
    {
        return Create(HFSessionCustomParameter.CreateForRecognition(), HFDetectMode.AlwaysDetect, maxFaces);
    }

    // ── 人脸检测/跟踪 ──

    /// <summary>
    /// 执行人脸检测/跟踪
    /// </summary>
    /// <param name="imageStream">图像流</param>
    /// <returns>多人脸检测结果</returns>
    public HFMultipleFaceData FaceDetect(ImageStream imageStream)
    {
        NativeMethods.HFExecuteFaceTrack(Handle, imageStream.Handle, out var results).CheckResult();
        return results;
    }

    /// <summary>
    /// 清除跟踪的人脸数据
    /// </summary>
    public void ClearTracking()
    {
        NativeMethods.HFSessionClearTrackingFace(Handle).CheckResult();
    }

    /// <summary>
    /// 设置人脸检测阈值
    /// </summary>
    public void SetDetectThreshold(float threshold)
    {
        NativeMethods.HFSessionSetFaceDetectThreshold(Handle, threshold).CheckResult();
    }

    /// <summary>
    /// 设置最小人脸像素大小
    /// </summary>
    public void SetMinFacePixelSize(int minSize)
    {
        NativeMethods.HFSessionSetFilterMinimumFacePixelSize(Handle, minSize).CheckResult();
    }

    /// <summary>
    /// 设置跟踪预览大小
    /// </summary>
    public void SetTrackPreviewSize(int previewSize)
    {
        NativeMethods.HFSessionSetTrackPreviewSize(Handle, previewSize).CheckResult();
    }

    // ── 人脸特征提取 ──

    /// <summary>
    /// 从检测到的人脸中提取特征
    /// </summary>
    /// <param name="imageStream">图像流</param>
    /// <param name="faceToken">人脸令牌（从检测结果获取）</param>
    /// <returns>人脸特征</returns>
    public FaceFeature ExtractFeature(ImageStream imageStream, HFFaceBasicToken faceToken)
    {
        NativeMethods.HFFaceFeatureExtract(Handle, imageStream.Handle, faceToken, out var feature).CheckResult();
        return new FaceFeature(feature);
    }

    /// <summary>
    /// 提取特征到 float 数组
    /// </summary>
    public float[] ExtractFeatureArray(ImageStream imageStream, HFFaceBasicToken faceToken)
    {
        int featureLen = InspireFaceLibrary.GetFeatureLength();
        var featureArr = new float[featureLen];
        NativeMethods.HFFaceFeatureExtractCpy(Handle, imageStream.Handle, faceToken, featureArr).CheckResult();
        return featureArr;
    }

    /// <summary>
    /// 获取人脸对齐图像
    /// </summary>
    public ImageBitmap GetFaceAlignmentImage(ImageStream imageStream, HFFaceBasicToken faceToken)
    {
        NativeMethods.HFFaceGetFaceAlignmentImage(Handle, imageStream.Handle, faceToken, out IntPtr handle).CheckResult();
        return new ImageBitmap(handle);
    }

    // ── Pipeline 处理 ──

    /// <summary>
    /// 多人脸管线处理（活体/口罩/属性等）
    /// </summary>
    public void RunPipeline(ImageStream imageStream, ref HFMultipleFaceData faces, HFSessionCustomParameter parameter)
    {
        NativeMethods.HFMultipleFacePipelineProcess(Handle, imageStream.Handle, ref faces, parameter).CheckResult();
    }

    /// <summary>
    /// 获取 RGB 活体置信度
    /// </summary>
    public HFRGBLivenessConfidence GetRGBLivenessConfidence()
    {
        NativeMethods.HFGetRGBLivenessConfidence(Handle, out var confidence).CheckResult();
        return confidence;
    }

    /// <summary>
    /// 获取口罩检测置信度
    /// </summary>
    public HFFaceMaskConfidence GetFaceMaskConfidence()
    {
        NativeMethods.HFGetFaceMaskConfidence(Handle, out var confidence).CheckResult();
        return confidence;
    }

    /// <summary>
    /// 获取人脸质量置信度
    /// </summary>
    public HFFaceQualityConfidence GetFaceQualityConfidence()
    {
        NativeMethods.HFGetFaceQualityConfidence(Handle, out var confidence).CheckResult();
        return confidence;
    }

    /// <summary>
    /// 获取人脸属性结果
    /// </summary>
    public HFFaceAttributeResult GetFaceAttributeResult()
    {
        NativeMethods.HFGetFaceAttributeResult(Handle, out var result).CheckResult();
        return result;
    }

    /// <summary>
    /// 获取人脸情绪结果
    /// </summary>
    public HFFaceEmotionResult GetFaceEmotionResult()
    {
        NativeMethods.HFGetFaceEmotionResult(Handle, out var result).CheckResult();
        return result;
    }

    /// <summary>
    /// 获取单个人脸的五个关键点
    /// </summary>
    public HPoint2f[] GetFiveLandmarks(HFFaceBasicToken faceToken)
    {
        var landmarks = new HPoint2f[5];
        NativeMethods.HFGetFaceFiveKeyPointsFromFaceToken(faceToken, landmarks, 5).CheckResult();
        return landmarks;
    }

    /// <summary>
    /// 获取密集关键点
    /// </summary>
    public HPoint2f[] GetDenseLandmarks(HFFaceBasicToken faceToken)
    {
        NativeMethods.HFGetNumOfFaceDenseLandmark(out int num).CheckResult();
        var landmarks = new HPoint2f[num];
        NativeMethods.HFGetFaceDenseLandmarkFromFaceToken(faceToken, landmarks, num).CheckResult();
        return landmarks;
    }

    public void Dispose()
    {
        if (!_disposed && Handle != IntPtr.Zero)
        {
            NativeMethods.HFReleaseInspireFaceSession(Handle);
            Handle = IntPtr.Zero;
            _disposed = true;
        }
    }
}

// =============================================================================
// FaceFeature - 人脸特征封装
// =============================================================================

/// <summary>
/// 人脸特征向量的托管封装
/// </summary>
public sealed class FaceFeature
{
    /// <summary>原生特征结构体</summary>
    internal HFFaceFeature Native { get; }

    /// <summary>特征向量数据（float 数组副本）</summary>
    public float[] Data { get; }

    /// <summary>特征向量维度</summary>
    public int Size => Data.Length;

    internal FaceFeature(HFFaceFeature native)
    {
        Native = native;
        Data = native.ToArray();
    }

    /// <summary>
    /// 从 float 数组创建特征
    /// </summary>
    public FaceFeature(float[] data)
    {
        Data = (float[])data.Clone();
        Native = new HFFaceFeature { Size = data.Length, Data = IntPtr.Zero };
    }

    /// <summary>
    /// 1:1 人脸比较，返回余弦相似度
    /// </summary>
    [Obsolete("建议使用 Compare(ReadOnlySpan<float>, ReadOnlySpan<float>) 方法，避免不必要的数组复制和 GCHandle 开销")]
    public static float Compare(FaceFeature feature1, FaceFeature feature2)
    {
        // 需要 pin 数据
        var pinned1 = GCHandle.Alloc(feature1.Data, GCHandleType.Pinned);
        var pinned2 = GCHandle.Alloc(feature2.Data, GCHandleType.Pinned);
        try
        {
            var f1 = new HFFaceFeature { Size = feature1.Size, Data = pinned1.AddrOfPinnedObject() };
            var f2 = new HFFaceFeature { Size = feature2.Size, Data = pinned2.AddrOfPinnedObject() };
            NativeMethods.HFFaceComparison(f1, f2, out float result).CheckResult();
            return result;
        }
        finally
        {
            pinned1.Free();
            pinned2.Free();
        }
    }

    /// <summary>
    /// 1:1 人脸比较，返回余弦相似度
    /// </summary>
    public static unsafe float Compare(ReadOnlySpan<float> feature1, ReadOnlySpan<float> feature2)
    {
        fixed (float* ptr1 = feature1)
        fixed (float* ptr2 = feature2)
        {
            var f1 = new HFFaceFeature { Size = feature1.Length, Data = (nint)ptr1 };
            var f2 = new HFFaceFeature { Size = feature2.Length, Data = (nint)ptr2 };
            NativeMethods.HFFaceComparison(f1, f2, out float result).CheckResult();
            return result;
        }
    }

    /// <summary>
    /// 将余弦相似度转换为百分比
    /// </summary>
    public static float CosineToPercentage(float cosine)
    {
        NativeMethods.HFCosineSimilarityConvertToPercentage(cosine, out float result).CheckResult();
        return result;
    }
}

// =============================================================================
// FeatureHub - 特征库管理
// =============================================================================

/// <summary>
/// InspireFace 内置轻量级人脸特征库管理。
/// 支持搜索、插入、删除、更新操作，提供内存模式和持久化模式。
/// </summary>
public static class FeatureHub
{
    /// <summary>
    /// 启用 FeatureHub（内存模式）
    /// </summary>
    public static void Enable(float searchThreshold = 0.48f, HFSearchMode searchMode = HFSearchMode.Exhaustive)
    {
        var config = new HFFeatureHubConfiguration
        {
            PrimaryKeyMode = HFPKMode.AutoIncrement,
            EnablePersistence = 0,
            PersistenceDbPath = IntPtr.Zero,
            SearchThreshold = searchThreshold,
            SearchMode = searchMode,
        };
        NativeMethods.HFFeatureHubDataEnable(config).CheckResult();
    }

    /// <summary>
    /// 启用 FeatureHub（持久化模式）
    /// </summary>
    public static void EnableWithPersistence(string dbPath, float searchThreshold = 0.48f,
        HFSearchMode searchMode = HFSearchMode.Exhaustive, HFPKMode pkMode = HFPKMode.AutoIncrement)
    {
        var dbPathPtr = Marshal.StringToHGlobalAnsi(dbPath);
        try
        {
            var config = new HFFeatureHubConfiguration
            {
                PrimaryKeyMode = pkMode,
                EnablePersistence = 1,
                PersistenceDbPath = dbPathPtr,
                SearchThreshold = searchThreshold,
                SearchMode = searchMode,
            };
            NativeMethods.HFFeatureHubDataEnable(config).CheckResult();
        }
        finally
        {
            Marshal.FreeHGlobal(dbPathPtr);
        }
    }

    /// <summary>
    /// 禁用 FeatureHub
    /// </summary>
    public static void Disable()
    {
        NativeMethods.HFFeatureHubDataDisable().CheckResult();
    }

    /// <summary>
    /// 插入人脸特征，返回分配的 ID
    /// </summary>
    public static long InsertFeature(FaceFeature feature, long manualId = -1)
    {
        var pinned = GCHandle.Alloc(feature.Data, GCHandleType.Pinned);
        try
        {
            var nativeFeature = new HFFaceFeature { Size = feature.Size, Data = pinned.AddrOfPinnedObject() };
            // 需要一个指向 HFFaceFeature 的指针
            var featurePtr = Marshal.AllocHGlobal(Marshal.SizeOf<HFFaceFeature>());
            try
            {
                Marshal.StructureToPtr(nativeFeature, featurePtr, false);
                var identity = new HFFaceFeatureIdentity
                {
                    Id = manualId,
                    Feature = featurePtr,
                };
                NativeMethods.HFFeatureHubInsertFeature(identity, out long allocId).CheckResult();
                return allocId;
            }
            finally
            {
                Marshal.FreeHGlobal(featurePtr);
            }
        }
        finally
        {
            pinned.Free();
        }
    }

    /// <summary>
    /// 搜索最相似的人脸
    /// </summary>
    /// <returns>匹配的人脸 ID 和置信度；如果没找到返回 null</returns>
    public static (long Id, float Confidence)? SearchFace(FaceFeature feature)
    {
        var pinned = GCHandle.Alloc(feature.Data, GCHandleType.Pinned);
        try
        {
            var searchFeature = new HFFaceFeature { Size = feature.Size, Data = pinned.AddrOfPinnedObject() };
            int result = NativeMethods.HFFeatureHubFaceSearch(searchFeature, out float confidence, out var identity);
            if (result != ErrorCode.HSUCCEED)
                return null;
            if (confidence <= 0)
                return null;
            return (identity.Id, confidence);
        }
        finally
        {
            pinned.Free();
        }
    }

    /// <summary>
    /// 移除人脸特征
    /// </summary>
    public static void RemoveFeature(long id)
    {
        NativeMethods.HFFeatureHubFaceRemove(id).CheckResult();
    }

    /// <summary>
    /// 获取特征库中的人脸数量
    /// </summary>
    public static int GetFaceCount()
    {
        NativeMethods.HFFeatureHubGetFaceCount(out int count).CheckResult();
        return count;
    }

    /// <summary>
    /// 设置搜索阈值
    /// </summary>
    public static void SetSearchThreshold(float threshold)
    {
        NativeMethods.HFFeatureHubFaceSearchThresholdSetting(threshold).CheckResult();
    }
}
