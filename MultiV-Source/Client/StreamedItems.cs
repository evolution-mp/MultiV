using System.Collections.Generic;
using MultiVShared;

namespace MultiV.Networking
{
    public interface ILocalHandleable
    {
        int LocalHandle { get; set; }
    }

    public interface IStreamedItem
    {
        int RemoteHandle { get; set; }

        bool LocalOnly { get; set; }

        bool StreamedIn { get; set; }

        Vector3 Position { get; }

        byte EntityType { get; set; }

        int Dimension { get; set; }

        List<int> Attachables { get; set; }

    }

    public class RemotePlayer : ILocalHandleable
    {
        public int RemoteHandle { get; set; }

        public bool LocalOnly { get; set; }

        public bool StreamedIn { get; set; }

        public virtual int LocalHandle { get; set; }

        public override int GetHashCode()
        {
            return RemoteHandle;
        }
    }

    public class RemotePed : PedProperties, ILocalHandleable
    {
        public int RemoteHandle { get; set; }

        public bool LocalOnly { get; set; }

        public bool StreamedIn { get; set; }

        public int LocalHandle { get; set; }

        public override int GetHashCode()
        {
            return RemoteHandle;
        }
    }

    public class RemoteParticle : ILocalHandleable
    {
        public int RemoteHandle { get; set; }

        public bool LocalOnly { get; set; }

        public bool StreamedIn { get; set; }

        public int LocalHandle { get; set; }
    }

    public class RemoteVehicle : VehicleProperties, ILocalHandleable
    {
        public int LocalHandle { get; set; }
        public int RemoteHandle { get; set; }
        public bool LocalOnly { get; set; }
        public bool StreamedIn { get; set; }


        public override int GetHashCode()
        {
            return RemoteHandle;
        }

    }

    public class RemoteProp : EntityProperties, ILocalHandleable
    {
        public int LocalHandle { get; set; }
        public int RemoteHandle { get; set; }
        public bool LocalOnly { get; set; }
        public bool StreamedIn { get; set; }
        public override int GetHashCode()
        {
            return RemoteHandle;
        }
    }

    public class RemoteBlip : BlipProperties, ILocalHandleable
    {
        public int LocalHandle { get; set; }
        public int RemoteHandle { get; set; }
        public bool LocalOnly { get; set; }
        public bool StreamedIn { get; set; }
        public override int GetHashCode()
        {
            return RemoteHandle;
        }
    }

    public class RemoteMarker : MarkerProperties
    {
        public int RemoteHandle { get; set; }
        public bool LocalOnly { get; set; }
        public bool StreamedIn { get; set; }
        public override int GetHashCode()
        {
            return RemoteHandle;
        }
    }

    public class RemotePickup : PickupProperties, ILocalHandleable
    {
        public int LocalHandle { get; set; }
        public int RemoteHandle { get; set; }
        public bool LocalOnly { get; set; }
        public bool StreamedIn { get; set; }
        public override int GetHashCode()
        {
            return RemoteHandle;
        }
    }

    public class RemoteTextLabel
    {
        public int RemoteHandle { get; set; }
        public bool LocalOnly { get; set; }
        public bool StreamedIn { get; set; }
        public override int GetHashCode()
        {
            return RemoteHandle;
        }
    }
}