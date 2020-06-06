ffmpeg -y \
  -framerate 60 \
  -i Sample4DViews_SwordFighting_60fps_FILTERED_ABC_TEXTURE/tex.%05d.png \
  -c:v hap Sample4DViews_SwordFighting_60fps_FILTERED.mov
