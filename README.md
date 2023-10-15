# Getting Started

To play on Windows you can [download](https://github.com/YsmanKhamidullin/Unity_Quest_Map/releases) zip with .exe

You can clone this repo for watch in Unity v.2022.3.4f1

````
git clone https://github.com/YsmanKhamidullin/Unity_Quest_Map.git
````

# How to
Data of missions and heroes containing in Scriptable Objects. This provides easy way for changes. 

> Scriptable object of mission:
> 
> ![MissionInfo.png](MissionInfo.png)
> 
> 1. To change screen position of Node you can easily drag ScreenPos values. 
> Value (0,0) - is left down corner, and value (100,100) is upper right corner
> 2. If you want create new mission dont forget add to scriptable object MissionMap, 
> that represents list of all mission. 
> Then you need select game-object Root in Hierarchy and press Test Generate button to see node position.

> About code:
> 1. Root.cs creates copy data from scriptable objects. 
Then Root initialize required components and give them data.
> 2. After initialization, MissionMapScreen.cs will generate a node map corresponding to the mission information.
> 3. When player open new window - MissionMapScreen.cs sends data to this windows  
> ![MissionMapScreen.png](MissionMapScreen.png) 

# Imported packages

TextMeshPro ([Unity Tutorial](https://learn.unity.com/tutorial/working-with-textmesh-pro#))

> Ultimate text solution for Unity.
> It’s the perfect replacement for Unity’s UI Text and the legacy Text Mesh.

NaughtyAttributes ([GitHub](https://github.com/dbrizov/NaughtyAttributes))

> It expands the range of attributes that Unity provides so that you can create powerful inspectors
> without the need of custom editors or property drawers.

# What I learned
> 1. It is useful to use ScriptableObjects if you want to store a lot of similar data
> 2. You should be sure that data of ScriptableObjects will not change
> 3. Instead of creating clones of ScriptableObject at startup, 
> I needed to have mutable fields in MissionNode.cs that would be created from a reference to ScriptableObject
# About project

This project is oriented to complete test task for Unity Gameplay position

[Repo Owner](https://github.com/YsmanKhamidullin)