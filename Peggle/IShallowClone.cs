namespace Peggle
{
    //I've chosen not to use the default ICloneable interface as it's purpose if ambiguous and it returns object
    interface IShallowClone<T>
    {
        T clone();
    }
}
