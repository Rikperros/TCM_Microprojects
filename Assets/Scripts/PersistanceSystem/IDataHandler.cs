//This interface provide the signature of the methods that will be used to load and save data by our components
//Notice: an interface is like an abstract class that allows us to define methods that are actually implemented by
//Different classes without following a class inheritance strategy
public interface IDataHandler
{
    //Here we pass our data as a reference object because we need to modify the original object
    public void SaveData(ref PersistentData data);
    //Here we pass it by value since we don't want to grant write access to data object in this method
    public void LoadData(PersistentData data);
}
