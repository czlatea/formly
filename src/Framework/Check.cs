using System;

namespace Framework
{
  public static class Check
  {
    public static void NotNullOrEmpty(string value, string parameterName)
    {
      if (string.IsNullOrEmpty(value))
      {
        throw new ArgumentNullException(parameterName, $"{parameterName} can not be null or empty.");
      }
    }

    public static void Assigned(object value, string parameterName)
    {
      if (value == null)
      {
        throw new ArgumentNullException(parameterName, $"{parameterName} can not be null or empty.");
      }
    }
  }
}
