// =============================================================================
// InspireFace .NET Interop - Error Codes
// Mapped from herror.h
// =============================================================================

namespace InspireFace;

/// <summary>
/// InspireFace SDK 错误码定义
/// </summary>
public static class ErrorCode
{
  /// <summary>成功</summary>
  public const int HSUCCEED = 0;

  // ── 基础错误 (1-99) ──
  public const int HERR_UNKNOWN = 0x0001;
  public const int HERR_INVALID_PARAM = 0x0002;
  public const int HERR_INVALID_IMAGE_STREAM_HANDLE = 0x0003;
  public const int HERR_INVALID_CONTEXT_HANDLE = 0x0004;
  public const int HERR_INVALID_FACE_TOKEN = 0x0005;
  public const int HERR_INVALID_FACE_FEATURE = 0x0006;
  public const int HERR_INVALID_FACE_LIST = 0x0007;
  public const int HERR_INVALID_BUFFER_SIZE = 0x0008;
  public const int HERR_INVALID_IMAGE_STREAM_PARAM = 0x0009;
  public const int HERR_INVALID_SERIALIZATION_FAILED = 0x000A;
  public const int HERR_INVALID_DETECTION_INPUT = 0x000B;
  public const int HERR_INVALID_IMAGE_BITMAP_HANDLE = 0x000C;
  public const int HERR_IMAGE_STREAM_DECODE_FAILED = 0x000D;

  // ── Session 错误 (100-199) ──
  public const int HERR_SESS_FUNCTION_UNUSABLE = 0x0065;
  public const int HERR_SESS_TRACKER_FAILURE = 0x0066;
  public const int HERR_SESS_PIPELINE_FAILURE = 0x0067;
  public const int HERR_SESS_INVALID_RESOURCE = 0x0068;
  public const int HERR_SESS_LANDMARK_NUM_NOT_MATCH = 0x0069;
  public const int HERR_SESS_LANDMARK_NOT_ENABLE = 0x006A;
  public const int HERR_SESS_KEY_POINT_NUM_NOT_MATCH = 0x006B;
  public const int HERR_SESS_REC_EXTRACT_FAILURE = 0x006C;
  public const int HERR_SESS_REC_CONTRAST_FEAT_ERR = 0x006D;
  public const int HERR_SESS_FACE_DATA_ERROR = 0x006E;
  public const int HERR_SESS_FACE_REC_OPTION_ERROR = 0x006F;

  // ── FeatureHub 错误 (200-249) ──
  public const int HERR_FT_HUB_DISABLE = 0x00C9;
  public const int HERR_FT_HUB_INSERT_FAILURE = 0x00CA;
  public const int HERR_FT_HUB_NOT_FOUND_FEATURE = 0x00CB;

  // ── Archive 错误 (250-299) ──
  public const int HERR_ARCHIVE_LOAD_FAILURE = 0x00FB;
  public const int HERR_ARCHIVE_LOAD_MODEL_FAILURE = 0x00FC;
  public const int HERR_ARCHIVE_FILE_FORMAT_ERROR = 0x00FD;
  public const int HERR_ARCHIVE_REPETITION_LOAD = 0x00FE;
  public const int HERR_ARCHIVE_NOT_LOAD = 0x00FF;

  // ── 设备/硬件错误 (300-349) ──
  public const int HERR_DEVICE_CUDA_NOT_SUPPORT = 0x012D;
  public const int HERR_DEVICE_CUDA_TENSORRT_NOT_SUPPORT = 0x012E;
  public const int HERR_DEVICE_CUDA_UNKNOWN_ERROR = 0x012F;
  public const int HERR_DEVICE_CUDA_DISABLE = 0x0130;

  // ── 扩展模块错误 (350-549) ──
  public const int HERR_EXTENSION_ERROR = 0x015F;
  public const int HERR_EXTENSION_MLMODEL_LOAD_FAILED = 0x0160;
  public const int HERR_EXTENSION_HETERO_MODEL_TAG_ERROR = 0x0161;
  public const int HERR_EXTENSION_HETERO_REC_HEAD_CONFIG_ERROR = 0x0162;
  public const int HERR_EXTENSION_HETERO_MODEL_NOT_MATCH = 0x0163;
  public const int HERR_EXTENSION_HETERO_MODEL_NOT_LOADED = 0x0164;

  /// <summary>
  /// 判断返回值是否为成功
  /// </summary>
  public static bool IsSuccess(int result) => result == HSUCCEED;

  /// <summary>
  /// 获取错误码的描述文本
  /// </summary>
  public static string GetDescription(int code) => code switch
  {
    HSUCCEED => "操作成功",
    HERR_UNKNOWN => "未知错误",
    HERR_INVALID_PARAM => "无效参数",
    HERR_INVALID_IMAGE_STREAM_HANDLE => "无效图像流句柄",
    HERR_INVALID_CONTEXT_HANDLE => "无效上下文句柄",
    HERR_INVALID_FACE_TOKEN => "无效人脸令牌",
    HERR_INVALID_FACE_FEATURE => "无效人脸特征",
    HERR_INVALID_FACE_LIST => "无效人脸列表",
    HERR_INVALID_BUFFER_SIZE => "无效缓冲区大小",
    HERR_INVALID_IMAGE_STREAM_PARAM => "无效图像参数",
    HERR_INVALID_SERIALIZATION_FAILED => "序列化失败",
    HERR_INVALID_DETECTION_INPUT => "检测器输入尺寸修改失败",
    HERR_INVALID_IMAGE_BITMAP_HANDLE => "无效位图句柄",
    HERR_IMAGE_STREAM_DECODE_FAILED => "图像流解码失败",
    HERR_SESS_FUNCTION_UNUSABLE => "Session 功能不可用",
    HERR_SESS_TRACKER_FAILURE => "跟踪模块未初始化",
    HERR_SESS_PIPELINE_FAILURE => "Pipeline 模块未初始化",
    HERR_SESS_INVALID_RESOURCE => "无效静态资源",
    HERR_SESS_REC_EXTRACT_FAILURE => "人脸特征提取未注册",
    HERR_SESS_REC_CONTRAST_FEAT_ERR => "比较特征向量长度不正确",
    HERR_FT_HUB_DISABLE => "FeatureHub 已禁用",
    HERR_FT_HUB_INSERT_FAILURE => "数据插入错误",
    HERR_FT_HUB_NOT_FOUND_FEATURE => "未找到人脸特征",
    HERR_ARCHIVE_LOAD_FAILURE => "资源加载失败",
    HERR_ARCHIVE_LOAD_MODEL_FAILURE => "模型加载失败",
    HERR_ARCHIVE_NOT_LOAD => "模型未加载",
    HERR_DEVICE_CUDA_NOT_SUPPORT => "CUDA 不支持",
    HERR_DEVICE_CUDA_DISABLE => "CUDA 支持已禁用",
    _ => $"未知错误码 (0x{code:X4})",
  };
}
