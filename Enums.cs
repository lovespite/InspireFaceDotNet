// =============================================================================
// InspireFace .NET Interop - Enumerations
// Auto-mapped from inspireface.h / intypedef.h / herror.h
// =============================================================================

namespace InspireFace;

/// <summary>
/// 图像流格式
/// </summary>
public enum HFImageFormat
{
  /// <summary>RGB 格式</summary>
  RGB = 0,
  /// <summary>BGR 格式 (OpenCV Mat 默认)</summary>
  BGR = 1,
  /// <summary>RGBA 格式</summary>
  RGBA = 2,
  /// <summary>BGRA 格式</summary>
  BGRA = 3,
  /// <summary>YUV NV12 格式</summary>
  YUV_NV12 = 4,
  /// <summary>YUV NV21 格式</summary>
  YUV_NV21 = 5,
  /// <summary>I420 格式</summary>
  I420 = 6,
  /// <summary>灰度格式</summary>
  GRAY = 7,
}

/// <summary>
/// 图像旋转角度
/// </summary>
public enum HFRotation
{
  /// <summary>不旋转</summary>
  Rotation0 = 0,
  /// <summary>旋转 90°</summary>
  Rotation90 = 1,
  /// <summary>旋转 180°</summary>
  Rotation180 = 2,
  /// <summary>旋转 270°</summary>
  Rotation270 = 3,
}

/// <summary>
/// 人脸检测模式
/// </summary>
public enum HFDetectMode
{
  /// <summary>始终检测模式，适用于图片</summary>
  AlwaysDetect = 0,
  /// <summary>轻量跟踪模式，适用于视频流、前置摄像头</summary>
  LightTrack = 1,
  /// <summary>基于检测的跟踪模式，适用于高分辨率/监控场景</summary>
  TrackByDetection = 2,
}

/// <summary>
/// 图像处理后端
/// </summary>
public enum HFImageProcessingBackend
{
  /// <summary>CPU 后端（默认）</summary>
  CPU = 0,
  /// <summary>Rockchip RGA 后端（需硬件支持）</summary>
  RGA = 1,
}

/// <summary>
/// Apple CoreML 推理模式
/// </summary>
public enum HFAppleCoreMLInferenceMode
{
  CPU = 0,
  GPU = 1,
  ANE = 2,
}

/// <summary>
/// 关键点引擎
/// </summary>
public enum HFSessionLandmarkEngine
{
  HypLmV2_025 = 0,
  HypLmV2_050 = 1,
  InsightFace2D106Track = 2,
}

/// <summary>
/// 特征搜索模式
/// </summary>
public enum HFSearchMode
{
  /// <summary>急切模式：遇到满足阈值的即停止</summary>
  Eager = 0,
  /// <summary>穷举模式：搜索直到找到最佳匹配</summary>
  Exhaustive = 1,
}

/// <summary>
/// 主键模式
/// </summary>
public enum HFPKMode
{
  /// <summary>自动递增</summary>
  AutoIncrement = 0,
  /// <summary>手动输入</summary>
  ManualInput = 1,
}

/// <summary>
/// 日志级别
/// </summary>
public enum HFLogLevel
{
  None = 0,
  Debug = 1,
  Info = 2,
  Warn = 3,
  Error = 4,
  Fatal = 5,
}
