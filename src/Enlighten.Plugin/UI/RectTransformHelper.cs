using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enlighten.src.Enlighten.Plugin.UI
{
	internal class RectTransformHelper
	{
		public static void ClampRectTransformWithinBounds(RectTransform target, RectTransform bounds)
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
	}
}
