using System.Collections.Generic;

[System.Serializable]

public class User
{
    public string UserName;
    public long Meters;
    public long CUprice;
    public long QTprice;
    public List<Ship> shipList = new List<Ship>();
    public List<Index> indexList = new List<Index>();
}