using System;

public class EnumHandler
{
    public Data data;
    public FlagData flagData;
    
    void Function(){

        // comparing enum values
        if(data == Data.data1){
            // perform
        }

        // Lenght of enum values
        int count = Enum.GetValues(typeof(Data)).Length;
        for(int i = 0; i < count; i++){
            // perform
        }

        // Get enum values at index 
        int index = 0;  // index you want to
        string value = ((Data)index).ToString();

        // Comparing enum values to string values 
        if( value == Data.data1.ToString()){
            // perform
        }

        // Comparing enum values to int values 
        if( 1 == (int) data){
            // perform
        }

        flagData = FlagData.data1 | FlagData.data3;         // assigning multiple values
    }
}

public enum Data{
    data1, data2, data3, data4, data5, data6
}


// FlagEnums are used to select multiple data at once
[System.Flags]
public enum FlagData{
    data1 = 1, data2 = 2, data3 = 4, data4 = 8, data5 = 16, data6 = 32
}