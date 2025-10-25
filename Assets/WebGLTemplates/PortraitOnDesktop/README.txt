
Unity WebGL Template — Portrait on Desktop (Non-Fullscreen)

What this does
--------------
• Locks your WebGL build into a portrait 9:16 frame even on desktop.
• Centers the game with black side bars (pillarboxing). 
• Caps the visible width (default 480px) so it never expands to full-width.
• Uses matchWebGLToCanvasSize=true so Unity renders at the CSS size we control.
• Caps devicePixelRatio to 2 for performance.

How to use
----------
1) In your Unity project, create the folder:
   Assets/WebGLTemplates/PortraitOnDesktop

2) Drop this index.html into that folder.

3) Player Settings → WebGL → Resolution and Presentation → WebGL Template:
   Choose "PortraitOnDesktop" (it will appear with the folder name).

4) Build for WebGL. That’s it.

Tweaks
------
• Change the maximum desktop width by editing:  --max-shell-width: 480px
• Increase the vertical usage by editing:       --vh-usage: 90vh
• If you want a different aspect (e.g., 10:16), update:
  - the CSS 'aspect-ratio' and the JS calculations.
• If you must hide any fullscreen UI inside your game, ensure you don’t call
  Screen.fullScreen = true from Unity code.

Notes
-----
• This template removes Unity’s default fullscreen button.
• On mobile browsers, it will still be portrait and fill up to 90% of height.
• Keep ‘matchWebGLToCanvasSize’ true to avoid blurry scaling.
