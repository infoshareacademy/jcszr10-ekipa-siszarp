using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebTaskMaster.Extensions;

public static class TempDataToastExtensions
{
    private const string MessageKey = "ToastMessage";
    private const string ColorClassKey = "ToastColorClass";

    private const string SuccessColorClass = "bg-success";
    private const string DangerColorClass = "bg-danger";

    public static void SetSuccessToastMessage(this ITempDataDictionary tempData, string toastMessage)
    {
        tempData[MessageKey] = toastMessage;
        tempData[ColorClassKey] = SuccessColorClass;
    }

    public static void SetDangerToastMessage(this ITempDataDictionary tempData, string toastMessage)
    {
        tempData[MessageKey] = toastMessage;
        tempData[ColorClassKey] = DangerColorClass;
    }

    public static bool ContainsToastMessage(this ITempDataDictionary tempData)
    {
        return tempData.ContainsKey(MessageKey);
    }

    public static string GetToastMessage(this ITempDataDictionary tempData)
    {
        var messageObject = tempData[MessageKey];

        if (messageObject is string message)
        {
            return message;
        }

        return string.Empty;
    }

    public static string GetToastColorClass(this ITempDataDictionary tempData)
    {
        var colorClassObject = tempData[ColorClassKey];

        if (colorClassObject is string colorClass)
        {
            return colorClass;
        }

        return SuccessColorClass;
    }
}