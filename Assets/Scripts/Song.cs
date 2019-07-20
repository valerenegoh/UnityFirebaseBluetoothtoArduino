using System;

[Serializable]
public class Song{
    public int popularity;
    public string title;

    public void setPopularity(int p){
        popularity = p;
    }

    public int getPopularity(){
        return popularity;
    }

    public void setTitle(string t){
        title = t;
    }

    public string getTitle(){
        return title;
    }
}