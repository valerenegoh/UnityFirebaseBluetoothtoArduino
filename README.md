# UnityBluetoothtoArduino
Stage 4 of SUTD Capstone 45 Pico Musical Engineering Installation project.<br/>
Refer to the following [link](https://youtu.be/EubplCl5Q8s) for a demo.

Unity directly reads the corresponding text file and sends the notes to play to an Arduino via Serial communication.

Bluetooth communication between Unity and Arduino: https://assetstore.unity.com/packages/tools/network/bluetooth-le-for-ios-tvos-and-android-26661
This plugin only works for Android and iOS devices and not PC.

The Arduino script requires you to set up an electronic circuit with 25 LEDs/solenoids/etc as shown in the video. Ensure they are wired to the corresponding pin numbers as in the Arduino file. An Arduino MEGA was used for this project as it has enough number of pin holes.

2 octaves, fully chromatic scale is used to form 25 notes as shown below.
<img width="436" alt="midirange" src="https://user-images.githubusercontent.com/23626462/61297441-d77fdb80-a80e-11e9-857d-ced140331160.PNG">

Steps for uploading Arduino:
1. Wire the Arduino circuit containing HM10 Bluetooth module and solenoids.
2. Connect to your Arduino and upload the sketch in Arduino folder. 
3. If you run into TimeOut error, try the following:
  a. Ensure RXTX is disconnected during upload
  b. Ensure you have set the COM Port under Tools
  b. Click the reset button on the Arduino.

## Deployment
A current version has already been deployed under the `Build` folder. If further changes are made to the Unity file, 

### Steps for deploying on Android devices [Windows PC is needed]:
1. Open the Jukebox project under Build folder in Android Studio.
2. Ensure the following changes are made in the Manifest file: </br>
  `<application android:isGame="false">` </br>
    `<activity`  </br>
      `android:launchMode="singleTop"`  </br>
      `android:clearTaskOnLaunch="false" android:alwaysRetainTaskState="true">` </br>
    `</activity>` </br>
  `</application>`
3. Enable USB Debugging options in your Android device, connect device to computer and Run App on your device.
4. Unplug your device if you wish. You will need to enable Bluetooth beofre you run the App.

### Steps for deploying on  iOS devices [Mac PC is needed]:
1. [to be updated]

### To test your electronic circuit:
For testing purposes a "Test Scale" has been added under `Assets > Scripts > txt`.
1. Set up the 25 solenoids to your Arduino MEGA. It should look something like below:
![img](https://user-images.githubusercontent.com/23626462/61359978-14e87580-a8b0-11e9-82e7-bcc2f16c1cfd.jpg)
2. In the app, run the first item in the playlist "Test Scale". It should play the full chromatic scale of 25 notes.

## Customisations
You will need Unity to make changes to the App Interface.

### To open the Unity Editor:
1. Install Unity Hub and create an account: https://docs.unity3d.com/Manual/GettingStartedInstallingHub.html
2. Download any version of 2019. You must check the boxes for Android/iOS APK packages depending on what device you wish to deploy to. This may take a while.
3. Open the project folder in Unity Hub.

### To add new songs to the playlist:
1. Run Part 1 of https://github.com/ValereneGoh/MiditoArduino to convert midi files into interpretable text file formats.
2. Add the text file to the folder under Assets > Scripts > txt.
3. Open Unity and manually create a new CD player and holder for the new song via the Prefab folder.

### To change the tempo of the overall installation:
1. Edit line 244 of the DropZone.cs in `Assets > Scripts`.