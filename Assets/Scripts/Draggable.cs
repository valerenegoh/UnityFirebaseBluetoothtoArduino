using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Proyecto26;     // for Firebase

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    public Transform parentToReturnTo;
    public Transform parent;
    public TextAsset TextFile;
    public bool draggable = true;
    public int popularity;

    public void OnBeginDrag(PointerEventData eventData){
        if (parentToReturnTo.name == "Play"){
            eventData.pointerDrag = null;
        }
        else if (!draggable){
            eventData.pointerDrag = null;
        }
        else{
            parent = this.transform.parent;
            this.transform.SetParent(parent.parent);
        }
    }

    public void OnDrag(PointerEventData eventData){
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData){
        this.transform.SetParent(parentToReturnTo);
    }

    public int getPopularity(){
        return popularity;
    }

    public string getTitle(){
        return TextFile.name;
    }

    public void RetrieveFromDatabase(){
        Song song = new Song();
        RestClient.Get<Song>("https://pico-86a8b.firebaseio.com/" + TextFile.name + ".json").Then(response =>{
                song = response;
                popularity = song.getPopularity();   //update values
            });
    }
}
