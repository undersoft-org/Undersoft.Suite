namespace Undersoft.SDK.Serialization
{
    public interface IBinarySerializable
    {
        void Serialize(BinaryWriter writer, object graph);
        object Deserialize(BinaryReader reader);
        int GetTypeHandle();
        void SetTypeHandle(int h);
    }

    public abstract class BinarySerializable : IBinarySerializable
    {
        public abstract object Deserialize(BinaryReader reader);

        public abstract int GetTypeHandle();

        public abstract void Serialize(BinaryWriter writer, object graph);

        public abstract void SetTypeHandle(int h);
    }
}
