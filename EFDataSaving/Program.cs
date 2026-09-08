using EFDataSaving.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace EFDataSaving;

public static class Program
{
    public static void Main()
    {
        //BasicSaveWithTrackingWithTracking.Run();
        //ChangeTracking.Run();
        //CascadeDelete.Run();
        EfficientUpdating.Run();
    }

}


