namespace ScrapManagement.Infrastructure.CacheKeys
{
    public static class ScrapTypeCacheKeys
    {
        public static string ListKey => "ScrapTypeList";

        public static string SelectListKey => "ScrapTypeSelectList";

        public static string GetKey(int scrapTypeId) => $"ScrapType-{scrapTypeId}";

        public static string GetDetailsKey(int scrapTypeId) => $"ScrapTypeDetails-{scrapTypeId}";
    }
}