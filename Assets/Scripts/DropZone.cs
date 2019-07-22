using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO.Ports;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.Text;   // for Encoding
using Proyecto26;     // for Firebase

public class DropZone : MonoBehaviour, IDropHandler{

    public Dictionary<int, string> dict = new Dictionary<int, string>(){
            {48, "0"},  // C
            {49, "1"},  // C#
            {50, "2"},  // D
            {51, "3"},  // D#
            {52, "4"},  // E
            {53, "5"},  // F
            {54, "6"},  // F#
            {55, "7"},  // G
            {56, "8"},  // G#
            {57, "9"},  // A
            {58, "a"},  // A#
            {59, "b"},  // B
            {60, "c"},  // C
            {61, "d"},  // C#
            {62, "e"},  // D
            {63, "f"},  // D#
            {64, "g"},  // E
            {65, "h"},  // F
            {66, "i"},  // F#
            {67, "j"},  // G
            {68, "k"},  // G#
            {69, "l"},  // A
            {70, "m"},  // A#
            {71, "n"},  // B
            {72, "o"},  // C
            };

    public List<Draggable> CdList;
    public string title;
    public Draggable d;
    public GameObject scroll;
    private bool canRotatePlayer = false;
    // public SerialPort stream;

    public string ServiceUUID = "FFE0";
    public string Characteristic = "FFE1";
    public static string _hm10;

    void Start(){
        StartCoroutine(ShowStatistics());
    }

    // What happens when user drags and drops a disc onto the vinyl plate.
    public void OnDrop(PointerEventData eventData){
        d = eventData.pointerDrag.GetComponent<Draggable>();
        if(d != null){
            d.parentToReturnTo = this.transform;
        }
        canRotatePlayer = true;
        title = d.getTitle();
        foreach (Draggable cd in CdList){
            if (cd.getTitle() != title){
                cd.draggable = false;
            }
        }
        scroll.GetComponent<ScrollRect>().enabled = false;
        StartCoroutine(SendArduino());
    }

    void Update(){
        RotatePlayer();
    }

    // What happens when song playing is done.
    private IEnumerator ReturnPlayer(float waitTime){
        yield return new WaitForSeconds(waitTime);  //wait awhile before returning disc to slot
        canRotatePlayer = false;
        scroll.GetComponent<ScrollRect>().enabled = true;
        d.transform.SetParent(d.parent);
        d.parentToReturnTo = d.parent;
        d = null;
        foreach (Draggable cd in CdList){
            if (cd.name != title){
                cd.draggable = true;
            }
        }
    }

    // What happens during song play.
    private IEnumerator SendArduino(){
        d.RetrieveFromDatabase();
        yield return new WaitForSeconds(1.0f);  //wait awhile before getting popularity score & playing song
        print("Playing Track: " + d.getTitle() + " of popularity " + d.getPopularity());
        Song song = new Song();
        song.setPopularity(d.getPopularity() + 1);        // update view count
        song.setTitle(d.getTitle());
        RestClient.Put("https://pico-86a8b.firebaseio.com/" + d.getTitle() + ".json", song);

        WaitForSeconds wait = new WaitForSeconds(0.5f);
        string[] lines = d.TextFile.text.Split('\n');
        foreach (string line in lines){
            if(!string.IsNullOrWhiteSpace(line)){    // beat contains notes
                foreach(string note in Regex.Split(line, " ")){
                    int key = int.Parse(note);
                    // stream.Write(dict[key]);
                    var data = Encoding.UTF8.GetBytes (dict[key]);
                    BluetoothLEHardwareInterface.WriteCharacteristic (_hm10, ServiceUUID, Characteristic, data, data.Length, false, (characteristicUUID) => {
                        BluetoothLEHardwareInterface.Log ("Write Succeeded");
                    });
                }   
            }
            yield return wait; //tempo of song
        }
        StartCoroutine(ReturnPlayer(1.0f));
    }

    protected void RotatePlayer(){
        if(canRotatePlayer){
            d.transform.Rotate(Vector3.forward, -90.0f * Time.deltaTime);
        }
    }

    // =========================== SETTINGS SCREEN =========================== 

    public Draggable TestFile;
    public Text statistics;
    public Dictionary<string, string> playlist = new Dictionary<string, string>();

    public void TestScale(){
        d = TestFile;
        StartCoroutine(SendArduino());
    }

    public IEnumerator ShowStatistics(){
        foreach(Draggable cd in CdList){
            Song song = new Song();
            RestClient.Get<Song>("https://pico-86a8b.firebaseio.com/" + cd.getTitle() + ".json").Then(response =>{
                    song = response;
                    playlist[cd.getTitle()] = song.getPopularity().ToString();   //update values
                });
        }
        yield return new WaitForSeconds(1.0f);  //wait awhile before updating statistics
        statistics.text = "";
        int count = 0;
        foreach(KeyValuePair<string, string> pair in playlist){
            count += 1;
            statistics.text += string.Format("{0}.\t{1}: {2}\n", count, pair.Key, pair.Value);
        }
    }
}