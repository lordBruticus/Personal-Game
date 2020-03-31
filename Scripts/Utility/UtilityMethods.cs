using UnityEngine;

public static class UtilityMethods
{
    // Helper method for positioning UI elements to world objects
    public static void MoveUiElementToWorldPosition(RectTransform rectTransform, Vector3 worldPosition)
    {
        rectTransform.position = worldPosition + new Vector3(0, 3);
        rectTransform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 10000); // Needed to rotate UI the right way
        ScaleRectTransformBasedOnDistanceFromCamera(rectTransform);
    }

    private static void ScaleRectTransformBasedOnDistanceFromCamera(RectTransform rectTransform)
    {
        float distance = Vector3.Distance(Camera.main.transform.position, rectTransform.position);
        rectTransform.localScale = new Vector3(distance / UIManager.vrUiScaleDivider, distance / UIManager.vrUiScaleDivider, 1f);
    }

    // 1
    public static Quaternion SmoothlyLook(Transform fromTransform, Vector3 toVector3)
    {
        //2
        if (fromTransform.position == toVector3)
        {
            return fromTransform.localRotation;
        }

        //3
        Quaternion currentRotation = fromTransform.localRotation;
        Quaternion targetRotation = Quaternion.LookRotation(toVector3 - fromTransform.position);

        //4
        return Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 10f);
    }
}
