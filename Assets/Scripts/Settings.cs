using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour{
    
	public bool showAtStart = true;
    public GameObject overlay;
    
    void Start(){
        InitializeBluetooth();
        if (showAtStart) {
            ShowLaunchScreen();
        }else {
            StartMain();
        }
    }

    void Update(){
        ConnectBluetooth();
    }

    public void StartMain(){
        overlay.SetActive (false);
        showAtStart = false;
    }

    public void ShowLaunchScreen(){
        overlay.SetActive (true);
    }


    // =========================== BLUETOOTH FUNCTIONS =========================== 

    public Text HM10_Status;
    public string DeviceName = "HMSoft";
    public string ServiceUUID = "FFE0";
    public string Characteristic = "FFE1";
    public string _hm10;

     enum States{
        None,
        Scan,
        Connect,
        Subscribe,
        Unsubscribe,
        Disconnect,
        Communication,
    }

    public void InitializeBluetooth(){
        HM10_Status.text = "Initializing...";
        Reset();
        BluetoothLEHardwareInterface.Initialize (true, false, () => {
            SetState (States.Scan, 0.1f);
            HM10_Status.text = "Initialized";
        }, (error) => {
            BluetoothLEHardwareInterface.Log ("Error: " + error);
        });
    }

    public void ConnectBluetooth(){
        if (_timeout > 0f){
            _timeout -= Time.deltaTime;
            if (_timeout <= 0f){
                _timeout = 0f;
                switch (_state){
                case States.None:
                    break;

                case States.Scan:
                    HM10_Status.text = "Scanning for HM10 devices...";
                    BluetoothLEHardwareInterface.ScanForPeripheralsWithServices (null, (address, name) => {
                        if (name.Contains (DeviceName)){
                            _workingFoundDevice = true;
                            BluetoothLEHardwareInterface.StopScan ();    // stop scanning while you connect to a device
                            _hm10 = address;    // add it to the list and set to connect to it
                            DropZone._hm10 = address;
                            ScrollRectSnap._hm10 = address;
                            HM10_Status.text = "Found HM10";
                            SetState (States.Connect, 0.5f);
                            _workingFoundDevice = false;
                        }

                    }, null, false, false);
                    break;

                case States.Connect:
                    HM10_Status.text = "Connecting to HM10";
                    BluetoothLEHardwareInterface.ConnectToPeripheral (_hm10, null, null, (address, serviceUUID, characteristicUUID) => {
                        if (IsEqual (serviceUUID, ServiceUUID)){
                            if (IsEqual (characteristicUUID, Characteristic)) {
                                _connected = true;
                                SetState (States.Subscribe, 2f);
                            }
                        }
                    }, (disconnectedAddress) => {
                        BluetoothLEHardwareInterface.Log ("Device disconnected: " + disconnectedAddress);
                        HM10_Status.text = "Disconnected";
                    });
                    break;

                case States.Subscribe:
                    HM10_Status.text = "Connected to HM10";
                    enabled = true;
                    BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress (_hm10, ServiceUUID, Characteristic, null, (address, characteristicUUID, bytes) => {});
                    _state = States.None;    // set to the none state and the user can start sending and receiving data

                    break;

                case States.Unsubscribe:
                    BluetoothLEHardwareInterface.UnSubscribeCharacteristic (_hm10, ServiceUUID, Characteristic, null);
                    SetState (States.Disconnect, 4f);
                    break;

                case States.Disconnect:
                    if (_connected){
                        BluetoothLEHardwareInterface.DisconnectPeripheral (_hm10, (address) => {
                            BluetoothLEHardwareInterface.DeInitialize (() => {   
                                _connected = false;
                                _state = States.None;
                            });
                        });
                    }
                    else{
                        BluetoothLEHardwareInterface.DeInitialize (() => {
                            _state = States.None;
                        });
                        enabled = false;
                    }
                    break;
                }
            }
        }
    }

    private bool _workingFoundDevice = true;
    private bool _connected = false;
    private float _timeout = 0f;
    private States _state = States.None;

    void Reset (){
        _workingFoundDevice = false;    // used to guard against trying to connect to a second device while still connecting to the first
        _connected = false;
        _timeout = 0f;
        _state = States.None;
        _hm10 = null;
    }
    
    bool IsEqual(string uuid1, string uuid2){
        if (uuid1.Length == 4)
            uuid1 = FullUUID (uuid1);
        if (uuid2.Length == 4)
            uuid2 = FullUUID (uuid2);

        return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
    }

    string FullUUID (string uuid){
        return "0000" + uuid + "-0000-1000-8000-00805F9B34FB";
    }

    void SetState (States newState, float timeout){
        _state = newState;
        _timeout = timeout;
    }
}