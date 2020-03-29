namespace AttachmentApi.Enums
{
    public class EntityEnums
    {
        public enum EntityAction
        {
            Create,
            Update,
            Delete,
            Send
        }

        public enum EntityType
        {
            Post,
            Message,
            Comment,
            Push,
            Update
        }
    }
}