using UnityEngine;

namespace Enlighten.UI
{
	static internal class RectTransformHelper
	{
		public static Vector2 ClampPointInside(this RectTransform bounds, Vector2 point)
		{
			float x = Mathf.Clamp(point.x, bounds.rect.xMin, bounds.rect.xMax);
			float y = Mathf.Clamp(point.y, bounds.rect.yMin, bounds.rect.yMax);
			return new Vector2(x, y);
		}

		public static void ClampRectWithinBounds(this RectTransform target, RectTransform bounds)
		{
			// Get world corners of both the target and bounds RectTransforms
			Vector3[] targetCorners = new Vector3[4];
			target.GetWorldCorners(targetCorners);

			Vector3[] boundsCorners = new Vector3[4];
			bounds.GetWorldCorners(boundsCorners);

			// Calculate the size of the target RectTransform in world space
			Vector2 targetSize = new Vector2(targetCorners[2].x - targetCorners[0].x, targetCorners[2].y - targetCorners[0].y);

			// Calculate the minimum and maximum world position for the target's center
			Vector2 minBounds = new Vector2(boundsCorners[0].x, boundsCorners[0].y); // Bottom-left of bounds
			Vector2 maxBounds = new Vector2(boundsCorners[2].x, boundsCorners[2].y); // Top-right of bounds

			Vector2 minPosition = minBounds + targetSize / 2f;
			Vector2 maxPosition = maxBounds - targetSize / 2f;

			// Get the current position of the target in world space
			Vector2 targetPosition = target.position;

			// Clamp the target position in world space to stay within the bounds
			Vector2 clampedPosition = targetPosition;
			clampedPosition.x = Mathf.Clamp(clampedPosition.x, minPosition.x, maxPosition.x);
			clampedPosition.y = Mathf.Clamp(clampedPosition.y, minPosition.y, maxPosition.y);

			// Convert the clamped world position back to local space and set it to the target
			target.position = clampedPosition;
		}

		public static float GetLeft(this RectTransform rt)
		{
			return rt.offsetMin.x;
		}
		public static void SetLeft(this RectTransform rt, float left)
		{
			rt.offsetMin = new Vector2(left, rt.offsetMin.y);
		}

		public static float GetRight(this RectTransform rt)
		{
			return -rt.offsetMax.x;
		}
		public static void SetRight(this RectTransform rt, float right)
		{
			rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
		}

		public static float GetTop(this RectTransform rt)
		{
			return -rt.offsetMax.y;
		}
		public static void SetTop(this RectTransform rt, float top)
		{
			rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
		}

		public static float GetBottom(this RectTransform rt)
		{
			return rt.offsetMin.y;
		}
		public static void SetBottom(this RectTransform rt, float bottom)
		{
			rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
		}
	}
}
