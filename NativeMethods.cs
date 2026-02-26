// =============================================================================
// InspireFace .NET Interop - P/Invoke Native Method Declarations
// Complete mapping of inspireface.h C API exports
// =============================================================================

using System.Runtime.InteropServices;

namespace InspireFace;

/// <summary>
/// InspireFace C API 原生导出函数 P/Invoke 声明
/// </summary>
public static partial class NativeMethods
{
    /// <summary>
    /// 原生 DLL 名称。部署时确保 InspireFace 动态库在应用程序目录或系统 PATH 中。
    /// Windows: *.dll | Linux: *.so | macOS: *.dylib
    /// </summary>
    public const string DllName = "runtime/InspireFace_x64";

    // ========================================================================
    // SDK 生命周期
    // ========================================================================

    /// <summary>启动 InspireFace SDK</summary>
    /// <param name="resourcePath">资源文件路径</param>
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFLaunchInspireFace(string resourcePath);

    /// <summary>重新加载 InspireFace SDK</summary>
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFReloadInspireFace(string resourcePath);

    /// <summary>终止 InspireFace SDK 并释放所有资源</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFTerminateInspireFace();

    /// <summary>查询 SDK 启动状态</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFQueryInspireFaceLaunchStatus(out int status);

    // ========================================================================
    // ImageStream 图像流
    // ========================================================================

    /// <summary>从图像数据创建图像流</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCreateImageStream(ref HFImageData data, out IntPtr handle);

    /// <summary>创建空的图像流</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCreateImageStreamEmpty(out IntPtr handle);

    /// <summary>设置图像流的缓冲区</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFImageStreamSetBuffer(IntPtr handle, IntPtr buffer, int width, int height);

    /// <summary>设置图像流旋转角度</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFImageStreamSetRotation(IntPtr handle, HFRotation rotation);

    /// <summary>设置图像流格式</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFImageStreamSetFormat(IntPtr handle, HFImageFormat format);

    /// <summary>释放图像流</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFReleaseImageStream(IntPtr streamHandle);

    // ========================================================================
    // ImageBitmap 位图
    // ========================================================================

    /// <summary>从数据创建位图（默认 BGR 像素格式）</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCreateImageBitmap(ref HFImageBitmapData data, out IntPtr handle);

    /// <summary>从文件路径创建位图</summary>
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCreateImageBitmapFromFilePath(string filePath, int channels, out IntPtr handle);

    /// <summary>复制位图</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFImageBitmapCopy(IntPtr handle, out IntPtr copyHandle);

    /// <summary>释放位图</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFReleaseImageBitmap(IntPtr handle);

    /// <summary>从位图创建图像流</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCreateImageStreamFromImageBitmap(IntPtr handle, HFRotation rotation, out IntPtr streamHandle);

    /// <summary>从图像流创建位图</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCreateImageBitmapFromImageStreamProcess(IntPtr streamHandle, out IntPtr handle, int isRotate, float scale);

    /// <summary>将位图写入文件</summary>
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFImageBitmapWriteToFile(IntPtr handle, string filePath);

    /// <summary>在位图上绘制矩形</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFImageBitmapDrawRect(IntPtr handle, HFaceRect rect, HColor color, int thickness);

    /// <summary>在位图上绘制圆（浮点坐标）</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFImageBitmapDrawCircleF(IntPtr handle, HPoint2f point, int radius, HColor color, int thickness);

    /// <summary>在位图上绘制圆（整型坐标）</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFImageBitmapDrawCircle(IntPtr handle, HPoint2i point, int radius, HColor color, int thickness);

    /// <summary>获取位图数据</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFImageBitmapGetData(IntPtr handle, out HFImageBitmapData data);

    /// <summary>显示位图（需 OpenCV GUI 支持）</summary>
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFImageBitmapShow(IntPtr handle, string title, int delay);

    // ========================================================================
    // 硬件扩展
    // ========================================================================

    /// <summary>查询 RGA 编译选项</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFQueryExpansiveHardwareRGACompileOption(out int enable);

    /// <summary>设置 Rockchip DMA Heap 路径</summary>
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSetExpansiveHardwareRockchipDmaHeapPath(string path);

    /// <summary>切换图像处理后端</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSwitchImageProcessingBackend(HFImageProcessingBackend backend);

    /// <summary>设置图像处理对齐宽度</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSetImageProcessAlignedWidth(int width);

    /// <summary>设置 Apple CoreML 推理模式</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSetAppleCoreMLInferenceMode(HFAppleCoreMLInferenceMode mode);

    /// <summary>设置 CUDA 设备 ID</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSetCudaDeviceId(int deviceId);

    /// <summary>获取 CUDA 设备 ID</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetCudaDeviceId(out int deviceId);

    /// <summary>打印 CUDA 设备信息</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFPrintCudaDeviceInfo();

    /// <summary>获取 CUDA 设备数量</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetNumCudaDevices(out int numDevices);

    /// <summary>检查 CUDA 设备支持</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCheckCudaDeviceSupport(out int isSupport);

    // ========================================================================
    // FaceSession 会话
    // ========================================================================

    /// <summary>创建人脸分析会话</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCreateInspireFaceSession(
        HFSessionCustomParameter parameter,
        HFDetectMode detectMode,
        int maxDetectFaceNum,
        int detectPixelLevel,
        int trackByDetectModeFPS,
        out IntPtr handle);

    /// <summary>使用自定义选项创建会话</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCreateInspireFaceSessionOptional(
        int customOption,
        HFDetectMode detectMode,
        int maxDetectFaceNum,
        int detectPixelLevel,
        int trackByDetectModeFPS,
        out IntPtr handle);

    /// <summary>释放会话</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFReleaseInspireFaceSession(IntPtr handle);

    /// <summary>切换关键点引擎</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSwitchLandmarkEngine(HFSessionLandmarkEngine engine);

    /// <summary>查询支持的人脸检测像素级别</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFQuerySupportedPixelLevelsForFaceDetection(out HFFaceDetectPixelList pixelLevels);

    // ========================================================================
    // 人脸检测/跟踪
    // ========================================================================

    /// <summary>执行人脸检测/跟踪</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFExecuteFaceTrack(IntPtr session, IntPtr streamHandle, out HFMultipleFaceData results);

    /// <summary>清除跟踪的人脸</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionClearTrackingFace(IntPtr session);

    /// <summary>设置跟踪丢失恢复模式</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionSetTrackLostRecoveryMode(IntPtr session, int enable);

    /// <summary>设置轻量跟踪置信度阈值</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionSetLightTrackConfidenceThreshold(IntPtr session, float value);

    /// <summary>设置跟踪预览大小</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionSetTrackPreviewSize(IntPtr session, int previewSize);

    /// <summary>获取跟踪预览大小</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionGetTrackPreviewSize(IntPtr session, out int previewSize);

    /// <summary>设置最小人脸像素大小过滤</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionSetFilterMinimumFacePixelSize(IntPtr session, int minSize);

    /// <summary>设置人脸检测阈值</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionSetFaceDetectThreshold(IntPtr session, float threshold);

    /// <summary>设置跟踪模式平滑比率</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionSetTrackModeSmoothRatio(IntPtr session, float ratio);

    /// <summary>设置跟踪模式平滑缓存帧数</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionSetTrackModeNumSmoothCacheFrame(IntPtr session, int num);

    /// <summary>设置跟踪模式检测间隔</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionSetTrackModeDetectInterval(IntPtr session, int num);

    // ========================================================================
    // Token 操作
    // ========================================================================

    /// <summary>复制人脸令牌数据到缓冲区</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCopyFaceBasicToken(HFFaceBasicToken token, IntPtr buffer, int bufferSize);

    /// <summary>获取人脸令牌大小</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetFaceBasicTokenSize(out int bufferSize);

    /// <summary>获取密集关键点数量</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetNumOfFaceDenseLandmark(out int num);

    /// <summary>从人脸令牌获取密集关键点</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetFaceDenseLandmarkFromFaceToken(HFFaceBasicToken singleFace, [Out] HPoint2f[] landmarks, int num);

    /// <summary>从人脸令牌获取五个关键点</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetFaceFiveKeyPointsFromFaceToken(HFFaceBasicToken singleFace, [Out] HPoint2f[] landmarks, int num);

    // ========================================================================
    // 人脸识别 / 特征提取
    // ========================================================================

    /// <summary>提取人脸特征（SDK 分配内存）</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFaceFeatureExtract(IntPtr session, IntPtr streamHandle, HFFaceBasicToken singleFace, out HFFaceFeature feature);

    /// <summary>提取人脸特征到预分配的 HFFaceFeature</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFaceFeatureExtractTo(IntPtr session, IntPtr streamHandle, HFFaceBasicToken singleFace, HFFaceFeature feature);

    /// <summary>提取人脸特征并复制到 float 数组</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFaceFeatureExtractCpy(IntPtr session, IntPtr streamHandle, HFFaceBasicToken singleFace, [Out] float[] feature);

    /// <summary>创建人脸特征（分配内存）</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCreateFaceFeature(out HFFaceFeature feature);

    /// <summary>释放人脸特征</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFReleaseFaceFeature(ref HFFaceFeature feature);

    /// <summary>获取人脸对齐图像</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFaceGetFaceAlignmentImage(IntPtr session, IntPtr streamHandle, HFFaceBasicToken singleFace, out IntPtr handle);

    /// <summary>使用对齐图像提取特征</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFaceFeatureExtractWithAlignmentImage(IntPtr session, IntPtr streamHandle, HFFaceFeature feature);

    // ========================================================================
    // 人脸比较
    // ========================================================================

    /// <summary>
    /// 1:1 人脸特征比较。返回余弦相似度（范围 -1 到 1）
    /// </summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFaceComparison(HFFaceFeature feature1, HFFaceFeature feature2, out float result);

    /// <summary>获取推荐的余弦阈值</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetRecommendedCosineThreshold(out float threshold);

    /// <summary>将余弦相似度转换为百分比</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFCosineSimilarityConvertToPercentage(float similarity, out float result);

    /// <summary>更新相似度转换器配置</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFUpdateCosineSimilarityConverter(HFSimilarityConverterConfig config);

    /// <summary>获取相似度转换器配置</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetCosineSimilarityConverter(out HFSimilarityConverterConfig config);

    /// <summary>获取特征向量长度</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetFeatureLength(out int num);

    // ========================================================================
    // FeatureHub 特征库管理
    // ========================================================================

    /// <summary>启用 FeatureHub</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubDataEnable(HFFeatureHubConfiguration configuration);

    /// <summary>禁用 FeatureHub</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubDataDisable();

    /// <summary>设置搜索阈值</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubFaceSearchThresholdSetting(float threshold);

    /// <summary>插入人脸特征</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubInsertFeature(HFFaceFeatureIdentity featureIdentity, out long allocId);

    /// <summary>搜索最相似人脸</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubFaceSearch(HFFaceFeature searchFeature, out float confidence, out HFFaceFeatureIdentity mostSimilar);

    /// <summary>TopK 搜索</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubFaceSearchTopK(HFFaceFeature searchFeature, int topK, out HFSearchTopKResults results);

    /// <summary>移除人脸特征</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubFaceRemove(long id);

    /// <summary>更新人脸特征</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubFaceUpdate(HFFaceFeatureIdentity featureIdentity);

    /// <summary>通过 ID 获取人脸特征</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubGetFaceIdentity(long customId, out HFFaceFeatureIdentity identity);

    /// <summary>获取 FeatureHub 中的人脸数量</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubGetFaceCount(out int count);

    /// <summary>查看人脸数据库表</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubViewDBTable();

    /// <summary>获取所有已有 ID</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFeatureHubGetExistingIds(out HFFeatureHubExistingIds ids);

    // ========================================================================
    // Face Pipeline 人脸处理管线
    // ========================================================================

    /// <summary>多人脸管线处理</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFMultipleFacePipelineProcess(IntPtr session, IntPtr streamHandle, ref HFMultipleFaceData faces, HFSessionCustomParameter parameter);

    /// <summary>多人脸管线处理（自定义选项）</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFMultipleFacePipelineProcessOptional(IntPtr session, IntPtr streamHandle, ref HFMultipleFaceData faces, int customOption);

    // ========================================================================
    // 管线结果获取
    // ========================================================================

    /// <summary>获取 RGB 活体检测置信度</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetRGBLivenessConfidence(IntPtr session, out HFRGBLivenessConfidence confidence);

    /// <summary>获取口罩检测置信度</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetFaceMaskConfidence(IntPtr session, out HFFaceMaskConfidence confidence);

    /// <summary>获取人脸质量置信度</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetFaceQualityConfidence(IntPtr session, out HFFaceQualityConfidence confidence);

    /// <summary>检测单个人脸质量</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFFaceQualityDetect(IntPtr session, HFFaceBasicToken singleFace, out float confidence);

    /// <summary>获取人脸交互状态</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetFaceInteractionStateResult(IntPtr session, out HFFaceInteractionState result);

    /// <summary>获取人脸交互动作</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetFaceInteractionActionsResult(IntPtr session, out HFFaceInteractionsActions actions);

    /// <summary>获取人脸属性结果</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetFaceAttributeResult(IntPtr session, out HFFaceAttributeResult results);

    /// <summary>获取人脸情绪结果</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFGetFaceEmotionResult(IntPtr session, out HFFaceEmotionResult result);

    // ========================================================================
    // 系统 / 版本
    // ========================================================================

    /// <summary>查询 InspireFace 版本信息</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFQueryInspireFaceVersion(out HFInspireFaceVersion version);

    /// <summary>查询扩展信息</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFQueryInspireFaceExtendedInformation(out HFInspireFaceExtendedInformation information);

    /// <summary>设置日志级别</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSetLogLevel(HFLogLevel level);

    /// <summary>禁用日志</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFLogDisable();

    // ========================================================================
    // 调试工具
    // ========================================================================

    /// <summary>调试：显示图像流</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void HFDeBugImageStreamImShow(IntPtr streamHandle);

    /// <summary>调试：保存图像流到文件</summary>
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFDeBugImageStreamDecodeSave(IntPtr streamHandle, string savePath);

    /// <summary>调试：显示资源统计信息</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFDeBugShowResourceStatistics();

    /// <summary>调试：获取未释放的 Session 数量</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFDeBugGetUnreleasedSessionsCount(out int count);

    /// <summary>调试：获取未释放的图像流数量</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFDeBugGetUnreleasedStreamsCount(out int count);

    /// <summary>设置跟踪耗时统计</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionSetEnableTrackCostSpend(IntPtr session, int value);

    /// <summary>打印跟踪耗时</summary>
    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int HFSessionPrintTrackCostSpend(IntPtr session);
}
