# bottomsuplist

Steps to reproduce the issue:
1. (Obvious) Clone, build, and run the app
2. Resize the height of the window (this will trigger it to auto-generate some dummy data)
3. Do a two-finger swipe up on a precision touch pad (i.e. Surface Book) to attempt to scroll up.

*Expected:*
You see the rubber band effect where the content comes back to the bottom smoothly

*Actual:*
Either the content seems to fly off the screen (sometimes) or as its returning to hug the bottom it stutters for a bit or may seems to get stuck in a constant stutter.  If it doesn't happen on the first swipe up it may take another try or two.  It's usually pretty consistent, but not guaranteed.
