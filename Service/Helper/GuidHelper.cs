namespace Service.Helper
{
    public static class GuidHelper
    {
        public static Guid NewGuid => Guid.NewGuid();

        public static String GuidToString => Guid.NewGuid().ToString();
    }
}
