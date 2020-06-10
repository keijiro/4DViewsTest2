4DViewsTest2
============

![gif](https://i.imgur.com/8WI4EIl.gif)
![gif](https://i.imgur.com/YmyF0wB.gif)

**4DViewsTest2** is a Unity project where I'm trying several VFX ideas with
[4DViews] volumetric videos. In contrast to the [previous project] where I used
the 4DS plugin to replay the volumetric video, this project uses the
[Alembic format] to stream the reconstructed meshes.

[4DViews]: https://www.4dviews.com/
[previous project]: https://github.com/keijiro/4DViewsTest
[Alembic format]:
  https://docs.unity3d.com/Packages/com.unity.formats.alembic@latest

The volumetric video data files are missing from this repository. You can
download them from the [Resources page] of the 4DViews site.

[Resources page]: https://www.4dviews.com/volumetric-resources

You have to convert the texture files into a [HAP] encoded .mov file. Use
[ffmpeg.sh] for the conversion.

[HAP]: https://hap.video/
[ffmpeg.sh]: /ffmpeg.sh
