namespace EmpyrionManager.Abstractions.Archive {

    /// <summary>
    /// Enumerates the various package types for compressed items.
    /// </summary>
    public enum PackageType {
        Unpacked = 0,
        Zip = 1,
        SevenZip = 2,
        Rar = 3
    }
}