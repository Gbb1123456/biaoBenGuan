解决在代码内设置第一人称控制器的位置和旋转时会闪现回去的问题
增加静音选项
设置摄像机位置：SetLocalPosition（Vector3 pos）  
设置角色和摄像机四元数：SetLocalRotation(Quaternion characterRot, Quaternion cameraRot) 
获取角色和摄像机四元数：(Quaternion characterRot, Quaternion cameraRot) GetLocalRotations()  