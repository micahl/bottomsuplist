# bottomsuplist

A basic example of using an ItemsRepeater and the new ScrollViewer control from WinUI to create a simple bottoms-up list that you might find in a chat app or a logging screen.  The reason the scrolling sticks to the end is because of the VerticalAnchorRatio property on the ScrollViewer. Read up on it if you want to know more.

Caveats:
This example isn't a turn-key solution.  It doesn't include the following things you may forget about if you mistakenly believe ItemsRepeater provides the built-in policy of a ListView.  None of these are terribly difficult to add if you know to do it.  :)
Things to consider:
1. Support keyboard focus items in the list.  Basically you need to use something that derives from Control to represent each item since Control can take focus (FrameworkElement doesn't today).  You could just put a UserControl in your DataTemplate.  Make sure IsTabStop = True and consider UseSystemFocusVisuals = True.  ItemsRepeater by default enables XYFocusKeyboardNavigation for its items.  But if you're doing a chat app and the size of the items are fairly small and aligned at the edges of the screen then Up/Down based on spatial keyboard focus will stick to one side or the other.  If you want focus to go through each item then you'll want to have your container extend the full width, possibly with a transparent background.
2. Accessibility.  The ItemsRepeater doesn't automatically enable the "x of y" that Narrator would read.  Refer to the [Enable Accessibility](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/items-repeater#enable-accessibility) section of docs on how to do that.
3. Selection.  You'll have to do your own thing here.  Feel free to raise the need to make this easier on the [WinUI repo](https://github.com/microsoft/microsoft-ui-xaml)

