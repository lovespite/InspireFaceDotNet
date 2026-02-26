// =============================================================================
// InspireFace .NET - InspireFaceException
// =============================================================================

namespace InspireFace;

/// <summary>
/// InspireFace SDK 操作异常
/// </summary>
public class InspireFaceException : Exception
{
  /// <summary>
  /// 原生错误码
  /// </summary>
  public int NativeErrorCode { get; }

  public InspireFaceException(int errorCode)
      : base($"InspireFace 错误: {ErrorCode.GetDescription(errorCode)} (0x{errorCode:X4})")
  {
    NativeErrorCode = errorCode;
  }

  public InspireFaceException(int errorCode, string message)
      : base(message)
  {
    NativeErrorCode = errorCode;
  }
}

/// <summary>
/// 错误码辅助扩展
/// </summary>
internal static class ResultExtensions
{
  /// <summary>
  /// 检查返回值，非成功则抛异常
  /// </summary>
  public static void CheckResult(this int result)
  {
    if (result != ErrorCode.HSUCCEED)
      throw new InspireFaceException(result);
  }
}
