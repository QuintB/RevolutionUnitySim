# RevolutionUnitySim
In order to properly prototype an autonomous driving experience, I had to pull together many different free Unity packages and glue them together with custom code. This is that code.

The first file I had to edit was a speed controller to keep the vehicle from constantly accelerating. I named this file "woahBuddySlowDown" and added in some items to limit the vector speed of the rigidbody - in this case, the car. Later, I found this was also useful for controlling the speedometer (since it reads the speed of the vehicle), so I used it for that too with [some help from CodeMonkey](https://unitycodemonkey.com/video.php?v=3xSYkFdQiZ0 "CodeMonkey Tutorial Page").

After that, I edited the [Vehicle Controller from Unity](https://assetstore.unity.com/packages/essentials/tutorial-projects/vehicle-tools-83660 "Unity Store Download Page") to take input from the wheel controller. It wasn't a huge edit.

Then, I set to work editing the code from [the PathFollower package](https://assetstore.unity.com/packages/tools/utilities/b-zier-path-creator-136082 "Unity Store Download Page"), which is provided for free by Sebastian Lague. This is where I edited the most, including three states, a way to toggle between them, and a way to follow the path only on the X and Z positions so the Y position could be simulated with gravity. This was the most difficult part of the project, but also works well enough to be considered a success!
