

Class Variables

```
 RasterizerState wireFrameState;
 bool isWireframe;
```

In protected override void LoadContent()


```
wireFrameState = new RasterizerState()
{
    FillMode = FillMode.WireFrame,
    CullMode = CullMode.None,
};

```

At the start of the Draw (after the Clear() ) protected override void Draw(GameTime gameTime)


```
if (isWireframe)
{
    GraphicsDevice.RasterizerState = wireFrameState;
}
else
{
    GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
}

```