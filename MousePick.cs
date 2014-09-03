public Vector3 GetMousePickLocation(GraphicsDevice graphics, Matrix camProjection, Matrix camView)
{
  ////////////////
  // Make a Ray //
  ////////////////

  // Get the mouse screen position
  MouseState mouseState = Mouse.GetState();
  Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

  // Create 2 positions in screen space based on cursor position.
  // - screen space is also 3D, because (screenX, screenY, z-buffer depth)
  // - 0 means close to cam (in projection/screen space)
  // - 1 means close to infinity (in projection/screen space)
  Vector3 near = new Vector3(mousePosition, 0f);
  Vector3 far = new Vector3(mousePosition, 1f);

  // Calculate the 3D world space coords
  Vector3 nearWorld = graphics.Viewport.Unproject(near, camProjection, camView, Matrix.Identity);
  Vector3 farWorld = graphics.Viewport.Unproject(far, camProjection, camView, Matrix.Identity);

  // Get the direction vector of the ray to be casted
  Vector3 rayDirection = Vector3.Normalize(farWorld - nearWorld);

  // Cast a ray
  Ray ray = new Ray(nearWorld, rayDirection);

  ////////////////////////////
  // Calculate intersection //
  ////////////////////////////

  // make a temp infinite xz-plane
  Plane plane = new Plane(new Vector3(0f, 1f, 0f), 0f);

  // Calculate distance from the ray origin to the point of intersection on the plane
  // - float? return type is a Nullable float (just means can also contain null)
  float? distance = ray.Intersects(plane);

  // Calculate distance from the ray origin to the point of intersection on the plane (actual math behind)
  //float denominator = Vector3.Dot(plane.Normal, ray.Direction); //projection
  //float numerator = Vector3.Dot(plane.Normal, ray.Position) + plane.D;
  //float distance = -(numerator / denominator);

  // Calculate the picked position
  // - return the last destination if null, to indicate no movement
  if (distance != null)
      return nearWorld + rayDirection * (float)distance;
  else
      return Vector3.Zero;
}
